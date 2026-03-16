using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Net;
using System.Net.Sockets;
using Butterfly.HabboHotel.GameClients;
using Butterfly.HabboHotel.Rooms;
using Butterfly.Messages;
using Butterfly.Core;

using Database_Manager.Database.Session_Details.Interfaces;

namespace Butterfly.Net
{
    class MusSocket
    {
        private Socket msSocket;

        private String musIp;
        private int musPort;

        private HashSet<String> allowedIps;

        public MusSocket(String _musIp, int _musPort, String[] _allowedIps, int backlog)
        {
            musIp = _musIp;
            musPort = _musPort;

            allowedIps = new HashSet<String>();

            foreach (String ip in _allowedIps)
            {
                allowedIps.Add(ip);
            }

            try
            {
                msSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                msSocket.Bind(new IPEndPoint(IPAddress.Any, musPort));
                msSocket.Listen(backlog);

                msSocket.BeginAccept(OnEvent_NewConnection, msSocket);

                Logging.WriteLine("MUS socket -> READY!");
            }

            catch (Exception e)
            {
                throw new ArgumentException("Could not set up MUS socket:\n" + e.ToString());
            }
        }

        private void OnEvent_NewConnection(IAsyncResult iAr)
        {
            try
            {
                Socket socket = ((Socket)iAr.AsyncState).EndAccept(iAr);
                String ip = socket.RemoteEndPoint.ToString().Split(':')[0];
                if (allowedIps.Contains(ip) || ip == "127.0.0.1")
                {
                    MusConnection nC = new MusConnection(socket);
                }
                else
                {
                    socket.Close();
                }
            }
            catch (Exception) { }

            msSocket.BeginAccept(OnEvent_NewConnection, msSocket);
        }
    }

    class MusConnection
    {
        private Socket socket;
        private byte[] buffer = new byte[1024];

        public MusConnection(Socket _socket)
        {
            socket = _socket;

            try
            {
                socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, OnEvent_RecieveData, socket);
            }
            catch
            {
                tryClose();
            }
        }

        private void tryClose()
        {
            try
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
                socket.Dispose();
            }
            catch { }

