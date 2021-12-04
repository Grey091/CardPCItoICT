using SYNOPEX_ICT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace SYNOPEX_ICT.ViewModels
{
    [Serializable]
    class RemoteWindowVM : BaseVM
    {
        #region mess
        private string _Mess1;
        public string Mess1
        {
            get => _Mess1;
            set { _Mess1 = value; OnPropertyChanged("Mess1"); }
        }
        private string _Mess2;
        public string Mess2
        {
            get => _Mess2;
            set { _Mess2 = value; OnPropertyChanged("Mess2"); }
        }
        private string _Mess3;
        public string Mess3
        {
            get => _Mess3;
            set { _Mess3 = value; OnPropertyChanged("Mess3"); }
        }
        private string _Mess4;
        public string Mess4
        {
            get => _Mess4;
            set { _Mess4 = value; OnPropertyChanged("Mess4"); }
        }
        private string _Mess5;
        public string Mess5
        {
            get => _Mess5;
            set { _Mess5 = value; OnPropertyChanged("Mess5"); }
        }
        private string _Mess6;
        public string Mess6
        {
            get => _Mess6;
            set { _Mess6 = value; OnPropertyChanged("Mess6"); }
        }
        #endregion

        TCPServer tcpServer;
        DNRemotingServer dnRemotingServer;
        List<string> listLog;
        public delegate void UpdateTextBoxDelegate(string textbox, string text);

        public ICommand CloseWindowCommand { get; set; }        
        public ICommand SetValueCommand { get; set; }
        public RemoteWindowVM()
        {
            listLog = new List<string>();
            StartTCPServer();
            StartDNRemotingServer();

            CloseWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                tcpServer.ShutdownTCPServer();
                dnRemotingServer.ShutdownDNRemotingServer();
            });
            SetValueCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                string x = Mess5;
                dnRemotingServer.remoteObject.SetX(x);
            });
        }
        void StartTCPServer()
        {
            tcpServer = new TCPServer();

            //Subscribe to events and start the server
            tcpServer.logMessageEvent += new TCPServer.LogMessageEventDelegate(LogMessageEventHandler);
            tcpServer.TcpConnectionChangedEvent += new TCPServer.TcpConnectionChanged(tcpServer_TcpConnectionChangedEvent);
            tcpServer.TcpMessageReceivedEvent += new TCPServer.TcpMessageReceivedEventDelegate(tcpServer_TcpMessageReceivedEvent);

            tcpServer.StartTCPServer();
        }
        void StartDNRemotingServer()
        {
            dnRemotingServer = new DNRemotingServer();

            //Subscribe to events and start the server
            dnRemotingServer.logMessageEvent += new DNRemotingServer.LogMessageEventDelegate(LogMessageEventHandler);
            dnRemotingServer.DNRemotingConnectionChangedEvent += new DNRemotingServer.DNRemotingConnectionChanged(dnRemotingServer_DNRemotingConnectionChangedEvent);
            dnRemotingServer.StartDNRemotingServer();
            dnRemotingServer.remoteObject.DNRemotingClientConnectedEvent += new DNRemotingRemoteObject.DNRemoteObject.DNRemotingClientConnectedEventDelegate(remoteObject_DNRemotingClientConnectedEvent);
            dnRemotingServer.remoteObject.DNRemoteObjectXValueChangedEvent += new DNRemotingRemoteObject.DNRemoteObject.DNRemoteObjectXValueChangedEventDelegate(remoteObject_DNRemoteObjectXValueChangedEvent);
            dnRemotingServer.remoteObject.DNRemoteObjectSineCalculatedEvent += new DNRemotingRemoteObject.DNRemoteObject.DNRemoteObjectSineCalculatedEventDelegate(remoteObject_DNRemoteObjectSineCalculatedEvent);
        }

        #region TCP and .NET Remoting EventHandlers
        void LogMessageEventHandler(object sender, string message)
        {
            UpdateLog(sender.GetType().Name, message);
        }
        void tcpServer_TcpConnectionChangedEvent(string sender, string status)
        {
            if (sender == "Client")
                Mess1 = status;
            else if (sender == "Server")
                Mess2 = status;
        }
        void tcpServer_TcpMessageReceivedEvent(string function, string argument)
        {
            if (function == "PopupTestMessage")
                dnRemotingServer.remoteObject.PopupTestMessage(argument);

            else if (function == "SetX")
                dnRemotingServer.remoteObject.SetX(argument);

            else if (function == "GetX")
            {
                string x = dnRemotingServer.remoteObject.GetX();
                tcpServer.SendMessage(x.ToString());
            }

            else if (function == "CalculateSine")
            {
                double sine = dnRemotingServer.remoteObject.CalculateSine();
                tcpServer.SendMessage(sine.ToString());
            }
            else
                UpdateLog("Server", "Unknown TCP Command - " + function);

            tcpServer.WaitForMessages();
        }
        void dnRemotingServer_DNRemotingConnectionChangedEvent(string sender, string status)
        {
            if (sender == "Client")
                Mess3 = status;
            else if (sender == "Server")
                Mess4 = status;
        }
        void remoteObject_DNRemotingClientConnectedEvent()
        {
            Mess3 = "Connected";
            UpdateLog("DNRemotingServer", "Client Connected");
        }
        void remoteObject_DNRemoteObjectSineCalculatedEvent(double sine)
        {
            Mess6 = dnRemotingServer.remoteObject.Sine.ToString();
            UpdateLog("RemoteObject", "Sine(x) Calculated: " + sine.ToString());
        }
        void remoteObject_DNRemoteObjectXValueChangedEvent(string x)
        {
            try
            {
                //Application.Current.Dispatcher.Invoke(new Action(() =>
                //{
                //    Mess5 = dnRemotingServer.remoteObject.GetX().ToString();
                //}));
                UpdateTextBox(Mess5, dnRemotingServer.remoteObject.GetX().ToString());
                //MessageBox.Show(Mess5);
            }
            catch(Exception e) {
                MessageBox.Show(e.ToString());
            }
            UpdateLog("RemoteObject", "X Value Changed: " + x.ToString());
        }
        #endregion       
        private void UpdateLog(string v1, string v2)
        {
            listLog.Add(v2);
        }
        void UpdateTextBox(string textBox, string text)
        {
            if (Dispatcher.CurrentDispatcher.HasShutdownStarted)
            {
                //Allow for cross-thread updating
                Dispatcher.CurrentDispatcher.BeginInvoke(new UpdateTextBoxDelegate(UpdateTextBox), new object[] { textBox, text });
                return;
            }
            textBox = text;
        }
    }
}
