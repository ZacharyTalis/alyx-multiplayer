using SimpleTcp;
using System;
using System.Text;

namespace alyx_multiplayer
{
    class NetworkClient
    {
        /// <summary>
        /// Constructor which attempts to start the client.
        /// </summary>
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

        /// <summary>
        /// Kill the client. Brutal!
        /// </summary>
        public void Dispose()
        {
            client.Dispose();
        }

        /// <summary>
        /// Send any coordinates using SuperSimpleTcp's nifty implementation.
        /// </summary>
        /// <param name="coords">The coordinates to send.</param>
        public void Send(string coords)
        {
            try
            {
                client.Send(coords);
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

        /// <summary>
        /// Client event for when we connect to the server.
        /// </summary>
        /// <param name="sender">The server we connected to.</param>
        /// <param name="e">Event arguments.</param>
        static void Connected(object sender, EventArgs e)
        {
            Core.Log("*** Connected to peer", false);
        }

        /// <summary>
        /// Client event for when we disconnect from the server.
        /// </summary>
        /// <param name="sender">The server we disconnected from.</param>
        /// <param name="e">Event arguments.</param>
        static void Disconnected(object sender, EventArgs e)
        {
            Core.Log("*** Disconnected from peer", false);
        }

        /// <summary>
        /// Client event for when we receive data from the server.
        /// </summary>
        /// <param name="sender">The server we received data from.</param>
        /// <param name="e">Event arguments.</param>
        static void DataReceived(object sender, DataReceivedEventArgs e)
        {
            Core.Log("[" + e.IpPort + "] " + Encoding.UTF8.GetString(e.Data), false);
        }
    }
}
