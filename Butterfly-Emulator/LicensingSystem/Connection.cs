//using System;
//using System.Net;
//using System.Net.NetworkInformation;
//using System.Net.Sockets;
//using System.Security.Cryptography;
//using System.Text;
//using Butterfly.Core;
//using Butterfly.LicensingSystem.PacketEncoding;
//using System.Threading;

//namespace Butterfly.LicensingClient
//{
//    class Connection
//    {
//        private static Socket connection;

//        private static byte[] dataBuffer;

//        private static bool isClosed;
//        internal static int tokenID = 5487;

//        private static int packetID
//        {
//            get
//            {
//                return Properties.Settings.Default.packetkey;
//            }
//            set
//            {
//                Properties.Settings.Default.packetkey = value;
//                Properties.Settings.Default.Save();
//            }
//        }

//        internal static void Init()
//        {
//            try
//            {
//                dataBuffer = new byte[1024];
//                connection = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
//                isClosed = false;

//                int port = GetPort();
//                IPAddress test1 = IPAddress.Parse("109.236.85.189");
//                IPEndPoint endPoint = new IPEndPoint(test1, port);
//                connection.Connect(endPoint);

//                WaitForPackets();

//                if (tokenID < 9001)
//                {
//                    Console.ForegroundColor = ConsoleColor.Red;
//                    Die(tokenID);

//                }
//                else if (tokenID > 9001)
//                {
//                    Console.WriteLine("Starting up Butterfly Emulator...");

//                    ButterflyEnvironment.Initialize();

//                    while (true)
//                    {
//                        Console.CursorVisible = true;
//                        if (Logging.DisabledState)
//                            Console.Write("bfly> ");
//                        ConsoleCommandHandeling.InvokeCommand(Console.ReadLine());
//                    }
//                }
//            }
//            catch (SocketException)
//            {
//                Die(6587);
//            }
//            catch (CryptographicException)
//            {
//                Die(1487);
//            }
//            catch (MalformedPacketException)
//            {
//                Die(1354);
//            }
//            catch
//            {
//                Die(11);
//            }

//            Die(0);
//        }

//        private static void Die(int errorID)
//        {
//            Console.ForegroundColor = ConsoleColor.Red;
//            Console.WriteLine("Boot denied. Error code: EX_x000" + errorID);

//            while (true)
//            {
//                if (Console.ReadLine() == "keyreset")
//                {
//                    Properties.Settings.Default.packetkey = 0;
//                    Properties.Settings.Default.Save();
//                }
//            }
//        }

//        private static void WaitForPackets()
//        {
//            while (true)
//            {
//                int length = connection.Receive(dataBuffer);
//                string[] dataReceived = Encoding.Default.GetString(dataBuffer, 0, length).Split(',');
//                foreach (string packet in dataReceived)
//                {
//                    if (string.IsNullOrEmpty(packet))
//                        continue;

//                    Response decodedpacket = new Response(packet, packetID++);
//                    if (ProcessPacket(decodedpacket))
//                    {
//                        return;
//                    }
//                }

//                Thread.Sleep(100);
//            }
//        }

//        private static int GetPort()
//        {

//            //UTF8Encoding utf8 = new UTF8Encoding();
//            //WebClient webClient = new WebClient();
//            //string externalIp = utf8.GetString(webClient.DownloadData(
//            //"http://whatismyip.org"));
//            string externalIp = "127.0.0.1";
//            int key = 0;
//            string[] parts = externalIp.Split('.');
//            for (int i = 0; i < parts.Length; i++)
//            {
//                key += int.Parse(parts[i]);
//            }

//            return key;
//        }

//        private static void SendPacket(Packet packet)
//        {
//            try
//            {

//                byte[] toSend = System.Text.Encoding.Default.GetBytes(packet.GetString() + ",");
//                connection.Send(toSend);
//                //packetsSent++;
//                //connection.BeginSend(toSend, 0, toSend.Length, SocketFlags.None, sendCallback, connection);
//            }
//            catch
//            {
//                CloseConnection();
//            }
//        }

