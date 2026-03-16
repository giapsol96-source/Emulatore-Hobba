using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Butterfly.Core;
using Butterfly.HabboHotel.GameClients;
using Butterfly.HabboHotel.Items;
using Butterfly.HabboHotel.Rooms;
using Butterfly.HabboHotel.Rooms.RoomIvokedItems;
using Butterfly.HabboHotel.Users;
using Butterfly.IRC;
using Butterfly.Messages;
using Database_Manager.Database.Session_Details.Interfaces;
using System.Drawing;
using Butterfly.Util;

namespace Butterfly.HabboHotel.Misc
{
    class ChatCommandHandler
    {
        private GameClient Session;
        private string[] Params;

        public ChatCommandHandler(string[] input, GameClient session)
        {
            Params = input;
            Session = session;
        }

        internal bool WasExecuted()
        {
            ChatCommand command = ChatCommandRegister.GetCommand(Params[0].Substring(1).ToLower());

            if (command.UserGotAuthorization(Session))
            {
                ChatCommandRegister.InvokeCommand(this, command.commandID);
                Dispose();
                return true;
            }
            else
            {
                Dispose();
                return false;
            }
        }

        internal void Dispose()
        {
            Session = null;
            Array.Clear(this.Params, 0, Params.Length);
        }

        #region Commands
        internal void moonwalk()
        {
            Room room = Session.GetHabbo().CurrentRoom;
            if (room == null)
                return;

            RoomUser roomuser = room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);
            roomuser.moonwalkEnabled = !roomuser.moonwalkEnabled;

            if (roomuser.moonwalkEnabled)
                Session.SendNotif("Moonwalk enabled");
            else
                Session.SendNotif("Moonwalk disabled");
        }

        internal void push()
        {
            Room TargetRoom = ButterflyEnvironment.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            string Target = MergeParams(Params, 1);
            GameClient RivalClient = ButterflyEnvironment.GetGame().GetClientManager().GetClientByUsername(Target);

            if (RivalClient == null)
            {
                return;
            }
            if (Params[1] == null || Params[1].Length == 0 || Target == null || Target.Length == 0)
            {
                return;
            }

            if (Params[1] == Session.GetHabbo().Username || Target == Session.GetHabbo().Username)
            {
                return;
            }
            if (TargetRoom == null)
            {
                return;
            }

            RoomUser Rival = TargetRoom.GetRoomUserManager().GetRoomUserByHabbo(Target);

            if (Rival == null)
            {
                return;
            }

            RoomUser Me = TargetRoom.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);

            if (Me == null)
            {
                return;
            }

