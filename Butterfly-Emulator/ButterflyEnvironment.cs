using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime;
using Butterfly.Core;
using Butterfly.HabboHotel;
using Butterfly.HabboHotel.Pets;
using Butterfly.Messages;
using Database_Manager.Database;
using Database_Manager.Database.Session_Details.Interfaces;
using Butterfly.Messages.StaticMessageHandlers;
using Butterfly.Messages.ClientMessages;
using Butterfly.Net;
using System.Globalization;

namespace Butterfly
{
    static class ButterflyEnvironment
    {
        private static ConfigurationData Configuration;
        private static Encoding DefaultEncoding;
        private static ConnectionHandeling ConnectionManager;
        private static Game Game;
        internal static DateTime ServerStarted;
        private static DatabaseManager manager;
        //internal static IRCBot messagingBot;
        internal static bool IrcEnabled;
        internal static bool groupsEnabled;
        internal static bool SystemMute;
        internal static bool useSSO;
        internal static bool isLive;
        internal const string PrettyVersion = "Inferno Emulator R63A - Build 2408";
        internal static bool diagPackets = false;
        internal static int timeout = 500;
        internal static DatabaseType dbType;
        internal static MusSocket MusSystem;
        internal static CultureInfo cultureInfo;

        internal static void Initialize()
        {
            Console.Clear();
            DateTime Start = DateTime.Now;
            SystemMute = false;

            IrcEnabled = false;
            ServerStarted = DateTime.Now;
            Console.Title = "Loading " + PrettyVersion;
            DefaultEncoding = Encoding.Default;
            Logging.WriteLine(PrettyVersion);

            Logging.WriteLine("");
            Logging.WriteLine("");

            cultureInfo = CultureInfo.CreateSpecificCulture("en-GB");
            LanguageLocale.Init();

            try
            {
                ChatCommandRegister.Init();
                PetCommandHandeler.Init();
                PetLocale.Init();
                Configuration = new ConfigurationData(System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath,@"Settings/configuration.ini"));

                DateTime Starts = DateTime.Now;
                Logging.WriteLine("Connecting to database...");

                // No one uses MSSQL?
                //dbType = GetConfig().data.ContainsKey("db.mssql") && GetConfig().data["db.mssql"] == "true" ? DatabaseType.MSSQL : DatabaseType.MySQL;
                dbType = DatabaseType.MySQL;

                manager = new DatabaseManager(uint.Parse(ButterflyEnvironment.GetConfig().data["db.pool.maxsize"]), int.Parse(ButterflyEnvironment.GetConfig().data["db.pool.minsize"]), dbType);
                manager.setServerDetails(
                    ButterflyEnvironment.GetConfig().data["db.hostname"],
                    uint.Parse(ButterflyEnvironment.GetConfig().data["db.port"]),
                    ButterflyEnvironment.GetConfig().data["db.username"],
                    ButterflyEnvironment.GetConfig().data["db.password"],
                    ButterflyEnvironment.GetConfig().data["db.name"]);
                manager.init();

                TimeSpan TimeUsed2 = DateTime.Now - Starts;
                Logging.WriteLine("Connected to database! (" + TimeUsed2.Seconds + " s, " + TimeUsed2.Milliseconds + " ms)");

                LanguageLocale.InitSwearWord();

                Game = new Game(int.Parse(ButterflyEnvironment.GetConfig().data["game.tcp.conlimit"]));
                Game.ContinueLoading();

                ConnectionManager = new ConnectionHandeling(int.Parse(ButterflyEnvironment.GetConfig().data["game.tcp.port"]),
                    int.Parse(ButterflyEnvironment.GetConfig().data["game.tcp.conlimit"]),
                    int.Parse(ButterflyEnvironment.GetConfig().data["game.tcp.conperip"]),
                    ButterflyEnvironment.GetConfig().data["game.tcp.enablenagles"].ToLower() == "true");
                ConnectionManager.init();
                    
                ConnectionManager.Start();

                StaticClientMessageHandler.Initialize();
                ClientMessageFactory.Init();

                string[] arrayshit = ButterflyEnvironment.GetConfig().data["mus.tcp.allowedaddr"].Split(Convert.ToChar(","));
                
                MusSystem = new MusSocket(ButterflyEnvironment.GetConfig().data["mus.tcp.bindip"], int.Parse(ButterflyEnvironment.GetConfig().data["mus.tcp.port"]), arrayshit, 0);
                
                //InitIRC(); 

                groupsEnabled = false;
                if (Configuration.data.ContainsKey("groups.enabled"))
                {
                    if (Configuration.data["groups.enabled"] == "true")
                    {
                        groupsEnabled = true;
                    }
                }

                useSSO = true;
                if (Configuration.data.ContainsKey("auth.ssodisabled"))
                {
                    if (Configuration.data["auth.ssodisabled"] == "false")
                    {
                        useSSO = false;
                    }
                }

                TimeSpan TimeUsed = DateTime.Now - Start;

                Logging.WriteLine("ENVIRONMENT -> READY! (" + TimeUsed.Seconds + " s, " + TimeUsed.Milliseconds + " ms)");
                isLive = true;
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Logging.WriteLine("Server is debugging: Console writing enabled");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Logging.WriteLine("Server is not debugging: Console writing disabled");
                    Logging.DisablePrimaryWriting(false);
                }
            }
            catch (KeyNotFoundException e)
            {
                Logging.WriteLine("Please check your configuration file - some values appear to be missing.");
                Logging.WriteLine("Press any key to shut down ...");
                Logging.WriteLine(e.ToString());
                Console.ReadKey(true);
                ButterflyEnvironment.Destroy();

                return;
            }
            catch (InvalidOperationException e)
            {
                Logging.WriteLine("Failed to initialize: " + e.Message);
                Logging.WriteLine("Press any key to shut down ...");

                Console.ReadKey(true);
                ButterflyEnvironment.Destroy();

                return;
            }

