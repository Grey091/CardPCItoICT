using SYNOPEX_ICT.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Threading;
using MessageBox = System.Windows.MessageBox;

namespace SYNOPEX_ICT.ViewModels
{
    public class DigitalIOVM : WinApiServiceBase, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));           
        }

        private IntPtr _windowHandle;
        public IntPtr WindowHandle
        {
            get { return _windowHandle; }
            set
            {
                _windowHandle = value;
            }
        }

        #region ValuesBinding       
        private bool _isInterrupt;
        public bool isInterrupt
        {
            get => _isInterrupt;
            set { _isInterrupt = value; OnPropertyChanged("isInterrupt"); }
        }
        private bool _isRiging;
        public bool isRiging
        {
            get => _isRiging;
            set { _isRiging = value; OnPropertyChanged("isRiging"); }
        }
        private bool _isFalling;
        public bool isFalling
        {
            get => _isFalling;
            set { _isFalling = value; OnPropertyChanged("isFalling"); }
        }
        private bool _isEvent;
        public bool isEvent
        {
            get => _isEvent;
            set { _isEvent = value; OnPropertyChanged("isEvent"); }
        }
        private bool _isMessage;
        public bool isMessage
        {
            get => _isMessage;
            set { _isMessage = value; OnPropertyChanged("isMessage"); }
        }
        private bool _isCallback;
        public bool isCallback
        {
            get => _isCallback;
            set { _isCallback = value; OnPropertyChanged("isCallback"); }
        }
        #region OUTPUT
        private bool _out1;
        public bool out1
        {
            get => _out1;
            set { _out1 = value; OnPropertyChanged("out1"); }
        }
        private bool _out2;
        public bool out2
        {
            get => _out2;
            set { _out2 = value; OnPropertyChanged("out2"); }
        }
        private bool _out3;
        public bool out3
        {
            get => _out3;
            set { _out3 = value; OnPropertyChanged("out3"); }
        }
        private bool _out4;
        public bool out4
        {
            get => _out4;
            set { _out4 = value; OnPropertyChanged("out4"); }
        }
        private bool _out5;
        public bool out5
        {
            get => _out5;
            set { _out5 = value; OnPropertyChanged("out5"); }
        }
        private bool _out6;
        public bool out6
        {
            get => _out6;
            set { _out6 = value; OnPropertyChanged("out6"); }
        }
        private bool _out7;
        public bool out7
        {
            get => _out7;
            set { _out7 = value; OnPropertyChanged("out7"); }
        }
        private bool _out8;
        public bool out8
        {
            get => _out8;
            set { _out8 = value; OnPropertyChanged("out8"); }
        }
        private bool _out9;
        public bool out9
        {
            get => _out9;
            set { _out9 = value; OnPropertyChanged("out9"); }
        }
        private bool _out10;
        public bool out10
        {
            get => _out10;
            set { _out10 = value; OnPropertyChanged("out10"); }
        }
        private bool _out11;
        public bool out11
        {
            get => _out11;
            set { _out11 = value; OnPropertyChanged("out11"); }
        }
        private bool _out12;
        public bool out12
        {
            get => _out12;
            set { _out12 = value; OnPropertyChanged("out12"); }
        }
        private bool _out13;
        public bool out13
        {
            get => _out13;
            set { _out13 = value; OnPropertyChanged("out13"); }
        }
        private bool _out14;
        public bool out14
        {
            get => _out14;
            set { _out14 = value; OnPropertyChanged("out14"); }
        }
        private bool _out15;
        public bool out15
        {
            get => _out15;
            set { _out15 = value; OnPropertyChanged("out15"); }
        }
        private bool _out16;
        public bool out16
        {
            get => _out16;
            set { _out16 = value; OnPropertyChanged("out16"); }
        }
        #endregion

        #region OUTPUT1
        private bool _out1a;
        public bool out1a
        {
            get => _out1a;
            set { _out1a = value; OnPropertyChanged("out1a"); }
        }
        private bool _out2a;
        public bool out2a
        {
            get => _out2a;
            set { _out2a = value; OnPropertyChanged("out2a"); }
        }
        private bool _out3a;
        public bool out3a
        {
            get => _out3a;
            set { _out3a = value; OnPropertyChanged("out3a"); }
        }
        private bool _out4a;
        public bool out4a
        {
            get => _out4a;
            set { _out4a = value; OnPropertyChanged("out4a"); }
        }
        private bool _out5a;
        public bool out5a
        {
            get => _out5a;
            set { _out5a = value; OnPropertyChanged("out5a"); }
        }
        private bool _out6a;
        public bool out6a
        {
            get => _out6a;
            set { _out6a = value; OnPropertyChanged("out6a"); }
        }
        private bool _out7a;
        public bool out7a
        {
            get => _out7a;
            set { _out7a = value; OnPropertyChanged("out7a"); }
        }
        private bool _out8a;
        public bool out8a
        {
            get => _out8a;
            set { _out8a = value; OnPropertyChanged("out8a"); }
        }
        private bool _out9a;
        public bool out9a
        {
            get => _out9a;
            set { _out9a = value; OnPropertyChanged("out9a"); }
        }
        private bool _out10a;
        public bool out10a
        {
            get => _out10a;
            set { _out10a = value; OnPropertyChanged("out10a"); }
        }
        private bool _out11a;
        public bool out11a
        {
            get => _out11a;
            set { _out11a = value; OnPropertyChanged("out11a"); }
        }
        private bool _out12a;
        public bool out12a
        {
            get => _out12a;
            set { _out12a = value; OnPropertyChanged("out12a"); }
        }
        private bool _out13a;
        public bool out13a
        {
            get => _out13a;
            set { _out13a = value; OnPropertyChanged("out13a"); }
        }
        private bool _out14a;
        public bool out14a
        {
            get => _out14a;
            set { _out14a = value; OnPropertyChanged("out14a"); }
        }
        private bool _out15a;
        public bool out15a
        {
            get => _out15a;
            set { _out15a = value; OnPropertyChanged("out15a"); }
        }
        private bool _out16a;
        public bool out16a
        {
            get => _out16a;
            set { _out16a = value; OnPropertyChanged("out16a"); }
        }
        #endregion

        #region INPUT
        private bool _in1;
        public bool in1
        {
            get => _in1;
            set { _in1 = value; OnPropertyChanged("in1"); }
        }
        private bool _in2;
        public bool in2
        {
            get => _in2;
            set { _in2 = value; OnPropertyChanged("in2"); }
        }
        private bool _in3;
        public bool in3
        {
            get => _in3;
            set { _in3 = value; OnPropertyChanged("in3"); }
        }
        private bool _in4;
        public bool in4
        {
            get => _in4;
            set { _in4 = value; OnPropertyChanged("in4"); }
        }
        private bool _in5;
        public bool in5
        {
            get => _in5;
            set { _in5 = value; OnPropertyChanged("in5"); }
        }
        private bool _in6;
        public bool in6
        {
            get => _in6;
            set { _in6 = value; OnPropertyChanged("in6"); }
        }
        private bool _in7;
        public bool in7
        {
            get => _in7;
            set { _in7 = value; OnPropertyChanged("in7"); }
        }
        private bool _in8;
        public bool in8
        {
            get => _in8;
            set { _in8 = value; OnPropertyChanged("in8"); }
        }
        private bool _in9;
        public bool in9
        {
            get => _in9;
            set { _in9 = value; OnPropertyChanged("in9"); }
        }
        private bool _in10;
        public bool in10
        {
            get => _in10;
            set { _in10 = value; OnPropertyChanged("in10"); }
        }
        private bool _in11;
        public bool in11
        {
            get => _in11;
            set { _in11 = value; OnPropertyChanged("in11"); }
        }
        private bool _in12;
        public bool in12
        {
            get => _in12;
            set { _in12 = value; OnPropertyChanged("in12"); }
        }
        private bool _in13;
        public bool in13
        {
            get => _in13;
            set { _in13 = value; OnPropertyChanged("in13"); }
        }
        private bool _in14;
        public bool in14
        {
            get => _in14;
            set { _in14 = value; OnPropertyChanged("in14"); }
        }
        private bool _in15;
        public bool in15
        {
            get => _in15;
            set { _in15 = value; OnPropertyChanged("in15"); }
        }
        private bool _in16;
        public bool in16
        {
            get => _in16;
            set { _in16 = value; OnPropertyChanged("in16"); }
        }
        #endregion

        #region INPUT1
        private bool _in1a;
        public bool in1a
        {
            get => _in1a;
            set { _in1a = value; OnPropertyChanged("in1a"); }
        }
        private bool _in2a;
        public bool in2a
        {
            get => _in2a;
            set { _in2a = value; OnPropertyChanged("in2a"); }
        }
        private bool _in3a;
        public bool in3a
        {
            get => _in3a;
            set { _in3a = value; OnPropertyChanged("in3a"); }
        }
        private bool _in4a;
        public bool in4a
        {
            get => _in4a;
            set { _in4a = value; OnPropertyChanged("in4a"); }
        }
        private bool _in5a;
        public bool in5a
        {
            get => _in5a;
            set { _in5a = value; OnPropertyChanged("in5a"); }
        }
        private bool _in6a;
        public bool in6a
        {
            get => _in6a;
            set { _in6a = value; OnPropertyChanged("in6a"); }
        }
        private bool _in7a;
        public bool in7a
        {
            get => _in7a;
            set { _in7a = value; OnPropertyChanged("in7a"); }
        }
        private bool _in8a;
        public bool in8a
        {
            get => _in8a;
            set { _in8a = value; OnPropertyChanged("in8a"); }
        }
        private bool _in9a;
        public bool in9a
        {
            get => _in9a;
            set { _in9a = value; OnPropertyChanged("in9a"); }
        }
        private bool _in10a;
        public bool in10a
        {
            get => _in10a;
            set { _in10a = value; OnPropertyChanged("in10a"); }
        }
        private bool _in11a;
        public bool in11a
        {
            get => _in11a;
            set { _in11a = value; OnPropertyChanged("in11a"); }
        }
        private bool _in12a;
        public bool in12a
        {
            get => _in12a;
            set { _in12a = value; OnPropertyChanged("in12a"); }
        }
        private bool _in13a;
        public bool in13a
        {
            get => _in13a;
            set { _in13a = value; OnPropertyChanged("in13a"); }
        }
        private bool _in14a;
        public bool in14a
        {
            get => _in14a;
            set { _in14a = value; OnPropertyChanged("in14a"); }
        }
        private bool _in15a;
        public bool in15a
        {
            get => _in15a;
            set { _in15a = value; OnPropertyChanged("in15a"); }
        }
        private bool _in16a;
        public bool in16a
        {
            get => _in16a;
            set { _in16a = value; OnPropertyChanged("in16a"); }
        }
        #endregion
        #endregion
        #region Commands
        public ICommand LowCheckCommand1 { get; set; }
        public ICommand LowCheckCommand2 { get; set; }
        public ICommand LowCheckCommand3 { get; set; }
        public ICommand LowCheckCommand4 { get; set; }
        public ICommand LowCheckCommand5 { get; set; }
        public ICommand LowCheckCommand6 { get; set; }
        public ICommand LowCheckCommand7 { get; set; }
        public ICommand LowCheckCommand8 { get; set; }
        public ICommand LowCheckCommand9 { get; set; }
        public ICommand LowCheckCommand11 { get; set; }
        public ICommand LowCheckCommand12{ get; set; }
        public ICommand LowCheckCommand10 { get; set; }
        public ICommand LowCheckCommand13 { get; set; }
        public ICommand LowCheckCommand14 { get; set; }
        public ICommand LowCheckCommand15 { get; set; }
        public ICommand LowCheckCommand16 { get; set; }
        public ICommand LowCheckCommand17 { get; set; }
        public ICommand LowCheckCommand18 { get; set; }
        public ICommand LowCheckCommand19 { get; set; }
        public ICommand LowCheckCommand20 { get; set; }
        public ICommand LowCheckCommand21 { get; set; }
        public ICommand LowCheckCommand22 { get; set; }
        public ICommand LowCheckCommand23 { get; set; }
        public ICommand LowCheckCommand24 { get; set; }
        public ICommand LowCheckCommand25 { get; set; }
        public ICommand LowCheckCommand26 { get; set; }
        public ICommand LowCheckCommand27 { get; set; }
        public ICommand LowCheckCommand28 { get; set; }
        public ICommand LowCheckCommand29 { get; set; }
        public ICommand LowCheckCommand30 { get; set; }
        public ICommand LowCheckCommand31 { get; set; }
        public ICommand LowCheckCommand32 { get; set; }

        #endregion

        public readonly static uint INFINITE = 0xFFFFFFFF;
        public readonly static uint STATUS_WAIT_0 = 0x00000000;
        public readonly static uint WAIT_OBJECT_0 = ((STATUS_WAIT_0) + 0);

        private uint hInterrupt = 0;
        private Thread EventThread = null;
        private bool bThread = false;
        DispatcherTimer TimerDisplay1;

        private List<string> _listIOModule;
        public List<string> listIOModule
        {
            get => _listIOModule;
            set 
            { 
                _listIOModule = value;
                OnPropertyChanged();
            }
        }
        CAXHS.AXT_INTERRUPT_PROC Callbackfunction = new CAXHS.AXT_INTERRUPT_PROC(InterruptCallback);

        DispatcherTimer Timer1;
        DispatcherTimer Timer2;
        //DispatcherTimer TimerCheckchanged;

        [DllImport("kernel32", EntryPoint = "WaitForSingleObject", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern uint WaitForSingleObject(uint hHandle, uint dwMilliseconds);

        [DllImport("KERNEL32", EntryPoint = "SetEvent", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool SetEvent(long hEvent);


        public DigitalIOVM()
        {
            listIOModule = new List<string>();

            Timer1 = new DispatcherTimer();
            Timer1.Interval = TimeSpan.FromMilliseconds(10);
            Timer1.Tick += Timer1_Tick;

            TimerDisplay1 = new DispatcherTimer();
            TimerDisplay1.Interval = TimeSpan.FromMilliseconds(100);
            TimerDisplay1.Tick += TimerDisplay1_Tick; ;

            Timer2 = new DispatcherTimer();
            Timer2.Interval = TimeSpan.FromMilliseconds(100);
            Timer2.Tick += Timer2_Tick;

            if (OpenDevice())
            {
                isMessage = true;
                Timer1.Start();
                Timer2.Start();
                TimerDisplay1.Start();
                SelectModule1();
                SelectModule2();               
            }
            #region Commands
            //LowCheckCommand1 = new RelayCommand<CheckBox>((p) => { return true; }, (p) =>
            //{
            //    SelectLowIndex(0, 0, Convert.ToUInt32(out1));   
            //});
            //LowCheckCommand2 = new RelayCommand<CheckBox>((p) => { return true; }, (p) =>
            //{
            //    SelectLowIndex(0, 1, Convert.ToUInt32(out2));
            //});
            //LowCheckCommand3 = new RelayCommand<CheckBox>((p) => { return true; }, (p) =>
            //{
            //    SelectLowIndex(0, 2, Convert.ToUInt32(out3));
            //});
            //LowCheckCommand4 = new RelayCommand<CheckBox>((p) => { return true; }, (p) =>
            //{
            //    SelectLowIndex(0, 3, Convert.ToUInt32(out4));
            //});
            //LowCheckCommand5 = new RelayCommand<CheckBox>((p) => { return true; }, (p) =>
            //{
            //    SelectLowIndex(0, 4, Convert.ToUInt32(out5));
            //});
            //LowCheckCommand6 = new RelayCommand<CheckBox>((p) => { return true; }, (p) =>
            //{
            //    SelectLowIndex(0, 5, Convert.ToUInt32(out6));
            //});
            //LowCheckCommand7 = new RelayCommand<CheckBox>((p) => { return true; }, (p) =>
            //{
            //    SelectLowIndex(0, 6, Convert.ToUInt32(out7));
            //});
            //LowCheckCommand8 = new RelayCommand<CheckBox>((p) => { return true; }, (p) =>
            //{
            //    SelectLowIndex(0, 7, Convert.ToUInt32(out8));
            //});
            //LowCheckCommand9 = new RelayCommand<CheckBox>((p) => { return true; }, (p) =>
            //{
            //    SelectLowIndex(0, 8, Convert.ToUInt32(out9));
            //});
            //LowCheckCommand10 = new RelayCommand<CheckBox>((p) => { return true; }, (p) =>
            //{
            //    SelectLowIndex(0, 9, Convert.ToUInt32(out10));
            //});
            //LowCheckCommand11 = new RelayCommand<CheckBox>((p) => { return true; }, (p) =>
            //{
            //    SelectLowIndex(0, 10, Convert.ToUInt32(out11));
            //});
            //LowCheckCommand12 = new RelayCommand<CheckBox>((p) => { return true; }, (p) =>
            //{
            //    SelectLowIndex(0, 11, Convert.ToUInt32(out12));
            //});
            //LowCheckCommand13 = new RelayCommand<CheckBox>((p) => { return true; }, (p) =>
            //{
            //    SelectLowIndex(0, 12, Convert.ToUInt32(out13));
            //});
            //LowCheckCommand14 = new RelayCommand<CheckBox>((p) => { return true; }, (p) =>
            //{
            //    SelectLowIndex(0, 13, Convert.ToUInt32(out14));
            //});
            //LowCheckCommand15 = new RelayCommand<CheckBox>((p) => { return true; }, (p) =>
            //{
            //    SelectLowIndex(0, 14, Convert.ToUInt32(out15));
            //});
            //LowCheckCommand16 = new RelayCommand<CheckBox>((p) => { return true; }, (p) =>
            //{
            //    SelectLowIndex(0, 15, Convert.ToUInt32(out16));
            //});
            //LowCheckCommand17 = new RelayCommand<CheckBox>((p) => { return true; }, (p) =>
            //{
            //    SelectLowIndex(1, 0, Convert.ToUInt32(out1a));
            //});
            //LowCheckCommand18 = new RelayCommand<CheckBox>((p) => { return true; }, (p) =>
            //{
            //    SelectLowIndex(1, 1, Convert.ToUInt32(out2a));
            //});
            //LowCheckCommand19 = new RelayCommand<CheckBox>((p) => { return true; }, (p) =>
            //{
            //    SelectLowIndex(1, 2, Convert.ToUInt32(out3a));
            //});
            //LowCheckCommand20 = new RelayCommand<CheckBox>((p) => { return true; }, (p) =>
            //{
            //    SelectLowIndex(1, 3, Convert.ToUInt32(out4a));
            //});
            //LowCheckCommand21 = new RelayCommand<CheckBox>((p) => { return true; }, (p) =>
            //{
            //    SelectLowIndex(1, 4, Convert.ToUInt32(out5a));
            //});
            //LowCheckCommand22 = new RelayCommand<CheckBox>((p) => { return true; }, (p) =>
            //{
            //    SelectLowIndex(1, 5, Convert.ToUInt32(out6a));
            //});
            //LowCheckCommand23 = new RelayCommand<CheckBox>((p) => { return true; }, (p) =>
            //{
            //    SelectLowIndex(1, 6, Convert.ToUInt32(out7a));
            //});
            //LowCheckCommand24 = new RelayCommand<CheckBox>((p) => { return true; }, (p) =>
            //{
            //    SelectLowIndex(1, 7, Convert.ToUInt32(out8a));
            //});
            //LowCheckCommand25 = new RelayCommand<CheckBox>((p) => { return true; }, (p) =>
            //{
            //    SelectLowIndex(1, 8, Convert.ToUInt32(out9a));
            //});
            //LowCheckCommand26 = new RelayCommand<CheckBox>((p) => { return true; }, (p) =>
            //{
            //    SelectLowIndex(1, 9, Convert.ToUInt32(out10a));
            //});
            //LowCheckCommand27 = new RelayCommand<CheckBox>((p) => { return true; }, (p) =>
            //{
            //    SelectLowIndex(1, 10, Convert.ToUInt32(out11a));
            //});
            //LowCheckCommand28 = new RelayCommand<CheckBox>((p) => { return true; }, (p) =>
            //{
            //    SelectLowIndex(1, 11, Convert.ToUInt32(out12a));
            //});
            //LowCheckCommand29 = new RelayCommand<CheckBox>((p) => { return true; }, (p) =>
            //{
            //    SelectLowIndex(1, 12, Convert.ToUInt32(out13a));
            //});
            //LowCheckCommand30 = new RelayCommand<CheckBox>((p) => { return true; }, (p) =>
            //{
            //    SelectLowIndex(1, 13, Convert.ToUInt32(out14a));
            //});
            //LowCheckCommand31 = new RelayCommand<CheckBox>((p) => { return true; }, (p) =>
            //{
            //    SelectLowIndex(1, 14, Convert.ToUInt32(out15a));
            //});
            //LowCheckCommand32 = new RelayCommand<CheckBox>((p) => { return true; }, (p) =>
            //{
            //    SelectLowIndex(1, 15, Convert.ToUInt32(out16a));
            //});
            #endregion
        }

        private void TimerDisplay1_Tick(object sender, EventArgs e)
        {
            //GC.Collect();
            //GC.WaitForPendingFinalizers();
            //GC.Collect();

            SelectLowIndex(0, 0, Convert.ToUInt32(out1));
            SelectLowIndex(0, 1, Convert.ToUInt32(out2));
            SelectLowIndex(0, 2, Convert.ToUInt32(out3));
            SelectLowIndex(0, 3, Convert.ToUInt32(out4));
            SelectLowIndex(0, 4, Convert.ToUInt32(out5));
            SelectLowIndex(0, 5, Convert.ToUInt32(out6));
            SelectLowIndex(0, 6, Convert.ToUInt32(out7));
            SelectLowIndex(0, 7, Convert.ToUInt32(out8));
            SelectLowIndex(0, 8, Convert.ToUInt32(out9));
            SelectLowIndex(0, 9, Convert.ToUInt32(out10));
            SelectLowIndex(0, 10, Convert.ToUInt32(out11));
            SelectLowIndex(0, 11, Convert.ToUInt32(out12));
            SelectLowIndex(0, 12, Convert.ToUInt32(out13));
            SelectLowIndex(0, 13, Convert.ToUInt32(out14));
            SelectLowIndex(0, 14, Convert.ToUInt32(out15));
            SelectLowIndex(0, 15, Convert.ToUInt32(out16));

            SelectLowIndex(1, 0, Convert.ToUInt32(out1a));
            SelectLowIndex(1, 1, Convert.ToUInt32(out2a));
            SelectLowIndex(1, 2, Convert.ToUInt32(out3a));
            SelectLowIndex(1, 3, Convert.ToUInt32(out4a));
            SelectLowIndex(1, 4, Convert.ToUInt32(out5a));
            SelectLowIndex(1, 5, Convert.ToUInt32(out6a));
            SelectLowIndex(1, 6, Convert.ToUInt32(out7a));
            SelectLowIndex(1, 7, Convert.ToUInt32(out8a));
            SelectLowIndex(1, 8, Convert.ToUInt32(out9a));
            SelectLowIndex(1, 9, Convert.ToUInt32(out10a));
            SelectLowIndex(1, 10, Convert.ToUInt32(out11a));
            SelectLowIndex(1, 11, Convert.ToUInt32(out12a));
            SelectLowIndex(1, 12, Convert.ToUInt32(out13a));
            SelectLowIndex(1, 13, Convert.ToUInt32(out14a));
            SelectLowIndex(1, 14, Convert.ToUInt32(out15a));
            SelectLowIndex(1, 15, Convert.ToUInt32(out16a));

            //GC.Collect();
            //GC.WaitForPendingFinalizers();
            //GC.Collect();

            //#region Board1
            //if (out1 == true)
            //{
            //    SelectLowIndex(0, 0, Convert.ToUInt32(out1));
            //}
            //if (out2 == true)
            //{
            //    SelectLowIndex(0, 1, Convert.ToUInt32(out2));
            //}
            //if (out3 == true)
            //{
            //    SelectLowIndex(0, 2, Convert.ToUInt32(out3));
            //}
            //if (out4 == true)
            //{
            //    SelectLowIndex(0, 3, Convert.ToUInt32(out4));
            //}
            //if (out5 == true)
            //{
            //    SelectLowIndex(0, 4, Convert.ToUInt32(out5));
            //}
            //if (out6 == true)
            //{
            //    SelectLowIndex(0, 5, Convert.ToUInt32(out6));
            //}
            //if (out7 == true)
            //{
            //    SelectLowIndex(0, 6, Convert.ToUInt32(out7));
            //}
            //if (out8 == true)
            //{
            //    SelectLowIndex(0, 7, Convert.ToUInt32(out8));
            //}
            //if (out9 == true)
            //{
            //    SelectLowIndex(0, 8, Convert.ToUInt32(out9));
            //}
            //if (out10 == true)
            //{
            //    SelectLowIndex(0, 9, Convert.ToUInt32(out10));
            //}
            //if (out11 == true)
            //{
            //    SelectLowIndex(0, 10, Convert.ToUInt32(out11));
            //}
            //if (out12 == true)
            //{
            //    SelectLowIndex(0, 11, Convert.ToUInt32(out12));
            //}
            //if (out13 == true)
            //{
            //    SelectLowIndex(0, 12, Convert.ToUInt32(out13));
            //}
            //if (out14 == true)
            //{
            //    SelectLowIndex(0, 13, Convert.ToUInt32(out14));
            //}
            //if (out15 == true)
            //{
            //    SelectLowIndex(0, 14, Convert.ToUInt32(out15));
            //}
            //if (out16 == true)
            //{
            //    SelectLowIndex(0, 15, Convert.ToUInt32(out16));
            //}
            //#endregion

            //#region Board2
            //if (out1a == true)
            //{
            //    SelectLowIndex(1, 0, Convert.ToUInt32(out1a));
            //}
            //if (out2a == true)
            //{
            //    SelectLowIndex(1, 1, Convert.ToUInt32(out2a));
            //}
            //if (out3a == true)
            //{
            //    SelectLowIndex(1, 2, Convert.ToUInt32(out3a));
            //}
            //if (out4a == true)
            //{
            //    SelectLowIndex(1, 3, Convert.ToUInt32(out4a));
            //}
            //if (out5a == true)
            //{
            //    SelectLowIndex(1, 4, Convert.ToUInt32(out5a));
            //}
            //if (out6a == true)
            //{
            //    SelectLowIndex(1, 5, Convert.ToUInt32(out6a));
            //}
            //if (out7a == true)
            //{
            //    SelectLowIndex(1, 6, Convert.ToUInt32(out7a));
            //}
            //if (out8a == true)
            //{
            //    SelectLowIndex(1, 7, Convert.ToUInt32(out8a));
            //}
            //if (out9a == true)
            //{
            //    SelectLowIndex(1, 8, Convert.ToUInt32(out9a));
            //}
            //if (out10a == true)
            //{
            //    SelectLowIndex(1, 9, Convert.ToUInt32(out10a));
            //}
            //if (out11a == true)
            //{
            //    SelectLowIndex(1, 10, Convert.ToUInt32(out11a));
            //}
            //if (out12a == true)
            //{
            //    SelectLowIndex(1, 11, Convert.ToUInt32(out12a));
            //}
            //if (out13a == true)
            //{
            //    SelectLowIndex(1, 12, Convert.ToUInt32(out13a));
            //}
            //if (out14a == true)
            //{
            //    SelectLowIndex(1, 13, Convert.ToUInt32(out14a));
            //}
            //if (out15a == true)
            //{
            //    SelectLowIndex(1, 14, Convert.ToUInt32(out15a));
            //}
            //if (out16a == true)
            //{
            //    SelectLowIndex(1, 15, Convert.ToUInt32(out16a));
            //}
            //#endregion
        }
        private void Timer2_Tick(object sender, EventArgs e)
        {
            //GC.Collect();
            //GC.WaitForPendingFinalizers();
            //GC.Collect();

            short nIndex = 0;
            uint uDataHigh = 0;
            uint uDataLow = 0;
            uint uFlagHigh = 0;
            uint uFlagLow = 0;
            int nBoardNo = 0;
            int nModulePos = 0;
            uint uModuleID = 0;
            bool[] checkHigh = new bool[32];
            bool[] checkLow = new bool[32];            

            CAXD.AxdInfoGetModule(1, ref nBoardNo, ref nModulePos, ref uModuleID);

            switch ((AXT_MODULE)uModuleID)
            {
                case AXT_MODULE.AXT_SIO_DI32:
                case AXT_MODULE.AXT_SIO_RDI32:
                case AXT_MODULE.AXT_SIO_RSIMPLEIOMLII:
                case AXT_MODULE.AXT_SIO_RDO16AMLII:
                case AXT_MODULE.AXT_SIO_RDO16BMLII:
                case AXT_MODULE.AXT_SIO_DI32_P:
                case AXT_MODULE.AXT_SIO_RDI32RTEX:
                    //++
                    // Read inputting signal in WORD
                    CAXD.AxdiReadInportWord(1, 0, ref uDataHigh);
                    CAXD.AxdiReadInportWord(1, 1, ref uDataLow);

                    for (nIndex = 16; nIndex < 32; nIndex++)
                    {
                        // Verify the last bit value of data read
                        uFlagHigh = uDataHigh & 0x0001;
                        uFlagLow = uDataLow & 0x0001;

                        // Shift rightward by bit by bit
                        uDataHigh = uDataHigh >> 1;
                        uDataLow = uDataLow >> 1;

                        // Updat bit value in control
                        if (uFlagHigh == 1)
                            checkHigh[nIndex] = true;
                        else
                            checkHigh[nIndex] = false;

                        if (uFlagLow == 1)
                            checkLow[nIndex] = true;
                        else
                            checkLow[nIndex] = false;
                    }
                    
                    in1a = checkHigh[16]; in9a = checkHigh[24];                   
                    in2a = checkHigh[17]; in10a = checkHigh[25];                   
                    in3a = checkHigh[18]; in11a = checkHigh[26];                    
                    in4a = checkHigh[19]; in12a = checkHigh[27];                    
                    in5a = checkHigh[20]; in13a = checkHigh[28];                   
                    in6a = checkHigh[21]; in14a = checkHigh[29];                    
                    in7a = checkHigh[22]; in15a = checkHigh[30];                    
                    in8a = checkHigh[23]; in16a = checkHigh[31]; 
                    
                    out1a = checkLow[16]; out9a = checkLow[24];                    
                    out2a = checkLow[17]; out10a = checkLow[25];                    
                    out3a = checkLow[18]; out11a = checkLow[26];                   
                    out4a = checkLow[19]; out12a = checkLow[27];                    
                    out5a = checkLow[20]; out13a = checkLow[28];                    
                    out6a = checkLow[21]; out14a = checkLow[29];                   
                    out7a = checkLow[22]; out15a = checkLow[30];                    
                    out8a = checkLow[23]; out16a = checkLow[31];
                    break;

                case AXT_MODULE.AXT_SIO_DB32P:
                case AXT_MODULE.AXT_SIO_DB32T:
                case AXT_MODULE.AXT_SIO_RDB32T:
                case AXT_MODULE.AXT_SIO_RDB32RTEX:
                case AXT_MODULE.AXT_SIO_RDB96MLII:
                case AXT_MODULE.AXT_SIO_RDB128MLII:
                    //++
                    // Read inputting signal in WORD
                    CAXD.AxdiReadInportWord(1, 0, ref uDataHigh);

                    for (nIndex = 16; nIndex < 32; nIndex++)
                    {
                        // Verify the last bit value of data read
                        uFlagHigh = uDataHigh & 0x0001;

                        // Shift rightward by bit by bit
                        uDataHigh = uDataHigh >> 1;

                        // Updat bit value in control
                        if (uFlagHigh == 1)
                            checkHigh[nIndex] = true;
                        else
                            checkHigh[nIndex] = false;
                    }
                    
                    in1a = checkHigh[16]; in9a = checkHigh[24];                    
                    in2a = checkHigh[17]; in10a = checkHigh[25];                    
                    in3a = checkHigh[18]; in11a = checkHigh[26];                   
                    in4a = checkHigh[19]; in12a = checkHigh[27];                    
                    in5a = checkHigh[20]; in13a = checkHigh[28];                   
                    in6a = checkHigh[21]; in14a = checkHigh[29];                   
                    in7a = checkHigh[22]; in15a = checkHigh[30];                    
                    in8a = checkHigh[23]; in16a = checkHigh[31];
                    break;
            }
            //GC.Collect();
            //GC.WaitForPendingFinalizers();
            //GC.Collect();
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            //GC.Collect();
            //GC.WaitForPendingFinalizers();
            //GC.Collect();

            short nIndex = 0;
            uint uDataHigh = 0;
            uint uDataLow = 0;
            uint uFlagHigh = 0;
            uint uFlagLow = 0;
            int nBoardNo = 0;
            int nModulePos = 0;
            uint uModuleID = 0;
            bool[] checkHigh = new bool[32];
            bool[] checkLow = new bool[32];           

            CAXD.AxdInfoGetModule(0, ref nBoardNo, ref nModulePos, ref uModuleID);

            switch ((AXT_MODULE)uModuleID)
            {
                case AXT_MODULE.AXT_SIO_DI32:
                case AXT_MODULE.AXT_SIO_RDI32:
                case AXT_MODULE.AXT_SIO_RSIMPLEIOMLII:
                case AXT_MODULE.AXT_SIO_RDO16AMLII:
                case AXT_MODULE.AXT_SIO_RDO16BMLII:
                case AXT_MODULE.AXT_SIO_DI32_P:
                case AXT_MODULE.AXT_SIO_RDI32RTEX:
                    //++
                    // Read inputting signal in WORD
                    CAXD.AxdiReadInportWord(0, 0, ref uDataHigh);
                    CAXD.AxdiReadInportWord(0, 1, ref uDataLow);

                    for (nIndex = 0; nIndex < 16; nIndex++)
                    {
                        // Verify the last bit value of data read
                        uFlagHigh = uDataHigh & 0x0001;
                        uFlagLow = uDataLow & 0x0001;

                        // Shift rightward by bit by bit
                        uDataHigh = uDataHigh >> 1;
                        uDataLow = uDataLow >> 1;

                        // Updat bit value in control
                        if (uFlagHigh == 1)
                            checkHigh[nIndex] = true;
                        else
                            checkHigh[nIndex] = false;

                        if (uFlagLow == 1)
                            checkLow[nIndex] = true;
                        else
                            checkLow[nIndex] = false;
                    }
                    out1 = checkLow[00]; out9  = checkLow[08]; 
                    out2 = checkLow[01]; out10 = checkLow[09]; 
                    out3 = checkLow[02]; out11 = checkLow[10]; 
                    out4 = checkLow[03]; out12 = checkLow[11];
                    out5 = checkLow[04]; out13 = checkLow[12]; 
                    out6 = checkLow[05]; out14 = checkLow[13]; 
                    out7 = checkLow[06]; out15 = checkLow[14]; 
                    out8 = checkLow[07]; out16 = checkLow[15]; 

                    in1 = checkHigh[00]; in9  = checkHigh[08]; 
                    in2 = checkHigh[01]; in10 = checkHigh[09]; 
                    in3 = checkHigh[02]; in11 = checkHigh[10];
                    in4 = checkHigh[03]; in12 = checkHigh[11]; 
                    in5 = checkHigh[04]; in13 = checkHigh[12];
                    in6 = checkHigh[05]; in14 = checkHigh[13];
                    in7 = checkHigh[06]; in15 = checkHigh[14];
                    in8 = checkHigh[07]; in16 = checkHigh[15]; 
                    break;

                case AXT_MODULE.AXT_SIO_DB32P:
                case AXT_MODULE.AXT_SIO_DB32T:
                case AXT_MODULE.AXT_SIO_RDB32T:
                case AXT_MODULE.AXT_SIO_RDB32RTEX:
                case AXT_MODULE.AXT_SIO_RDB96MLII:
                case AXT_MODULE.AXT_SIO_RDB128MLII:
                    //++
                    // Read inputting signal in WORD
                    CAXD.AxdiReadInportWord(0, 0, ref uDataHigh);

                    for (nIndex = 0; nIndex < 16; nIndex++)
                    {
                        // Verify the last bit value of data read
                        uFlagHigh = uDataHigh & 0x0001;

                        // Shift rightward by bit by bit
                        uDataHigh = uDataHigh >> 1;

                        // Updat bit value in control
                        if (uFlagHigh == 1)
                            checkHigh[nIndex] = true;
                        else
                            checkHigh[nIndex] = false;
                    }
                    in1 = checkHigh[00]; in9 = checkHigh[08]; 
                    in2 = checkHigh[01]; in10 = checkHigh[09];
                    in3 = checkHigh[02]; in11 = checkHigh[10];
                    in4 = checkHigh[03]; in12 = checkHigh[11]; 
                    in5 = checkHigh[04]; in13 = checkHigh[12];
                    in6 = checkHigh[05]; in14 = checkHigh[13];
                    in7 = checkHigh[06]; in15 = checkHigh[14];
                    in8 = checkHigh[07]; in16 = checkHigh[15];
                    break;
            }
            //GC.Collect();
            //GC.WaitForPendingFinalizers();
            //GC.Collect();
        }
        private bool OpenDevice()
        {
            //++
            // Initialize library 
            if (CAXL.AxlOpen(7) == (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
            {
                uint uStatus = 0;

                if (CAXD.AxdInfoIsDIOModule(ref uStatus) == (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                {
                    if ((AXT_EXISTENCE)uStatus == AXT_EXISTENCE.STATUS_EXIST)
                    {
                        int nModuleCount = 0;

                        if (CAXD.AxdInfoGetModuleCount(ref nModuleCount) == (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                        {
                            short i = 0;
                            int nBoardNo = 0;
                            int nModulePos = 0;
                            uint uModuleID = 0;
                            string strData = "";

                            for (i = 0; i < nModuleCount; i++)
                            {
                                if (CAXD.AxdInfoGetModule(i, ref nBoardNo, ref nModulePos, ref uModuleID) == (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                                {
                                    switch ((AXT_MODULE)uModuleID)
                                    {
                                        case AXT_MODULE.AXT_SIO_RDI32MLIII: strData = String.Format("[{0:D2}:{1:D2}] SIO-DI32", nBoardNo, i); break;
                                        case AXT_MODULE.AXT_SIO_RDI32MSMLIII: strData = String.Format("[{0:D2}:{1:D2}] SIO-DO32P", nBoardNo, i); break;
                                        case AXT_MODULE.AXT_SIO_RDI32PMLIII: strData = String.Format("[{0:D2}:{1:D2}] SIO-DB32P", nBoardNo, i); break;
                                        case AXT_MODULE.AXT_SIO_RDI32RTEX: strData = String.Format("[{0:D2}:{1:D2}] SIO-DO32T", nBoardNo, i); break;
                                        case AXT_MODULE.AXT_SIO_DI32_P: strData = String.Format("[{0:D2}:{1:D2}] SIO-DB32T", nBoardNo, i); break;
                                        case AXT_MODULE.AXT_SIO_RDI32: strData = String.Format("[{0:D2}:{1:D2}] SIO_RDI32", nBoardNo, i); break;
                                        case AXT_MODULE.AXT_SIO_DI32: strData = String.Format("[{0:D2}:{1:D2}] SIO_RDO32", nBoardNo, i); break;
                                        case AXT_MODULE.AXT_SIO_RDO32MLIII: strData = String.Format("[{0:D2}:{1:D2}] SIO-RDB128MLII", nBoardNo, i); break;
                                        case AXT_MODULE.AXT_SIO_RDO32AMSMLIII: strData = String.Format("[{0:D2}:{1:D2}] SIO-RSIMPLEIOMLII", nBoardNo, i); break;
                                        case AXT_MODULE.AXT_SIO_RDO32PMLIII: strData = String.Format("[{0:D2}:{1:D2}] SIO-RDO16AMLII", nBoardNo, i); break;
                                        case AXT_MODULE.AXT_SIO_RDO16AMLII: strData = String.Format("[{0:D2}:{1:D2}] SIO-RDO16BMLII", nBoardNo, i); break;
                                        case AXT_MODULE.AXT_SIO_RDO16BMLII: strData = String.Format("[{0:D2}:{1:D2}] SIO-RDB96MLII", nBoardNo, i); break;
                                        case AXT_MODULE.AXT_SIO_RDO32RTEX: strData = String.Format("[{0:D2}:{1:D2}] SIO-RDO32RTEX", nBoardNo, i); break;
                                        case AXT_MODULE.AXT_SIO_DO32T_P: strData = String.Format("[{0:D2}:{1:D2}] SIO-RDI32RTEX", nBoardNo, i); break;
                                        case AXT_MODULE.AXT_SIO_RDO32: strData = String.Format("[{0:D2}:{1:D2}] SIO-RDB32RTEX", nBoardNo, i); break;
                                        case AXT_MODULE.AXT_SIO_DO32P: strData = String.Format("[{0:D2}:{1:D2}] SIO-DI32_P", nBoardNo, i); break;
                                        case AXT_MODULE.AXT_SIO_DO32T: strData = String.Format("[{0:D2}:{1:D2}] SIO-DO32T_P", nBoardNo, i); break;
                                        case AXT_MODULE.AXT_SIO_RDB32MLIII: strData = String.Format("[{0:D2}:{1:D2}] SIO-RDB32T", nBoardNo, i); break;
                                        case AXT_MODULE.AXT_SIO_RDB32PMLIII: strData = String.Format("[{0:D2}:{1:D2}] SIO-DI32", nBoardNo, i); break;
                                        case AXT_MODULE.AXT_SIO_RDB128MLIIIAI: strData = String.Format("[{0:D2}:{1:D2}] SIO-DO32P", nBoardNo, i); break;
                                        case AXT_MODULE.AXT_SIO_RDB96MLII: strData = String.Format("[{0:D2}:{1:D2}] SIO-DB32P", nBoardNo, i); break;
                                        case AXT_MODULE.AXT_SIO_RDB32RTEX: strData = String.Format("[{0:D2}:{1:D2}] SIO-DO32T", nBoardNo, i); break;
                                        case AXT_MODULE.AXT_SIO_RDB128MLII: strData = String.Format("[{0:D2}:{1:D2}] SIO-DB32T", nBoardNo, i); break;
                                        case AXT_MODULE.AXT_SIO_DB32P: strData = String.Format("[{0:D2}:{1:D2}] SIO_RDI32", nBoardNo, i); break;
                                        case AXT_MODULE.AXT_SIO_RDB32T: strData = String.Format("[{0:D2}:{1:D2}] SIO_RDO32", nBoardNo, i); break;
                                        case AXT_MODULE.AXT_SIO_DB32T: strData = String.Format("[{0:D2}:{1:D2}] SIO-RDB128MLII", nBoardNo, i); break;
                                        case AXT_MODULE.AXT_SIO_UNDEFINEMLIII: strData = String.Format("[{0:D2}:{1:D2}] SIO-RSIMPLEIOMLII", nBoardNo, i); break;
                                        case AXT_MODULE.AXT_SIO_RSIMPLEIOMLII: strData = String.Format("[{0:D2}:{1:D2}] SIO-RDO16AMLII", nBoardNo, i); break;
                                    }

                                    listIOModule.Add(strData);
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Module not exist.");
                        return false;
                    }
                }
            }
            else
            {
                
            }

            return true;
        }
        private bool SelectModule1()
        {
            int nModuleCount = 0;

            bool[] checkHigh = new bool[32];
            bool[] checkLow = new bool[32];

            CAXD.AxdInfoGetModuleCount(ref nModuleCount);

            if (nModuleCount > 0)
            {
                int nBoardNo = 0;
                int nModulePos = 0;
                uint uModuleID = 0;
                short nIndex = 0;
                uint uDataHigh = 0;
                uint uDataLow = 0;
                uint uFlagHigh = 0;
                uint uFlagLow = 0;
                uint uUse = 0;

                CAXD.AxdInfoGetModule(0, ref nBoardNo, ref nModulePos, ref uModuleID);

                switch ((AXT_MODULE)uModuleID)
                {
                    case AXT_MODULE.AXT_SIO_DI32:
                    case AXT_MODULE.AXT_SIO_RDI32:
                    case AXT_MODULE.AXT_SIO_DI32_P:
                    case AXT_MODULE.AXT_SIO_RDI32RTEX:
                        
                        if (((AXT_MODULE)uModuleID) == AXT_MODULE.AXT_SIO_RDI32)
                        {
                            isInterrupt = false;                           
                            isRiging = false;
                            isFalling = false;
                        }
                        else
                        {
                            isInterrupt = true;                            
                            isRiging = true;
                            isFalling = true;

                            CAXD.AxdiInterruptGetModuleEnable(0, ref uUse);
                            if (uUse == (uint)AXT_USE.ENABLE)
                            {
                                isInterrupt = true;
                                SelectMessage();
                            }
                            else
                                isInterrupt = false;

                            CAXD.AxdiInterruptEdgeGetWord(0, 0, (uint)AXT_DIO_EDGE.UP_EDGE, ref uDataHigh);
                            CAXD.AxdiInterruptEdgeGetWord(0, 1, (uint)AXT_DIO_EDGE.UP_EDGE, ref uDataLow);
                            if (uDataHigh == 0xFFFF && uDataLow == 0xFFFF)
                                isRiging = true;
                            else
                                isRiging = false;

                            CAXD.AxdiInterruptEdgeGetWord(0, 0, (uint)AXT_DIO_EDGE.DOWN_EDGE, ref uDataHigh);
                            CAXD.AxdiInterruptEdgeGetWord(0, 1, (uint)AXT_DIO_EDGE.DOWN_EDGE, ref uDataLow);
                            if (uDataHigh == 0xFFFF && uDataLow == 0xFFFF)
                                isFalling = true;
                            else
                                isFalling = false;
                        }                       
                        break;

                    case AXT_MODULE.AXT_SIO_RDO32MLIII:
                    case AXT_MODULE.AXT_SIO_RDO32AMSMLIII:
                    case AXT_MODULE.AXT_SIO_RDO32PMLIII:
                    case AXT_MODULE.AXT_SIO_RDO32RTEX:
                    case AXT_MODULE.AXT_SIO_DO32T_P:
                    case AXT_MODULE.AXT_SIO_RDO32:
                    case AXT_MODULE.AXT_SIO_DO32P:
                    case AXT_MODULE.AXT_SIO_DO32T:
                        isInterrupt = false;                        
                        isRiging = false;
                        isFalling = false;
                        //++
                        // Read outputting signal in WORD
                        CAXD.AxdoReadOutportWord(0, 0, ref uDataHigh);
                        CAXD.AxdoReadOutportWord(0, 1, ref uDataLow);

                        for (nIndex = 0; nIndex < 16; nIndex++)
                        {
                            // Verify the last bit value of data read
                            uFlagHigh = uDataHigh & 0x0001;
                            uFlagLow = uDataLow & 0x0001;

                            // Shift rightward by bit by bit
                            uDataHigh = uDataHigh >> 1;
                            uDataLow = uDataLow >> 1;

                            // Update bit value in control
                            if (uFlagHigh == 1)
                                checkHigh[nIndex] = true;
                            else
                                checkHigh[nIndex] = false;

                            if (uFlagLow == 1)
                                checkLow[nIndex] = true;
                            else
                                checkLow[nIndex] = false;                                       
                        }
                        out1 = checkLow[00]; out9 = checkLow[08];  /*out1a = checkHigh[16]; out9a = checkHigh[24];*/
                        out2 = checkLow[01]; out10 = checkLow[09]; /*out2a = checkHigh[17]; out10a = checkHigh[25];*/
                        out3 = checkLow[02]; out11 = checkLow[10]; /*out3a = checkHigh[18]; out11a = checkHigh[26];*/
                        out4 = checkLow[03]; out12 = checkLow[11]; /*out4a = checkHigh[19]; out12a = checkHigh[27];*/
                        out5 = checkLow[04]; out13 = checkLow[12]; /*out5a = checkHigh[20]; out13a = checkHigh[28];*/
                        out6 = checkLow[05]; out14 = checkLow[13]; /* out6a = checkHigh[21]; out14a = checkHigh[29];*/
                        out7 = checkLow[06]; out15 = checkLow[14]; /*out7a = checkHigh[22]; out15a = checkHigh[30];*/
                        out8 = checkLow[07]; out16 = checkLow[15]; /*out8a = checkHigh[23]; out16a = checkHigh[31];*/

                        in1 = checkHigh[00]; in9 = checkHigh[08]; /*in1a = checkLow[16]; in9a = checkLow[24];*/
                        in2 = checkHigh[01]; in10 = checkHigh[09]; /*in2a = checkLow[17]; in10a = checkLow[25];*/
                        in3 = checkHigh[02]; in11 = checkHigh[10];/* in3a = checkLow[18]; in11a = checkLow[26];*/
                        in4 = checkHigh[03]; in12 = checkHigh[11]; /*in4a = checkLow[19]; in12a = checkLow[27];*/
                        in5 = checkHigh[04]; in13 = checkHigh[12];/* in5a = checkLow[20]; in13a = checkLow[28];*/
                        in6 = checkHigh[05]; in14 = checkHigh[13];/* in6a = checkLow[21]; in14a = checkLow[29];*/
                        in7 = checkHigh[06]; in15 = checkHigh[14];/* in7a = checkLow[22]; in15a = checkLow[30];*/
                        in8 = checkHigh[07]; in16 = checkHigh[15]; /*in8a = checkLow[23]; in16a = checkLow[31];*/
                        break;

                    case AXT_MODULE.AXT_SIO_DB32P:
                    case AXT_MODULE.AXT_SIO_RDB32T:
                    case AXT_MODULE.AXT_SIO_DB32T:
                    case AXT_MODULE.AXT_SIO_UNDEFINEMLIII:
                        isInterrupt = true;
                        isRiging = true;
                        isFalling = true;
                        CAXD.AxdiInterruptGetModuleEnable(0, ref uUse);
                        if (uUse == (uint)AXT_USE.ENABLE)
                        {
                            isInterrupt = true;
                            SelectMessage();
                        }
                        else
                            isInterrupt = false;
                            CAXD.AxdiInterruptEdgeGetWord(0, 0, (uint)AXT_DIO_EDGE.UP_EDGE, ref uDataHigh);
                        if (uDataHigh == 0xFFFF)
                            isRiging = true;
                        else
                            isRiging = false;

                                    CAXD.AxdiInterruptEdgeGetWord(0, 0, (uint)AXT_DIO_EDGE.DOWN_EDGE, ref uDataHigh);
                        if (uDataHigh == 0xFFFF)
                            isFalling = true;
                        else
                            isFalling = false;

                        //++
                        // Read outputting signal in WORD
                        CAXD.AxdoReadOutportWord(0, 0, ref uDataLow);

                        for (nIndex = 0; nIndex < 16; nIndex++)
                        {
                            // Verify the last bit value of data read
                            uFlagLow = uDataLow & 0x0001;

                            // Shift rightward by bit by bit
                            uDataLow = uDataLow >> 1;

                            // Update bit value in control
                            if (uFlagLow == 1)
                                checkLow[nIndex] = true;
                            else
                                checkLow[nIndex] = false;                                   
                        }
                        out1 = checkLow[00]; out9 = checkLow[08];  /*out1a = checkHigh[16]; out9a = checkHigh[24];*/
                        out2 = checkLow[01]; out10 = checkLow[09]; /*out2a = checkHigh[17]; out10a = checkHigh[25];*/
                        out3 = checkLow[02]; out11 = checkLow[10]; /*out3a = checkHigh[18]; out11a = checkHigh[26];*/
                        out4 = checkLow[03]; out12 = checkLow[11]; /*out4a = checkHigh[19]; out12a = checkHigh[27];*/
                        out5 = checkLow[04]; out13 = checkLow[12]; /*out5a = checkHigh[20]; out13a = checkHigh[28];*/
                        out6 = checkLow[05]; out14 = checkLow[13]; /* out6a = checkHigh[21]; out14a = checkHigh[29];*/
                        out7 = checkLow[06]; out15 = checkLow[14]; /*out7a = checkHigh[22]; out15a = checkHigh[30];*/
                        out8 = checkLow[07]; out16 = checkLow[15]; /*out8a = checkHigh[23]; out16a = checkHigh[31];*/
                        break;
                }                
            }
            return true;
        }
        private bool SelectModule2()
        {
            int nModuleCount = 0;

            bool[] checkHigh = new bool[32];
            bool[] checkLow = new bool[32];

            CAXD.AxdInfoGetModuleCount(ref nModuleCount);

            if (nModuleCount > 0)
            {
                int nBoardNo = 0;
                int nModulePos = 0;
                uint uModuleID = 0;
                short nIndex = 0;
                uint uDataHigh = 0;
                uint uDataLow = 0;
                uint uFlagHigh = 0;
                uint uFlagLow = 0;
                uint uUse = 0;

                CAXD.AxdInfoGetModule(1, ref nBoardNo, ref nModulePos, ref uModuleID);

                switch ((AXT_MODULE)uModuleID)
                {
                    case AXT_MODULE.AXT_SIO_DI32:
                    case AXT_MODULE.AXT_SIO_RDI32:
                    case AXT_MODULE.AXT_SIO_DI32_P:
                    case AXT_MODULE.AXT_SIO_RDI32RTEX:

                        if (((AXT_MODULE)uModuleID) == AXT_MODULE.AXT_SIO_RDI32)
                        {
                            isInterrupt = false;
                            isRiging = false;
                            isFalling = false;
                        }
                        else
                        {
                            isInterrupt = true;
                            isRiging = true;
                            isFalling = true;

                            CAXD.AxdiInterruptGetModuleEnable(1, ref uUse);
                            if (uUse == (uint)AXT_USE.ENABLE)
                            {
                                isInterrupt = true;
                                SelectMessage();
                            }
                            else
                                isInterrupt = false;

                            CAXD.AxdiInterruptEdgeGetWord(1, 0, (uint)AXT_DIO_EDGE.UP_EDGE, ref uDataHigh);
                            CAXD.AxdiInterruptEdgeGetWord(1, 1, (uint)AXT_DIO_EDGE.UP_EDGE, ref uDataLow);
                            if (uDataHigh == 0xFFFF && uDataLow == 0xFFFF)
                                isRiging = true;
                            else
                                isRiging = false;

                            CAXD.AxdiInterruptEdgeGetWord(1, 0, (uint)AXT_DIO_EDGE.DOWN_EDGE, ref uDataHigh);
                            CAXD.AxdiInterruptEdgeGetWord(1, 1, (uint)AXT_DIO_EDGE.DOWN_EDGE, ref uDataLow);
                            if (uDataHigh == 0xFFFF && uDataLow == 0xFFFF)
                                isFalling = true;
                            else
                                isFalling = false;
                        }
                        break;

                    case AXT_MODULE.AXT_SIO_RDO32MLIII:
                    case AXT_MODULE.AXT_SIO_RDO32AMSMLIII:
                    case AXT_MODULE.AXT_SIO_RDO32PMLIII:
                    case AXT_MODULE.AXT_SIO_RDO32RTEX:
                    case AXT_MODULE.AXT_SIO_DO32T_P:
                    case AXT_MODULE.AXT_SIO_RDO32:
                    case AXT_MODULE.AXT_SIO_DO32P:
                    case AXT_MODULE.AXT_SIO_DO32T:
                        isInterrupt = false;
                        isRiging = false;
                        isFalling = false;
                        //++
                        // Read outputting signal in WORD
                        CAXD.AxdoReadOutportWord(1, 0, ref uDataHigh);
                        CAXD.AxdoReadOutportWord(1, 1, ref uDataLow);

                        for (nIndex = 16; nIndex < 32; nIndex++)
                        {
                            // Verify the last bit value of data read
                            uFlagHigh = uDataHigh & 0x0001;
                            uFlagLow = uDataLow & 0x0001;

                            // Shift rightward by bit by bit
                            uDataHigh = uDataHigh >> 1;
                            uDataLow = uDataLow >> 1;

                            // Update bit value in control
                            if (uFlagHigh == 1)
                                checkHigh[nIndex] = true;
                            else
                                checkHigh[nIndex] = false;

                            if (uFlagLow == 1)
                                checkLow[nIndex] = true;
                            else
                                checkLow[nIndex] = false;
                        }
                        /* out1 = checkHigh[00]; out9 = checkHigh[08]*/
                        out1a = checkLow[16]; out9a = checkLow[24];
                        /* out2 = checkHigh[01]; out10 = checkHigh[09];*/
                        out2a = checkLow[17]; out10a = checkLow[25];
                        /*out3 = checkHigh[02]; out11 = checkHigh[10]; */
                        out3a = checkLow[18]; out11a = checkLow[26];
                        /*out4 = checkHigh[03]; out12 = checkHigh[11];*/
                        out4a = checkLow[19]; out12a = checkLow[27];
                        /*out5 = checkHigh[04]; out13 = checkHigh[12];*/
                        out5a = checkLow[20]; out13a = checkLow[28];
                        /*out6 = checkHigh[05]; out14 = checkHigh[13];*/
                        out6a = checkLow[21]; out14a = checkLow[29];
                        /*out7 = checkHigh[06]; out15 = checkHigh[14]*/
                        out7a = checkLow[22]; out15a = checkLow[30];
                        /*out8 = checkHigh[07]; out16 = checkHigh[15];*/
                        out8a = checkLow[23]; out16a = checkLow[31];

                        /*in1 = checkLow[00]; in9 = checkLow[08];*/
                        in1a = checkHigh[16]; in9a = checkHigh[24];
                        /*in2 = checkLow[01]; in10 = checkLow[09];*/
                        in2a = checkHigh[17]; in10a = checkHigh[25];
                        /* in3 = checkLow[02]; in11 = checkLow[10];*/
                        in3a = checkHigh[18]; in11a = checkHigh[26];
                        /*in4 = checkLow[03]; in12 = checkLow[11];*/
                        in4a = checkHigh[19]; in12a = checkHigh[27];
                        /*in5 = checkLow[04]; in13 = checkLow[12];*/
                        in5a = checkHigh[20]; in13a = checkHigh[28];
                        /*in6 = checkLow[05]; in14 = checkLow[13];*/
                        in6a = checkHigh[21]; in14a = checkHigh[29];
                        /*in7 = checkLow[06]; in15 = checkLow[14];*/
                        in7a = checkHigh[22]; in15a = checkHigh[30];
                        /*in8 = checkLow[07]; in16 = checkLow[15];*/
                        in8a = checkHigh[23]; in16a = checkHigh[31];
                        break;

                    case AXT_MODULE.AXT_SIO_DB32P:
                    case AXT_MODULE.AXT_SIO_RDB32T:
                    case AXT_MODULE.AXT_SIO_DB32T:
                    case AXT_MODULE.AXT_SIO_UNDEFINEMLIII:
                        CAXD.AxdiInterruptGetModuleEnable(1, ref uUse);
                        if (uUse == (uint)AXT_USE.ENABLE)
                        {
                            isInterrupt = true;
                            SelectMessage();
                        }
                        else
                            isInterrupt = false;
                        CAXD.AxdiInterruptEdgeGetWord(1, 0, (uint)AXT_DIO_EDGE.UP_EDGE, ref uDataHigh);
                        if (uDataHigh == 0xFFFF)
                            isRiging = true;
                        else
                            isRiging = false;

                        CAXD.AxdiInterruptEdgeGetWord(1, 0, (uint)AXT_DIO_EDGE.DOWN_EDGE, ref uDataHigh);
                        if (uDataHigh == 0xFFFF)
                            isFalling = true;
                        else
                            isFalling = false;

                        //++
                        // Read outputting signal in WORD
                        CAXD.AxdoReadOutportWord(1, 0, ref uDataLow);

                        for (nIndex = 16; nIndex < 32; nIndex++)
                        {
                            // Verify the last bit value of data read
                            uFlagLow = uDataLow & 0x0001;

                            // Shift rightward by bit by bit
                            uDataLow = uDataLow >> 1;

                            // Update bit value in control
                            if (uFlagLow == 1)
                                checkLow[nIndex] = true;
                            else
                                checkLow[nIndex] = false;
                        }
                        /* out1 = checkHigh[00]; out9 = checkHigh[08]*/
                        out1a = checkLow[16]; out9a = checkLow[24];
                        /* out2 = checkHigh[01]; out10 = checkHigh[09];*/
                        out2a = checkLow[17]; out10a = checkLow[25];
                        /*out3 = checkHigh[02]; out11 = checkHigh[10]; */
                        out3a = checkLow[18]; out11a = checkLow[26];
                        /*out4 = checkHigh[03]; out12 = checkHigh[11];*/
                        out4a = checkLow[19]; out12a = checkLow[27];
                        /*out5 = checkHigh[04]; out13 = checkHigh[12];*/
                        out5a = checkLow[20]; out13a = checkLow[28];
                        /*out6 = checkHigh[05]; out14 = checkHigh[13];*/
                        out6a = checkLow[21]; out14a = checkLow[29];
                        /*out7 = checkHigh[06]; out15 = checkHigh[14]*/
                        out7a = checkLow[22]; out15a = checkLow[30];
                        /*out8 = checkHigh[07]; out16 = checkHigh[15];*/
                        out8a = checkLow[23]; out16a = checkLow[31];
                        break;
                }               
            }
            return true;
        }
        public enum AXT_INTERRUPT_CLASS : uint
        {
            KIND_MESSAGE,
            KIND_CALLBACK,
            KIND_EVENT
        }
        private static bool InterruptProc(AXT_INTERRUPT_CLASS uClass, int nModuleNo, uint uFlag)
        {
            int i = 0;
            int j = 0;
            uint uValue = 0;
            string strClass = "";
            string strInt = "";

            switch (uClass)
            {
                case AXT_INTERRUPT_CLASS.KIND_MESSAGE:
                    strClass = "Message";
                    break;

                case AXT_INTERRUPT_CLASS.KIND_CALLBACK:
                    strClass = "Callback";
                    break;

                case AXT_INTERRUPT_CLASS.KIND_EVENT:
                    strClass = "Event";
                    break;
            }

            for (i = 0; i < 4; i++)
            {
                for (j = 0; j < 8; j++)
                {
                    if ((((uFlag >> (i * 8)) >> j) & 0x01) == 0x01)
                    {
                        CAXD.AxdiReadInportBit(nModuleNo, ((i * 8) + j), ref uValue);

                        if (uValue == 0x01)
                            strInt = String.Format("{0:s} : Rising Int Set Bit {1:X2}", strClass, (i * 8) + j);
                        else
                            strInt = String.Format("{0:s} : Falling Int Set Bit {1:X2}", strClass, (i * 8) + j);


                        //if (textInterrupt.TextLength == 0)
                        //    textInterrupt.Text += strInt;
                        //else
                        //    textInterrupt.Text += "\r\n" + strInt;


                        //textInterrupt.SelectionStart = textInterrupt.TextLength;
                        //textInterrupt.ScrollToCaret();

                    }
                }
            }
            return true;
        }
        protected override void WndProc(Message m)
        {
            switch (m.Msg)
            {
                case (int)AXT_EVENT.WM_AXL_INTERRUPT:
                    InterruptProc(AXT_INTERRUPT_CLASS.KIND_MESSAGE, (int)m.WParam, (uint)m.LParam);
                    break;
            }

            base.WndProc(m);
        }
        private bool SelectMessage()
        {
            uint pEvent = 0;

            if (EventThread != null)
            {
                bThread = false;
                SetEvent(hInterrupt);
                EventThread.Abort();
                EventThread = null;
            }
            CAXD.AxdiInterruptSetModule(0, WindowHandle, (uint)AXT_EVENT.WM_AXL_INTERRUPT, null, ref pEvent);

            return true;
        }
        public static void InterruptCallback(int nModuleNo, uint uFlag)
        {
            InterruptProc(AXT_INTERRUPT_CLASS.KIND_CALLBACK, nModuleNo, uFlag);            
        }
        private void ThreadProc()
        {
            while (bThread)
            {
                if (WaitForSingleObject(hInterrupt, INFINITE) == WAIT_OBJECT_0)
                {
                    int nModuleNo = 0;
                    uint uFlag = 0;
                    CAXD.AxdiInterruptRead(ref nModuleNo, ref uFlag);
                    InterruptProc(AXT_INTERRUPT_CLASS.KIND_EVENT, nModuleNo, uFlag);
                }
            }
            EventThread = null;
        }
        private bool SelectCallback()
        {
            uint pEvent = 0;

            if (EventThread != null)
            {
                EventThread.Abort();
                EventThread = null;
                bThread = false;
            }

            CAXD.AxdiInterruptSetModule(0, (IntPtr)null, 0, Callbackfunction, ref pEvent);

            return true;
        }
        private bool SelectEvent()
        {
            CAXD.AxdiInterruptSetModule(0, (IntPtr)null, 0, null, ref hInterrupt);

            if (EventThread != null)
            {
                EventThread.Abort();
                EventThread = null;
                bThread = false;
            }

            if (!bThread)
            {
                bThread = true;
                EventThread = new Thread(new ThreadStart(this.ThreadProc));
                EventThread.Start();
            }

            return true;
        }
        private bool SelectHighIndex(int nIndex, uint uValue)
        {
            int nModuleCount = 0;

            CAXD.AxdInfoGetModuleCount(ref nModuleCount);

            if (nModuleCount > 0)
            {
                int nBoardNo = 0;
                int nModulePos = 0;
                uint uModuleID = 0;

                CAXD.AxdInfoGetModule(0, ref nBoardNo, ref nModulePos, ref uModuleID);

                switch ((AXT_MODULE)uModuleID)
                {
                    case AXT_MODULE.AXT_SIO_DO32P:
                    case AXT_MODULE.AXT_SIO_DO32T:
                    case AXT_MODULE.AXT_SIO_RDO32:
                        CAXD.AxdoWriteOutportBit(0, nIndex, uValue);
                        break;

                    default:
                        return false;
                }
            }

            return true;
        }
        private bool SelectLowIndex(int board, int nIndex, uint uValue)
        {
            int nModuleCount = 0;

            CAXD.AxdInfoGetModuleCount(ref nModuleCount);

            if (nModuleCount > 0)
            {
                int nBoardNo = 0;
                int nModulePos = 0;
                uint uModuleID = 0;

                CAXD.AxdInfoGetModule(board, ref nBoardNo, ref nModulePos, ref uModuleID);

                switch ((AXT_MODULE)uModuleID)
                {
                    case AXT_MODULE.AXT_SIO_DO32P:
                    case AXT_MODULE.AXT_SIO_DO32T:
                    case AXT_MODULE.AXT_SIO_RDO32:
                        CAXD.AxdoWriteOutportBit(board, nIndex + 16, uValue);
                        break;
                    case AXT_MODULE.AXT_SIO_DB32P:
                    case AXT_MODULE.AXT_SIO_DB32T:
                    case AXT_MODULE.AXT_SIO_RDB128MLII:
                        CAXD.AxdoWriteOutportBit(board, nIndex, uValue);
                        break;

                    default:
                        return false;
                }
            }

            return true;
        }
        private void CloseWindow()
        {
            if (EventThread != null)
            {
                bThread = false;
                SetEvent(hInterrupt);
                EventThread.Abort();
                EventThread = null;

            }

            CAXL.AxlClose();
        }
    }
}
