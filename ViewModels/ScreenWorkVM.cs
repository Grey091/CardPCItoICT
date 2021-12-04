using Microsoft.Win32;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using SYNOPEX_ICT.Models;
using SYNOPEX_ICT.Stored;
using SYNOPEX_ICT.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace SYNOPEX_ICT.ViewModels
{
    [Serializable]
    class ScreenWorkVM : BaseVM
    {
        #region INITfeild
        DigitalIOVM digitalIOVM;
        DigitalIO digitalIOWD;
        ErrorsOnBoardFPCB errorsOnBoardFPCB;
        string cmdShort = "short (1,10,3,10,50000)\r\n";
        string cmdOpen = "open(50,20,3,500,9000)\r\n";

        bool flagPress = false;
        bool flagPool = false;
        bool flagDone = false;

        bool check1 = false;
        bool check2 = false;

        public int m_lAxisCounts = 0;                               // Khai báo và khởi tạo số lượng trục có thể điều khiển
        private int m_lAxisNo = 0;                                  // Khai báo và khởi tạo số trục được điều khiển
        public uint m_uModuleID = 0;                                // Khai báo và khởi tạo mô-đun I / O của trục được điều khiển
        public uint m_lMoveMultiAxesCount = 0;                      // Khai báo và khởi tạo số lượng trục được dẫn động nhiều trục   
        public int[] m_lMoveMultiAxes = { -1, -1, -1, -1, -1, -1 };         // Khai báo và khởi tạo số mảng trục được điều khiển theo nhiều trục...
        public int m_lBoardNo = 0, m_lModulePos = 0;
        public uint m_duOldResult = 0;
        String m_strResult;

        public double[] m_dOldCmdPos = { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };      // tmDisplay Trước đó để sử dụng trong Command Position	
        public double[] m_dOldActPos = { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };      // tmDisplay Trước đó để sử dụng trong Actual Position
        public double[] m_dOldCmdVel = { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 };      // tmDisplay Trước đó để sử dụng trong Command Velocity

        bool isCheckServoOn = true;
        bool chkMoveBlock = true;
        int counter = 0;

        List<string> listMotionModule;

        public static SerialPort COM_Mesuament;
        public static SerialPort COM_Marking;
        #endregion

        #region Timer&Socket
        [NonSerialized]
        DispatcherTimer TimerDisplay1;
        [NonSerialized]
        DispatcherTimer TimerHome1;
        [NonSerialized]
        DispatcherTimer TimerSystemZ;
        [NonSerialized]
        DispatcherTimer TimerFollowSignalFromIO;
        [NonSerialized]
        DispatcherTimer TimerGetDayWork;

        TCPServer tcpServer;
        public DNRemotingServer dnRemotingServer;
        List<string> listLog;
        public delegate void UpdateTextBoxDelegate(string textbox, string text);
        #endregion

        #region Commands
        public ICommand StopJogX { get; set; }
        public ICommand StopJogY { get; set; }
        public ICommand StopJogZ { get; set; }
        public ICommand MoveJogXPlus { get; set; }
        public ICommand MoveJogXMinus{ get;set; }
        public ICommand MoveJogYPlus { get; set; }
        public ICommand MoveJogYMinus { get; set; }
        public ICommand MoveJogZPlus { get; set; }
        public ICommand MoveJogZMinus { get; set; }
        public ICommand CloseWindowCommand { get; set; }
        public ICommand SetValueCommand { get; set; }
        public ICommand RunSystemCommand { get; set; }        
        public ICommand StopSystemCommand { get; set; }
       
        public ICommand RunMarkingCommand { get; set; }
        public ICommand SwitchMarkingCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        public ICommand HomeMoveCommand { get; set; }
        #endregion

        #region ValuesBinding 
        private bool _isButtonOriginEnable;
        public bool isButtonOriginEnable
        {
            get => _isButtonOriginEnable;
            set
            {
                _isButtonOriginEnable = value;
                OnPropertyChanged("isButtonOriginEnable");
            }
        }
        private bool _isButtonRunEnable;
        public bool isButtonRunEnable
        {
            get => _isButtonRunEnable;
            set
            {
                _isButtonRunEnable = value;
                OnPropertyChanged("isButtonRunEnable");
            }
        }
        private ObservableCollection<FPCB> _ListShow;
        public ObservableCollection<FPCB> ListShow
        {
            get => _ListShow;
            set
            {
                _ListShow = value;
                OnPropertyChanged("ListShow");
            }
        }
        private ObservableCollection<FPCB> _ListShow1;
        public ObservableCollection<FPCB> ListShow1
        {
            get => _ListShow1;
            set
            {
                _ListShow1 = value;
                OnPropertyChanged("ListShow1");
            }
        }
        private ObservableCollection<FPCB> _ListShow2;
        public ObservableCollection<FPCB> ListShow2
        {
            get => _ListShow2;
            set
            {
                _ListShow2 = value;
                OnPropertyChanged("ListShow2");
            }
        }
        private List<int> _ListDataShort;
        public List<int> ListDataShort
        {
            get => _ListDataShort;
            set
            {
                _ListDataShort = value;
                OnPropertyChanged("ListDataShort");
            }
        }
        private List<int> _ListDataOpen;
        public List<int> ListDataOpen
        {
            get => _ListDataOpen;
            set
            {
                _ListDataOpen = value;
                OnPropertyChanged("ListDataOpen");
            }
        }
        private string _status;
        public string status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged("status");
            }
        }
        private string _measuament;
        public string Measuament
        {
            get => _measuament;
            set
            {
                _measuament = value;
                OnPropertyChanged("Measuament");
            }
        }
        private double _commandPos1;
        public double commandPos1
        {
            get => _commandPos1;
            set { _commandPos1 = value; OnPropertyChanged("commandPos1"); }
        }
        private double _commandPos2;
        public double commandPos2
        {
            get => _commandPos2;
            set { _commandPos2 = value; OnPropertyChanged("commandPos2"); }
        }
        private double _commandPos3;
        public double commandPos3
        {
            get => _commandPos3;
            set { _commandPos3 = value; OnPropertyChanged("commandPos3"); }
        }
        private double _commandPos4;
        public double commandPos4
        {
            get => _commandPos4;
            set { _commandPos4 = value; OnPropertyChanged("commandPos4"); }
        }
        private double _commandPos5;
        public double commandPos5
        {
            get => _commandPos5;
            set { _commandPos5 = value; OnPropertyChanged("commandPos5"); }
        }
        private double _commandPos6;
        public double commandPos6
        {
            get => _commandPos6;
            set { _commandPos6 = value; OnPropertyChanged("commandPos6"); }
        }
        private double _feedBackPos1;
        public double feedBackPos1
        {
            get => _feedBackPos1;
            set { _feedBackPos1 = value; OnPropertyChanged("feedBackPos1"); }
        }
        private double _feedBackPos2;
        public double feedBackPos2
        {
            get => _feedBackPos2;
            set { _feedBackPos2 = value; OnPropertyChanged("feedBackPos2"); }
        }
        private double _feedBackPos3;
        public double feedBackPos3
        {
            get => _feedBackPos3;
            set { _feedBackPos3 = value; OnPropertyChanged("feedBackPos3"); }
        }
        private double _feedBackPos4;
        public double feedBackPos4
        {
            get => _feedBackPos4;
            set { _feedBackPos4 = value; OnPropertyChanged("feedBackPos4"); }
        }
        private double _feedBackPos5;
        public double feedBackPos5
        {
            get => _feedBackPos5;
            set { _feedBackPos5 = value; OnPropertyChanged("feedBackPos5"); }
        }
        private double _feedBackPos6;
        public double feedBackPos6
        {
            get => _feedBackPos6;
            set { _feedBackPos6 = value; OnPropertyChanged("feedBackPos6"); }
        }
        private double _commandVel1;
        public double commandVel1
        {
            get => _commandVel1;
            set { _commandVel1 = value; OnPropertyChanged("commandVel1"); }
        }
        private double _commandVel2;
        public double commandVel2
        {
            get => _commandVel2;
            set { _commandVel2 = value; OnPropertyChanged("commandVel2"); }
        }
        private double _commandVel3;
        public double commandVel3
        {
            get => _commandVel3;
            set { _commandVel3 = value; OnPropertyChanged("commandVel3"); }
        }
        private double _commandVel4;
        public double commandVel4
        {
            get => _commandVel4;
            set { _commandVel4 = value; OnPropertyChanged("commandVel4"); }
        }
        private double _commandVel5;
        public double commandVel5
        {
            get => _commandVel5;
            set { _commandVel5 = value; OnPropertyChanged("commandVel5"); }
        }
        private double _commandVel6;
        public double commandVel6
        {
            get => _commandVel6;
            set { _commandVel6 = value; OnPropertyChanged("commandVel6"); }
        }
        private double _targetPos1;
        public double targetPos1
        {
            get => _targetPos1;
            set { _targetPos1 = value; OnPropertyChanged("targetPos1"); }
        }
        private double _targetPos2;
        public double targetPos2
        {
            get => _targetPos2;
            set { _targetPos2 = value; OnPropertyChanged("targetPos2"); }
        }
        private double _targetPos3;
        public double targetPos3
        {
            get => _targetPos3;
            set { _targetPos3 = value; OnPropertyChanged("targetPos3"); }
        }
        private double _targetPosZO;
        public double targetPosZOpen
        {
            get => _targetPosZO;
            set { _targetPosZO = value; OnPropertyChanged("targetPosZOpen"); }
        }
        private double _targetPos4;
        public double targetPos4
        {
            get => _targetPos4;
            set { _targetPos4 = value; OnPropertyChanged("targetPos4"); }
        }
        private double _targetPos5;
        public double targetPos5
        {
            get => _targetPos5;
            set { _targetPos5 = value; OnPropertyChanged("targetPos5"); }
        }
        private double _targetPos6;
        public double targetPos6
        {
            get => _targetPos6;
            set { _targetPos6 = value; OnPropertyChanged("targetPos6"); }
        }
        private double _moveVelocity1;
        public double moveVelocity1
        {
            get => _moveVelocity1;
            set { _moveVelocity1 = value; OnPropertyChanged("moveVelocity1"); }
        }
        private double _moveVelocity2;
        public double moveVelocity2
        {
            get => _moveVelocity2;
            set { _moveVelocity2 = value; OnPropertyChanged("moveVelocity2"); }
        }
        private double _moveVelocity3;
        public double moveVelocity3
        {
            get => _moveVelocity3;
            set { _moveVelocity3 = value; OnPropertyChanged("moveVelocity3"); }
        }
        private double _moveVelocity4;
        public double moveVelocity4
        {
            get => _moveVelocity4;
            set { _moveVelocity4 = value; OnPropertyChanged("moveVelocity4"); }
        }
        private double _moveVelocity5;
        public double moveVelocity5
        {
            get => _moveVelocity5;
            set { _moveVelocity5 = value; OnPropertyChanged("moveVelocity5"); }
        }
        private double _moveVelocity6;
        public double moveVelocity6
        {
            get => _moveVelocity6;
            set { _moveVelocity6 = value; OnPropertyChanged("moveVelocity6"); }
        }
        private double _moveDecel1;
        public double moveDecel1
        {
            get => _moveDecel1;
            set { _moveDecel1 = value; OnPropertyChanged("moveDecel1"); }
        }
        private double _moveDecel2;
        public double moveDecel2
        {
            get => _moveDecel2;
            set { _moveDecel2 = value; OnPropertyChanged("moveDecel2"); }
        }
        private double _moveDecel3;
        public double moveDecel3
        {
            get => _moveDecel3;
            set { _moveDecel3 = value; OnPropertyChanged("moveDecel3"); }
        }
        private double _moveDecel4;
        public double moveDecel4
        {
            get => _moveDecel4;
            set { _moveDecel4 = value; OnPropertyChanged("moveDecel4"); }
        }
        private double _moveDecel5;
        public double moveDecel5
        {
            get => _moveDecel5;
            set { _moveDecel5 = value; OnPropertyChanged("moveDecel5"); }
        }
        private double _moveDecel6;
        public double moveDecel6
        {
            get => _moveDecel6;
            set { _moveDecel6 = value; OnPropertyChanged("moveDecel6"); }
        }
        private double _moveAccel1;
        public double moveAccel1
        {
            get => _moveAccel1;
            set { _moveAccel1 = value; OnPropertyChanged("moveAccel1"); }
        }
        private double _moveAccel2;
        public double moveAccel2
        {
            get => _moveAccel2;
            set { _moveAccel2 = value; OnPropertyChanged("moveAccel2"); }
        }
        private double _moveAccel3;
        public double moveAccel3
        {
            get => _moveAccel3;
            set { _moveAccel3 = value; OnPropertyChanged("moveAccel3"); }
        }
        private double _moveAccel4;
        public double moveAccel4
        {
            get => _moveAccel4;
            set { _moveAccel4 = value; OnPropertyChanged("moveAccel4"); }
        }
        private double _moveAccel5;
        public double moveAccel5
        {
            get => _moveAccel5;
            set { _moveAccel5 = value; OnPropertyChanged("moveAccel5"); }
        }
        private double _moveAccel6;
        public double moveAccel6
        {
            get => _moveAccel6;
            set { _moveAccel6 = value; OnPropertyChanged("moveAccel6"); }
        }
        private double _jogVelocity1;
        public double jogVelocity1
        {
            get => _jogVelocity1;
            set { _jogVelocity1 = value; OnPropertyChanged("jogVelocity1"); }
        }
        private double _jogVelocity2;
        public double jogVelocity2
        {
            get => _jogVelocity2;
            set { _jogVelocity2 = value; OnPropertyChanged("jogVelocity2"); }
        }
        private double _jogVelocity3;
        public double jogVelocity3
        {
            get => _jogVelocity3;
            set { _jogVelocity3 = value; OnPropertyChanged("jogVelocity3"); }
        }
        private double _jogVelocity4;
        public double jogVelocity4
        {
            get => _jogVelocity4;
            set { _jogVelocity4 = value; OnPropertyChanged("jogVelocity4"); }
        }
        private double _jogVelocity5;
        public double jogVelocity5
        {
            get => _jogVelocity5;
            set { _jogVelocity5 = value; OnPropertyChanged("jogVelocity5"); }
        }
        private double _jogVelocity6;
        public double jogVelocity6
        {
            get => _jogVelocity6;
            set { _jogVelocity6 = value; OnPropertyChanged("jogVelocity6"); }
        }
        private double _jogDecel1;
        public double jogDecel1
        {
            get => _jogDecel1;
            set { _jogDecel1 = value; OnPropertyChanged("jogDecel1"); }
        }
        private double _jogDecel2;
        public double jogDecel2
        {
            get => _jogDecel2;
            set { _jogDecel2 = value; OnPropertyChanged("jogDecel2"); }
        }
        private double _jogDecel3;
        public double jogDecel3
        {
            get => _jogDecel3;
            set { _jogDecel3 = value; OnPropertyChanged("jogDecel3"); }
        }
        private double _jogDecel4;
        public double jogDecel4
        {
            get => _jogDecel4;
            set { _jogDecel4 = value; OnPropertyChanged("jogDecel4"); }
        }
        private double _jogDecel5;
        public double jogDecel5
        {
            get => _jogDecel5;
            set { _jogDecel5 = value; OnPropertyChanged("jogDecel5"); }
        }
        private double _jogDecel6;
        public double jogDecel6
        {
            get => _jogDecel6;
            set { _jogDecel6 = value; OnPropertyChanged("jogDecel6"); }
        }
        private double _jogAccel1;
        public double jogAccel1
        {
            get => _jogAccel1;
            set { _jogAccel1 = value; OnPropertyChanged("jogAccel1"); }
        }
        private double _jogAccel2;
        public double jogAccel2
        {
            get => _jogAccel2;
            set { _jogAccel2 = value; OnPropertyChanged("jogAccel2"); }
        }
        private double _jogAccel3;
        public double jogAccel3
        {
            get => _jogAccel3;
            set { _jogAccel3 = value; OnPropertyChanged("jogAccel3"); }
        }
        private double _jogAccel4;
        public double jogAccel4
        {
            get => _jogAccel4;
            set { _jogAccel4 = value; OnPropertyChanged("jogAccel4"); }
        }
        private double _jogAccel5;
        public double jogAccel5
        {
            get => _jogAccel5;
            set { _jogAccel5 = value; OnPropertyChanged("jogAccel5"); }
        }
        private double _jogAccel6;
        public double jogAccel6
        {
            get => _jogAccel6;
            set { _jogAccel6 = value; OnPropertyChanged("jogAccel6"); }
        }
        private double _linePos1;
        public double linePos1
        {
            get => _linePos1;
            set { _linePos1 = value; OnPropertyChanged("linePos1"); }
        }
        private double _linePos2;
        public double linePos2
        {
            get => _linePos2;
            set { _linePos2 = value; OnPropertyChanged("linePos2"); }
        }
        private double _linePos3;
        public double linePos3
        {
            get => _linePos3;
            set { _linePos3 = value; OnPropertyChanged("linePos3"); }
        }
        private double _linePos4;
        public double linePos4
        {
            get => _linePos4;
            set { _linePos4 = value; OnPropertyChanged("linePos4"); }
        }
        private double _linePos5;
        public double linePos5
        {
            get => _linePos5;
            set { _linePos5 = value; OnPropertyChanged("linePos5"); }
        }
        private double _linePos6;
        public double linePos6
        {
            get => _linePos6;
            set { _linePos6 = value; OnPropertyChanged("linePos6"); }
        }
        private double _lineMoveVelocity;
        public double lineMoveVelocity
        {
            get => _lineMoveVelocity;
            set { _lineMoveVelocity = value; OnPropertyChanged("lineMoveVelocity"); }
        }
        private double _lineMoveAccel;
        public double lineMoveAccel
        {
            get => _lineMoveAccel;
            set { _lineMoveAccel = value; OnPropertyChanged("lineMoveAccel"); }
        }
        private double _lineMoveDecel;
        public double lineMoveDecel
        {
            get => _lineMoveDecel;
            set { _lineMoveDecel = value; OnPropertyChanged("lineMoveDecel"); }
        }
        #endregion

        #region mess
        private double _prosentovS;
        public double ProsentovS
        {
            get => _prosentovS;
            set { _prosentovS = value; OnPropertyChanged("ProsentovS"); }
        }
        private int _numNG;
        public int NumNG
        {
            get => _numNG;
            set { _numNG = value; OnPropertyChanged("NumNG"); }
        }
        private int _NumProducts;
        public int NumProducts
        {
            get => _NumProducts;
            set { _NumProducts = value; OnPropertyChanged("NumProducts"); }
        }
        private double _prosentov;
        public double Prosentov
        {
            get => _prosentov;
            set { _prosentov = value; OnPropertyChanged("Prosentov"); }
        }
        private int _numShort;
        public int NumShort
        {
            get => _numShort;
            set { _numShort = value; OnPropertyChanged("NumShort"); }
        }
        private int _numOpen;
        public int NumOpen
        {
            get => _numOpen;
            set { _numOpen = value; OnPropertyChanged("NumOpen"); }
        }

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
        private string _ShortMessMesuament;
        public string ShortMessMesuament
        {
            get => _ShortMessMesuament;
            set
            {
                _ShortMessMesuament = value; OnPropertyChanged("ShortMessMesuament");
            }
        }
        private string _OpenMessMesuament;
        public string OpenMessMesuament
        {
            get => _OpenMessMesuament;
            set
            {
                _OpenMessMesuament = value; OnPropertyChanged("OpenMessMesuament");
            }
        }
        private ObservableCollection<ErrorsOnBoardFPCB> _ListResults;
        public ObservableCollection<ErrorsOnBoardFPCB> ListResults
        {
            get => _ListResults;
            set
            {
                _ListResults = value;
                OnPropertyChanged("ListResults");
            }
        }
        #endregion
        
        public ScreenWorkVM(NavigationStore navigationStore){}
        public ScreenWorkVM()
        {

            #region LIST
            listLog = new List<string>();
            listMotionModule = new List<string>();
            ListResults = new ObservableCollection<ErrorsOnBoardFPCB>();
            ListShow1 = new ObservableCollection<FPCB>();
            ListShow2 = new ObservableCollection<FPCB>();
            errorsOnBoardFPCB = new ErrorsOnBoardFPCB();
            NumProducts = 0; NumNG = 0;

            isButtonOriginEnable = true;
            
            digitalIOWD = new DigitalIO();
            if (digitalIOWD.DataContext == null)
            {
                return;
            }
            else
            {
                digitalIOVM = digitalIOWD.DataContext as DigitalIOVM;
                digitalIOVM.out9 = true;
            }
            #endregion

            #region COM_init
            COM_Mesuament = new SerialPort("COM8", 115200, Parity.None, 8, StopBits.One);
            COM_Mesuament.ReadTimeout = 2000;
            COM_Mesuament.WriteTimeout = 2000;
            COM_Mesuament.Open();

            COM_Marking = new SerialPort("COM7", 9600, Parity.Odd, 8, StopBits.One);
            COM_Marking.ReadTimeout = 2000;
            COM_Marking.WriteTimeout = 2000;
            COM_Marking.ReadBufferSize = 100;
            COM_Marking.Open();
            #endregion

            #region TIMER
            TimerDisplay1 = new DispatcherTimer();
            TimerDisplay1.Interval = TimeSpan.FromMilliseconds(100);
            TimerDisplay1.Tick += Timer1_Tick;
            TimerDisplay1.Start();

            TimerHome1 = new DispatcherTimer();
            TimerHome1.Interval = TimeSpan.FromMilliseconds(100);
            TimerHome1.Tick += TimerHome1_Tick;
            TimerHome1.Start();

            TimerSystemZ = new DispatcherTimer();
            TimerSystemZ.Interval = TimeSpan.FromMilliseconds(10);
            TimerSystemZ.Tick += TimerSystemZ_Tick;

            TimerFollowSignalFromIO = new DispatcherTimer();
            TimerFollowSignalFromIO.Interval = TimeSpan.FromMilliseconds(10);
            TimerFollowSignalFromIO.Tick += TimerFollowSignalFromIO_Tick;

            TimerGetDayWork = new DispatcherTimer();
            TimerGetDayWork.Interval = TimeSpan.FromMilliseconds(100);
            TimerGetDayWork.Tick += TimerGetDayWork_Tick; ;
            #endregion

            #region startupFUNC
            InitLibrary();
            AddAxisInfo();
            UpdateState();
            TimerFollowSignalFromIO.Start();
            TimerGetDayWork.Start();
            #endregion

            #region Commands
            #region Jog
            StopJogX = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                StopMotorAll(1, 2);
            });
            StopJogY = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                StopMotorAll(0, 1);
            });
            StopJogZ = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                StopMotorAll(2, 3);
            });
            MoveJogXPlus = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MoveMotorByJog(1, 1);
            });
            MoveJogXMinus= new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MoveMotorByJog(1, -1);
            });
            MoveJogYPlus = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MoveMotorByJog(0, 1);
            });
            MoveJogYMinus = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MoveMotorByJog(0, -1);
            });
            MoveJogZPlus = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MoveMotorByJog(2, 1);
            });
            MoveJogZMinus = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MoveMotorByJog(2, -1);
            });
            #endregion
            #region marking
            SetValueCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                ExportDataToExcel(ListResults);
            });
            RunMarkingCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                var ListToSend = new List<byte>();
                //%01-----%#= 
                var d = "%$";

                var c = Encoding.ASCII.GetBytes(d);

                foreach (var i in c)
                {
                    ListToSend.Add(i);
                }
                ListToSend.Add(1);
                for (int i = 0; i < 6; i++)
                {
                    ListToSend.Add(0);
                }
                foreach (var i in c)
                {
                    ListToSend.Add(i);
                }
                var arrSend = ListToSend.ToArray();
                COM_Marking.Write(arrSend, 0, arrSend.Length);
            });
            SwitchMarkingCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                var ListToSend = new List<byte>();

                var d = "%$";

                var c = Encoding.ASCII.GetBytes(d);

                foreach (var i in c)
                {
                    ListToSend.Add(i);
                }
                ListToSend.Add(2);
                for (int i = 0; i < 6; i++)
                {
                    ListToSend.Add(0);
                }
                foreach (var i in c)
                {
                    ListToSend.Add(i);
                }
                var arrSend = ListToSend.ToArray();
                COM_Marking.Write(arrSend, 0, arrSend.Length);
            });
            #endregion          

            CloseWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {   
                TimerHome1.Stop();
                TimerDisplay1.Stop();
                TimerSystemZ.Stop();
                TimerGetDayWork.Stop();
                TimerFollowSignalFromIO.Stop();
            });
            RunSystemCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                isButtonRunEnable = false;
                //bat den xanh               
                digitalIOVM.out6a = true;
                digitalIOVM.out5a = false;
                digitalIOVM.out4a = false;                            

                //di chuyen XY short
                MoveMotorXYByPosition(targetPos1, targetPos2, 0, 20, 20);

                //di chuyen Z                
                TimerSystemZ.Start();
               
            });
            StopSystemCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                //tat check Z
                TimerSystemZ.Stop();

                //bo phun son marking
                var ListToSend = new List<byte>();

                var d = "%$";

                var c = Encoding.ASCII.GetBytes(d);

                foreach (var i in c)
                {
                    ListToSend.Add(i);
                }
                ListToSend.Add(2);
                for (int i = 0; i < 6; i++)
                {
                    ListToSend.Add(0);
                }
                foreach (var i in c)
                {
                    ListToSend.Add(i);
                }
                var arrSend = ListToSend.ToArray();
                COM_Marking.Write(arrSend, 0, arrSend.Length);

                //isButtonRunEnable = true;
                isButtonOriginEnable = true;

                //tat bang tai
                
                //digitalIOVM.out9 = false;
                digitalIOVM.out5a = true;
                digitalIOVM.out6a = false;
                digitalIOVM.out4a = false;

                digitalIOVM.out7 = false;
                digitalIOVM.out6 = false;
                digitalIOVM.out8a = false;

                FindHomePoint(0, 1, 2, 3, 100, 50, 20, 1, 100, 100);
                WaitFindHomePoint(2, 3);
                                       
               
                

                //FindHomePoint(1, 4, 0, 2, 20, 10, 5, 1, 100, 100);
                //WaitFindHomePoint(0, 2);
                //ClearPositionMotor();
                Thread.Sleep(5000);

                //dung motor
                StopMotorAll(0, 3);
            });           
            HomeMoveCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                //tat check Z
                TimerSystemZ.Stop();

                isButtonRunEnable = true;
                isButtonOriginEnable = false;

                //bat den xanh               
                digitalIOVM.out6a = false;
                digitalIOVM.out5a = true;
                digitalIOVM.out4a = false;

                digitalIOVM.out7 = false;
                digitalIOVM.out6 = false;
                digitalIOVM.out8a = false;

                //check trang thai dong co
                AllCheckServoIsOn();

                //ve Home XYZ
                FindHomePoint(0, 1, 2, 3, 100, 50, 20, 1, 100, 100);
                WaitFindHomePoint(2, 3);

                FindHomePoint(1, 4, 0, 2, 20, 10, 5, 1, 100, 100);
                WaitFindHomePoint(0, 2);
                ClearPositionMotor();

            });
            ResetCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {                
                DigitalIO DigitalIOWD = new DigitalIO();

                if (DigitalIOWD.DataContext == null)
                {
                    return;
                }
                else
                {
                    var digitalIOVM = DigitalIOWD.DataContext as DigitalIOVM;
                    ListResults.Clear();
                }
                NumNG = NumProducts = 0;
            });
            #endregion
        }

        #region TimerZandIO
        private void TimerGetDayWork_Tick(object sender, EventArgs e)
        {
           

            var check = DateTime.Now.Hour;

            if (check == 0)
            {
                ListResults.Clear();
            }
            if(Properties.Settings.Default.targetDefaultX != targetPos2 || Properties.Settings.Default.targetDefaultY != targetPos1 
                || Properties.Settings.Default.targetDefaultZshort != targetPos3 || Properties.Settings.Default.targetDefaultZopen != targetPosZOpen)
            {
                Properties.Settings.Default.targetDefaultX = targetPos2;
                Properties.Settings.Default.targetDefaultY = targetPos1;
                Properties.Settings.Default.targetDefaultZshort = targetPos3;
                Properties.Settings.Default.targetDefaultZopen = targetPosZOpen;
                Properties.Settings.Default.Save();
            }          

        }
        private void TimerFollowSignalFromIO_Tick(object sender, EventArgs e)
        {
            //kiem tra khi dau vao
            if (digitalIOVM.in9a == true)
            {
                status = "OK";
                //isButtonRunEnable = true;
            }
            else
            {
                status = "Fail";
                isButtonRunEnable = false;
            }
            //check xy lanh nhan truoc khi ve home
            if(digitalIOVM.in5 == true)
            {
                isButtonOriginEnable = false;
            }
            else
            {
                isButtonOriginEnable = true;
            }
            //nut start
            if (digitalIOVM.in10a == true)
            {
                //check trang thai dong co
                digitalIOVM.out6a = true;
                digitalIOVM.out5a = false;
                digitalIOVM.out4a = false;

                digitalIOVM.out7 = false;
                digitalIOVM.out6 = false;
                digitalIOVM.out8a = false;

                AllCheckServoIsOn();

                //ve Home Z
                FindHomePoint(0, 1, 2, 3, 100, 50, 20, 1, 100, 100);
                WaitFindHomePoint(2, 3);

                //FindHomePoint(1, 4, 0, 2, 20, 10, 5, 1, 100, 100);
                //WaitFindHomePoint(0, 2);
                //ClearPositionMotor();
                //Thread.Sleep(5000);

                ////di chuyen XY short
                //MoveMotorXYByPosition(targetPos1, targetPos2, 0, 20, 20);

                ////di chuyen Z                
                //TimerSystemZ.Start();
            }

            //nut reset
            if (digitalIOVM.in13a == true)
            {
                //tat check Z
                //TimerSystemZ.Stop();

                //bo phun son marking
                var ListToSend = new List<byte>();

                var d = "%$";

                var c = Encoding.ASCII.GetBytes(d);

                foreach (var i in c)
                {
                    ListToSend.Add(i);
                }
                ListToSend.Add(1);
                for (int i = 0; i < 6; i++)
                {
                    ListToSend.Add(0);
                }
                foreach (var i in c)
                {
                    ListToSend.Add(i);
                }
                var arrSend = ListToSend.ToArray();
                COM_Marking.Write(arrSend, 0, arrSend.Length);

                //isButtonRunEnable = true;
                isButtonOriginEnable = true;

                //tat bang tai

                //digitalIOVM.out9 = false;
                digitalIOVM.out5a = true;
                digitalIOVM.out6a = false;
                digitalIOVM.out4a = false;

                digitalIOVM.out7 = false;
                digitalIOVM.out6 = false;
                digitalIOVM.out8a = false;

                FindHomePoint(0, 1, 2, 3, 100, 50, 20, 1, 100, 100);
                WaitFindHomePoint(2, 3);




                //FindHomePoint(1, 4, 0, 2, 20, 10, 5, 1, 100, 100);
                //WaitFindHomePoint(0, 2);
                //ClearPositionMotor();
                Thread.Sleep(5000);

                //dung motor
                StopMotorAll(0, 3);
            }

            //nut emergence
            if (digitalIOVM.in12a == false)
            {
                //tat timer
                TimerSystemZ.Stop();

                //tat bang tai  && bat den && ha base xuong                  
                digitalIOVM.out9 = false;

                digitalIOVM.out5a = true;
                digitalIOVM.out6a = false;
                digitalIOVM.out4a = false;

                digitalIOVM.out7 = false;
                digitalIOVM.out6 = false;
                digitalIOVM.out8a = false;

                //dung motor
                StopMotorAll(0, 3);
            }

            //nut stop
            if (digitalIOVM.in11a == true)
            {
                //tat check Z
                TimerSystemZ.Stop();

                //bo phun son marking
                var ListToSend = new List<byte>();

                var d = "%$";

                var c = Encoding.ASCII.GetBytes(d);

                foreach (var i in c)
                {
                    ListToSend.Add(i);
                }
                ListToSend.Add(2);
                for (int i = 0; i < 6; i++)
                {
                    ListToSend.Add(0);
                }
                foreach (var i in c)
                {
                    ListToSend.Add(i);
                }
                var arrSend = ListToSend.ToArray();
                COM_Marking.Write(arrSend, 0, arrSend.Length);

                //isButtonRunEnable = true;
                isButtonOriginEnable = true;

                //tat bang tai

                //digitalIOVM.out9 = false;
                digitalIOVM.out5a = true;
                digitalIOVM.out6a = false;
                digitalIOVM.out4a = false;

                digitalIOVM.out7 = false;
                digitalIOVM.out6 = false;
                digitalIOVM.out8a = false;

                FindHomePoint(0, 1, 2, 3, 100, 50, 20, 1, 100, 100);
                WaitFindHomePoint(2, 3);




                //FindHomePoint(1, 4, 0, 2, 20, 10, 5, 1, 100, 100);
                //WaitFindHomePoint(0, 2);
                //ClearPositionMotor();
                Thread.Sleep(5000);

                //dung motor
                StopMotorAll(0, 3);
            }          
        }        
        private void TimerSystemZ_Tick(object sender, EventArgs e)
        {
            #region beginning           
            //check kep dau do
            if (digitalIOVM.out4 != false)
            {
                digitalIOVM.out4 = false;
            }

            //bat bang tai
            digitalIOVM.out9 = true;

            //sensor phat hien san pham co hay khong
            if (digitalIOVM.in1a == true)
            {
                //xi lanh can vat bat len
                digitalIOVM.out6 = true;
                Measuament = "";
                //ListData = null;
                //ListShow = null;
                //ListShow1 = null;
                //ListShow2 = null;
            }


            //kiem tra xi lanh len chua va san pham da vao vi tri do chua
            if (digitalIOVM.in3 == true && digitalIOVM.in2a == true && digitalIOVM.out6 == true)
            {
                //bat xi lanh nang vat de bat dau do
                digitalIOVM.out7 = true;
                digitalIOVM.out8a = true;               
            }
            #endregion

            #region Run  

            //kiem tra xi lanh nang vat hay chua va vat co nam dung vi tri do hay khong  && digitalIOVM.in3a == true
            if (digitalIOVM.in5 == true  && digitalIOVM.out7 == true && digitalIOVM.out5 == false)
            {
                //di chuyen Z xuong
                MoveMotorZByPosition(targetPos1,targetPos2, targetPos3 - 100, 200);
                MoveMotorZByPosition(targetPos1, targetPos2, targetPos3, 100);
                Measuament = "Testing..";
                // kiem tra vi tri hien tai cua Z
                double dCmdPos = 0.0;
                CAXM.AxmStatusGetCmdPos(m_lMoveMultiAxes[2], ref dCmdPos);

                //DO MACH 
                if (Math.Abs(targetPos3 - dCmdPos) < 1 && flagPress == false)
                {
                    //DO MACH SHORT
                    //"ok/r/n{sherr//}/r/nrev=1/r/n"                    
                    if (COM_Mesuament.IsOpen == true)
                    {
                        ShortMessMesuament = GetDataFromBoardTest(cmdShort, COM_Mesuament);
                    }
                    flagPress = true;

                    //hien thi ket qua                        
                    var check = ShortMessMesuament.Substring(ShortMessMesuament.Length - 3, 1);
                    if (check == "1")
                    {
                        errorsOnBoardFPCB.ShortLocations = "PASS";
                        ListDataShort = new List<int>();
                        check1 = true;
                    }
                    else
                    {
                        check1 = false;                       
                        ListDataShort = DataMesuamentToListLocationError(ShortMessMesuament, 5);
                        errorsOnBoardFPCB.ShortLocations = ListDataMesuamentToString(ListDataShort);
                    }
                }

                //keo z len va bat nut nhan
                if (flagPress == true)
                {
                    //keo Z len
                    MoveMotorZByPosition(targetPos1, targetPos2, targetPos3 - 100, 100);                    
                    digitalIOVM.out5 = true;                        
                    flagPool = true;
                       
                }
                    
            }
            //DO MACH OPEN va day ra marking
            if (flagPool == true && digitalIOVM.in2 == false && digitalIOVM.in1 == true)
            {
                //xuong Z open
                MoveMotorZByPosition(targetPos1, targetPos2, targetPosZOpen, 50);
                #region open
                if (COM_Mesuament.IsOpen == true)
                {                    
                    OpenMessMesuament = GetDataFromBoardTest(cmdOpen, COM_Mesuament);
                }

                //hien thi ket qua                        
                var check = OpenMessMesuament.Substring(OpenMessMesuament.Length - 3, 1);
                if (check == "1")
                {
                    errorsOnBoardFPCB.OpenLocations = "PASS";
                    check2 = true;
                    ListDataOpen = new List<int>();
                }
                else
                {
                    check2 = false;
                    ListDataOpen = DataMesuamentToListLocationError(OpenMessMesuament, 5);
                    errorsOnBoardFPCB.OpenLocations = ListDataMesuamentToString(ListDataOpen);
                }
                #endregion

                //show kq
                #region SHOW
                ListShow = ExportListForDisplay(ListDataShort, ListDataOpen, 56);
                SplitListToDisplay(ListShow);

                //dem so luong
                int ng = 0, open = 0, sho = 0;
                List<int> ListErrors = new List<int>();
                foreach (var item in ListShow)
                {
                    if (item.Judge == "NG")
                    {
                        ListErrors.Add(item.Id);
                        ng++;
                    }
                    if (item.OpenError == "NG")
                    {
                        open++;
                    }
                    if (item.ShortError == "NG")
                    {
                        sho++;
                    }
                }             

                NumOpen = open;
                NumShort = sho;
                Prosentov = 100 * Convert.ToDouble(String.Format("{0:0.000}", (double)ng / 56));

                NumNG += ng;
                NumProducts += 56;
                ProsentovS = 100 * Convert.ToDouble(String.Format("{0:0.000}", (double)NumNG / NumProducts));

                //day data cho marking     
                List<int> ListLocation = new List<int>();
                if (ListErrors.Count == 0)
                {
                    string fameData = "%#";
                    var arrFame = Encoding.ASCII.GetBytes(fameData);
                    var listToSend = new List<byte>();
                    foreach (var item in arrFame)
                    {
                        listToSend.Add(item);
                    }
                    for (int i = 0; i < 7; i++)
                    {
                        listToSend.Add(255);
                    }
                    foreach (var item in arrFame)
                    {
                        listToSend.Add(item);
                    }
                    var arrSend = listToSend.ToArray();
                    COM_Marking.Write(arrSend, 0, arrSend.Length);
                }
                else
                {
                    string fameData = "%#";
                    var arrFame = Encoding.ASCII.GetBytes(fameData);
                    var listToSend = new List<byte>();
                    foreach (var item in arrFame)
                    {
                        listToSend.Add(item);
                    }
                    foreach (var i in ListErrors)
                    {
                        ListLocation.Add(i);
                    }
                    var ListLocattionDistinct = ListLocation.Distinct().ToList();
                    var a = HandleListLocationToSend(ListLocattionDistinct);
                    foreach (var item in a)
                    {
                        listToSend.Add(item);
                    }
                    foreach (var item in arrFame)
                    {
                        listToSend.Add(item);
                    }
                    var arrSend = listToSend.ToArray();
                    COM_Marking.Write(arrSend, 0, arrSend.Length);
                }
                #endregion

                //di chuyen Z len                        
                MoveMotorZByPosition(targetPos1, targetPos2, 5, 200);

                //tat nut nhan
                digitalIOVM.out5 = false;

                //ha xi lanh cho vat quay lai bang tai
                digitalIOVM.out7 = false;

                //ha xi lanh can vat 
                digitalIOVM.out6 = false;
                flagPool = false;
                flagPress = false;
                flagDone = true;
            }

            //cho hang tiep theo vao va luu gia tri do lai
            if(flagDone == true && digitalIOVM.in2a == false)
            {
                //tat xy lanh chan o dau bang tai
                digitalIOVM.out8a = false;

                errorsOnBoardFPCB.Id = counter;
                if (NumOpen == 0 && NumShort == 0)
                {
                    errorsOnBoardFPCB.ResultEvaluation = "PASS";
                    Measuament = "PASS";
                }
                else
                {
                    Measuament = "NG";
                    errorsOnBoardFPCB.ResultEvaluation = "NG";
                }
                errorsOnBoardFPCB.TimeMesuament = DateTime.Now.ToString();

                ErrorsOnBoardFPCB Data = new ErrorsOnBoardFPCB();
                Data.Id = errorsOnBoardFPCB.Id;
                Data.ResultEvaluation = errorsOnBoardFPCB.ResultEvaluation;
                Data.ShortLocations = errorsOnBoardFPCB.ShortLocations;
                Data.OpenLocations = errorsOnBoardFPCB.OpenLocations;
                Data.TimeMesuament = errorsOnBoardFPCB.TimeMesuament;
                ListResults.Add(Data);                               
                
                flagDone = false;                   
                counter++;
            }
            #endregion            
        }
        #endregion

        #region dataWithLabview       
        public void CommunicateWithLapView(string x)
        {
            dnRemotingServer.remoteObject.SetX(x);
        }
        #endregion

        #region TimerTik
        private void TimerHome1_Tick(object sender, EventArgs e)
        {
            //GC.Collect();
            //GC.WaitForPendingFinalizers();
            //GC.Collect();

            uint duRetCode, duState = 0;
            uint duStepMain = 0, duStepSub = 0;

            //++ 지정한 축의 원점신호의 상태를 확인합니다.
            duRetCode = CAXM.AxmHomeReadSignal(m_lAxisNo, ref duState);
            if (duRetCode == (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS) 
            { 
                //chkHomeState.Checked = Convert.ToBoolean(duState); 
            }

            //++ 지정한 축의 원점신호의 상태를 확인합니다.
            CAXM.AxmHomeGetResult(m_lAxisNo, ref duState);
            if (m_duOldResult != duState)
            {
                //labelHomeSearch.Text = TranslateHomeResult(duState);
                m_duOldResult = duState;
            }
            //++ 지정한 축의 원점검색 결과를 확인합니다
            duRetCode = CAXM.AxmHomeGetRate(m_lAxisNo, ref duStepMain, ref duStepSub);
            if (duRetCode == (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
            {
                //labelHomeStepMain32.Text = Convert.ToString(duStepMain);
                //labelHomeStepSub33.Text = Convert.ToString(duStepSub);
            }
            //++ 지정한 축의 원점검색 진행율을 확인합니다.
            duRetCode = CAXM.AxmHomeGetRate(m_lAxisNo, ref duStepMain, ref duStepSub);
            if (duRetCode == (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
            {
                //prgHomeRate.Value = (int)duStepSub;
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

            double[] DisplayCmdPos = new double[6];
            double[] DisplayActPos = new double[6];
            double[] DisplayCmdVel = new double[6];

            uint lCount, lArrayIndex = 0;
            double dCmdPos = 0.0;
            double dActPos = 0.0;
            double dCmdVel = 0.0;

            for (lCount = 0; lCount < 6; lCount++)
            {
                //++ Trả về vị trí lệnh của trục được chỉ định.
                CAXM.AxmStatusGetCmdPos(m_lMoveMultiAxes[lArrayIndex], ref dCmdPos);
                if (m_dOldCmdPos[lCount] != dCmdPos)
                {
                    DisplayCmdPos[lCount] = Convert.ToDouble(String.Format("{0:0.000}", dCmdPos));
                    m_dOldCmdPos[lCount] = dCmdPos;
                }
                //Trả về vị trí (Phản hồi) thực tế của trục được chỉ định.
                CAXM.AxmStatusGetActPos(m_lMoveMultiAxes[lArrayIndex], ref dCmdPos);
                if (m_dOldActPos[lCount] != dActPos)
                {
                    DisplayActPos[lCount] = Convert.ToDouble(String.Format("{0:0.000}", dActPos));
                    m_dOldActPos[lCount] = dActPos;
                }
                //++ Trả về tốc độ truyền động của trục được chỉ định.
                CAXM.AxmStatusReadVel(m_lMoveMultiAxes[lArrayIndex], ref dCmdVel);
                if (m_dOldCmdVel[lCount] != dCmdVel)
                {
                    DisplayCmdVel[lCount] = Convert.ToDouble(String.Format("{0:0.000}", dCmdVel));
                    m_dOldCmdVel[lCount] = dCmdVel;
                }
                ++lArrayIndex;
            }
            commandPos1 = DisplayCmdPos[0]; commandPos2 = DisplayCmdPos[1]; commandPos3 = DisplayCmdPos[2]; commandPos4 = DisplayCmdPos[3]; commandPos5 = DisplayCmdPos[4]; commandPos6 = DisplayCmdPos[5];

            feedBackPos1 = DisplayActPos[0]; feedBackPos2 = DisplayActPos[1]; feedBackPos3 = DisplayActPos[2]; feedBackPos4 = DisplayActPos[3]; feedBackPos5 = DisplayActPos[4]; feedBackPos6 = DisplayActPos[5];

            commandVel1 = DisplayCmdVel[0]; commandVel2 = DisplayCmdVel[1]; commandVel3 = DisplayCmdVel[2]; commandVel4 = DisplayCmdVel[3]; commandVel5 = DisplayCmdVel[4]; commandVel6 = DisplayCmdVel[5];
           
            //GC.Collect();
            //GC.WaitForPendingFinalizers();
            //GC.Collect();
        }
        #endregion
        
        #region TCP and .NET Remoting EventHandlers
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
                tcpServer.SendMessage(x);
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
                UpdateTextBox(Mess5, dnRemotingServer.remoteObject.GetX().ToString());
                //MessageBox.Show(Mess5);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            UpdateLog("RemoteObject", "X Value Changed: " + x.ToString());
        }
        private void UpdateLog(string v1, string v2)
        {
            listLog.Add(v2);
        }
        void UpdateTextBox(string textBox, string text)
        {           
            textBox = text;
        }
        #endregion        

        #region functionInit
        public void UpdateState()
        {
            double[] MovePos = new double[6];
            MovePos[0] = targetPos1; MovePos[1] = targetPos2; MovePos[2] = targetPos3; MovePos[3] = targetPos4; MovePos[4] = targetPos5; MovePos[5] = targetPos6;
            double[] MoveVel = new double[6];
            MoveVel[0] = moveVelocity1; MoveVel[1] = moveVelocity2; MoveVel[2] = moveVelocity3; MoveVel[3] = moveVelocity4; MoveVel[4] = moveVelocity5; MoveVel[5] = moveVelocity6;
            double[] MoveAcc = new double[6];
            MoveAcc[0] = moveAccel1; MoveAcc[1] = moveAccel2; MoveAcc[2] = moveAccel3; MoveAcc[3] = moveAccel4; MoveAcc[4] = moveAccel5; MoveAcc[5] = moveAccel6;
            double[] MoveDec = new double[6];
            MoveDec[0] = moveDecel1; MoveDec[1] = moveDecel2; MoveDec[2] = moveDecel3; MoveDec[3] = moveDecel4; MoveDec[4] = moveDecel5; MoveDec[5] = moveDecel6;

            double[] JogVel = new double[6];
            JogVel[0] = jogVelocity1; JogVel[1] = jogVelocity2; JogVel[2] = jogVelocity3; JogVel[3] = jogVelocity4; JogVel[4] = jogVelocity5; JogVel[5] = jogVelocity6;
            double[] JogAcc = new double[6];
            JogAcc[0] = jogAccel1; JogAcc[1] = jogAccel2; JogAcc[2] = jogAccel3; JogAcc[3] = jogAccel4; JogAcc[4] = jogAccel5; JogAcc[5] = jogAccel5;
            double[] JogDec = new double[6];
            JogDec[0] = jogDecel1; JogDec[1] = jogDecel2; JogDec[2] = jogDecel3; JogDec[3] = jogDecel4; JogDec[4] = jogDecel5; JogDec[5] = jogDecel6;

            double[] LinePos = new double[6];
            LinePos[0] = linePos1; LinePos[1] = linePos2; LinePos[2] = linePos3; LinePos[3] = linePos4; LinePos[4] = linePos5; LinePos[5] = linePos6;

            uint duOnOff = 0;
            uint lCount;
            int lFirstAxisNo = 0;

            m_lMoveMultiAxesCount = 0;
            //++ Trả về thông tin trên trục được chỉ định.
            // [INFO] 여러개의 정보를 읽는 함수 사용시 불필요한 정보는 NULL(0)을 입력하면 됩니다.
            CAXM.AxmInfoGetAxis(m_lAxisNo, ref m_lBoardNo, ref m_lModulePos, ref m_uModuleID);

            //++ Trả về số trục đầu tiên của bảng / mô-đun được chỉ định.
            CAXM.AxmInfoGetFirstAxisNo(m_lBoardNo, m_lModulePos, ref lFirstAxisNo);

            for (lCount = 0; lCount < 6; lCount++)
            {
                //++ Trả về liệu trục được chỉ định có ở trạng thái có thể điều khiển được hay không.
                if (CAXM.AxmInfoGetAxisStatus(lFirstAxisNo) == (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                {                                     
                    m_lMoveMultiAxes[m_lMoveMultiAxesCount++] = lFirstAxisNo;                    
                }
                else
                {
                    m_lMoveMultiAxes[lCount] = -1;                                      
                }
                lFirstAxisNo++;
                //chkMoveMultiAxes[lCount].Text = strData;
            }
            
            for (lCount = 0; lCount < m_lMoveMultiAxesCount; lCount++)
            {
                //++ Trả về trạng thái bật servo của trục được chỉ định.
                CAXM.AxmSignalIsServoOn(m_lMoveMultiAxes[lCount], ref duOnOff);             
            }            
        }
        public void AddAxisInfo()
        {
            String strAxis = "";

            //++ Trả về tổng số trục chuyển động hợp lệ.
            CAXM.AxmInfoGetAxisCount(ref m_lAxisCounts);
            m_lAxisNo = 0;
            //++ Trả về thông tin trên trục được chỉ định.
            // [INFO] Khi sử dụng một hàm đọc nhiều thông tin, hãy nhập NULL (0) cho những thông tin không cần thiết.
            CAXM.AxmInfoGetAxis(m_lAxisNo, ref m_lBoardNo, ref m_lModulePos, ref m_uModuleID);
            for (int i = 0; i < m_lAxisCounts; i++)
            {
                switch (m_uModuleID)
                {
                    //++ Trả về thông tin trên trục được chỉ định.
                    // [INFO] Khi sử dụng một hàm đọc nhiều thông tin, hãy nhập NULL (0) cho những thông tin không cần thiết.
                    case (uint)AXT_MODULE.AXT_SMC_4V04: strAxis = String.Format("{0:0}-(AXT_SMC_4V04)", i); break;
                    case (uint)AXT_MODULE.AXT_SMC_R1V04: strAxis = String.Format("{0:0}-[AXT_SMC_R1V04]", i); break;
                    case (uint)AXT_MODULE.AXT_SMC_2V04: strAxis = String.Format("{0:0}-[AXT_SMC_2V04]", i); break;
                    case (uint)AXT_MODULE.AXT_SMC_R1V04MLIIPM: strAxis = String.Format("{0:0}-[AXT_SMC_R1V04MLIIPM]", i); break;
                    case (uint)AXT_MODULE.AXT_SMC_R1V04PM2Q: strAxis = String.Format("{0:0}-[AXT_SMC_R1V04PM2Q]", i); break;
                    case (uint)AXT_MODULE.AXT_SMC_R1V04PM2QE: strAxis = String.Format("{0:0}-[AXT_SMC_R1V04PM2QE]", i); break;
                    case (uint)AXT_MODULE.AXT_SMC_R1V04MLIIIPM: strAxis = String.Format("{0:0}-(AXT_SMC_R1V04MLIIIPM)", i); break;
                    case (uint)AXT_MODULE.AXT_SMC_R1V04MLIISV: strAxis = String.Format("{0:0}-[AXT_SMC_R1V04MLIISV]", i); break;
                    case (uint)AXT_MODULE.AXT_SMC_R1V04A5: strAxis = String.Format("{0:0}-[AXT_SMC_R1V04A4]", i); break;
                    case (uint)AXT_MODULE.AXT_SMC_R1V04A4: strAxis = String.Format("{0:0}-[AXT_SMC_R1V04MLIICL]", i); break;
                    case (uint)AXT_MODULE.AXT_SMC_R1V04SIIIHMIV: strAxis = String.Format("{0:0}-[AXT_SMC_R1V04SIIIHMIV]", i); break;
                    case (uint)AXT_MODULE.AXT_SMC_R1V04SIIIHMIV_R: strAxis = String.Format("{0:0}-[AXT_SMC_R1V04SIIIHMIV_R]", i); break;
                    case (uint)AXT_MODULE.AXT_SMC_R1V04MLIIISV: strAxis = String.Format("{0:0}-[AXT_SMC_R1V04MLIIISV]", i); break;
                    case (uint)AXT_MODULE.AXT_SMC_R1V04MLIIISV_MD: strAxis = String.Format("{0:0}-[AXT_SMC_R1V04MLIIISV_MD]", i); break;
                    case (uint)AXT_MODULE.AXT_SMC_R1V04MLIIIS7S: strAxis = String.Format("{0:0}-[AXT_SMC_R1V04MLIIIS7S]", i); break;
                    case (uint)AXT_MODULE.AXT_SMC_R1V04MLIIIS7W: strAxis = String.Format("{0:0}-[AXT_SMC_R1V04MLIIIS7W]", i); break;
                    default: strAxis = String.Format("{0:00}-[Unknown]", i); break;
                }
                listMotionModule.Add(strAxis);
            }
            InitControl();      // Control Đăng ký các biến và thiết lập cài đặt ban đầu.
        }
        private void InitControl()
        {
            uint lCount = 0;            

            double[] MovePos = new double[6];
            double[] MoveVel = new double[6];
            double[] MoveAcc = new double[6];
            double[] MoveDec = new double[6];
            double[] JogVel = new double[6];
            double[] JogAcc = new double[6];
            double[] JogDec = new double[6];
            double[] LinePos = new double[6];
            for (lCount = 0; lCount < 6; lCount++)
            {
                MovePos[lCount] = -29.5;
                MoveVel[lCount] = 20.000;
                MoveAcc[lCount] = 40;
                MoveDec[lCount] = 40;
                JogVel[lCount] = 100;
                JogAcc[lCount] = 40;
                JogDec[lCount] = 40;
                LinePos[lCount] = 100.000;
            }
            targetPos1 = Properties.Settings.Default.targetDefaultY; targetPos2 = Properties.Settings.Default.targetDefaultX;
            targetPos3 = Properties.Settings.Default.targetDefaultZshort; targetPosZOpen = Properties.Settings.Default.targetDefaultZopen;
            targetPos4 = MovePos[3]; targetPos5 =  MovePos[4]; targetPos6 = MovePos[5];
            moveVelocity1 = MoveVel[0];  moveVelocity2  = MoveVel[1] ; moveVelocity3 = 200 ; moveVelocity4 = MoveVel[3] ; moveVelocity5 = MoveVel[4]; moveVelocity6 = MoveVel[5];
            moveAccel1 = MoveAcc[0] ; moveAccel2 = MoveAcc[1]; moveAccel3 = 400; moveAccel4 = MoveAcc[3]; moveAccel5 = MoveAcc[4]; moveAccel6 = MoveAcc[5];
            moveDecel1 = MoveDec[0] ; moveDecel2 = MoveDec[1]; moveDecel3 = 400; moveDecel4 = MoveDec[3]; moveDecel5 = MoveDec[4]; moveDecel6 = MoveDec[5];

            jogVelocity1 = JogVel[0] ; jogVelocity2 = JogVel[1]; jogVelocity3 = JogVel[2]; jogVelocity4 = JogVel[3]; jogVelocity5 = JogVel[4]; jogVelocity6 = JogVel[5];
            jogAccel1 = JogAcc[0]; jogAccel2 = JogAcc[1]; jogAccel3 = JogAcc[2]; jogAccel4 = JogAcc[3]; jogAccel5 = JogAcc[4]; jogAccel6 = JogAcc[5];
            jogDecel1 = JogDec[0]; jogDecel2 = JogDec[1]; jogDecel3 = JogDec[2]; jogDecel4 = JogDec[3]; jogDecel5 = JogDec[4]; jogDecel6 = JogDec[5];

            linePos1 = LinePos[0]; linePos2 = LinePos[1]; linePos3 = LinePos[2]; linePos4 = LinePos[3]; linePos5 = LinePos[4]; linePos6 = LinePos[5];

            lineMoveVelocity = 100.000;
            lineMoveAccel = 40.000;
            lineMoveDecel = 20.000;            
        }
        public void InitLibrary()
        {
            String szFilePath = "C:\\Users\\IPC-7132\\Desktop\\flieMot2508.mot";
            //++ AXL(AjineXtek Library)Bật và khởi tạo các bảng được gắn kết.
            if (CAXL.AxlOpen(7) != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS) 
            {
                Mess6 = "Intialize Fail..!!";                
            }
            //++ Thay đổi hàng loạt và áp dụng các giá trị cài đặt của bảng chuyển động với các giá trị cài đặt của tệp Mot được chỉ định.
            if (CAXM.AxmMotLoadParaAll(szFilePath) != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS) 
            {
                //MessageBox.Show("Mot File Not Found.");
                Mess6 = "Mot File Not Found.";
                //Dispatcher.CurrentDispatcher.Invoke(() => { MessageBox.Show("Intialize Fail..!!"); });
            }
        }
        #endregion        

        #region MoveLineJog
        public void MoveMotorByLine()
        {
            double[] LinePos = new double[4];
            LinePos[0] = linePos1; LinePos[1] = linePos2;           

            uint lCount, lArrayIndex;
            double[] dMultiLinePos = { 0.0, 0.0 };
            double dMultiLineJogVel = 0.0, dMultiLineJogAcc = 0.0, dMultiLineJogDec = 0.0;

            lArrayIndex = 0;

            for (lCount = 0; lCount < 2; lCount++)
            {                
                dMultiLinePos[lArrayIndex] = Convert.ToDouble(LinePos[lCount]);
                ++lArrayIndex;                
            }

            dMultiLineJogVel = Convert.ToDouble(lineMoveVelocity);
            dMultiLineJogAcc = Convert.ToDouble(lineMoveAccel);
            dMultiLineJogDec = Convert.ToDouble(lineMoveDecel);

            //++ 선택한 축들을 (+/-)방향으로 지정한 위치 비율에따른 속도/가속도/감속도로 모션구동합니다.
            CAXM.AxmMoveStartLineVel((int)m_lMoveMultiAxesCount, m_lMoveMultiAxes, dMultiLinePos, dMultiLineJogVel, dMultiLineJogAcc, dMultiLineJogDec);
        }
        public void MoveMotorByJog(int NoLAxis, double direction)
        {
            uint duRetCode = 0;

            double JogVel = jogVelocity1 * direction;
            
            double JogAcc = jogAccel1;

            double JogDec = jogDecel1;           
           
            //++ Trục xác định được di chuyển theo hướng (+) với tốc độ / gia tốc / giảm tốc xác định.
            duRetCode = CAXM.AxmMoveVel(m_lMoveMultiAxes[NoLAxis], JogVel, JogAcc, JogDec);
            if (duRetCode != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                MessageBox.Show(String.Format("AxmMoveStartMultiPos return error[Code:{0:d}]", duRetCode));
        }
        #endregion

        #region MOVE XYZ
        public void MoveMotorXYByPosition(double tagetPosY, double tagetPosX, double tagetPosZ, double VelX, double VelY)
        {
            uint duRetCode = 0;

            double[] MovePos = new double[6];
            MovePos[0] = tagetPosY; MovePos[1] = tagetPosX; MovePos[2] = tagetPosZ; MovePos[3] = 0; MovePos[4] = 0; MovePos[5] = 0;
            double[] MoveVel = new double[6];
            MoveVel[0] = VelY; MoveVel[1] = VelX; MoveVel[2] = moveVelocity3; MoveVel[3] = moveVelocity4; MoveVel[4] = moveVelocity5; MoveVel[5] = moveVelocity6;
            double[] MoveAcc = new double[6];
            MoveAcc[0] = moveAccel1; MoveAcc[1] = moveAccel2; MoveAcc[2] = moveAccel3; MoveAcc[3] = moveAccel4; MoveAcc[4] = moveAccel5; MoveAcc[5] = moveAccel6;
            double[] MoveDec = new double[6];
            MoveDec[0] = moveDecel1; MoveDec[1] = moveDecel2; MoveDec[2] = moveDecel3; MoveDec[3] = moveDecel4; MoveDec[4] = moveDecel5; MoveDec[5] = moveDecel6;

            #region oldcode
            //uint lCount, lArrayIndex;

            //double[] dMultiPos = { 0.0, 0.0, 0.0, 0.0, 0.0 }, dMultiVel = { 0.0, 0.0, 0.0, 0.0, 0.0 },
            //         dMultiAcc = { 0.0, 0.0, 0.0, 0.0, 0.0 }, dMultiDec = { 0.0, 0.0, 0.0, 0.0, 0.0 };

            //lArrayIndex = 0;

            //for (lCount = 0; lCount < 5; lCount++)
            //{               
            //    dMultiPos[lArrayIndex] = Convert.ToDouble(MovePos[lCount]);
            //    dMultiVel[lArrayIndex] = Convert.ToDouble(MoveVel[lCount]);
            //    dMultiAcc[lArrayIndex] = Convert.ToDouble(MoveAcc[lCount]);
            //    dMultiDec[lArrayIndex] = Convert.ToDouble(MoveDec[lCount]);
            //    ++lArrayIndex;               
            //}
            #endregion

            if (chkMoveBlock)
            {
                //++ Các trục được chỉ định được điều khiển theo khoảng cách (hoặc vị trí) / vận tốc / tăng / giảm tốc đã chỉ định,
                //và khi chuyển động kết thúc, chức năng sẽ được thoát ra.
                duRetCode = CAXM.AxmMoveMultiPos((int)m_lMoveMultiAxesCount, m_lMoveMultiAxes, MovePos, MoveVel, MoveAcc, MoveDec);
                if (duRetCode != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                    MessageBox.Show(String.Format("AxmMoveMultiPos return error[Code:{0:d}]", duRetCode));
            }
            else
            {
                //++ Các trục được chỉ định được điều khiển theo khoảng cách (hoặc vị trí) / vận tốc / tăng / giảm tốc đã chỉ định,
                //và chức năng sẽ thoát ra bất kể chuyển động có kết thúc hay không.
                duRetCode = CAXM.AxmMoveStartMultiPos((int)m_lMoveMultiAxesCount, m_lMoveMultiAxes, MovePos, MoveVel, MoveAcc, MoveDec);
                if (duRetCode != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                    MessageBox.Show(String.Format("AxmMoveStartMultiPos return error[Code:{0:d}]", duRetCode));
            }

        }

        public void MoveMotorZByPosition(double tagetPosY, double tagetPosX, double tagetPosZ, double VelZ)
        {
            uint duRetCode = 0;

            double[] MovePos = new double[6];
            MovePos[0] = tagetPosY; MovePos[1] = tagetPosX; MovePos[2] = tagetPosZ; MovePos[3] = 0; MovePos[4] = 0; MovePos[5] = 0; 
            double[] MoveVel = new double[6];
            MoveVel[0] = moveVelocity1; MoveVel[1] = moveVelocity2; MoveVel[2] = VelZ; MoveVel[3] = moveVelocity4; MoveVel[4] = moveVelocity5; MoveVel[5] = moveVelocity6;
            double[] MoveAcc = new double[6];
            MoveAcc[0] = moveAccel1; MoveAcc[1] = moveAccel2; MoveAcc[2] = moveAccel3; MoveAcc[3] = moveAccel4; MoveAcc[4] = moveAccel5; MoveAcc[5] = moveAccel6;
            double[] MoveDec = new double[6];
            MoveDec[0] = moveDecel1; MoveDec[1] = moveDecel2; MoveDec[2] = moveDecel3; MoveDec[3] = moveDecel4; MoveDec[4] = moveDecel5; MoveDec[5] = moveDecel6;

            #region oldcode
            //uint lCount, lArrayIndex;

            //double[] dMultiPos = { 0.0, 0.0, 0.0, 0.0, 0.0 }, dMultiVel = { 0.0, 0.0, 0.0, 0.0, 0.0 },
            //         dMultiAcc = { 0.0, 0.0, 0.0, 0.0, 0.0 }, dMultiDec = { 0.0, 0.0, 0.0, 0.0, 0.0 };

            //lArrayIndex = 0;

            //for (lCount = 0; lCount < 5; lCount++)
            //{               
            //    dMultiPos[lArrayIndex] = Convert.ToDouble(MovePos[lCount]);
            //    dMultiVel[lArrayIndex] = Convert.ToDouble(MoveVel[lCount]);
            //    dMultiAcc[lArrayIndex] = Convert.ToDouble(MoveAcc[lCount]);
            //    dMultiDec[lArrayIndex] = Convert.ToDouble(MoveDec[lCount]);
            //    ++lArrayIndex;               
            //}
            #endregion

            if (chkMoveBlock)
            {
                //++ Các trục được chỉ định được điều khiển theo khoảng cách (hoặc vị trí) / vận tốc / tăng / giảm tốc đã chỉ định,
                //và khi chuyển động kết thúc, chức năng sẽ được thoát ra.
                duRetCode = CAXM.AxmMoveMultiPos((int)m_lMoveMultiAxesCount, m_lMoveMultiAxes, MovePos, MoveVel, MoveAcc, MoveDec);
                if (duRetCode != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                    MessageBox.Show(String.Format("AxmMoveMultiPos return error[Code:{0:d}]", duRetCode));
            }
            else
            {
                //++ Các trục được chỉ định được điều khiển theo khoảng cách (hoặc vị trí) / vận tốc / tăng / giảm tốc đã chỉ định,
                //và chức năng sẽ thoát ra bất kể chuyển động có kết thúc hay không.
                duRetCode = CAXM.AxmMoveStartMultiPos((int)m_lMoveMultiAxesCount, m_lMoveMultiAxes, MovePos, MoveVel, MoveAcc, MoveDec);
                if (duRetCode != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                    MessageBox.Show(String.Format("AxmMoveStartMultiPos return error[Code:{0:d}]", duRetCode));
            }

        }

        public void MoveMotorUTByPosition()
        {
            uint duRetCode = 0;

            double[] MovePos = new double[6];
            MovePos[0] = 0; MovePos[1] = 0; MovePos[2] = 0; MovePos[3] = 0; MovePos[4] = targetPos5; MovePos[5] = targetPos6;
            double[] MoveVel = new double[6];
            MoveVel[0] = moveVelocity1; MoveVel[1] = moveVelocity2; MoveVel[2] = moveVelocity3; MoveVel[3] = moveVelocity4; MoveVel[4] = moveVelocity5; MoveVel[5] = moveVelocity6;
            double[] MoveAcc = new double[6];
            MoveAcc[0] = moveAccel1; MoveAcc[1] = moveAccel2; MoveAcc[2] = moveAccel3; MoveAcc[3] = moveAccel4; MoveAcc[4] = moveAccel5; MoveAcc[5] = moveAccel6;
            double[] MoveDec = new double[6];
            MoveDec[0] = moveDecel1; MoveDec[1] = moveDecel2; MoveDec[2] = moveDecel3; MoveDec[3] = moveDecel4; MoveDec[4] = moveDecel5; MoveDec[5] = moveDecel6;

            #region oldcode
            //uint lCount, lArrayIndex;

            //double[] dMultiPos = { 0.0, 0.0, 0.0, 0.0, 0.0 }, dMultiVel = { 0.0, 0.0, 0.0, 0.0, 0.0 },
            //         dMultiAcc = { 0.0, 0.0, 0.0, 0.0, 0.0 }, dMultiDec = { 0.0, 0.0, 0.0, 0.0, 0.0 };

            //lArrayIndex = 0;

            //for (lCount = 0; lCount < 5; lCount++)
            //{               
            //    dMultiPos[lArrayIndex] = Convert.ToDouble(MovePos[lCount]);
            //    dMultiVel[lArrayIndex] = Convert.ToDouble(MoveVel[lCount]);
            //    dMultiAcc[lArrayIndex] = Convert.ToDouble(MoveAcc[lCount]);
            //    dMultiDec[lArrayIndex] = Convert.ToDouble(MoveDec[lCount]);
            //    ++lArrayIndex;               
            //}
            #endregion

            if (chkMoveBlock)
            {
                //++ Các trục được chỉ định được điều khiển theo khoảng cách (hoặc vị trí) / vận tốc / tăng / giảm tốc đã chỉ định,
                //và khi chuyển động kết thúc, chức năng sẽ được thoát ra.
                duRetCode = CAXM.AxmMoveMultiPos((int)m_lMoveMultiAxesCount, m_lMoveMultiAxes, MovePos, MoveVel, MoveAcc, MoveDec);
                if (duRetCode != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                    MessageBox.Show(String.Format("AxmMoveMultiPos return error[Code:{0:d}]", duRetCode));
            }
            else
            {
                //++ Các trục được chỉ định được điều khiển theo khoảng cách (hoặc vị trí) / vận tốc / tăng / giảm tốc đã chỉ định,
                //và chức năng sẽ thoát ra bất kể chuyển động có kết thúc hay không.
                duRetCode = CAXM.AxmMoveStartMultiPos((int)m_lMoveMultiAxesCount, m_lMoveMultiAxes, MovePos, MoveVel, MoveAcc, MoveDec);
                if (duRetCode != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                    MessageBox.Show(String.Format("AxmMoveStartMultiPos return error[Code:{0:d}]", duRetCode));
            }

        }

        //Chức năng xử lý được gọi khi nhấp vào nút "Xóa".
        //Đặt thông tin vị trí của các trục nội suy đã chọn thành 0.0.
        public void ClearPositionMotor()
        {
            uint lCount;

            for (lCount = 0; lCount < m_lMoveMultiAxesCount; lCount++)
                //CAXM.AxmStatusSetCmdPos(m_lMoveIntpAxes[lCount], 0);
                //CAXM.AxmStatusSetActPos(m_lMoveIntpAxes[lCount], 0);
                //++ Command위치와 Actual위치를 입력한 값으로 설정합니다.
                CAXM.AxmStatusSetPosMatch(m_lMoveMultiAxes[lCount], 0.0);
        }
        public void WaitFindHomePoint(uint step, uint range)
        {
            bool bRun = true;
            uint upHomeResult = 0;
            uint uHomeStepNumber = 0, uHomeMainStepNumber = 0;
            while (bRun)
            {
                uint lCount;
                for (lCount = step; lCount < range; lCount++)
                    CAXM.AxmHomeGetResult(m_lMoveMultiAxes[lCount], ref upHomeResult);
                switch (upHomeResult)
                {
                    case (uint)AXT_MOTION_HOME_RESULT.HOME_ERR_UNKNOWN:
                        bRun = false;
                        break;
                    case (uint)AXT_MOTION_HOME_RESULT.HOME_ERR_GNT_RANGE:
                        bRun = false;
                        break;
                    case (uint)AXT_MOTION_HOME_RESULT.HOME_ERR_USER_BREAK:
                        bRun = false;
                        break;
                    case (uint)AXT_MOTION_HOME_RESULT.HOME_ERR_VELOCITY:
                        bRun = false;
                        break;
                    case (uint)AXT_MOTION_HOME_RESULT.HOME_ERR_AMP_FAULT:
                        bRun = false;
                        break;
                    case (uint)AXT_MOTION_HOME_RESULT.HOME_ERR_NEG_LIMIT:
                        bRun = false;
                        break;
                    case (uint)AXT_MOTION_HOME_RESULT.HOME_ERR_POS_LIMIT:
                        bRun = false;
                        break;
                    case (uint)AXT_MOTION_HOME_RESULT.HOME_SEARCHING:
                        for (lCount = step; lCount < range; lCount++)
                            CAXM.AxmHomeGetRate(m_lMoveMultiAxes[lCount], ref uHomeMainStepNumber, ref uHomeStepNumber);
                        Thread.Sleep(1000);
                        break;
                    case (uint)AXT_MOTION_HOME_RESULT.HOME_SUCCESS:
                        bRun = false;
                        break;
                }
            }
        }
        public void FindHomePoint(int iHomeDir, uint duHomeSignal ,uint step, uint range, double dVelFirst, double dVelSecond, double dVelThird, double dVelLast, double dAccFirst, double dAccSecond)
        {
            //set Home Level
            uint duLevel;
            uint duRetCode3 = 0;
            //"01: HIGH" "02: UNUSED" (AXT_SMC_R1V04MLIIIS7W 00-LOW)
            duLevel = 1;
            //++Thay đổi mức hoạt động của tín hiệu gốc của trục được chỉ định.
            uint lCount;
            for (lCount = step; lCount < range; lCount++)
                duRetCode3 = CAXM.AxmHomeSetSignalLevel(m_lMoveMultiAxes[lCount], duLevel);
                if (duRetCode3 != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                MessageBox.Show(String.Format("AxmHomeSetSignalLevel return error[Code:{0:d}]", duRetCode3));

            //set method
            //Zphase : "00: DISABLE"  "01: DIR_CHOME" "02: DIR_HOME"

            //HomeDir: "00: -DIR_CCW", "01: +DIR_CW" 

            //HomeSignal: 00: PosEndLimit, 01: NegEndLimit, 04: HomeSensor, 05: EncodZPhase
            
            uint duRetCode = 0;
            
            uint duZPhaseUse = 0;
            double dHomeClrTime = 0.0, dHomeOffset = 0.0;
            //++ Thay đổi phương pháp tìm kiếm điểm gốc của trục được chỉ định.            
            for (lCount = step; lCount < range; lCount++)
                duRetCode = CAXM.AxmHomeSetMethod(m_lMoveMultiAxes[lCount], iHomeDir, duHomeSignal, duZPhaseUse, dHomeClrTime, dHomeOffset);
                if (duRetCode != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                MessageBox.Show(String.Format("AxmHomeSetMethod return error[Code:{0:d}]", duRetCode));
            //CAXM.AxmHomeSetMethod(m_lMoveMultiAxes[1], 1, duHomeSignal, duZPhaseUse, dHomeClrTime, dHomeOffset);


            //set Vel
            uint duRetCode2 = 0;           

            // Truy xuất cài đặt từ mỗi điều khiển Chỉnh sửa
            //dVelFirst = 100;
            //dVelSecond = 20;
            //dVelThird = 10;
            //dVelLast = 1;
            //dAccFirst = 20;
            //dAccSecond = 20;

            //++ Đặt tốc độ từng bước được sử dụng để tìm kiếm nguồn gốc.            
            for (lCount = step; lCount < range; lCount++)
                duRetCode2 = CAXM.AxmHomeSetVel(m_lMoveMultiAxes[lCount], dVelFirst, dVelSecond, dVelThird, dVelLast, dAccFirst, dAccSecond);
                if (duRetCode != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                MessageBox.Show(String.Format("AxmHomeSetVel return error[Code:{0:d}]", duRetCode2));


            //go to Home
            uint duRetCode1 = 0;            
            for (lCount = step; lCount < range; lCount++)
                //++ Tìm kiếm nguồn gốc được thực hiện trên trục được chỉ định.
                duRetCode1 = CAXM.AxmHomeSetStart(m_lMoveMultiAxes[lCount]);
                if (duRetCode1 != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                MessageBox.Show(String.Format("AxmHomeSetStart return error[Code:{0:d}]", duRetCode1));
        }
        public void AllCheckServoIsOn()
        {
            uint lCount;

            for (lCount = 0; lCount < m_lMoveMultiAxesCount; lCount++)
                //++ Xuất tín hiệu Bật / Tắt Servo của trục được chỉ định.
                CAXM.AxmSignalServoOn(m_lMoveMultiAxes[lCount], (uint)Convert.ToInt32(isCheckServoOn));
        }
        public void StopMotorAll(uint step, uint range)
        {
            uint lCount;

            for (lCount = step; lCount < range; lCount++)
                //++ giảm tốc và dừng truyền động của trục được chỉ định.
                CAXM.AxmMoveSStop(m_lMoveMultiAxes[lCount]);
        }
        public String TranslateHomeResult(uint duHomeResult)
        {
            switch (duHomeResult)
            {
                case (uint)AXT_MOTION_HOME_RESULT.HOME_SUCCESS: m_strResult = ("[01H] HOME_SUCCESS"); break;
                case (uint)AXT_MOTION_HOME_RESULT.HOME_SEARCHING: m_strResult = ("([02H] HOME_SEARCHING"); break;
                case (uint)AXT_MOTION_HOME_RESULT.HOME_ERR_GNT_RANGE: m_strResult = ("[10H] HOME_ERR_GNT_RANGE"); break;
                case (uint)AXT_MOTION_HOME_RESULT.HOME_ERR_USER_BREAK: m_strResult = ("[11H] HOME_ERR_USER_BREAK"); break;
                case (uint)AXT_MOTION_HOME_RESULT.HOME_ERR_VELOCITY: m_strResult = ("[12H] HOME_ERR_VELOCITY"); break;
                case (uint)AXT_MOTION_HOME_RESULT.HOME_ERR_AMP_FAULT: m_strResult = ("[13H] HOME_ERR_AMP_FAULT"); break;
                case (uint)AXT_MOTION_HOME_RESULT.HOME_ERR_NEG_LIMIT: m_strResult = ("[14H] HOME_ERR_NEG_LIMIT"); break;
                case (uint)AXT_MOTION_HOME_RESULT.HOME_ERR_POS_LIMIT: m_strResult = ("[15H] HOME_ERR_POS_LIMIT"); break;
                case (uint)AXT_MOTION_HOME_RESULT.HOME_ERR_NOT_DETECT: m_strResult = ("[16H] HOME_ERR_NOT_DETECT"); break;
                case (uint)AXT_MOTION_HOME_RESULT.HOME_ERR_UNKNOWN: m_strResult = ("[FFH] HOME_ERR_UNKNOWN"); break;
            }
            return m_strResult;
        }
        #endregion

        #region Data
        //get string results
        string GetDataFromBoardTest(string cmd, SerialPort COM_Mesuament)
        {
            string result;
            COM_Mesuament.Write(cmd);
            StringBuilder buffer = new StringBuilder();
            string a;
            do
            {
                a = COM_Mesuament.ReadExisting();
                buffer.Append(a);
            } while (buffer.ToString().Contains("rev") == false);
            result = buffer.ToString();
            return result;
        }
        //analys to list point errors
        List<int> DataMesuamentToListLocationError(string property, int numPad)
        {
            List<int> result = new List<int>();
            try
            {
                var pointToCut = property.IndexOf('/') + 1;
                var pointToCut2 = property.LastIndexOf('/');
                var arrFiller = property.Remove(pointToCut2, property.Length - pointToCut2);
                var arrLocation = arrFiller.Remove(0, pointToCut).Split(',').ToList();

                foreach (var item in arrLocation)
                {
                    var i = item.IndexOf('-');
                    var valueGetFromString = Convert.ToInt16(item.Remove(i, item.Length - i));                    
                    var data = valueGetFromString / numPad + 1;
                    result.Add(data);                                      
                }
            }
            catch { }
            return result;
        }

        //convert list errors to string for log file
        string ListDataMesuamentToString(List<int> locations)
        {
            string result;
            if (locations.Count != 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in locations)
                {
                    sb.Append(item);
                    sb.Append(',');
                }
                result = sb.ToString();
            }
            else
            {                
                result = "OK";                
            }
            return result;
        }

        //list to Display
        public ObservableCollection<FPCB> ExportListForDisplay(List<int> lShort, List<int> lOpen, int numFPCB)
        {
            ObservableCollection<FPCB> List = new ObservableCollection<FPCB>();
            for (int i = 1; i < (numFPCB + 1); i++)
            {
                FPCB FPCB = new FPCB();
                FPCB.Id = i;
                if (lShort.Contains(i))
                {
                    FPCB.ShortError = "NG";
                }
                else
                {
                    FPCB.ShortError = "OK";
                }
                if (lOpen.Contains(i))
                {
                    FPCB.OpenError = "NG";
                }
                else
                {
                    FPCB.OpenError = "OK";
                }

                //danh gia chung
                if (FPCB.OpenError == "NG" || FPCB.ShortError == "NG")
                {
                    FPCB.Judge = "NG";
                }
                else
                {
                    FPCB.Judge = "OK";
                }
                List.Add(FPCB);
            }
            return List;
        }
        void SplitListToDisplay(ObservableCollection<FPCB> List)
        {
            ListShow1 = new ObservableCollection<FPCB>();
            ListShow2 = new ObservableCollection<FPCB>();
            
            for (int i = 0; i < List.Count; i++)
            {
                if (i <= (List.Count / 2))
                {
                    ListShow1.Add(List[i]);
                }                
                else
                {
                    ListShow2.Add(List[i]);
                }
            }
        }
        List<byte> HandleListLocationToSend(List<int> ListResult)
        {
            byte fst = 0, sed = 0, thr = 0, fot = 0, fif = 0, six = 0, sev = 0;
            foreach (var item in ListResult)
            {
                if (item < 9)
                {
                    fst += Convert.ToByte(Math.Pow(2, item - 1));
                }
                else if (8 < item && item < 17)
                {
                    var i = item % 8;
                    if (i == 0)
                    {
                        i = 8;
                    }
                    sed += Convert.ToByte(Math.Pow(2, i - 1));
                }
                else if (16 < item && item < 25)
                {
                    var i = item % 8;
                    if (i == 0)
                    {
                        i = 8;
                    }
                    thr += Convert.ToByte(Math.Pow(2, i - 1));
                }
                else if (24 < item && item < 33)
                {
                    var i = item % 8;
                    if (i == 0)
                    {
                        i = 8;
                    }
                    fot += Convert.ToByte(Math.Pow(2, i - 1));
                }
                else if (32 < item && item < 41)
                {
                    var i = item % 8;
                    if (i == 0)
                    {
                        i = 8;
                    }
                    fif += Convert.ToByte(Math.Pow(2, i - 1));
                }
                else if (40 < item && item < 49)
                {
                    var i = item % 8;
                    if (i == 0)
                    {
                        i = 8;
                    }
                    six += Convert.ToByte(Math.Pow(2, i - 1));
                }
                else if (48 < item && item < 57)
                {
                    var i = item % 8;
                    if (i == 0)
                    {
                        i = 8;
                    }
                    sev += Convert.ToByte(Math.Pow(2, i - 1));
                }
            }
            List<byte> ListLocation = new List<byte>() { (byte)(255 - fst), (byte)(255 - sed), (byte)(255 - thr),
                                                         (byte)(255 - fot), (byte)(255 - fif), (byte)(255 - six), (byte)(255 - sev)};
            return ListLocation;
        }
        void ExportDataToExcel(ObservableCollection<ErrorsOnBoardFPCB> listData)
        {
            string filePath = "";
            // tạo SaveFileDialog để lưu file excel
            SaveFileDialog dialog = new SaveFileDialog();

            // chỉ lọc ra các file có định dạng Excel
            dialog.Filter = "Excel | *.xlsx | Excel 2003 | *.xls";

            // Nếu mở file và chọn nơi lưu file thành công sẽ lưu đường dẫn lại dùng
            if (dialog.ShowDialog() == true)
            {
                filePath = dialog.FileName;
            }
            // nếu đường dẫn null hoặc rỗng thì báo không hợp lệ và return hàm
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Đường dẫn báo cáo không hợp lệ");
                return;
            }

            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage p = new ExcelPackage())
                {
                    if (p == null)
                    {
                        return;
                    }
                    // đặt tên người tạo file
                    p.Workbook.Properties.Author = "RAV";

                    // đặt tiêu đề cho file
                    p.Workbook.Properties.Title = "Báo cáo thống kê ICT" + " " + DateTime.UtcNow.ToString();

                    //Tạo một sheet để làm việc trên đó
                    p.Workbook.Worksheets.Add("Report" + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString());

                    // lấy sheet vừa add ra để thao tác
                    ExcelWorksheet ws = p.Workbook.Worksheets[0];  // BUG***

                    // đặt tên cho sheet
                    ws.Name = DateTime.Now.Day.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Year.ToString();

                    // fontsize mặc định cho cả sheet
                    ws.Cells.Style.Font.Size = 11;

                    // font family mặc định cho cả sheet
                    ws.Cells.Style.Font.Name = "Calibri";

                    // Tạo danh sách các column header
                    string[] arrColumnHeader = { "ID", "Đánh giá", "Vị trí Lỗi đo ngắn mạch",
                                                    "Vị trí Lỗi đo hở mạch", "Thời Gian" };

                    // lấy ra số lượng cột cần dùng dựa vào số lượng header
                    var countColHeader = arrColumnHeader.Count();

                    // merge các column lại từ column 1 đến số column header
                    // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
                    ws.Cells[1, 1].Value = "Thống kê kết quả kiểm tra ICT" + " " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    ws.Cells[1, 1, 1, countColHeader].Merge = true;
                    // in đậm
                    ws.Cells[1, 1, 1, countColHeader].Style.Font.Bold = true;
                    // căn giữa
                    ws.Cells[1, 1, 1, countColHeader].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    int colIndex = 1;
                    int rowIndex = 2;

                    //tạo các header từ column header đã tạo từ bên trên
                    foreach (var item in arrColumnHeader)
                    {
                        var cell = ws.Cells[rowIndex, colIndex];
                        ws.Cells[rowIndex, colIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        //set màu thành gray
                        var fill = cell.Style.Fill;
                        fill.PatternType = ExcelFillStyle.Solid;
                        fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

                        //căn chỉnh các border
                        var border = cell.Style.Border;
                        border.Bottom.Style =
                        border.Top.Style =
                        border.Left.Style =
                        border.Right.Style = ExcelBorderStyle.Thin;

                        //gán giá trị
                        cell.Value = item;
                        colIndex++;
                    }

                    // với mỗi item trong danh sách ListData sẽ ghi trên 1 dòng
                    foreach (var item in listData)
                    {
                        // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                        colIndex = 1;

                        // rowIndex tương ứng từng dòng dữ liệu
                        rowIndex++;

                        // Gán giá trị cho từng cell                
                        // Lưu ý phải .ToShortDateString để dữ liệu khi in ra Excel là ngày như ta vẫn thấy.Nếu không sẽ ra tổng số :v
                        ws.Cells[rowIndex, colIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        ws.Cells[rowIndex, colIndex++].Value = item.Id;

                        ws.Cells[rowIndex, colIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        ws.Cells[rowIndex, colIndex++].Value = item.ResultEvaluation;

                        ws.Cells[rowIndex, colIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        ws.Cells[rowIndex, colIndex++].Value = item.ShortLocations;

                        ws.Cells[rowIndex, colIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        ws.Cells[rowIndex, colIndex++].Value = item.OpenLocations;

                        ws.Cells[rowIndex, colIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        ws.Cells[rowIndex, colIndex++].Value = item.TimeMesuament.ToString();
                    }
                    //p.Save(filePath);

                    //Lưu file lại
                    Byte[] bin = p.GetAsByteArray();
                    File.WriteAllBytes(filePath, bin);
                }
                MessageBox.Show("Xuất file excel thành công!");
            }

            catch (Exception EE)
            {
                MessageBox.Show("Xuất excel thất bại, hãy thử lại!" + " Error code: " + EE.Message);
            }
        }
        //"ok/r/n{sherr/1,6,8,9,/}rnrev=1rn"
        public ObservableCollection<ErrorFPCB> AnalysMessFromLabview(string mess)
        {
            ObservableCollection<ErrorFPCB> List = new ObservableCollection<ErrorFPCB>();
            try
            {
                string typeCheck;
                if ( mess.Contains("sherr") == true)
                {
                    typeCheck = "sherr";
                }
                else
                {
                    typeCheck = "operr";
                }
                var pointToCut = mess.IndexOf('/') + 1;
                var pointToCut2 = mess.LastIndexOf('/');
                var arrFiller = mess.Remove(pointToCut2, mess.Length - pointToCut2);
                var arrLocation = arrFiller.Remove(0, pointToCut).Split(',').ToList();

                if (typeCheck == "sherr")
                {
                    foreach (var item in arrLocation)
                    {
                        ErrorFPCB errorFPCB = new ErrorFPCB();
                        var i = item.IndexOf('-');
                        errorFPCB.ErrorTypes = ErrorType.SHORT;
                        errorFPCB.Location = Convert.ToInt16(item.Remove(i, item.Length - i)) / 5 + 1;
                        List.Add(errorFPCB);
                    }
                }
                else if (typeCheck == "operr")
                {
                    foreach (var item in arrLocation)
                    {
                        ErrorFPCB errorFPCB = new ErrorFPCB();
                        var i = item.IndexOf('-');
                        errorFPCB.ErrorTypes = ErrorType.OPEN;
                        errorFPCB.Location = Convert.ToInt16(item.Remove(i, item.Length - i)) / 5 + 1;
                        List.Add(errorFPCB);
                    }
                }
            }
            catch { }
            return List;
        }
        public ObservableCollection<FPCB> AnalysDataFromComPort(string shortmess, string openmess)
        {
            ObservableCollection<FPCB> List = new ObservableCollection<FPCB>();
            List<int> listShort = new List<int>();
            List<int> listOpen = new List<int>();
            try
            {
                var pointToCut = shortmess.IndexOf('/') + 1;
                var pointToCut2 = shortmess.LastIndexOf('/');
                var arrFillerS = shortmess.Remove(pointToCut2, shortmess.Length - pointToCut2);
                var arrLocationS = arrFillerS.Remove(0, pointToCut).Split(',').ToList();
                if (arrLocationS.Count != 0)
                {
                    foreach (var item in arrLocationS)
                    {
                        var i = item.IndexOf('-');
                        var j = Convert.ToInt16(item.Remove(i, item.Length - i)) / 5 + 1;
                        listShort.Add(j);
                    }
                }
                else
                {
                    listShort.Add(0);
                }
            }
            catch { }
            try
            {
                var pointToCutO = openmess.IndexOf('/') + 1;
                var pointToCut2O = openmess.LastIndexOf('/');
                var arrFillerO = openmess.Remove(pointToCut2O, openmess.Length - pointToCut2O);
                var arrLocationO = arrFillerO.Remove(0, pointToCutO).Split(',').ToList();
                if (arrLocationO.Count != 0)
                {
                    foreach (var item in arrLocationO)
                    {
                        var i = item.IndexOf('-');
                        var j = Convert.ToInt16(item.Remove(i, item.Length - i)) / 5 + 1;
                        listOpen.Add(j);
                    }
                }
                else
                {
                    listOpen.Add(0);
                }
            }
            catch { }
            for (int i = 1; i < 57; i++)
            {
                FPCB FPCB = new FPCB();
                FPCB.Id = i;
                if (listShort.Contains(i))
                {
                    FPCB.ShortError = "NG";
                }
                else
                {
                    FPCB.ShortError = "OK";
                }
                if (listOpen.Contains(i))
                {
                    FPCB.OpenError = "NG";
                }
                else
                {
                    FPCB.OpenError = "OK";
                }
                if (FPCB.OpenError == "NG" || FPCB.ShortError == "NG")
                {
                    FPCB.Judge = "NG";
                }
                else
                {
                    FPCB.Judge = "OK";
                }
                List.Add(FPCB);
            }
            return List;
        }
        public ObservableCollection<ErrorFPCB> AnalysMessOpenByOtherWay(string mess)
        {
            ObservableCollection<ErrorFPCB> List = new ObservableCollection<ErrorFPCB>();
            List<int> listP = new List<int>();
            List<int> listEP = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
                                                 11, 12 ,13, 14, 15, 16, 17, 18, 19, 20,
                                                 21, 22, 23, 24, 25, 26, 27, 28, 29, 30,
                                                 31, 32, 33, 34, 35, 36, 37, 38, 39, 40,
                                                 41, 42, 43, 44, 45, 46, 47, 48, 49, 50,
                                                 51, 52, 53, 54, 55, 56};
            try
            {                               
                var pointToCut = mess.IndexOf('/') + 1;
                var pointToCut2 = mess.LastIndexOf('/');
                var arrFiller = mess.Remove(pointToCut2, mess.Length - pointToCut2);
                var arrLocation = arrFiller.Remove(0, pointToCut).Split(',').ToList();
                if (arrLocation.Count != 0)
                {
                    foreach (var item in arrLocation)
                    {
                        var i = item.IndexOf('-');
                        var p = Convert.ToInt16(item.Remove(i, item.Length - i)) / 5 + 1;
                        listP.Add(p);
                    }
                }
                else
                {
                    listP.Add(0);
                }
            }
            catch { }
            var a = listP.Distinct().ToList();
            for(int i = 0; i < a.Count; i++)
            {
                int count = 0;
                for(int j = 0; j < listP.Count; j++)
                {
                    if(a[i] == listP[j])
                    {
                        count++;
                    }
                }
                if(count == 3)
                {
                    listEP.Remove(a[i]);
                }
            }
            foreach (var item in listEP)
            {
                ErrorFPCB errorFPCB = new ErrorFPCB();                
                errorFPCB.ErrorTypes = ErrorType.OPEN;
                errorFPCB.Location = item;
                List.Add(errorFPCB);
            }
            return List;
        }
        public ObservableCollection<FPCB> AnalysDataFromComPortByOtherWay(string shortmess, string openmess)
        {
            ObservableCollection<FPCB> List = new ObservableCollection<FPCB>();
            List<int> listShort = new List<int>();
            List<int> listP = new List<int>();
            List<int> listOpen = new List<int>(){ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10,
                                                 11, 12 ,13, 14, 15, 16, 17, 18, 19, 20,
                                                 21, 22, 23, 24, 25, 26, 27, 28, 29, 30,
                                                 31, 32, 33, 34, 35, 36, 37, 38, 39, 40,
                                                 41, 42, 43, 44, 45, 46, 47, 48, 49, 50,
                                                 51, 52, 53, 54, 55, 56};
            try
            {
                var pointToCut = shortmess.IndexOf('/') + 1;
                var pointToCut2 = shortmess.LastIndexOf('/');
                var arrFillerS = shortmess.Remove(pointToCut2, shortmess.Length - pointToCut2);
                var arrLocationS = arrFillerS.Remove(0, pointToCut).Split(',').ToList();
                if (arrLocationS.Count != 0)
                {
                    foreach (var item in arrLocationS)
                    {
                        var i = item.IndexOf('-');
                        var j = Convert.ToInt16(item.Remove(i, item.Length - i)) / 5 + 1;
                        listShort.Add(j);
                    }
                }
                else
                {
                    listShort.Add(0);
                }
            }
            catch { }
            try
            {
                var pointToCutO = openmess.IndexOf('/') + 1;
                var pointToCut2O = openmess.LastIndexOf('/');
                var arrFillerO = openmess.Remove(pointToCut2O, openmess.Length - pointToCut2O);
                var arrLocationO = arrFillerO.Remove(0, pointToCutO).Split(',').ToList();
                if (arrLocationO.Count != 0)
                {
                    foreach (var item in arrLocationO)
                    {
                        var i = item.IndexOf('-');
                        var p = Convert.ToInt16(item.Remove(i, item.Length - i)) / 5 + 1;
                        listP.Add(p);
                    }
                }
                else
                {
                    listP.Add(0);
                }
            }
            catch { }
            var a = listP.Distinct().ToList();
            for (int i = 0; i < a.Count; i++)
            {
                int count = 0;
                for (int j = 0; j < listP.Count; j++)
                {
                    if (a[i] == listP[j])
                    {
                        count++;
                    }
                }
                if (count == 3)
                {
                    listOpen.Remove(a[i]);
                }
            }
            for (int i = 1; i < 57; i++)
            {
                FPCB FPCB = new FPCB();
                FPCB.Id = i;
                if (listShort.Contains(i))
                {
                    FPCB.ShortError = "NG";
                }
                else
                {
                    FPCB.ShortError = "OK";
                }
                if (listOpen.Contains(i))
                {
                    FPCB.OpenError = "NG";
                }
                else
                {
                    FPCB.OpenError = "OK";
                }
                if (FPCB.OpenError == "NG" || FPCB.ShortError == "NG")
                {
                    FPCB.Judge = "NG";
                }
                else
                {
                    FPCB.Judge = "OK";
                }
                List.Add(FPCB);
            }
            return List;
        }
        #endregion
    }
}
//new version
