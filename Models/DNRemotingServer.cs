using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

using DNRemotingRemoteObject;

namespace SYNOPEX_ICT.Models
{
    //Class DNRemotingServer:
    //This class creates an instance of DNRemoteObject (the shared .NET Remoting object) and
    //hosts it.
    //
    //It will notify the instantiating class when:
    //1. A client connects
    [Serializable]
    class DNRemotingServer
    {
        public DNRemoteObject remoteObject;
        [NonSerialized()] TcpChannel tcpChannel;

        public event LogMessageEventDelegate logMessageEvent;
        public delegate void LogMessageEventDelegate(object sender, string message);

        public event DNRemotingConnectionChanged DNRemotingConnectionChangedEvent;
        public delegate void DNRemotingConnectionChanged(string sender, string status);

        void Log(string message)
        {
            logMessageEvent(this, message);
        }

        public void StartDNRemotingServer()
        {
            try
            {
                BinaryServerFormatterSinkProvider provider = new BinaryServerFormatterSinkProvider();
                provider.TypeFilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full;

                // Creating the IDictionary to set the port on the channel instance.
                System.Collections.IDictionary props = new System.Collections.Hashtable();
                props["port"] = RemotingLibrary.port;
                tcpChannel = new TcpChannel(props, null, provider);
            }

            catch (Exception ex)
            {
                //The server might already be running
                Log("The channel could not be created. \nThe server may already be running or a different program might be using the port\n"
                    + "Error: " + ex.Message + "\n");

                return;
            }

            ChannelServices.RegisterChannel(tcpChannel, false);

            //Create the Remote Object and register it
            WellKnownServiceTypeEntry remobj = new WellKnownServiceTypeEntry
                (typeof(DNRemoteObject), "RemoteObject", WellKnownObjectMode.Singleton);

            RemotingConfiguration.RegisterWellKnownServiceType(remobj);
            Log("Remote Object created: " + remobj.ObjectUri + ":" + RemotingLibrary.port);
            DNRemotingConnectionChangedEvent("Server", "Connected: " + remobj.ObjectUri + ":" + RemotingLibrary.port);

            remoteObject = RemotingLibrary.GetRemoteObject();
        }

        public void ShutdownDNRemotingServer()
        {
            Log("Shutting down");
            DNRemotingConnectionChangedEvent("Client", "No client connected");
            DNRemotingConnectionChangedEvent("Server", "");
            ChannelServices.UnregisterChannel(tcpChannel);
        }
    }
}
