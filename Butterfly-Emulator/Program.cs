using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Butterfly.Core;
using Butterfly.LicensingSystem;
using System.Net;
using System.Collections.Specialized;
using System.Net.Cache;
using Butterfly.MainServerConnectionHandeling;

namespace Butterfly
{
    internal class Program
    {
        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(EventHandler handler, bool add);
        public static MainServerConnectionHolder LicHandeler { get; private set; }
        private delegate bool EventHandler(CtrlType sig);

        enum CtrlType
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT = 1,
            CTRL_CLOSE_EVENT = 2,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT = 6
        }

        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlAppDomain)]
        [STAThreadAttribute]
        internal static void Main()
        {
            ConsoleWriter.Writer.Init();
            string appLocation = System.Windows.Forms.Application.StartupPath;
            string name = "";
            string parm1 = "";
            string parm2 = "";
            string parm3 = "";
            ConfigurationData license = new ConfigurationData(System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "Settings/license.ini"), true);

            if (license.fileHasBeenRead)
            {
                try
                {
                    if (license.data.ContainsKey("license.name") && license.data.ContainsKey("license.license"))
                    {
                        name = license.data["license.name"];
                        string[] licenseBits = license.data["license.license"].Split('-');
                        if (licenseBits.Length == 3)
                        {
                            parm1 = licenseBits[0];
                            parm2 = licenseBits[1];
                            parm3 = licenseBits[2];
                        }
                    }

                }
                catch
                { }
            }

            //License lic = new Butterfly.LicensingSystem.License("CYCXGKE7ME", "2.0", name, parm1, parm2, parm3, false);
            //string result = lic.Serial;
            //lic.ClearCache();


            //LicHandeler = new MainServerConnectionHolder("127.0.0.1", 9001, name, license.data["license.license"]);
            Program.InitEnvironment();

            while (true)
            {
                Console.CursorVisible = true;
                if (Logging.DisabledState)
                    Console.Write("bfly> ");
                ConsoleCommandHandeling.InvokeCommand(Console.ReadLine());
            }
        }

        [MTAThread]
        internal static void InitEnvironment()
        {
            if (!ButterflyEnvironment.isLive)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.CursorVisible = false;
                AppDomain currentDomain = AppDomain.CurrentDomain;
                currentDomain.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);

                ButterflyEnvironment.Initialize();
            }
        }

        static void MyHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Logging.DisablePrimaryWriting(true);
            Exception e = (Exception)args.ExceptionObject;
            Logging.LogCriticalException("SYSTEM CRITICAL EXCEPTION: " + e.ToString());
            ButterflyEnvironment.SendMassMessage("A fatal error crashed the server, server shutting down.");
            ButterflyEnvironment.PreformShutDown(true);
        }
    }
}