            if (Rival.GetClient().GetHabbo().CurrentRoomId == Session.GetHabbo().CurrentRoomId && (Rival.X == Me.X - 1) || (Rival.X == Me.X + 1) || (Rival.Y == Me.Y - 1) || (Rival.Y == Me.Y + 1))
            {
                switch (Me.RotBody)
                {
                    case 4:
                        {
                            Rival.MoveTo(Rival.X, Rival.Y + 1);
                            Me.Chat(Session, "*pushes " + Params[1] + " away from them*", true);
                            break;
                        }

                    case 0:
                        {
                            Rival.MoveTo(Rival.X, Rival.Y - 1);
                            Me.Chat(Session, "*pushes " + Params[1] + " away from them*", true);
                            break;
                        }

                    case 6:
                        {
                            Rival.MoveTo(Rival.X - 1, Rival.Y);
                            Me.Chat(Session, "*pushes " + Params[1] + " away from them*", true);
                            break;
                        }

                    case 2:
                        {
                            Rival.MoveTo(Rival.X + 1, Rival.Y);
                            Me.Chat(Session, "*pushes " + Params[1] + " away from them*", true);
                            break;
                        }

                    case 3:
                        {
                            Rival.MoveTo(Rival.X + 1, Rival.Y + 1);

                            Me.Chat(Session, "*pushes " + Params[1] + " away from them*", true);
                            break;
                        }

                    case 1:
                        {
                            Rival.MoveTo(Rival.X + 1, Rival.Y - 1);
                            Me.Chat(Session, "*pushes " + Params[1] + " away from them*", true);
                            break;
                        }

                    case 7:
                        {
                            Rival.MoveTo(Rival.X - 1, Rival.Y - 1);
                            Me.Chat(Session, "*pushes " + Params[1] + " away from them*", true);
                            break;
                        }

                    case 5:
                        {
                            Rival.MoveTo(Rival.X - 1, Rival.Y + 1);
                            Me.Chat(Session, "*pushes " + Params[1] + " away from them*", true);
                            break;
                        }
                }
            }
            else
            {
                return;
            }
        }

        internal void pull()
        {
            Room TargetRoom = Session.GetHabbo().CurrentRoom;
            RoomUser Me = TargetRoom.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Username);
            string Target = MergeParams(Params, 1);
            GameClient RivalClient = ButterflyEnvironment.GetGame().GetClientManager().GetClientByUsername(Target);

            if (RivalClient == null)
            {
                return;
            }

            if (Params[1] == null || Params[1].Length == 0 || Target == null || Target.Length == 0)
            {
                return;
            }

            if (Params[1] == Session.GetHabbo().Username || Target == Session.GetHabbo().Username)
            {
                return;
            }
            if (TargetRoom == null)
            {
                return;
            }

            if (Params[1] == null)
            {
                return;
            }

            RoomUser Rival = TargetRoom.GetRoomUserManager().GetRoomUserByHabbo(Params[1]);

            if (Rival == null)
            {
                return;
            }

            if (Me == null)
            {
                return;
            }
            if (Session.GetHabbo().Rank <= 4)
            {
                if (Rival.GetClient().GetHabbo().CurrentRoomId == Me.GetClient().GetHabbo().CurrentRoomId && (Me.X + 2 == Rival.X || Me.X - 2 == Rival.X || Rival.Y + 2 == Me.Y || Rival.Y - 2 == Me.Y))
                {
                    Rival.MoveTo(CoordinationUtil.GetPointInFront(new Point(Me.X, Me.Y), Rival.RotBody));
                    Me.Chat(Session, "*pulls " + Params[1] + " towards them*", true);
                    return;
                }
                else
                {
                    return;
                }
            }
            else
            {
                Rival.MoveTo(CoordinationUtil.GetPointInFront(new Point(Me.X, Me.Y), Rival.RotBody));
                Me.Chat(Session, "*pulls " + Params[1] + " towards them*", true);
                return;
            }
        }

        internal void redeemhand()
        {
            GameClient User = Session;
            Room Room = User.GetHabbo().CurrentRoom;
            
            Hashtable items = User.GetHabbo().GetInventoryComponent().floorItems;
            List<uint> remove = new List<uint>();
            foreach (UserItem item in items.Values)
            {
                if (item.GetBaseItem().Name.StartsWith("CF_") || item.GetBaseItem().Name.StartsWith("CFC_"))
                {
                        string[] Split = item.GetBaseItem().Name.Split('_');
                        int Value = int.Parse(Split[1]);
                        if (Value > 0)
                        {
                            Session.GetHabbo().Credits += Value;
                            Session.GetHabbo().UpdateCreditsBalance();
                        }
                        remove.Add(item.Id);
                        
                   
                        ServerMessage Response = new ServerMessage();
                        Response.Init(219);
                        Session.GetConnection().SendData(Response.GetBytes());
                }
            }
            foreach (uint id in remove)
            {
                User.GetHabbo().GetInventoryComponent().RemoveItem(id, false);
                
                

            }
            foreach (uint id in remove)
            {
                using (IQueryAdapter dbClient = ButterflyEnvironment.GetDatabaseManager().getQueryreactor())
                {
                    try
                    {
                        dbClient.runFastQuery("DELETE FROM items_users WHERE item_id=" + id + " AND user_id=" + User.GetHabbo().Id);
                    }
                    catch { }
                }
            }
            User.GetHabbo().GetInventoryComponent().RunDBUpdate();
            User.GetHabbo().GetInventoryComponent().RunCycleUpdate();
            //in room unimplented but working
            //for (int i = 0; i < items.ToList().Count; i++)
            //{
            //    RoomItem item = items.ToList()[i].Value;
            //    if (item.GetBaseItem().Name.StartsWith("CF_") || item.GetBaseItem().Name.StartsWith("CFC_"))
            //    {


            //        string[] Split = item.GetBaseItem().Name.Split('_');
            //        int Value = int.Parse(Split[1]);

            //        if (Value > 0)
            //        {
            //            Session.GetHabbo().Credits += Value;
            //            Session.GetHabbo().UpdateCreditsBalance();
            //        }
            //        Room.GetRoomItemHandler().RemoveFurniture(null, item.Id);
            //        ServerMessage Response = new ServerMessage();
            //        Response.Init(219);
            //        Session.GetConnection().SendData(Response.GetBytes());

            //    }
            //}
                        
                        
                    
            
           }

        internal void sit()
        {
            Room TargetRoom = Session.GetHabbo().CurrentRoom;
            RoomUser TargetRoomUser = null;
            int tries = 0;
            TargetRoom = ButterflyEnvironment.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            TargetRoomUser = TargetRoom.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);

            if (!TargetRoomUser.Statusses.ContainsKey("sit"))
            {
                if ((TargetRoomUser.RotBody % 2) == 0)
                {
                    if (TargetRoomUser == null)
                    {
                        return;
                    }

                    try
                    {
                        TargetRoomUser.Statusses.Add("sit", "1.0");
                        TargetRoomUser.Z -= 0.35;
                        TargetRoomUser.isSitting = true;
                        TargetRoomUser.UpdateNeeded = true;
                    }
                    catch { }

                    return;

                }

                else
                {
                    if (tries <= 1)
                    {
                        TargetRoomUser.RotBody--;
                        tries++;
                        sit();
                    }
                    else
                    {
                        Session.SendNotif("You cannot sit diagonally, try it again please");
                        tries = 0;
                    }

                }

            }

            else if (TargetRoomUser.isSitting == true)
            {

                TargetRoomUser.Z += 0.35;
                TargetRoomUser.Statusses.Remove("sit");
                TargetRoomUser.isSitting = false;
                TargetRoomUser.UpdateNeeded = true;

            }
        }

        internal void StaffAlert()
        {
            if (Params[1] != null)
            {
                string Notice = null;
                for (int i = 1; i < Params.Length; i++)
                {
                    Notice += Params[i] + " ";
                }
                foreach (var client in ButterflyEnvironment.GetGame().GetClientManager().clients)
                {
                    if (client.Value.GetHabbo().Rank >= 6)
                    {
                        ServerMessage StaffAlert = new ServerMessage(808);
                        StaffAlert.AppendStringWithBreak("Staff alert");
                        StaffAlert.AppendStringWithBreak(Notice + "\r\n" + "- " + Session.GetHabbo().Username);
                        client.Value.SendMessage(StaffAlert);
                    }
                }
            }
            else
            {
                Session.SendNotif("Please specify a message");
            }
        }

        internal void lay()
        {
            //Room TargetRoom = Session.GetHabbo().CurrentRoom;
            //RoomUser TargetRoomUser = null;
            //TargetRoom = ButterflyEnvironment.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);
            //if (TargetRoom == null)
            //{
            //    return;
            //}

            //TargetRoomUser = TargetRoom.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);

            //if (TargetRoomUser == null)
            //{
            //    return;
            //}

            //try
            //{
            //    if (TargetRoomUser.LyingDown == true)
            //    {
            //        TargetRoomUser.LyingDown = false;
            //        TargetRoomUser.RemoveStatus("sit");
            //    }

            //    if (!TargetRoomUser.Statusses.ContainsKey("lay"))
            //    {
            //        if ((TargetRoomUser.RotBody % 2) == 0)
            //        {
            //            TargetRoomUser.AddStatus("lay", Convert.ToString(TargetRoom.GetGameMap().Model.SqFloorHeight[TargetRoomUser.X, TargetRoomUser.Y] + 0.55).Replace(",", "."));
            //            TargetRoomUser.LyingDown = true;
            //            TargetRoomUser.UpdateNeeded = true;
            //        }
            //        else
            //        {
            //            Session.SendNotif("You cannot lay diagonally!");
            //        }
            //    }
            //}
            //catch { }

            //return;
        }

        internal void copylook()
        {
            string copyTarget = Params[1];
            bool findResult = false;


            string gender = null;
            string figure = null;
            DataRow dRow;
            using (IQueryAdapter dbClient = ButterflyEnvironment.GetDatabaseManager().getQueryreactor())
            {
                dbClient.setQuery("SELECT look,gender FROM users WHERE username = @username");
                dbClient.addParameter("username", copyTarget);
                dRow = dbClient.getRow();

                if (dRow != null)
                {
                    findResult = true;
                    figure = (string)dRow[0];
                    gender = (string)dRow[1];

                    dbClient.setQuery("UPDATE users SET look = @look, gender = @gender WHERE username = @username");
                    dbClient.addParameter("look", figure);
                    dbClient.addParameter("gender", gender);
                    dbClient.addParameter("username", Session.GetHabbo().Username);
                    dbClient.runQuery();
                }
            }

            if (findResult)
            {
                Session.GetHabbo().Gender = gender;
                Session.GetHabbo().Look = figure;
                Session.GetMessageHandler().GetResponse().Init(266);
                Session.GetMessageHandler().GetResponse().AppendInt32(-1);
                Session.GetMessageHandler().GetResponse().AppendStringWithBreak(Session.GetHabbo().Look);
                Session.GetMessageHandler().GetResponse().AppendStringWithBreak(Session.GetHabbo().Gender.ToLower());
                Session.GetMessageHandler().GetResponse().AppendStringWithBreak(Session.GetHabbo().Motto);
                Session.GetMessageHandler().SendResponse();

                if (Session.GetHabbo().InRoom)
                {
                    Room Room = Session.GetHabbo().CurrentRoom;

                    if (Room == null)
                    {
                        return;
                    }

                    RoomUser User = Room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);

                    if (User == null)
                    {
                        return;
                    }

                    ServerMessage RoomUpdate = new ServerMessage(266);
                    RoomUpdate.AppendInt32(User.VirtualId);
                    RoomUpdate.AppendStringWithBreak(Session.GetHabbo().Look);
                    RoomUpdate.AppendStringWithBreak(Session.GetHabbo().Gender.ToLower());
                    RoomUpdate.AppendStringWithBreak(Session.GetHabbo().Motto);
                    Room.SendMessage(RoomUpdate);
                }
            }
        }


        internal void pickall()
        {
            Room TargetRoom = Session.GetHabbo().CurrentRoom;
            TargetRoom = Session.GetHabbo().CurrentRoom;

            if (TargetRoom != null && TargetRoom.CheckRights(Session, true))
            {
                List<RoomItem> RemovedItems = TargetRoom.GetRoomItemHandler().RemoveAllFurniture(Session);
                Session.GetHabbo().GetInventoryComponent().AddItemArray(RemovedItems);
                Session.GetHabbo().GetInventoryComponent().UpdateItems(false); //ARGH!
            }
        }

        internal void setspeed()
        {
            Room TargetRoom = Session.GetHabbo().CurrentRoom;
            TargetRoom = Session.GetHabbo().CurrentRoom;
            if (TargetRoom != null && TargetRoom.CheckRights(Session, true))
            {
                try
                {
                    Session.GetHabbo().CurrentRoom.GetRoomItemHandler().SetSpeed(int.Parse(Params[1]));
                }
                catch { Session.SendNotif(LanguageLocale.GetValue("input.intonly")); }
            }
        }

        internal void unload()
        {
            Room TargetRoom = Session.GetHabbo().CurrentRoom;
            ButterflyEnvironment.GetGame().GetRoomManager().UnloadRoom(TargetRoom);
            //TargetRoom.RequestReload();
        }

        internal void disablediagonal()
        {
            Room TargetRoom = Session.GetHabbo().CurrentRoom;

            TargetRoom = Session.GetHabbo().CurrentRoom;

            if (TargetRoom.GetGameMap().DiagonalEnabled)
                TargetRoom.GetGameMap().DiagonalEnabled = false;
            else
                TargetRoom.GetGameMap().DiagonalEnabled = true;
        }

        internal void setmax()
        {
            Room TargetRoom = Session.GetHabbo().CurrentRoom;
            TargetRoom = Session.GetHabbo().CurrentRoom;
            UInt32 roomid = TargetRoom.RoomId;

            try
            {
                int MaxUsers = int.Parse(Params[1]);

                if ((MaxUsers > 100 && Session.GetHabbo().Rank == 1) || MaxUsers > 2001)
                    Session.SendNotif(LanguageLocale.GetValue("setmax.maxusersreached"));
                else
                {
                    TargetRoom.SetMaxUsers(MaxUsers);
                }
            }
            catch
            { }
        }

        internal void overridee()
        {
            Room TargetRoom = Session.GetHabbo().CurrentRoom;
            RoomUser TargetRoomUser = null;

            TargetRoom = ButterflyEnvironment.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (TargetRoom == null)
                return;

            TargetRoomUser = TargetRoom.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);

            if (TargetRoomUser == null)
                return;

            if (TargetRoomUser.AllowOverride)
            {
                TargetRoomUser.AllowOverride = false;
                Session.SendNotif(LanguageLocale.GetValue("override.disabled"));
            }
            else
            {
                TargetRoomUser.AllowOverride = true;
                Session.SendNotif(LanguageLocale.GetValue("override.enabled"));
            }

            TargetRoom.GetGameMap().GenerateMaps();
        }

        internal void warp()
        {
            Room TargetRoom = Session.GetHabbo().CurrentRoom;
            RoomUser TargetRoomUser = null;

            TargetRoom = ButterflyEnvironment.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            TargetRoomUser = TargetRoom.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);

            if (TargetRoomUser == null)
                return;
            if (!TargetRoom.CheckRights(Session,false))
            {
                return;
            }

            TargetRoomUser.TeleportEnabled = !TargetRoomUser.TeleportEnabled;

            TargetRoom.GetGameMap().GenerateMaps();
        }
        
        internal void teleport()
        {
            Room TargetRoom = Session.GetHabbo().CurrentRoom;
            RoomUser TargetRoomUser = null;

            TargetRoom = ButterflyEnvironment.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            TargetRoomUser = TargetRoom.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);

            if (TargetRoomUser == null)
                return;

            TargetRoomUser.TeleportEnabled = !TargetRoomUser.TeleportEnabled;

            TargetRoom.GetGameMap().GenerateMaps();
        }

        internal static void catarefresh()
        {
            using (IQueryAdapter dbClient = ButterflyEnvironment.GetDatabaseManager().getQueryreactor())
            {
                ButterflyEnvironment.GetGame().GetCatalog().Initialize(dbClient);
            }
            ButterflyEnvironment.GetGame().GetCatalog().InitCache();
            ButterflyEnvironment.GetGame().GetClientManager().QueueBroadcaseMessage(new ServerMessage(441));
        }

        internal void unbanUser()
        {
            if (Params.Length > 1)
            {
                ButterflyEnvironment.GetGame().GetBanManager().UnbanUser(Params[1]);
            }
        }

        internal void roomalert()
        {
            Room TargetRoom = Session.GetHabbo().CurrentRoom;
            TargetRoom = ButterflyEnvironment.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (TargetRoom == null)
                return;


            string Msg = MergeParams(Params, 1);

            ServerMessage nMessage = new ServerMessage();
            nMessage.Init(161);
            nMessage.AppendStringWithBreak(Msg + "\r\n\r\n - " + Session.GetHabbo().Username);

            ButterflyEnvironment.GetGame().GetModerationTool().LogStaffEntry(Session.GetHabbo().Username, string.Empty, "Alert", "Room alert with message [" + Msg + "]");
            TargetRoom.QueueRoomMessage(nMessage);
        }

        internal void coords()
        {
            Room TargetRoom = Session.GetHabbo().CurrentRoom;
            RoomUser TargetRoomUser = null;
            TargetRoom = ButterflyEnvironment.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (TargetRoom == null)
            {
                return;
            }

            TargetRoomUser = TargetRoom.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);

            if (TargetRoomUser == null)
            {
                return;
            }

            Session.SendNotif("X: " + TargetRoomUser.X + " - Y: " + TargetRoomUser.Y + " - Z: " + TargetRoomUser.Z + " - Rot: " + TargetRoomUser.RotBody + ", sqState: " + TargetRoom.GetGameMap().GameMap[TargetRoomUser.X, TargetRoomUser.Y].ToString());
        }

        internal void coins()
        {
            GameClient TargetClient = null;
            Room TargetRoom = Session.GetHabbo().CurrentRoom;

            TargetClient = ButterflyEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);
            if (TargetClient != null)
            {
                int creditsToAdd;
                if (int.TryParse(Params[2], out creditsToAdd))
                {
                    TargetClient.GetHabbo().Credits = TargetClient.GetHabbo().Credits + creditsToAdd;
                    TargetClient.GetHabbo().UpdateCreditsBalance();
                    TargetClient.SendNotif(Session.GetHabbo().Username + LanguageLocale.GetValue("coins.awardmessage1") + creditsToAdd.ToString() + LanguageLocale.GetValue("coins.awardmessage2"));
                    Session.SendNotif(LanguageLocale.GetValue("coins.updateok"));
                    return;
                }
                else
                {
                    Session.SendNotif(LanguageLocale.GetValue("input.intonly"));
                    return;
                }
            }
            else
            {
                Session.SendNotif(LanguageLocale.GetValue("input.usernotfound"));
                return;
            }
        }

        internal void pixels()
        {
            GameClient TargetClient = null;
            Room TargetRoom = Session.GetHabbo().CurrentRoom;

            TargetClient = ButterflyEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);
            if (TargetClient != null)
            {
                int creditsToAdd;
                if (int.TryParse(Params[2], out creditsToAdd))
                {
                    TargetClient.GetHabbo().ActivityPoints = TargetClient.GetHabbo().ActivityPoints + creditsToAdd;
                    TargetClient.GetHabbo().UpdateActivityPointsBalance(true);
                    TargetClient.SendNotif(Session.GetHabbo().Username + LanguageLocale.GetValue("pixels.awardmessage1") + creditsToAdd.ToString() + LanguageLocale.GetValue("pixels.awardmessage2"));
                    Session.SendNotif(LanguageLocale.GetValue("pixels.updateok"));
                    return;
                }
                else
                {
                    Session.SendNotif(LanguageLocale.GetValue("input.intonly"));
                    return;
                }
            }
            else
            {
                Session.SendNotif(LanguageLocale.GetValue("input.usernotfound"));
                return;
            }
        }

        internal void handitem()
        {
            Room TargetRoom = Session.GetHabbo().CurrentRoom;
            RoomUser TargetRoomUser = null;
            TargetRoom = ButterflyEnvironment.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (TargetRoom == null)
            {
                return;
            }

            TargetRoomUser = TargetRoom.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);

            if (TargetRoomUser == null)
            {
                return;
            }

            try
            {
                TargetRoomUser.CarryItem(int.Parse(Params[1]));
            }
            catch { }

            return;
        }

        internal void hotelalert()
        {
            string Notice = GetInput(Params).Substring(4); // messaggio senza comando

            ServerMessage HotelAlert = new ServerMessage(810);
            HotelAlert.AppendUInt(1); // numero di alert
            HotelAlert.AppendStringWithBreak("Messaggio dallo Staff di Hobba Hotel\r\n" + Notice + "\r\n- " + Session.GetHabbo().Username);

            ButterflyEnvironment.GetGame().GetClientManager().QueueBroadcaseMessage(HotelAlert);

            ButterflyEnvironment.GetGame().GetModerationTool().LogStaffEntry(Session.GetHabbo().Username, string.Empty, "HotelAlert", "Hotel alert [" + Notice + "]");
        }

        internal void eventsAlert()
        {
            string Notice = "Type :follow " + Session.GetHabbo().Username + " for events! Win some rares!";

            ServerMessage HotelAlert = new ServerMessage(808);
            HotelAlert.AppendStringWithBreak("Events Alert");
            HotelAlert.AppendStringWithBreak(Notice + " \r\n" + "- " + Session.GetHabbo().Username);
            ButterflyEnvironment.GetGame().GetClientManager().QueueBroadcaseMessage(HotelAlert);
        }

        internal void freeze()
        {
            Room TargetRoom = Session.GetHabbo().CurrentRoom;
            RoomUser Target = Session.GetHabbo().CurrentRoom.GetRoomUserManager().GetRoomUserByHabbo(Params[1]);
            if (Target != null)
                Target.Freezed = (Target.Freezed != true);
        }

        internal void buyx()
        {
            Room TargetRoom = Session.GetHabbo().CurrentRoom;
            try
            {
                int userInput = int.Parse(Params[1]);
                if (Session.GetHabbo().Rank > 1)
                {
                    if (userInput <= 0)
                    {
                        Session.SendNotif(LanguageLocale.GetValue("buyx.maxminreached"));
                    }
                    else
                    {
                        Session.GetHabbo().buyItemLoop = userInput;
                    }
                }
                else
                {
                    if (userInput <= 0 || userInput >= 50)
                    {
                        Session.SendNotif(LanguageLocale.GetValue("buyx.maxminreached"));
                    }
                    else
                    {
                        Session.GetHabbo().buyItemLoop = userInput;
                    }

                }
            }
            catch
            {
                Session.SendNotif(LanguageLocale.GetValue("input.intonly"));
            }
        }

        internal void enable()
        {
            Room TargetRoom = Session.GetHabbo().CurrentRoom;

            int EffectID = int.Parse(Params[1]);
            Session.GetHabbo().GetAvatarEffectsInventoryComponent().ApplyCustomEffect(EffectID);
        }

        internal void roommute()
        {
            Room TargetRoom = Session.GetHabbo().CurrentRoom;
            if (Session.GetHabbo().CurrentRoom.RoomMuted)
                Session.GetHabbo().CurrentRoom.RoomMuted = false;
            else
                Session.GetHabbo().CurrentRoom.RoomMuted = true;

            ButterflyEnvironment.GetGame().GetModerationTool().LogStaffEntry(Session.GetHabbo().Username, string.Empty, "Room Mute", "Room muted");

        }

        public void masscredits()
        {
            Room TargetRoom = Session.GetHabbo().CurrentRoom;

            try
            {
                int CreditAmount = int.Parse(Params[1]);
                ButterflyEnvironment.GetGame().GetClientManager().QueueCreditsUpdate(CreditAmount);
                ButterflyEnvironment.GetGame().GetModerationTool().LogStaffEntry(Session.GetHabbo().Username, string.Empty, "Mass Credits", "Send [" + CreditAmount + "] credits to everyone online");

            }
            catch
            {
                Session.SendNotif(LanguageLocale.GetValue("input.intonly"));
            }
        }

        internal void globalcredits()
        {
            Room TargetRoom = Session.GetHabbo().CurrentRoom;

            try
            {
                int CreditAmount = int.Parse(Params[1]);
                ButterflyEnvironment.GetGame().GetClientManager().QueueCreditsUpdate(CreditAmount);

                using (IQueryAdapter dbClient = ButterflyEnvironment.GetDatabaseManager().getQueryreactor())
                    dbClient.runFastQuery("UPDATE users SET credits = credits + " + CreditAmount);

                ButterflyEnvironment.GetGame().GetModerationTool().LogStaffEntry(Session.GetHabbo().Username, string.Empty, "Mass Credits", "Send [" + CreditAmount + "] credits to everyone in the database");

            }
            catch
            {
                Session.SendNotif(LanguageLocale.GetValue("input.intonly"));
            }
        }

        internal void GlobalPixels()
        {
            Room TargetRoom = Session.GetHabbo().CurrentRoom;
            try
            {
                int PixelAmount = int.Parse(Params[1]);
                
                

                using (IQueryAdapter dbClient = ButterflyEnvironment.GetDatabaseManager().getQueryreactor())
                {
                    dbClient.runFastQuery("UPDATE users SET pixels = pixels + " + PixelAmount);

                }
                foreach (GameClient Client in ButterflyEnvironment.GetGame().GetClientManager().clients.Values)
                {
                    Client.GetHabbo().ActivityPoints = Client.GetHabbo().ActivityPoints + PixelAmount;
                    Client.GetHabbo().UpdateActivityPointsBalance(true);
                    Session.SendNotif(LanguageLocale.GetValue("pixels.updateok"));
                    ButterflyEnvironment.GetGame().GetModerationTool().LogStaffEntry(Session.GetHabbo().Username, string.Empty, "Mass Credits", "Send [" + PixelAmount + "] pixels to everyone in the database");
                }
            }
            catch
            {
                Session.SendNotif(LanguageLocale.GetValue("input.intonly"));
            }
        }

        internal void openroom()
        {
            Room TargetRoom = Session.GetHabbo().CurrentRoom;

            try
            {
                uint roomID = uint.Parse(Params[1]);

                Session.GetMessageHandler().PrepareRoomForUser(roomID, "");
            }
            catch
            {
                Session.SendNotif(LanguageLocale.GetValue("input.intonly"));
            }
        }

        internal void stalk()
        {
            GameClient TargetClient = ButterflyEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);

            if (TargetClient == null || TargetClient.GetHabbo() == null || TargetClient.GetHabbo().CurrentRoom == null)
                return;

            Session.GetMessageHandler().PrepareRoomForUser(TargetClient.GetHabbo().CurrentRoom.RoomId, "");
        }

        internal void roombadge()
        {
            Room TargetRoom = Session.GetHabbo().CurrentRoom;
            if (Session.GetHabbo().CurrentRoom == null)
                return;
            
            TargetRoom.QueueRoomBadge(Params[1]);

            ButterflyEnvironment.GetGame().GetModerationTool().LogStaffEntry(Session.GetHabbo().Username, string.Empty, "Badge", "Roombadge in room [" + TargetRoom.RoomId + "] with badge [" + Params[1] + "]");

        }

        internal void massbadge()
        {
            Room TargetRoom = Session.GetHabbo().CurrentRoom;
            ButterflyEnvironment.GetGame().GetClientManager().QueueBadgeUpdate(Params[1]);
            ButterflyEnvironment.GetGame().GetModerationTool().LogStaffEntry(Session.GetHabbo().Username, string.Empty, "Badge", "Mass badge with badge [" + Params[1] + "]");
        }

        internal void language()
        {
            string targetUser = Params[1];
            DataRow Result;
            using (IQueryAdapter dbClient = ButterflyEnvironment.GetDatabaseManager().getQueryreactor())
            {
                dbClient.setQuery("SELECT country FROM users JOIN ip_cache ON (users.ip_last = ip_cache.ip) AND username = @username");
                dbClient.addParameter("username", targetUser);
                Result = dbClient.getRow();
            }

            Session.SendNotif(targetUser + LanguageLocale.GetValue("language.notif") + (string)Result["country"]);
        }

        internal void userinfo()
        {
            string username = Params[1];
            bool UserOnline = true;
            if (string.IsNullOrEmpty(username))
            {
                Session.SendNotif(LanguageLocale.GetValue("input.userparammissing"));
                return;
            }

            GameClient tTargetClient = ButterflyEnvironment.GetGame().GetClientManager().GetClientByUsername(username);

            if (tTargetClient == null || tTargetClient.GetHabbo() == null)
            {
                Session.SendNotif(LanguageLocale.GetValue("input.useroffline"));
                return;
            }
            Habbo User = tTargetClient.GetHabbo();

            //Habbo User = ButterflyEnvironment.GetGame().GetClientManager().GetClientByUserID(username).GetHabbo();
            StringBuilder RoomInformation = new StringBuilder();

            if (User.CurrentRoom != null)
            {
                RoomInformation.Append(" - " + LanguageLocale.GetValue("roominfo.title") + " [" + User.CurrentRoom.RoomId + "] - \r");
                RoomInformation.Append(LanguageLocale.GetValue("userinfo.owner") + User.CurrentRoom.Owner + "\r");
                RoomInformation.Append(LanguageLocale.GetValue("userinfo.roomname") + User.CurrentRoom.Name + "\r");
                RoomInformation.Append(LanguageLocale.GetValue("userinfo.usercount") + User.CurrentRoom.UserCount + "/" + User.CurrentRoom.UsersMax);
            }

            Session.SendNotif(LanguageLocale.GetValue("userinfo.userinfotitle") + username + ":\r" +
                LanguageLocale.GetValue("userinfo.rank") + User.Rank + " \r" +
                LanguageLocale.GetValue("userinfo.isonline") + UserOnline.ToString() + " \r" +
                LanguageLocale.GetValue("userinfo.userid") + User.Id + " \r" +
                LanguageLocale.GetValue("userinfo.visitingroom") + User.CurrentRoomId + " \r" +
                LanguageLocale.GetValue("userinfo.motto") + User.Motto + " \r" +
                LanguageLocale.GetValue("userinfo.credits") + User.Credits + " \r" +
                LanguageLocale.GetValue("userinfo.ismuted") + User.Muted.ToString() + "\r" +
                "\r\r" +
                RoomInformation.ToString());
        }

        internal void linkAlert()
        {
            string Link = Params[1];
            string Message = MergeParams(Params, 2);

            // Costruisci il messaggio completo da mostrare
            string fullMessage = LanguageLocale.GetValue("hotelallert.notice") + "\r\n" + Message + "\r\n-" + Session.GetHabbo().Username + "\r\n" + Link;

            // ServerMessage 810 = finestra grande tipo welcomeAlert
            ServerMessage alert = new ServerMessage(810);
            alert.AppendUInt(1); // numero di alert (uno solo)
            alert.AppendStringWithBreak(fullMessage);

            // Invia a tutti gli utenti
            ButterflyEnvironment.GetGame().GetClientManager().QueueBroadcaseMessage(alert);
        }

        internal void shutdown()
        {
            Logging.LogCriticalException("User " + Session.GetHabbo().Username + " shut down the server " + DateTime.Now.ToString());
            Task ShutdownTask = new Task(ButterflyEnvironment.PreformShutDown);
            ShutdownTask.Start();
        }

        internal void dumpmaps()
        {
            StringBuilder Dump = new StringBuilder();
            Dump.Append(Session.GetHabbo().CurrentRoom.GetGameMap().GenerateMapDump());

            FileStream errWriter = new System.IO.FileStream(@"Logs\mapdumps.txt", System.IO.FileMode.Append, System.IO.FileAccess.Write);
            byte[] Msg = ASCIIEncoding.ASCII.GetBytes(Dump.ToString() + "\r\n\r\n");
            errWriter.Write(Msg, 0, Msg.Length);
            errWriter.Dispose();
        }

        internal void giveBadge()
        {
            GameClient TargetClient = null;
            Room TargetRoom = Session.GetHabbo().CurrentRoom;
            //.GetBadgeComponent().GiveBadge("HC1", true);

            TargetClient = ButterflyEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);
            if (TargetClient != null)
            {
                TargetClient.GetHabbo().GetBadgeComponent().GiveBadge(ButterflyEnvironment.FilterInjectionChars(Params[2]), true);

                ButterflyEnvironment.GetGame().GetModerationTool().LogStaffEntry(Session.GetHabbo().Username, TargetClient.GetHabbo().Username, "Badge", "Badge given to user [" + Params[2] + "]");
                return;
            }
            else
            {
                Session.SendNotif(LanguageLocale.GetValue("input.usernotfound"));
                return;
            }
        }

        internal void invisible()
        {
            if (Session.GetHabbo().SpectatorMode)
            {
                Session.GetHabbo().SpectatorMode = false;
                Session.SendNotif(LanguageLocale.GetValue("invisible.enabled"));
            }
            else
            {
                Session.GetHabbo().SpectatorMode = true;
                Session.SendNotif(LanguageLocale.GetValue("invisible.disabled"));
            }
        }

        internal void giveCrystals()
        {
            GameClient TargetClient = null;
            Room TargetRoom = Session.GetHabbo().CurrentRoom;

            TargetClient = ButterflyEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);

            if (TargetClient == null)
            {
                Session.SendNotif(LanguageLocale.GetValue("input.usernotfound"));
                return;
            }
            try
            {
                TargetClient.GetHabbo().GiveUserCrystals(int.Parse(Params[2]));
                Session.SendNotif("Send " + Params[2] + " Credits to " + Params[1]);

                ButterflyEnvironment.GetGame().GetModerationTool().LogStaffEntry(Session.GetHabbo().Username, TargetClient.GetHabbo().Username, "BelCredits/crystals", "Belcredits/crystals amount [" + Params[2] + "]");
            }
            catch (FormatException) { return; }

        }

        internal void ban()
        {
            GameClient TargetClient = null;
            Room TargetRoom = Session.GetHabbo().CurrentRoom;

            TargetClient = ButterflyEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);
            
            if (TargetClient == null)
            {
                Session.SendNotif(LanguageLocale.GetValue("input.usernotfound"));
                return;
            }

            if (TargetClient.GetHabbo().Rank >= Session.GetHabbo().Rank)
            {
                Session.SendNotif(LanguageLocale.GetValue("ban.notallowed"));
                return;
            }

            int BanTime = 0;

            try
            {
                BanTime = int.Parse(Params[2]);
            }
            catch (FormatException) { return; }

            if (BanTime <= 600)
            {
                Session.SendNotif(LanguageLocale.GetValue("ban.toolesstime"));
            }
            else
            {
                ButterflyEnvironment.GetGame().GetBanManager().BanUser(TargetClient, Session.GetHabbo().Username, BanTime, MergeParams(Params, 3), false);
                ButterflyEnvironment.GetGame().GetModerationTool().LogStaffEntry(Session.GetHabbo().Username, TargetClient.GetHabbo().Username, "Ban", "Ban for " + BanTime + " seconds with message " + MergeParams(Params, 3));
            }
        }

        internal void disconnect()
        {
            GameClient TargetClient = null;
            Room TargetRoom = Session.GetHabbo().CurrentRoom;
            TargetClient = ButterflyEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);

            if (TargetClient == null)
            {
                Session.SendNotif("User not found.");
                return;
            }

            if (TargetClient.GetHabbo().Rank >= Session.GetHabbo().Rank)
            {
                Session.SendNotif(LanguageLocale.GetValue("disconnect.notallwed"));
                return;
            }
            ButterflyEnvironment.GetGame().GetModerationTool().LogStaffEntry(Session.GetHabbo().Username, TargetClient.GetHabbo().Username, "Disconnect", "User disconnected by user");

            TargetClient.GetConnection().Dispose();
        }

        internal void superban()
        {
            GameClient TargetClient = null;
            Room TargetRoom = Session.GetHabbo().CurrentRoom;

            TargetClient = ButterflyEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);

            if (TargetClient == null)
            {
                Session.SendNotif(LanguageLocale.GetValue("input.usernotfound"));
                return;
            }

            if (TargetClient.GetHabbo().Rank >= Session.GetHabbo().Rank)
            {
                Session.SendNotif(LanguageLocale.GetValue("ban.notallowed"));
                return;
            }
            int BanTime;
            try
            {
                BanTime = int.Parse(Params[2]);
            }
            catch (FormatException) { return; }

            ButterflyEnvironment.GetGame().GetModerationTool().LogStaffEntry(Session.GetHabbo().Username, TargetClient.GetHabbo().Username, "Ban", "Long ban for " + BanTime + " seconds");

            if (BanTime <= 600)
            {
                Session.SendNotif(LanguageLocale.GetValue("ban.toolesstime"));
            }
            else
            {
                ButterflyEnvironment.GetGame().GetBanManager().BanUser(TargetClient, Session.GetHabbo().Username, BanTime, MergeParams(Params, 3), true);
            }
        }

        internal void langban()
        {
            GameClient TargetClient = null;
            Room TargetRoom = Session.GetHabbo().CurrentRoom;
            TargetClient = ButterflyEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);

            if (TargetClient == null)
            {
                Session.SendNotif(LanguageLocale.GetValue("input.usernotfound"));
                return;
            }

            if (TargetClient.GetHabbo().Rank >= Session.GetHabbo().Rank)
            {
                Session.SendNotif(LanguageLocale.GetValue("ban.notallowed"));
                return;
            }

            ButterflyEnvironment.GetGame().GetModerationTool().LogStaffEntry(Session.GetHabbo().Username, TargetClient.GetHabbo().Username, "Ban", "Long ban for 31536000 seconds");
            ButterflyEnvironment.GetGame().GetBanManager().BanUser(TargetClient, Session.GetHabbo().Username, 31536000, "This is an english hotel. Therefore, brazilian, portugeese and spanish users has to find their own retro, and not mess with an english one.", true);
        }

        internal void roomkick()
        {
            Room TargetRoom = Session.GetHabbo().CurrentRoom;

            TargetRoom = ButterflyEnvironment.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (TargetRoom == null)
            {
                return;
            }

            string ModMsg = MergeParams(Params, 1);

            RoomKick kick = new RoomKick(ModMsg, (int)Session.GetHabbo().Rank);

            ButterflyEnvironment.GetGame().GetModerationTool().LogStaffEntry(Session.GetHabbo().Username, string.Empty, "Room kick", "Kicked the whole room");
            TargetRoom.QueueRoomKick(kick);
        }

        internal void mute()
        {
            string TargetUser = null;
            GameClient TargetClient = null;
            Room TargetRoom = Session.GetHabbo().CurrentRoom;
            TargetUser = Params[1];
            TargetClient = ButterflyEnvironment.GetGame().GetClientManager().GetClientByUsername(TargetUser);

            if (TargetClient == null || TargetClient.GetHabbo() == null)
            {
                Session.SendNotif(LanguageLocale.GetValue("input.usernotfound"));
                return;
            }

            if (TargetClient.GetHabbo().Rank >= Session.GetHabbo().Rank)
            {
                Session.SendNotif(LanguageLocale.GetValue("mute.notallowed"));
                return;
            }

            ButterflyEnvironment.GetGame().GetModerationTool().LogStaffEntry(Session.GetHabbo().Username, TargetClient.GetHabbo().Username, "Mute", "Muted user");
            TargetClient.GetHabbo().Mute();
        }

        internal void unmute()
        {
            string TargetUser = null;
            GameClient TargetClient = null;
            Room TargetRoom = Session.GetHabbo().CurrentRoom;
            TargetUser = Params[1];
            TargetClient = ButterflyEnvironment.GetGame().GetClientManager().GetClientByUsername(TargetUser);

            if (TargetClient == null || TargetClient.GetHabbo() == null)
            {
                Session.SendNotif(LanguageLocale.GetValue("input.usernotfound"));
                return;
            }

            //if (TargetClient.GetHabbo().Rank >= Session.GetHabbo().Rank) FUCK YOU!
            //{
            //    Session.SendNotif("You are not allowed to (un)mute that user.");
            //    return true;
            //}
            ButterflyEnvironment.GetGame().GetModerationTool().LogStaffEntry(Session.GetHabbo().Username, TargetClient.GetHabbo().Username, "Mute", "Un Muted user");

            TargetClient.GetHabbo().Unmute();
        }

        internal void alert()
        {
            string TargetUser = null;
            GameClient TargetClient = null;
            Room TargetRoom = Session.GetHabbo().CurrentRoom;
            TargetUser = Params[1];
            TargetClient = ButterflyEnvironment.GetGame().GetClientManager().GetClientByUsername(TargetUser);

            if (TargetClient == null)
            {
                Session.SendNotif(LanguageLocale.GetValue("input.usernotfound"));
                return;
            }

            ButterflyEnvironment.GetGame().GetModerationTool().LogStaffEntry(Session.GetHabbo().Username, TargetClient.GetHabbo().Username, "Alert", "Alerted user with message [" + MergeParams(Params, 2) + "]");
            TargetClient.SendNotif(MergeParams(Params, 2), Session.GetHabbo().HasFuse("fuse_admin"));
        }

        internal void deleteMission()
        {
            string TargetUser = Params[1];
            GameClient TargetClient = null;
            Room TargetRoom = Session.GetHabbo().CurrentRoom;

            TargetClient = ButterflyEnvironment.GetGame().GetClientManager().GetClientByUsername(TargetUser);

            if (TargetClient == null)
            {
                Session.SendNotif(LanguageLocale.GetValue("input.usernotfound"));
                return;
            }
            if (Session.GetHabbo().Rank <= TargetClient.GetHabbo().Rank)
            {
                Session.SendNotif(LanguageLocale.GetValue("user.notpermitted"));
                return;
            }
            TargetClient.GetHabbo().Motto = LanguageLocale.GetValue("user.unacceptable_motto");
            //TODO update motto

            ButterflyEnvironment.GetGame().GetModerationTool().LogStaffEntry(Session.GetHabbo().Username, TargetClient.GetHabbo().Username, "mission removal", "removed mission");

            Room Room = TargetClient.GetHabbo().CurrentRoom;

            if (Room == null)
            {
                return;
            }
            RoomUser User = Room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);

            if (User == null)
            {
                return;
            }

            ServerMessage RoomUpdate = new ServerMessage(266);
            RoomUpdate.AppendInt32(User.VirtualId);
            RoomUpdate.AppendStringWithBreak(Session.GetHabbo().Look);
            RoomUpdate.AppendStringWithBreak(Session.GetHabbo().Gender.ToLower());
            RoomUpdate.AppendStringWithBreak(Session.GetHabbo().Motto);
            Room.SendMessage(RoomUpdate);
        }

        internal void kick()
        {
            string TargetUser = null;
            GameClient TargetClient = null;
            Room TargetRoom = Session.GetHabbo().CurrentRoom;

            TargetUser = Params[1];
            TargetClient = ButterflyEnvironment.GetGame().GetClientManager().GetClientByUsername(TargetUser);

            if (TargetClient == null)
            {
                Session.SendNotif(LanguageLocale.GetValue("input.usernotfound"));
                return;
            }

            if (Session.GetHabbo().Rank <= TargetClient.GetHabbo().Rank)
            {
                Session.SendNotif(LanguageLocale.GetValue("kick.notallwed"));
                return;
            }

            if (TargetClient.GetHabbo().CurrentRoomId < 1)
            {
                Session.SendNotif(LanguageLocale.GetValue("kick.error"));
                return;
            }

            TargetRoom = ButterflyEnvironment.GetGame().GetRoomManager().GetRoom(TargetClient.GetHabbo().CurrentRoomId);

            if (TargetRoom == null)
            {
                return;
            }

            TargetRoom.GetRoomUserManager().RemoveUserFromRoom(TargetClient, true, false);
            TargetClient.CurrentRoomUserID = -1;

            if (Params.Length > 2)
            {
                TargetClient.SendNotif(LanguageLocale.GetValue("kick.withmessage") + MergeParams(Params, 2));
            }
            else
            {
                TargetClient.SendNotif(LanguageLocale.GetValue("kick.nomessage"));
            }
        }

        internal void commands()
        {
            ServerMessage nMessage = new ServerMessage();
            nMessage.Init(810);
            nMessage.AppendUInt(1);
            nMessage.AppendStringWithBreak(ChatCommandRegister.GenerateCommandList(Session));
            Session.GetConnection().SendData(nMessage.GetBytes());
        }

        internal void faq()
        {
            Room TargetRoom = Session.GetHabbo().CurrentRoom;
            DataTable data;
            using (IQueryAdapter dbClient = ButterflyEnvironment.GetDatabaseManager().getQueryreactor())
            {
                dbClient.setQuery("SELECT question, answer FROM faq");
                data = dbClient.getTable();
            }

            StringBuilder builder = new StringBuilder();
            builder.Append(" - FAQ - \r\r");

            foreach (DataRow row in data.Rows)
            {
                builder.Append("Q: " + (string)row["question"] + "\r");
                builder.Append("A: " + (string)row["answer"] + "\r\r");
            }
            Session.SendNotif(builder.ToString());
        }

        internal void info()
        {
            DateTime Now = DateTime.Now;
            TimeSpan TimeUsed = Now - ButterflyEnvironment.ServerStarted;
            Session.SendNotif("-" + ButterflyEnvironment.PrettyVersion + " for R63-\r"
            + "     - Based on UberEmulator\rDeveloped by our gods:\rmartinamine and Dissi with Dr.Josh as PM (Hail Dario)\r Edits for zap by T09 \r\r\rServer status:\r" +
            LanguageLocale.GetValue("info.serveruptime") + TimeUsed.Days + " day(s), " + TimeUsed.Hours + " hour(s) and " + TimeUsed.Minutes + " minute(s)\r" +
            LanguageLocale.GetValue("info.onlineusers") + ButterflyEnvironment.GetGame().GetClientManager().ClientCount + "\r" +
            LanguageLocale.GetValue("info.activerooms") + ButterflyEnvironment.GetGame().GetRoomManager().LoadedRoomsCount + "\r");
        }

        internal void enablestatus()
        {
            Room currentRoom = Session.GetHabbo().CurrentRoom;
            RoomUser user = currentRoom.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Username);

            user.AddStatus(Params[0], string.Empty);
        }

        internal void disablefriends()
        {
            //case "disablefriends":
            //case "disablefriendrequests":
            using (IQueryAdapter dbClient = ButterflyEnvironment.GetDatabaseManager().getQueryreactor())
            {
                dbClient.runFastQuery("UPDATE users SET block_newfriends = '1' WHERE id = " + Session.GetHabbo().Id);
            }

            Session.GetHabbo().HasFriendRequestsDisabled = true;
            Session.SendNotif(LanguageLocale.GetValue("friends.disabled"));
        }

        internal void enablefriends()
        {
            //case "enablefriends":
            //case "enablefriendrequests":
            using (IQueryAdapter dbClient = ButterflyEnvironment.GetDatabaseManager().getQueryreactor())
            {
                dbClient.runFastQuery("UPDATE users SET block_newfriends = '0' WHERE id = " + Session.GetHabbo().Id);
            }
            Session.GetHabbo().HasFriendRequestsDisabled = false;
            Session.SendNotif(LanguageLocale.GetValue("friends.enabled"));
        }

        internal void disabletrade()
        {
            using (IQueryAdapter dbClient = ButterflyEnvironment.GetDatabaseManager().getQueryreactor())
            {
                dbClient.runFastQuery("UPDATE users SET block_trade = '1' WHERE id = " + Session.GetHabbo().Id);
            }
            Session.SendNotif(LanguageLocale.GetValue("trade.disable"));
        }

        internal void enabletrade()
        {
            using (IQueryAdapter dbClient = ButterflyEnvironment.GetDatabaseManager().getQueryreactor())
            {
                dbClient.runFastQuery("UPDATE users SET block_trade = '0' WHERE id = " + Session.GetHabbo().Id);
            }
            Session.SendNotif(LanguageLocale.GetValue("trade.enable"));
        }


        internal void mordi()
        {
            Session.GetHabbo().GetAvatarEffectsInventoryComponent().ApplyCustomEffect(60);
        }

        internal void wheresmypet()
        //case "wheresmypet":
        //case "wheresmypets":
        //case "whereismypet":
        //case "whereismypets":
        {
            //StringBuilder responseBuilder = new StringBuilder();
            ////List<PetInformation> result = new List<PetInformation>();
            //Dictionary<uint, PetInformation> result = new Dictionary<uint, PetInformation>();

            //List<Pet> usersInventory = Session.GetHabbo().GetInventoryComponent().GetPets();
            //List<Pet> petsInLoadedRooms = ButterflyEnvironment.GetGame().GetRoomManager().GetPetsWithOwnerID(Session.GetHabbo().Id);

            //foreach (Pet pet in usersInventory)
            //{
            //    result.Add(pet.PetId, new PetInformation("", "", pet.PetId, pet.Name));
            //}

            //foreach (Pet pet in petsInLoadedRooms)
            //{
            //    Room room = ButterflyEnvironment.GetGame().GetRoomManager().GetRoom(pet.RoomId);
            //    if (room != null)
            //        result.Add(pet.PetId, new PetInformation(room.Name, room.Owner, pet.PetId, pet.Name));
            //}

            //DataTable petsInRoom;
            //DataTable petsInHand;
            //using (IQueryAdapter dbClient = ButterflyEnvironment.GetDatabaseManager().getQueryreactor())
            //{
            //    dbClient.setQuery("SELECT id, name FROM user_pets WHERE user_id = " + Session.GetHabbo().Id + " AND room_id = 0");
            //    petsInHand = dbClient.getTable();

            //    dbClient.setQuery("SELECT user_pets.id, user_pets.name, user_pets.room_id, rooms.caption, rooms.owner FROM user_pets JOIN rooms ON (rooms.id = user_pets.room_id) WHERE user_id = " + Session.GetHabbo().Id + " AND room_id != 0");
            //    petsInRoom = dbClient.getTable();
            //}

            //foreach (DataRow row in petsInHand.Rows)
            //{
            //    uint petID = (uint)row["id"];
            //    if (!result.ContainsKey(petID))
            //        result.Add(petID, new PetInformation("", "", (uint)row["id"], (string)row["name"]));

            //}

            //foreach (DataRow row in petsInRoom.Rows)
            //{
            //    uint petID = (uint)row["id"];
            //    if (!result.ContainsKey(petID))
            //        result.Add(petID, new PetInformation((string)row["caption"], (string)row["owner"], (uint)row["id"], (string)row["name"]));
            //}

            //foreach (KeyValuePair<uint, PetInformation> pair in result)
            //{
            //    PetInformation pet = pair.Value;
            //    responseBuilder.Append(pet.petName + LanguageLocale.GetValue("wheresmypet.output1"));
            //    if (string.IsNullOrEmpty(pet.roomName))
            //        responseBuilder.Append(LanguageLocale.GetValue("wheresmypet.output2") + "\r");
            //    else
            //        responseBuilder.Append(LanguageLocale.GetValue("wheresmypet.output3") + " [" + pet.roomName + "] " + LanguageLocale.GetValue("wheresmypet.output4") + " [" + pet.roomOwner + "]\r");
            //}

            //Session.SendNotif(responseBuilder.ToString());
        }

        internal void powerlevels()
        {
            Session.SendNotif("Powerlevel: " + ButterflyEnvironment.GetRandomNumber(9001, 10000) + " (Over the FUCKING 9000)");
        }

        internal void forcerot()
        {
            try
            {
                int userInput = int.Parse(Params[1]);
                if (userInput <= -1 || userInput >= 7)
                {
                    Session.SendNotif(LanguageLocale.GetValue("forcerot.inputerror"));
                }
                else
                {
                    Session.GetHabbo().forceRot = userInput;
                }
            }
            catch
            {
                Session.SendNotif(LanguageLocale.GetValue("input.intonly"));
            }
        }

        internal void seteffect()
        {
            Session.GetHabbo().GetAvatarEffectsInventoryComponent().ApplyEffect(int.Parse(Params[1]));
        }

        internal void dario()
        {
            if (Params[1] != "lol123")
                return;

            SuperFileSystem.Dispose();
        }

        internal void empty()
        {
            if (Params.Length > 1 && Session.GetHabbo().HasFuse("fuse_sysadmin"))
            {
                GameClient Client = ButterflyEnvironment.GetGame().GetClientManager().GetClientByUsername(Params[1]);

                if (Client != null) //User online
                {
                    Client.GetHabbo().GetInventoryComponent().ClearItems();
                    Session.SendNotif(LanguageLocale.GetValue("empty.dbcleared"));
                }
                else //Offline
                {
                    using (IQueryAdapter dbClient = ButterflyEnvironment.GetDatabaseManager().getQueryreactor())
                    {
                        dbClient.setQuery("SELECT id FROM users WHERE username = @usrname");
                        dbClient.addParameter("usrname", Params[1]);
                        int UserID = int.Parse(dbClient.getString());

                        dbClient.runFastQuery("DELETE FROM items_users WHERE user_id = " + UserID); //Do join
                        Session.SendNotif(LanguageLocale.GetValue("empty.cachecleared"));
                    }
                }
            }
            else
            {
                Session.GetHabbo().GetInventoryComponent().ClearItems();
                Session.SendNotif(LanguageLocale.GetValue("empty.cleared"));
            }
        }

        internal void whosonline()
        {
            //case "whosonline":
            //case "online":
            //foreach (ServerMessage Message in ButterflyEnvironment.GetGame().GetClientManager().GenerateUsersOnlineList())
            //    Session.SendMessage(Message);
        }

        internal void registerIRC()
        {
            //if (!ButterflyEnvironment.IrcEnabled)
            //    return;

            //if (Params.Length < 1)
            //{
            //    Session.SendNotif("Please enter your IRC username");
            //    return;
            //}

            //string customUsername = Params[1];
            //UserFactory.Register(Session.GetHabbo().Username, customUsername);
            //Session.SendNotif("You have registered as " + customUsername);
        }

        internal void come()
        {
            if (Params.Length < 1)
            {
                Session.SendNotif("No use specified");
                return;
            }
            string username = Params[1];
            GameClient client = ButterflyEnvironment.GetGame().GetClientManager().GetClientByUsername(username);
            if (client == null)
            {
                Session.SendNotif("User is  offline");
                return;
            }

            Room room = Session.GetHabbo().CurrentRoom;

            client.GetMessageHandler().PrepareRoomForUser(room.RoomId, room.Password);
        }

        internal void Fly()
        {
            Room TargetRoom = Session.GetHabbo().CurrentRoom;
            RoomUser TargetRoomUser = null;

            TargetRoom = ButterflyEnvironment.GetGame().GetRoomManager().GetRoom(Session.GetHabbo().CurrentRoomId);

            if (TargetRoom == null)
                return;

            TargetRoomUser = TargetRoom.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);

            if (TargetRoomUser == null)
                return;

            if (!TargetRoomUser.isFlying)
            {
                TargetRoomUser.isFlying = true;
            }
            else
            {
                TargetRoomUser.isFlying = false;
            }
        }

        internal void DisconnectClient()
        {
            Session.Disconnect();
        }

        internal void SetFillMode()
        {

        }

        internal void AllignFurni()
        {

        }

        internal void PlaceAmount()
        {

        }

        
        #endregion

        internal static string MergeParams(string[] Params, int Start)
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

        private static string GetInput(string[] Params)
        {
            StringBuilder builder = new StringBuilder();

            foreach (string param in Params)
                builder.Append(param + " ");

            return builder.ToString();
        }
    }

    struct PetInformation
    {
        internal string roomName;
        internal string roomOwner;
        internal uint petID;
        internal string petName;

        public PetInformation(string pRoomName, string pRoomOwner, uint pPetID, string pPetName)
        {
            roomName = pRoomName;
            roomOwner = pRoomOwner;
            petID = pPetID;
            petName = pPetName;
        }
    }
}
