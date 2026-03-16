using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClientImplementation.ConnectionClasses;
using System.Threading;
using ClientSide;
using SharedPacketLib.DataPackets;
using System.Windows.Forms;
using Database_Manager.Database.Session_Details.Interfaces;

namespace Butterfly.MainServerConnectionHandeling
{
    public class MainServerConnectionHolder
    {
        private ClientConnectionManager connection;

        private Thread aliveCheck;

        private bool keepThreadAlive = false;

        private int retryCount = 0;

        private string ip;
        private int port;

        private string username;
        private string serial;
        private ConnectionState state = ConnectionState.closed;

        public int AmountOfSlots { get; private set; }

        /// <summary>
        /// Creates a new MainServerConnection handeler with the given details
        /// </summary>
        /// <param name="ip">The ip address of the server</param>
        /// <param name="port">The port to connect to</param>
        public MainServerConnectionHolder(string ip, int port, string username, string license)
        {
            this.ip = ip;
            this.port = port;
            this.username = username;
            this.serial = license;
            keepThreadAlive = true;
            connection = new ClientConnectionManager(ip, port);
            connection.OnConnectionChange += ConStatusChanged;
            aliveCheck = new Thread(DoHealthChecks);
            aliveCheck.Start();
        }

        private void DoHealthChecks()
        {
            while (keepThreadAlive)
            {
                bool keepConnecting = true;
                while (state != ConnectionState.open && keepConnecting)
                {
                    if (state != ConnectionState.connecting && state != ConnectionState.open)
                    {
                        state = ConnectionState.connecting;
                        connection.reset();
                        Thread.Sleep(1000);
                        addPacketHandelers();
                        connection.openConnection();
                    }
                    
                    while (state == ConnectionState.connecting)
                    {
                        Thread.Sleep(1000);
                    }
                    if (state == ConnectionState.failed || state == ConnectionState.closed)
                    {
                        retryCount += 1;
                        if (retryCount <= 5)
                        {
                            Console.WriteLine("Connection attempt [" + (retryCount) + "] of [5] failed, Waiting 2 seconds...");
                            Thread.Sleep(2000);
                        }
                        else
                        {
                            Console.WriteLine("Shutting down... License server could nog be reached");
                            ButterflyEnvironment.PreformShutDown(true);
                            this.keepThreadAlive = false;
                            keepConnecting = false;
                        }
                    }
                    else if (state == ConnectionState.open)
                    {
                        keepConnecting = false;
                    }
                }
                while (state == ConnectionState.open && keepThreadAlive)
                {
                    this.retryCount = 0;
                    connection.processSyncedMessages();
                    Thread.Sleep(10);
                }
            }
            Console.WriteLine("Shut down due to error..");
            if (ButterflyEnvironment.isLive)
                ButterflyEnvironment.Destroy();
        }

        private void addPacketHandelers()
        {
            this.connection.registerPacket(ClientOpCode.Ping_Received, PingReceived, true);
            this.connection.registerPacket(ClientOpCode.Request_Ping, SendPingBack, true);
            this.connection.registerPacket(ClientOpCode.Core_send_version, SendServerVersion, true);
            this.connection.registerPacket(ClientOpCode.Core_receive_version_failed, SendServerVersion, true);
            this.connection.registerPacket(ClientOpCode.Request_License, SendLicenseToServer, true);
            this.connection.registerPacket(ClientOpCode.Force_Shutdown_server, ShutDownServer, true);
            this.connection.registerPacket(ClientOpCode.Shutdown_server, ShutDownServerDialogged, true);
            this.connection.registerPacket(ClientOpCode.Start_Server, StartServer, true);
            this.connection.registerPacket(ClientOpCode.Run_Query, RunQuery, true);
        }

        private void SendLicenseToServer(ClientIncomingPacket packet)
        {
            connection.sendData(new OutgoingSerialPacket(this.username, this.serial));
        }
        
        private void ShutDownServer(ClientIncomingPacket packet)
        {
            this.keepThreadAlive = false;
            ButterflyEnvironment.PreformShutDown(true);
            Environment.Exit(Environment.ExitCode);
        }

        private void ShutDownServerDialogged(ClientIncomingPacket packet)
        {
            this.keepThreadAlive = false;
            MessageBox.Show("Startup has failed due to the following reason\r\n\r\n" + packet.ReadString(), "Startup failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification, false);
            ButterflyEnvironment.PreformShutDown(true);
            Environment.Exit(Environment.ExitCode);
        }

        private void StartServer(ClientIncomingPacket packet)
        {
            this.AmountOfSlots = packet.ReadInt();
            //Program.InitEnvironment();
        }

        private void VersionOutdated(ClientIncomingPacket packet)
        {
            
            Console.WriteLine("Version is outdated!!!! please update ASAP");
        }

        private void SendServerVersion(ClientIncomingPacket packet)
        {
            connection.sendData(new ServerVersionResponse(1));
        }

        private void PingReceived(ClientIncomingPacket packet)
        {
            //connection.sendData(new PingRepsonse());
        }
        private void SendPingBack(ClientIncomingPacket packet)
        {
            connection.sendData(new PingRepsonse());
        }

        private void RunQuery(ClientIncomingPacket packet)
        {
            try
            {
                using (IQueryAdapter dbClient = ButterflyEnvironment.GetDatabaseManager().getQueryreactor())
                {
                    dbClient.runFastQuery(packet.ReadString());
                }
                connection.sendData(new QueryResult(true));
            }
            catch
            {
                connection.sendData(new QueryResult(false));
            }
        }

        private void ConStatusChanged(ConnectionState state)
        {
            this.state = state;
        }

        internal void ReportFullServer()
        {
            connection.sendData(new ServerFullMessage());
        }
    }
}