            socket = null;
            buffer = null;
        }

        private void OnEvent_RecieveData(IAsyncResult iAr)
        {
            try
            {
                int bytes = 0;

                try
                {
                    bytes = socket.EndReceive(iAr);
                }
                catch { tryClose(); return; }

                String data = Encoding.Default.GetString(buffer, 0, bytes);

                if (data.Length > 0)
                    processCommand(data);
            }
            catch { }

            tryClose();
        }

        private void processCommand(String data)
        {
            String header = "";
            String param = "";
            int lala = data.IndexOf(Convert.ToChar(1));
            if (data.IndexOf(Convert.ToChar(1)) != -1)
            {
                header = data.Split(Convert.ToChar(1))[0];
                param = data.Split(Convert.ToChar(1))[1];
            }
            else
            {
                header = data;
                param = "";

            }

            string[] pars = param.Split(' ');
            GameClient Client = null;

            switch (header.ToLower())
            {
                case "updatecredits":
                    {
                        Client = ButterflyEnvironment.GetGame().GetClientManager().GetClientByUsername(pars[0]);

                        if (Client == null)
                        {
                            return;
                        }

                        DataRow newCredits;

                        using (IQueryAdapter dbClient = ButterflyEnvironment.GetDatabaseManager().getQueryreactor())
                        {
                            dbClient.setQuery("SELECT credits FROM users WHERE id = @userid");
                            dbClient.addParameter("userid", (int)Client.GetHabbo().Id);
                            newCredits = dbClient.getRow();
                        }

                        Client.GetHabbo().Credits = (int)newCredits["credits"];
                        Client.GetHabbo().UpdateCreditsBalance();
                        Respond("Credits for userid " + pars[0] + "updated.");
                        break;
                    }

                case "signout":
                    {
                        ButterflyEnvironment.GetGame().GetClientManager().GetClientByUserID(uint.Parse(pars[0])).Disconnect();
                        Respond("User disconnected.");
                        break;
                    }
                case "ha":
                    {
                        string Notice = MergeParams(pars, 0);

                        // ServerMessage 810 = finestra grande tipo welcomeAlert
                        ServerMessage HotelAlert = new ServerMessage(810);
                        HotelAlert.AppendUInt(1); // numero di alert (uno solo)
                        HotelAlert.AppendStringWithBreak("Messaggio dallo Staff di Hobba Hotel");
                        HotelAlert.AppendStringWithBreak(Notice + " \r\n" + "- the hotel management");

                        ButterflyEnvironment.GetGame().GetClientManager().QueueBroadcaseMessage(HotelAlert);
                        Respond("Hotel alert executed.");
                        break;
                    }

                case "hal":
                    {
                        string Link = pars[0];
                        string Message = MergeParams(pars, 1);
                        ServerMessage nMessage = new ServerMessage(810);
                        nMessage.AppendStringWithBreak(LanguageLocale.GetValue("hotelallert.notice") + "\r\n" + Message + "\r\n- the hotel Management");
                        nMessage.AppendStringWithBreak(Link);
                        ButterflyEnvironment.GetGame().GetClientManager().QueueBroadcaseMessage(nMessage);
                        Respond("Hotel Link Alert has been sent.");
                        break;
                    }

                case "alert":
                    {
                        string TargetUser = null;
                        GameClient TargetClient = null;
                        TargetUser = pars[0];
                        TargetClient = ButterflyEnvironment.GetGame().GetClientManager().GetClientByUsername(TargetUser);

                        if (TargetClient == null)
                        {
                            Respond(LanguageLocale.GetValue("input.usernotfound"));
                        }

                        TargetClient.SendNotif(MergeParams(pars, 1));
                        Respond("Alert has been sent.");
                        break;
                    }


                case "ban":
                    {
                        GameClient TargetClient = null;

                        TargetClient = ButterflyEnvironment.GetGame().GetClientManager().GetClientByUsername(pars[0]);

                        if (TargetClient == null)
                        {
                            Respond(LanguageLocale.GetValue("input.usernotfound"));
                        }

                        int BanTime = 0;

                        try
                        {
                            BanTime = int.Parse(pars[1]);
                        }
                        catch (FormatException) { Respond("An error has occured."); }

                        if (BanTime <= 600)
                        {
                            Respond(LanguageLocale.GetValue("ban.toolesstime"));
                        }
                        else
                        {
                            ButterflyEnvironment.GetGame().GetBanManager().BanUser(TargetClient, "Staff", BanTime, MergeParams(pars, 2), false);
                        }
                        break;
                    }

                case "unban":
                    {
                        if (pars[0].Length > 1)
                        {
                            ButterflyEnvironment.GetGame().GetBanManager().UnbanUser(pars[0]);
                            Respond("Ban Removed.");
                        }
                        break;
                    }

                case "shutdown":
                    {
                        Logging.LogMessage("Server exiting at " + DateTime.Now);
                        Logging.DisablePrimaryWriting(true);
                        Console.WriteLine("The server is saving users furniture, rooms, etc. WAIT FOR THE SERVER TO CLOSE, DO NOT EXIT THE PROCESS IN TASK MANAGER!!");
                        Respond("Shutting down.");

                        ButterflyEnvironment.PreformShutDown(true);

                        break;
                    }

                case "givebadge":
                    {
                        GameClient TargetClient = null;
                        TargetClient = ButterflyEnvironment.GetGame().GetClientManager().GetClientByUsername(pars[0]);
                        if (TargetClient != null)
                        {
                            TargetClient.GetHabbo().GetBadgeComponent().GiveBadge(ButterflyEnvironment.FilterInjectionChars(pars[1]), true);

                            ButterflyEnvironment.GetGame().GetModerationTool().LogStaffEntry("Staff", TargetClient.GetHabbo().Username, "Badge", "Badge given to user [" + pars[0] + "]");
                            Respond("Badge given to " + TargetClient);
                        }
                        else
                        {
                            Respond("input.usernotfound");
                        }
                        break;
                    }

                case "coins":
                    {
                        GameClient TargetClient = null;

                        TargetClient = ButterflyEnvironment.GetGame().GetClientManager().GetClientByUsername(pars[0]);
                        if (TargetClient != null)
                        {
                            int creditsToAdd;
                            if (int.TryParse(pars[1], out creditsToAdd))
                            {
                                TargetClient.GetHabbo().Credits = TargetClient.GetHabbo().Credits + creditsToAdd;
                                TargetClient.GetHabbo().UpdateCreditsBalance();
                                TargetClient.SendNotif("Staff" + LanguageLocale.GetValue("coins.awardmessage1") + creditsToAdd.ToString() + LanguageLocale.GetValue("coins.awardmessage2"));
                                Respond(LanguageLocale.GetValue("coins.updateok"));

                            }
                            else
                            {
                                Respond(LanguageLocale.GetValue("input.intonly"));

                            }
                        }
                        else
                        {
                            Respond(LanguageLocale.GetValue("input.usernotfound"));

                        }
                        break;
                    }

                case "pixels":
                    {
                        GameClient TargetClient = null;

                        TargetClient = ButterflyEnvironment.GetGame().GetClientManager().GetClientByUsername(pars[0]);
                        if (TargetClient != null)
                        {
                            int PixelsToAdd;
                            if (int.TryParse(pars[1], out PixelsToAdd))
                            {
                                TargetClient.GetHabbo().ActivityPoints = TargetClient.GetHabbo().ActivityPoints + PixelsToAdd;
                                TargetClient.GetHabbo().UpdateActivityPointsBalance(true);
                                TargetClient.SendNotif("Staff" + LanguageLocale.GetValue("pixels.awardmessage1") + PixelsToAdd.ToString() + LanguageLocale.GetValue("pixels.awardmessage2"));
                                Respond(LanguageLocale.GetValue("pixels.updateok"));

                            }
                            else
                            {
                                Respond(LanguageLocale.GetValue("input.intonly"));

                            }
                        }
                        else
                        {
                            Respond(LanguageLocale.GetValue("input.usernotfound"));

                        }
                        break;
                    }

                case "globalcredits":
                    {
                        try
                        {
                            int CreditAmount = int.Parse(pars[0]);
                            ButterflyEnvironment.GetGame().GetClientManager().QueueCreditsUpdate(CreditAmount);

                            using (IQueryAdapter dbClient = ButterflyEnvironment.GetDatabaseManager().getQueryreactor())
                                dbClient.runFastQuery("UPDATE users SET credits = credits + " + CreditAmount);

                            ButterflyEnvironment.GetGame().GetModerationTool().LogStaffEntry("Staff", string.Empty, "Mass Credits", "Send [" + CreditAmount + "] credits to everyone in the database");
                            Respond("Global credits updated");
                        }
                        catch
                        {
                            Respond(LanguageLocale.GetValue("input.intonly"));
                        }
                        break;
                    }

                case "massbadge":
                    {
                        ButterflyEnvironment.GetGame().GetClientManager().QueueBadgeUpdate(pars[0]);
                        break;
                    }

                case "crystals":
                    {
                        GameClient TargetClient = null;

                        TargetClient = ButterflyEnvironment.GetGame().GetClientManager().GetClientByUsername(pars[0]);

                        if (TargetClient == null)
                        {
                            Respond(LanguageLocale.GetValue("input.usernotfound"));
                        }
                        try
                        {
                            TargetClient.GetHabbo().GiveUserCrystals(int.Parse(pars[1]));
                            Respond("Send " + pars[1] + " Crystals to " + pars[0]);
                        }
                        catch (FormatException) { Respond("An error has occured"); }
                        break;
                    }

                default:
                    {

                        break;
                    }
            }

        }

        //Send response back to Rcon Client
        private void Respond(string response)
        {
            byte[] respond = System.Text.Encoding.UTF8.GetBytes(response);
            socket.Send(respond, respond.Length, SocketFlags.None);
        }

        private static string MergeParams(string[] Params, int Start)
        {
            StringBuilder MergedParams = new StringBuilder();

            for (int i = 0; i < Params.Length; i++)
            {
                if (i < Start)
                {
                    continue;
                }

                if (i > Start)
                {
                    MergedParams.Append(" ");
                }

                MergedParams.Append(Params[i]);
            }

            return MergedParams.ToString();
        }
    }
}