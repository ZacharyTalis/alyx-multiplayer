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

        public void ReconfigureServerPort(string newServerPort)
        {
            server.Dispose();
            client.Dispose();
            serverPort = newServerPort;
            server = new NetworkServer(serverIP + ":" + serverPort);
            client = new NetworkClient(clientIP + ":" + clientPort);
        }

        public void ReconfigureClientIP(string newClientIP)
        {
            client.Dispose();
            clientIP = newClientIP;
            client = new NetworkClient(clientIP + ":" + clientPort);
        }

        public void ReconfigureClientPort(string newClientPort)
        {
            client.Dispose();
            clientPort = newClientPort;
            client = new NetworkClient(clientIP + ":" + clientPort);
        }

        public void SendCoords(string msg) {
            client.Send(msg);
        }

        public string GetCoords()
        {
            return server.RetrieveCoords();
        }
    }
}
