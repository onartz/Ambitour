using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace SocketTester
{
    class Program
    {
        static void Main(string[] args)
        {
            string dest = "b@10.10.68.92:1099/JADE";
            String response = String.Format("DONE {0}", "1234");
            string request = String.Format("(INFORM\r\n :receiver  (set ( agent-identifier :name {0} ) ) :content  \"{1}\"\r\n :language  fipa-sl )", dest, response);
            try
            {
               
                SocketSendReceive("10.10.68.92", 6789, request);
            }
            catch (SocketException ex)
            {
                Console.WriteLine("Erreur : " + ex.Message);
            }
        }

       

        private static Socket ConnectSocket(string server, int port)
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
        private static string SocketSendReceive(string server, int port, string request)
        {
            Console.WriteLine(String.Format("Sending {0} to {1}:{2}", request, server, port));
            // Console.WriteLine("Entering SocketSendReceive");
            //string address = "TBI540Inv1@192.168.0.21:1099/JADE";

            Byte[] bytesSent = Encoding.ASCII.GetBytes(request);
            Byte[] bytesReceived = new Byte[256];

            string page = "";

            // Create a socket connection with the specified server and port.
            try
            {

                IPAddress hostIP = IPAddress.Parse("10.10.68.92");
                Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                s.Connect(hostIP, 6789);
                if (s.Connected)
                {
                    Console.WriteLine("Hourra, connectés");
                }
                else
                    return page;

               // Socket s = ConnectSocket(server, port);
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
                    page = page + Encoding.ASCII.GetString(bytesReceived, 0, bytes);
                }
                while (bytes == bytesReceived.Length);
            }
            catch (SocketException e)
            {
                throw e;
            }


            return page;
        }
    }
}