//        private static void DataSent(IAsyncResult callback)
//        {
//            try
//            {
//                connection.EndSend(callback);
//            }
//            catch
//            {
//                CloseConnection();
//            }
//        }

//        internal static void CloseConnection()
//        {
//            if (!isClosed)
//            {
//                isClosed = true;
//                try
//                {
//                    connection.Close();
//                }
//                catch { }

//                //Console.ForegroundColor = ConsoleColor.Red;
//                //Console.WriteLine("Connection closed");
//                //ButterflyEnvironment.PreformShutDown();
//            }
//        }

//        private static bool ProcessPacket(Response packet)
//        {
//            switch (packet.PacketHeader)
//            {
//                case "hi":
//                    {

//                        ConfigurationData configData = new ConfigurationData(@"Settings/configuration.ini");
//                        if (!configData.data.ContainsKey("license.id"))
//                        {
//                            Console.WriteLine("FATAL ERROR: No license found.");
//                            break;
//                        }

//                        Console.WriteLine("Authorization request from server");
//                        Packet reply = new Packet(packetID++, "WHO");
//                        reply.Append(configData.data["license.id"]);
//                        reply.Append(Environment.MachineName);
//                        reply.Append(GenerateComputerIDHash());

//                        SendPacket(reply);
//                        break;
//                    }

//                case "rep":
//                    {
//                        switch (packet.GetArgument(0))
//                        {
//                            case "0":
//                                {
//                                    tokenID = 10;
//                                    return true;
//                                }

//                            case "1":
//                                {
//                                    tokenID = 6597;
//                                    return true;
//                                }

//                            case "2":
//                                {
//                                    tokenID = 10000;
//                                    return true;
//                                }
//                        }

//                        break;
//                    }
//            }
//            return false;
//        }

//        private static string RandomHash()
//        {
//            StringBuilder hash = new StringBuilder();
//            Random Rnd = new Random();
//            for (int f = 0; f < 10; f++)
//            {
//                string[] Chars = { "a", "b", "c", "d", "e", "f", "g", "h", "i", 
//                                 "j", "k", "l", "m", "n", "o", "p", "q", "r", 
//                                 "s", "t", "u", "v", "w", "x", "y", "z", "A", 
//                                 "B", "C", "D", "E", "F", "G", "H", "I", "J", 
//                                 "K", "L", "M", "N", "O", "P", "Q", "R", "S", 
//                                 "T", "U", "V", "W", "X", "Y", "Z", "1", "2", 
//                                 "3", "4", "5", "6", "7", "8", "9", "0" }; // "?", "!", "#", "¤", "%", "&", "(", ")", "=" };
//                for (int i = 0; i < 20; i++)
//                {
//                    hash.Append(Chars[Rnd.Next(0, Chars.Length)]);
//                }
//            }

//            return hash.ToString();
//        }

//        internal static string GenerateComputerIDHash()
//        {
//            return HashString(Environment.UserName + GetMacAddress() + Environment.OSVersion + Environment.MachineName + "sJe28KgPr71IpFcJtO5P");
//        }

//        private static string HashString(string Value)
//        {
//            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
//            byte[] data = System.Text.Encoding.ASCII.GetBytes(Value);
//            data = x.ComputeHash(data);
//            string ret = "";
//            for (int i = 0; i < data.Length; i++)
//                ret += data[i].ToString("x2").ToLower();
//            return ret;
//        }

//        private static string GetMacAddress()
//        {
//            string macAddresses = "";
//            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
//            {
//                if (nic.OperationalStatus == OperationalStatus.Up)
//                {
//                    macAddresses += nic.GetPhysicalAddress().ToString();
//                    break;
//                }
//            }
//            return macAddresses;
//        }
//    }
//}
