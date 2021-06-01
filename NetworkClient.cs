using SimpleTcp;
using System;
using System.Text;

namespace alyx_multiplayer
{
    class NetworkClient
    {
        private string ipPort;
        private bool shouldShowPeerError = true;
        public SimpleTcpClient client;
        public NetworkClient(string ipPort)
        {
            // Attempt to connect to the server
            try
            {
                // Instantiate client
                this.ipPort = ipPort;
                client = new SimpleTcpClient(this.ipPort);

                // Set client events
                client.Events.Connected += Connected;
                client.Events.Disconnected += Disconnected;
                client.Events.DataReceived += DataReceived;

                // Actually attempt a connection
                client.Connect();
            } catch
            {
                // For now, don't show a server failure message (since Core.Log may not be instantiated)
            }
        }

        public void Dispose()
        {
            client.Dispose();
        }

        public void Send(string msg)
        {
            try
            {
                client.Send(msg);
                shouldShowPeerError = true;
            } catch
            {
                if (shouldShowPeerError)
                {
                    shouldShowPeerError = false;
                    Core.Log("Failed to send coords to peer! Are the IP and port set correctly?", false);
                }
            }            
        }

        static void Connected(object sender, EventArgs e)
        {
            Core.Log("*** Connected to peer", false);
        }

        static void Disconnected(object sender, EventArgs e)
        {
            Core.Log("*** Disconnected from peer", false);
        }

        static void DataReceived(object sender, DataReceivedEventArgs e)
        {
            Core.Log("[" + e.IpPort + "] " + Encoding.UTF8.GetString(e.Data), false);
        }
    }
}
