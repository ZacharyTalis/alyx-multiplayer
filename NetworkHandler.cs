using SimpleTcp;
using System;
using System.Text;
using System.Threading;

namespace alyx_multiplayer
{
    class NetworkHandler {

        private static string serverIP = "0.0.0.0";
        private static string serverPort = "6655";
        private static string clientIP = "127.0.0.1";
        private static string clientPort = "6655";

        public NetworkServer server = new NetworkServer(serverIP + ":" + serverPort);
        public NetworkClient client = new NetworkClient(clientIP + ":" + clientPort);

        /// <summary>
        /// Restart the network with a new server port.
        /// </summary>
        /// <param name="newServerPort">The server port to implement.</param>
        public void ReconfigureServerPort(string newServerPort)
        {
            server.Dispose();
            client.Dispose();
            serverPort = newServerPort;
            server = new NetworkServer(serverIP + ":" + serverPort);
            client = new NetworkClient(clientIP + ":" + clientPort);
        }

        /// <summary>
        /// Restart the network with a new client IP.
        /// </summary>
        /// <param name="newClientIP">The client IP to implement.</param>
        public void ReconfigureClientIP(string newClientIP)
        {
            client.Dispose();
            clientIP = newClientIP;
            client = new NetworkClient(clientIP + ":" + clientPort);
        }

        /// <summary>
        /// Restart the network with a new client port.
        /// </summary>
        /// <param name="newClientPort">The client port to implement.</param>
        public void ReconfigureClientPort(string newClientPort)
        {
            client.Dispose();
            clientPort = newClientPort;
            client = new NetworkClient(clientIP + ":" + clientPort);
        }

        /// <summary>
        /// Pass coordinates to the client's Send() implementation.
        /// </summary>
        /// <param name="coords">The coordinates to send.</param>
        public void SendCoords(string coords) {
            client.Send(coords);
        }

        /// <summary>
        /// Get coordinates from the server's RetrieveCoords() implementation.
        /// </summary>
        /// <returns>The coordinates cached in the server.</returns>
        public string GetCoords()
        {
            return server.RetrieveCoords();
        }
    }
}
