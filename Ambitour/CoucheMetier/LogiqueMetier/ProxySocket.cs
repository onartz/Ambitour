﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

namespace Ambitour.CoucheMetier.LogiqueMetier
{
    public static class ProxySocket
    {
        public static Socket ConnectSocket(string server, int port)
        {
           
            Socket s = null;
            IPHostEntry hostEntry = null; 
            // Get host related information.
            try
            {
                hostEntry = Dns.GetHostEntry(server);
            }
            catch (SocketException se)
            {
                Trace.TraceError(String.Format("DNS.GetHostEntry throw SocketException : {0}, {1}", se.SocketErrorCode, se.Message));
            }
            catch (ArgumentException ae)
            {
                Trace.TraceError(String.Format("DNS.GetHostEntry throw ArgumentException : {0}", ae.Message));
                return s;
            }
            
            // Loop through the AddressList to obtain the supported AddressFamily. This is to avoid
            // an exception that occurs when the host IP Address is not compatible with the address family
            // (typical in the IPv6 case).
            foreach (IPAddress address in Dns.GetHostAddresses(server))
            {
              
                IPEndPoint ipe = new IPEndPoint(address, port);
               
                Socket tempSocket =
                    new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    Trace.TraceInformation(String.Format("Trying to connect to {0}", ipe.Address.ToString()));
                    tempSocket.Connect(ipe);
                }
                catch (SocketException e)
                {
                    Trace.TraceError(String.Format("SocketException : {0} ", e.Message));
                    //throw e;
                }

                if (tempSocket.Connected)
                {
                    Trace.TraceInformation(String.Format("Connected to {0}", ipe.Address.ToString()));
                    s = tempSocket;
                    break;
                }
                else
                {
                    Trace.TraceError(String.Format("Cannot connect to {0}", ipe.Address.ToString()));
                    continue;
                }
            }
            return s;
        }
        //public static Socket ConnectSocket(string server, int port)
        //{
        //    Socket s = null;
        //    IPHostEntry hostEntry = null;

        //    // Get host related information.
        //    hostEntry = Dns.GetHostEntry(server);

        //    // Loop through the AddressList to obtain the supported AddressFamily. This is to avoid
        //    // an exception that occurs when the host IP Address is not compatible with the address family
        //    // (typical in the IPv6 case).
        //    foreach (IPAddress address in hostEntry.AddressList)
        //    {
        //        IPEndPoint ipe = new IPEndPoint(address, port);
        //        Socket tempSocket =
        //            new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        //        try
        //        {
        //            tempSocket.Connect(ipe);
        //        }
        //        catch (SocketException e)
        //        {
        //            Log.Write(String.Format("SocketException : {0} ", e.Message));
        //            //throw e;
        //        }

        //        if (tempSocket.Connected)
        //        {
        //            Log.Write("Socket connected");
        //            s = tempSocket;
        //            break;
        //        }
        //        else
        //        {
        //            continue;
        //        }
        //    }
        //    return s;
        //}

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
                if (s == null || !s.Connected)
                {
                    Trace.TraceError(DateTime.Now + " : Module SocketSendReceive Socket not connected");
                    return ("Connection failed");
                }

                //Log.Write("Module SocketSendReceive Socket connected");
           
              
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
                Trace.TraceError(String.Format("{0} : Module SocketSendReceive SocketException num {1} : {2}", DateTime.Now.ToString(), e.ErrorCode, e.Message));
           
                return (e.Message);
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
                if (s == null || !s.Connected)
                {
                    Trace.TraceError(DateTime.Now + " : Module SocketSendReceive Socket not connected");
                   
                    return ("Connection failed");
                }

                // Send request to the server.
                s.Send(bytesSent, bytesSent.Length, 0);

                // Receive the server home page content.
                int bytes = 0;

    
            }
            catch (Exception e)
            {
                Trace.TraceError(String.Format("{0} : Module SocketSendReceive SocketException: {1}", DateTime.Now.ToString(), e.Message));
                return (e.Message);
            }


            return res;
        }

    }
}