            catch (Exception e)
            {
                Console.WriteLine("Fatal error during startup: " + e.ToString());
                Console.WriteLine("Press a key to exit");

                Console.ReadKey();
                Environment.Exit(1);
            }
        }

        internal static void InitIRC()
        {
            //if (ButterflyEnvironment.GetConfig().data["irc.enabled"] == "true")
            //{
            //    UserFactory.Init();

            //    messagingBot = new IRCBot(
            //        ButterflyEnvironment.GetConfig().data["irc.server"],
            //        int.Parse(ButterflyEnvironment.GetConfig().data["irc.port"]),
            //        ButterflyEnvironment.GetConfig().data["irc.user"],
            //        ButterflyEnvironment.GetConfig().data["irc.nick"],
            //        ButterflyEnvironment.GetConfig().data["irc.channel"],
            //        ButterflyEnvironment.GetConfig().data["irc.password"]);

            //    messagingBot.Start();
            //    IrcEnabled = true;
            //}
        }

        //private static string encodeVL64(int i)
        //{
        //    byte[] wf = new byte[6];
        //    int pos = 0;
        //    int startPos = pos;
        //    int bytes = 1;
        //    int negativeMask = i >= 0 ? 0 : 4;
        //    i = Math.Abs(i);
        //    wf[pos++] = (byte)(64 + (i & 3));
        //    for (i >>= 2; i != 0; i >>= 6)
        //    {
        //        bytes++;
        //        wf[pos++] = (byte)(64 + (i & 0x3f));
        //    }

        //    wf[startPos] = (byte)(wf[startPos] | bytes << 3 | negativeMask);

        //    System.Text.ASCIIEncoding encoder = new ASCIIEncoding();
        //    string tmp = encoder.GetString(wf);
        //    return tmp.Replace("\0", "");
        //}

        internal static bool EnumToBool(string Enum)
        {
            return (Enum == "1");
        }

        

        internal static string BoolToEnum(bool Bool)
        {
            if (Bool)
            {
                return "1";
            }

            return "0";
        }

        internal static int GetRandomNumber(int Min, int Max)
        {
            RandomBase Quick = new Quick();
            return Quick.Next(Min, Max);
        }

        internal static int GetUnixTimestamp()
        {
            TimeSpan ts = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            double unixTime = ts.TotalSeconds;
            return (int)unixTime;
        }

        internal static string FilterInjectionChars(string Input)
        {
            return FilterInjectionChars(Input, false);
        }

        private static readonly List<char> allowedchars = new List<char>(new char[]{ 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 
                                                'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 
                                                'y', 'z', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '-', '.' });

        internal static string FilterFigure(string figure)
        {
            foreach (char character in figure)
            {
                if (!isValid(character))
                    return "lg-3023-1335.hr-828-45.sh-295-1332.hd-180-4.ea-3168-89.ca-1813-62.ch-235-1332";
            }

            return figure;
        }

        private static bool isValid(char character)
        {
            return allowedchars.Contains(character);
        }

        internal static string FilterInjectionChars(string Input, bool AllowLinebreaks)
        {
            Input = Input.Replace(Convert.ToChar(1), ' ');
            Input = Input.Replace(Convert.ToChar(2), ' ');
            //Input = Input.Replace(Convert.ToChar(3), ' ');
            Input = Input.Replace(Convert.ToChar(9), ' ');

            if (!AllowLinebreaks)
            {
                Input = Input.Replace(Convert.ToChar(13), ' ');
            }

            return Input;
        }

        internal static bool IsValidAlphaNumeric(string inputStr)
        {
            if (string.IsNullOrEmpty(inputStr))
            {
                return false;
            }

            for (int i = 0; i < inputStr.Length; i++)
            {
                if (!(char.IsLetter(inputStr[i])) && (!(char.IsNumber(inputStr[i]))))
                {
                    return false;
                }
            }

            return true;
        }

        internal static ConfigurationData GetConfig()
        {
            return Configuration;
        }

        //internal static DatabaseManagerOld GetDatabase()
        //{
        //    return DatabaseManager;
        //}

        internal static Encoding GetDefaultEncoding()
        {
            return DefaultEncoding;
        }

        internal static ConnectionHandeling GetConnectionManager()
        {
            return ConnectionManager;
        }

        internal static Game GetGame()
        {
            return Game;
        }

        //internal static Game GameInstance
        //{
        //    get
        //    {
        //        return Game;
        //    }
        //    set
        //    {
        //        Game = value;
        //    }
        //}

        internal static void Destroy()
        {
            isLive = false;
            Logging.WriteLine("Destroying Butterfly environment...");

            if (GetGame() != null)
            {
                GetGame().Destroy();
                Game = null;
            }

            if (GetConnectionManager() != null)
            {
                Logging.WriteLine("Destroying connection manager.");
                GetConnectionManager().Destroy();
                //ConnectionManager = null;
            }

            if (manager != null)
            {
                try
                {
                    Logging.WriteLine("Destroying database manager.");
                    //GetDatabase().StopClientMonitor();
                    manager.destroy();
                    //GetDatabase().DestroyDatabaseManager();
                    //DatabaseManager = null;
                }
                catch { }
            }

            Logging.WriteLine("Uninitialized successfully. Closing.");

            //Environment.Exit(0); Cba :P
        }

        private static bool ShutdownInitiated = false;

        internal static bool ShutdownStarted
        {
            get
            {
                return ShutdownInitiated;
            }
        }

        internal static void SendMassMessage(string Message)
        {
            try
            {
                ServerMessage HotelAlert = new ServerMessage(139);
                HotelAlert.AppendStringWithBreak(Message);
                ButterflyEnvironment.GetGame().GetClientManager().QueueBroadcaseMessage(HotelAlert);
            }
            catch (Exception e) { Logging.HandleException(e, "ButterflyEnvironment.SendMassMessage"); }
        }

        internal static DatabaseManager GetDatabaseManager()
        {
            return manager;
        }

        internal static void PreformShutDown()
        {
            PreformShutDown(true);
        }

        internal static void PreformShutDown(bool ExitWhenDone)
        {
            if (ShutdownInitiated || !isLive)
                return;

            StringBuilder builder = new StringBuilder();

            DateTime ShutdownStart = DateTime.Now;

            DateTime MessaMessage = DateTime.Now;
            ShutdownInitiated = true;

            SendMassMessage(LanguageLocale.GetValue("shutdown.alert"));
            AppendTimeStampWithComment(ref builder, MessaMessage, "Hotel pre-warning");

            Game.StopGameLoop();
            Console.Write("Game loop stopped");

            DateTime ConnectionClose = DateTime.Now;
            Console.WriteLine("Server shutting down...");
            Console.Title = "<<- SERVER SHUTDOWN ->>";
            
            GetConnectionManager().Destroy();
            AppendTimeStampWithComment(ref builder, ConnectionClose, "Socket close");

            DateTime sConnectionClose = DateTime.Now;
            GetGame().GetClientManager().CloseAll();
            AppendTimeStampWithComment(ref builder, sConnectionClose, "Furni pre-save and connection close");

            DateTime RoomRemove = DateTime.Now;
            Console.WriteLine("<<- SERVER SHUTDOWN ->> ROOM SAVE");
            Game.GetRoomManager().RemoveAllRooms();
            AppendTimeStampWithComment(ref builder, RoomRemove, "Room destructor");

            DateTime DbSave = DateTime.Now;

            using (IQueryAdapter dbClient = manager.getQueryreactor())
            {
                dbClient.runFastQuery("TRUNCATE TABLE user_tickets");
                dbClient.runFastQuery("TRUNCATE TABLE room_active");
                dbClient.runFastQuery("UPDATE users SET online = 0");
                dbClient.runFastQuery("UPDATE rooms SET users_now = 0");
            }
            AppendTimeStampWithComment(ref builder, DbSave, "Database pre-save");

            DateTime connectionShutdown = DateTime.Now;
            ConnectionManager.Destroy();
            AppendTimeStampWithComment(ref builder, connectionShutdown, "Connection shutdown");

            DateTime gameDestroy = DateTime.Now;
            Game.Destroy();
            AppendTimeStampWithComment(ref builder, gameDestroy, "Game destroy");

            DateTime databaseDeconstructor = DateTime.Now;
            try
            {
                Console.WriteLine("Destroying database manager.");

                manager.destroy();
            }
            catch { }
            AppendTimeStampWithComment(ref builder, databaseDeconstructor, "Database shutdown");

            TimeSpan timeUsedOnShutdown = DateTime.Now - ShutdownStart;
            builder.AppendLine("Total time on shutdown " + TimeSpanToString(timeUsedOnShutdown));
            builder.AppendLine("You have reached ==> [END OF SESSION]");
            builder.AppendLine();
            builder.AppendLine();
            builder.AppendLine();

            Logging.LogShutdown(builder);

            Console.WriteLine("System disposed, goodbye!");
            if (ExitWhenDone)
                Environment.Exit(Environment.ExitCode);
        }

        internal static string TimeSpanToString(TimeSpan span)
        {
            return span.Seconds + " s, " + span.Milliseconds + " ms";
        }

        internal static void AppendTimeStampWithComment(ref StringBuilder builder, DateTime time, string text)
        {
            builder.AppendLine(text + " =>[" + TimeSpanToString(DateTime.Now - time) + "]");
        }

        
    }
}
