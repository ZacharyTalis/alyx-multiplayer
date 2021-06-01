using SimpleTcp;
using System;
using System.Text;

namespace alyx_multiplayer
{
    class NetworkServer
    {
        private string ipPort;
        private static string coords;
        public SimpleTcpServer server;
        public NetworkServer(string ipPort)
        {
            // Attempt to start the server
            try
            {
                // Instantiate server
                this.ipPort = ipPort;
                server = new SimpleTcpServer(this.ipPort);

                // Set server events
                server.Events.ClientConnected += ClientConnected;
                server.Events.ClientDisconnected += ClientDisconnected;
                server.Events.DataReceived += DataReceived;

                // Actually start the server
                server.Start();
            } catch
            {
                // For now, don't show a server failure message (since Core.Log may not be instantiated)
            }
            
        }

        public void Dispose()
        {
            server.Dispose();
        }

        public void Send(string msg)
        {
            server.Send(ipPort, msg);
        }

        public string RetrieveCoords()
        {
            return coords;
        }

        static void ClientConnected(object sender, ClientConnectedEventArgs e)
        {
            Core.Log("[" + e.IpPort + "] client connected", false);
        }

        static void ClientDisconnected(object sender, ClientDisconnectedEventArgs e)
        {
            Core.Log("[" + e.IpPort + "] client disconnected: " + e.Reason.ToString(), false);
        }

        static void DataReceived(object sender, DataReceivedEventArgs e)
        {
            coords = Encoding.UTF8.GetString(e.Data);
        }
    }
}
