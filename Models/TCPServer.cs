using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SYNOPEX_ICT.Models
{
    //Class TCPServer:
    //This class opens a TCP port and listens for messages on that port
    //
    //It will notify the instantiating class when:
    //1. A client connects or disconnects
    //2. When a message is received
    //It also allows the instantiating class to send a TCP message on the port
    //
    //This server is 'dumb' in that it doesn't interpret the messages. It simply
    //passes them along to the instantiating class to interpret.
    [Serializable]
    public class TCPServer
    {
        [NonSerialized()] public TcpListener tcpListener;
        [NonSerialized()] public TcpClient tcpClient;

        //Event to notify when a message needs to be logged
        public event LogMessageEventDelegate logMessageEvent;
        public delegate void LogMessageEventDelegate(object sender, string message);

        //Event to notify when a client connects/disconnects from the port
        public event TcpConnectionChanged TcpConnectionChangedEvent;
        public delegate void TcpConnectionChanged(string sender, string status);
        public bool isTcpClientConnected = false;

        //Evnent to notify when a message is received from a client
        public event TcpMessageReceivedEventDelegate TcpMessageReceivedEvent;
        public delegate void TcpMessageReceivedEventDelegate(string function, string argument);


        const int TCPPort = 3000;

        //For thread synchronization
        public static ManualResetEvent tcpClientConnected = new ManualResetEvent(false);

        [NonSerialized()] Thread tcpListenerThread;       //Thread to listen for incoming TCP Connections
        [NonSerialized()] Thread tcpReaderThread;         //Thread to listen for incoming TCP Messages

        int tcpBytesRead = 0;
        byte[] tcpReadData;

        void Log(string message)
        {
            logMessageEvent(this, message);
        }

        //StartTCPServer: Open a port and listen for incoming connections
        public void StartTCPServer()
        {
            tcpListener = new TcpListener(IPAddress.Loopback, TCPPort);
            Log("TCP Server Launched, Reading on:" + tcpListener.LocalEndpoint.ToString());
            TcpConnectionChangedEvent("Server", "Connected: " + tcpListener.LocalEndpoint);
            
            tcpListenerThread = new Thread(new ThreadStart(WaitForConnection));
            tcpListenerThread.Start();         
        }

        //WaitForConnection: Wait for a client to connect to the TCP Port
        private void WaitForConnection()
        {
            Log("Waiting for incoming connections");
            TcpConnectionChangedEvent("Client", "Waiting for connection...");
            tcpClientConnected.Reset();
            tcpListener.Start();

            tcpListener.BeginAcceptTcpClient(new AsyncCallback(DoAcceptTcpClientCallback), null);
            //Wait until a client connects
            tcpClientConnected.WaitOne();

            Log("Client connected to Read Port");
            isTcpClientConnected = true;
            TcpConnectionChangedEvent("Client", "Connected: " + tcpListener.LocalEndpoint);

            //Once a client connects, wait for messages on that port
            WaitForMessages();
        }

        // Process the client connection.
        void DoAcceptTcpClientCallback(IAsyncResult ar)
        {
            //TcpListener tcpListener = (TcpListener)ar.AsyncState;

            if (tcpListener == null)
                return;

            // End the operation and display the received data on 
            // the console.
            tcpClient = tcpListener.EndAcceptTcpClient(ar);

            // Signal the calling thread to continue.
            tcpClientConnected.Set();
        }

        //WaitForMessages: Asynchronously wait for a message on the port
        public void WaitForMessages()
        {
            tcpReaderThread = new Thread(new ThreadStart(TcpWaitAndReadData));
            tcpReaderThread.Start();
        }

        //TcpWaitAndReadData: Wait for a TCP Message and notify. Blocking call. 
        private void TcpWaitAndReadData()
        {
            NetworkStream clientStream = tcpClient.GetStream();

            tcpReadData = new byte[4096];
            Log("Listening for messages...");

            try
            {
                tcpBytesRead = clientStream.Read(tcpReadData, 0, 4096);

                ASCIIEncoding encoder = new ASCIIEncoding();
                if (tcpBytesRead == 0)
                {
                    //Connection Lost
                    throw new Exception();
                }
                string message = encoder.GetString(tcpReadData, 0, tcpBytesRead);
                Log("Received: \"" + message + "\"");

                int index = message.IndexOf(',');
                string function = message.Substring(0, index);
                string arguments = message.Substring(index+1, message.Length - (index+1));

                TcpMessageReceivedEvent(function, arguments);
            }

            catch (Exception)
            {
                //Connection Lost
                ClientDisconnected();
            }
        }

        //SendMessage: Send a TCP message on the port
        public void SendMessage(string message)
        {
            NetworkStream clientStream = tcpClient.GetStream();

            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] buffer = encoder.GetBytes(message);

            clientStream.Write(buffer, 0, buffer.Length);
            clientStream.Flush();

            Log("Sent: \"" + message + "\"");
        }

        //ClientDisconnected: Handle a client disconnect. Go back to listening for incoming connections.
        void ClientDisconnected()
        {
            try
            {
                Log("TCP Client disconnected");
                isTcpClientConnected = false;
                TcpConnectionChangedEvent("Client", "No client connected");
                tcpClient.GetStream().Close();
                tcpClient.Close();
                tcpClient = null;

                //Start Listening for connections again
                tcpListenerThread = new Thread(new ThreadStart(WaitForConnection));
                tcpListenerThread.Start();
            }
            catch
            {
            }
        }

        public void ShutdownTCPServer()
        {
            Log("Shutting down");
            isTcpClientConnected = false;
            TcpConnectionChangedEvent("Client", "No client connected");
            if (tcpReaderThread != null && tcpReaderThread.IsAlive)
                tcpReaderThread.Abort();

            if (tcpListenerThread != null && tcpListenerThread.IsAlive)
                tcpListenerThread.Abort();

            if (tcpClient != null)
            {
                tcpClient.GetStream().Close();
                tcpClient.Close();
            }

            if (tcpListener != null)
            {
                tcpListener.Stop();
                tcpListener = null;
            }
        }
    }
}
