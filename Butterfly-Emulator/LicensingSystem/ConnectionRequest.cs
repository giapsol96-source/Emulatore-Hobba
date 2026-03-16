//using System;
//using System.Net;
//using System.Net.Sockets;
//using Butterfly.LicensingClient;

//namespace Butterfly.LicensingSystem
//{
//    class ConnectionRequest
//    {
//        private Socket connection;

//        private bool isClosed;

//        public ConnectionRequest()
//        {
//            try
//            {
//                this.connection = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
//                this.isClosed = false;

//                IPAddress test1 = IPAddress.Parse("109.236.85.189");
//                IPEndPoint endPoint = new IPEndPoint(test1, 9002);
//                Console.WriteLine("Connecting...");
//                connection.Connect(endPoint);
//            }
//            catch (SocketException)
//            {
//                Die(10);
//            }
//            catch
//            {
//                Die(0);
//            }

//            CloseConnection();
//        }

//        private static void Die(int errorID)
//        {
//            Console.ForegroundColor = ConsoleColor.Red;
//            Console.WriteLine("Boot denied. Error code: EX_x100" + errorID);

//            while (true)
//            {
//                Console.ReadLine();
//            }
//        }


//        internal void CloseConnection()
//        {
//            if (!isClosed)
//            {
//                isClosed = true;
//                try
//                {
                    
//                    connection.Close();
//                }
//                catch { }

//                Console.WriteLine("Connection closed.");
//                Connection.Init();
//            }
//        }
//    }
//}
