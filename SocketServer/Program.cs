using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace SocketServer
{
    // State object for reading client data asynchronously
    public class StateObject
    {
        // Client  socket.
        public Socket workSocket = null;
        // Size of receive buffer.
        public const int BufferSize = 1024;
        // Receive buffer.
        public byte[] buffer = new byte[BufferSize];
        // Received data string.
        public StringBuilder sb = new StringBuilder();
    }

    public class AsynchronousSocketListener
    {
        //The port the server is listening to
        static int port = 11000;

        // Thread signal.
        public static ManualResetEvent allDone = new ManualResetEvent(false);

        public AsynchronousSocketListener()
        {
        }

        public static void StartListening()
        {
            // Data buffer for incoming data.
            byte[] bytes = new Byte[1024];

            // Establish the local endpoint for the socket.
            // The DNS name of the computer
            //IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
             IPHostEntry hostEntry = null;

            // Get host related information.
             hostEntry = Dns.GetHostByName("aip-olivier");
            //IPHostEntry ipHostInfo = Dns.GetHostEntry(
             IPAddress ipAddress = hostEntry.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);

            // Create a TCP/IP socket.
            Socket listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and listen for incoming connections.
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                while (true)
                {
                    // Set the event to nonsignaled state.
                    allDone.Reset();

                    // Start an asynchronous socket to listen for connections.
                    Console.WriteLine("Waiting for a connection...");
                    listener.BeginAccept(
                        new AsyncCallback(AcceptCallback),
                        listener);

                    // Wait until a connection is made before continuing.
                    allDone.WaitOne();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }

        public static void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.
            allDone.Set();

            // Get the socket that handles the client request.
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            // Create the state object.
            StateObject state = new StateObject();
            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
        }

        public static void ReadCallback(IAsyncResult ar)
        {
            XmlSerializer SerializerObj = new XmlSerializer(typeof(ACLMessage));

            String content = String.Empty;

            // Retrieve the state object and the handler socket
            // from the asynchronous state object.
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            // Read data from the client socket. 
            int bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                // There  might be more data, so store the data received so far.
                state.sb.Append(Encoding.ASCII.GetString(
                    state.buffer, 0, bytesRead));

                // Check for end-of-file tag. If it is not there, read 
                // more data.
                content = state.sb.ToString();
                if (content.IndexOf("\r\n\r\n") > -1)
                {
                    // All the data has been read from the 
                    // client. Display it on the console.
                    Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                        content.Length, content);
                    //Deserialize message to ACLMessage : if OK, save to queue
                    try{
                        XmlReader xmlReader = XmlReader.Create(new StringReader(content));
                        ACLMessage msg = (ACLMessage)SerializerObj.Deserialize(xmlReader);
                        if (msg != null)
                        {
                            TextWriter tw = new StreamWriter(@"C:\Ambitour\Queue\inputQueue\" + Guid.NewGuid() + ".xml");
                            SerializerObj.Serialize(tw, msg);
                            tw.Close();
                        }
                        
                    }
                    catch(XmlException ex){
                        throw ex;
                       
                    }

                    // Echo the data back to the client.
                    Send(handler, content);

                    //Send response to Proxy
                   // String conversationId = xmldoc.DocumentElement.GetAttribute("conversation-id");

                   // string dest = "b@10.10.68.92:1099/JADE";
                   // String response = String.Format("DONE {0}", conversationId);
                   // string request = String.Format("(INFORM\r\n :receiver  (set ( agent-identifier :name {0} ) ) :content  \"{1}\"\r\n :language  fipa-sl )", dest, response);
                   ////string request = String.Format("(REQUEST\r\n :receiver  (set ( agent-identifier :name {0} ) )\r\n :content  \"((action (agent-identifier :name {0}) (UpdateQuantity\r\n :command Remove :qty {1})))\"\r\n  :language  fipa-sl  :ontology  ambiflux-logistic )", dest, numericUpDown1.Value.ToString());
          
                    
                    //String destAID = xmldoc.DocumentElement.GetAttribute("from");
                    //String destAddress = String.Format("{0}@", destAID);
                    //SendToProxy(request, dest);

                    /*
                     * A réinitégrer dans Ambitour
                     * 
                    string dest = "invTBI540-1@" + GlobalSettings.Default.jadeServerAddress + ":1099/JADE";
                    string request = String.Format("(REQUEST\r\n :receiver  (set ( agent-identifier :name {0} ) )\r\n :content  \"((action (agent-identifier :name {0}) (UpdateQuantity\r\n :command Remove :qty {1})))\"\r\n  :language  fipa-sl  :ontology  ambiflux-logistic )", dest, numericUpDown1.Value.ToString());
                   
                   
                    SendToProxy();
                    */

                }
                else
                {
                    // Not all data received. Get more.
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
                }
            }
        }

        /*
         * A intégrer dans Ambitour
         */
        private static void SendToProxy(String data, String dest)
        {
            string result = "";
            try
            {
                Console.WriteLine(String.Format("Sending {0}", data));
                result = SocketSendReceive("10.10.68.92", 6789, data);
                Console.WriteLine(String.Format("Result {0}", result));
               
                //TODO: à modifier
                //if (result.Contains("((done"))
                //{
                //    txtInventoryLevel.Text = (Int16.Parse(txtInventoryLevel.Text) - numericUpDown1.Value).ToString();
                //    numericUpDown1.Value = 0;
                //}
            }
            catch (SocketException ex)
            {
                //errorList.Add(ex.Message);
                //updateLogView();
                return;
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
            Console.WriteLine(String.Format("Sending {0} to {1}:{2}",request, server, port));
           // Console.WriteLine("Entering SocketSendReceive");
            //string address = "TBI540Inv1@192.168.0.21:1099/JADE";

            Byte[] bytesSent = Encoding.ASCII.GetBytes(request);
            Byte[] bytesReceived = new Byte[256];

            string page = "";

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
        

        private static void Send(Socket handler, String data)
        {
            // Convert the string data to byte data using ASCII encoding.
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        class Program
        {
            static int Main(string[] args)
            {
               
                
                StartListening();
                return 0;
            }
        }
    }
}

