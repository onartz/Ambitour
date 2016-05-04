using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Ambitour.CoucheMetier.LogiqueMetier
{
    public static class ProxySocket
    {
        public static Socket ConnectSocket(string server, int port)
        {
            Socket s = null;
            IPHostEntry hostEntry = null;

            // Get host related information.
            hostEntry = Dns.GetHostEntry(server);

            // Loop through the AddressList to obtain the supported AddressFamily. This is to avoid
            // an exception that occurs when the host IP Address is not compatible with the address family
            // (typical in the IPv6 case).
            foreach (IPAddress address in hostEntry.AddressList)
            {
                IPEndPoint ipe = new IPEndPoint(address, port);
                Socket tempSocket =
                    new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    tempSocket.Connect(ipe);
                }
                catch (SocketException e)
                {
                    throw e;
                }

                if (tempSocket.Connected)
                {
                    s = tempSocket;
                    break;
                }
                else
                {
                    continue;
                }
            }
            return s;
        }

        // This method sends a request and wait for answer from agent
        public static string SocketSendReceive(string server, int port, string request)
        {

            Byte[] bytesSent = Encoding.ASCII.GetBytes(request);
            Byte[] bytesReceived = new Byte[256];

            string res = "";

            // Create a socket connection with the specified server and port.
            try
            {
                Socket s = ConnectSocket(server, port);
                if (s == null)
                    return ("Connection failed");
                // Send request to the server.
                s.Send(bytesSent, bytesSent.Length, 0);

                // Receive the server home page content.
                int bytes = 0;


                // The following will block until te page is transmitted.
                do
                {
                    bytes = s.Receive(bytesReceived, bytesReceived.Length, 0);
                    res = res + Encoding.ASCII.GetString(bytesReceived, 0, bytes);
                }
                while (bytes == bytesReceived.Length);
            }
            catch (SocketException e)
            {
                throw e;
            }


            return res;
        }

        // This method sends a message
        public static string SocketSend(string server, int port, string request)
        {

            Byte[] bytesSent = Encoding.ASCII.GetBytes(request);
            Byte[] bytesReceived = new Byte[256];

            string res = "";

            // Create a socket connection with the specified server and port.
            try
            {
                Socket s = ConnectSocket(server, port);
                if (s == null)
                    return ("Connection failed");
                // Send request to the server.
                s.Send(bytesSent, bytesSent.Length, 0);

                // Receive the server home page content.
                int bytes = 0;


                //// The following will block until te page is transmitted.
                //do
                //{
                //    bytes = s.Receive(bytesReceived, bytesReceived.Length, 0);
                //    res = res + Encoding.ASCII.GetString(bytesReceived, 0, bytes);
                //}
                //while (bytes == bytesReceived.Length);
            }
            catch (SocketException e)
            {
                throw e;
            }


            return res;
        }

    }
}
