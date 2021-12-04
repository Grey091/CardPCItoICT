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
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace SYNOPEX_ICT.ViewModels
{
    class ResultsWindowVM : BaseVM
    {
        public int[] m_lMoveMultiAxes = { -1, -1, -1, -1, -1, -1 };
        public int m_lAxisCounts = 0;                               // Khai báo và khởi tạo số lượng trục có thể điều khiển
        private int m_lAxisNo = 0;                                  // Khai báo và khởi tạo số trục được điều khiển
        public uint m_uModuleID = 0;                                // Khai báo và khởi tạo mô-đun I / O của trục được điều khiển
        public uint m_lMoveMultiAxesCount = 0;
        public int m_lBoardNo = 0, m_lModulePos = 0;
        List<string> listMotionModule;
        bool isCheckServoOn = true;
        bool chkMoveBlock = true;

        DispatcherTimer TimerFollowSignalFromIO;
        DigitalIOVM digitalIOVM;
        DigitalIO digitalIOWD;

        double dCmdPos = 0.0;
        double dCmdPosX = 0.0;
        double dCmdPosY = 0.0;

        #region cmds
        public ICommand OnkepCommand { get; set; }
        public ICommand OffkepCommand { get; set; }
        public ICommand FixWidthConveyorCommandPlus { get; set; }
        public ICommand FixWidthConveyorCommandMinus { get; set; }
        public ICommand FixPotionBaseCommandPlus { get; set; }
        public ICommand FixPotionBaseCommandMinus { get; set; }
        public ICommand StopFixConveyorCommand { get; set; }
        public ICommand StopFixPotionBaseCommand { get; set; }
        public ICommand StopCommand { get; set; }

        public ICommand ChangeModelommand { get; set; }
        public ICommand PutModelCommand { get; set; }
        public ICommand HomeMoveCommand { get; set; }
        public ICommand StopJogX { get; set; }
        public ICommand StopJogY { get; set; }
        public ICommand StopJogZ { get; set; }
        public ICommand MoveJogXPlus { get; set; }
        public ICommand MoveJogXMinus { get; set; }
        public ICommand MoveJogYPlus { get; set; }
        public ICommand MoveJogYMinus { get; set; }
        public ICommand MoveJogZPlus { get; set; }
        public ICommand MoveJogZMinus { get; set; }

        public ICommand OnBangtaiCommand { get; set; }
        public ICommand OnCanVatCommand { get; set; }
        public ICommand OnNangVatCommand { get; set; }
        public ICommand OnNutNhanCommand { get; set; }

        public ICommand OffBangtaiCommand { get; set; }
        public ICommand OffCanVatCommand { get; set; }
        public ICommand OffNangVatCommand { get; set; }
        public ICommand OffNutNhanCommand { get; set; }

        public ICommand JogMarkingXPlusCommand { get; set; }
        public ICommand JogMarkingXMinusCommand { get; set; }
        public ICommand JogMarkingXStopCommand { get; set; }
        public ICommand JogMarkingXSaveCommand { get; set; }

        public ICommand JogMarkingYPlusCommand { get; set; }
        public ICommand JogMarkingYMinusCommand { get; set; }
        public ICommand JogMarkingYStopCommand { get; set; }
        public ICommand JogMarkingYSaveCommand { get; set; }

        public ICommand PrintMarkingCommand { get; set; }
        #endregion

        #region binding
        private string _Mess6;
        public string Mess6
        {
            get => _Mess6;
            set { _Mess6 = value; OnPropertyChanged("Mess6"); }
        }
        public ResultsWindowVM(NavigationStore navigationStore) { }

        private ObservableCollection<ErrorFPCB> _ListData;
        public ObservableCollection<ErrorFPCB> ListData
        {
            get => _ListData;
            set
            {
                _ListData = value;
                OnPropertyChanged("ListData");
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
        private bool _isEnableButton;
        public bool IsEnableButton
        {
            get => _isEnableButton;
            set
            {
                _isEnableButton = value;
                OnPropertyChanged("IsEnableButton");
            }
        }
        private bool _isEnableButtonXLN;
        public bool IsEnableButtonXLN
        {
            get => _isEnableButtonXLN;
            set
            {
                _isEnableButtonXLN = value;
                OnPropertyChanged("IsEnableButtonXLN");
            }
        }
        private double _jogVelocity1;
        public double jogVelocity1
        {
            get => _jogVelocity1;
            set { _jogVelocity1 = value; OnPropertyChanged("jogVelocity1"); }
        }
        private double _jogAccel1;
        public double jogAccel1
        {
            get => _jogAccel1;
            set { _jogAccel1 = value; OnPropertyChanged("jogAccel1"); }
        }
        private double _jogDecel1;
        public double jogDecel1
        {
            get => _jogDecel1;
            set { _jogDecel1 = value; OnPropertyChanged("jogDecel1"); }
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
        #endregion
        public ResultsWindowVM()
        {
            #region change model
            FixWidthConveyorCommandPlus = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                MoveMotorByJog(5, 1, 2);
            });
            FixWidthConveyorCommandMinus = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                MoveMotorByJog(5, -1, 2);
            });
            FixPotionBaseCommandPlus = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                MoveMotorByJog(4, 1, 2);
            });
            FixPotionBaseCommandMinus = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                MoveMotorByJog(4, -1, 2);
            });
            StopFixConveyorCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                //dung motor
                StopMotorAll(4, 6);
            });
            StopFixPotionBaseCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                //dung motor
                StopMotorAll(4, 6);
            });
            StopCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                //dung motor
                StopMotorAll(0, 6);
            });

            ChangeModelommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                DigitalIO DigitalIOWD = new DigitalIO();

                if (DigitalIOWD.DataContext == null)
                {
                    return;
                }
                else
                {
                    var digitalIOVM = DigitalIOWD.DataContext as DigitalIOVM;
                    digitalIOVM.out4 = true;
                }
            });
            PutModelCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                DigitalIO DigitalIOWD = new DigitalIO();

                if (DigitalIOWD.DataContext == null)
                {
                    return;
                }
                else
                {
                    var digitalIOVM = DigitalIOWD.DataContext as DigitalIOVM;
                    digitalIOVM.out4 = false;
                }
            });
            #endregion

            #region Jog
            HomeMoveCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                //bat den xanh
                DigitalIO DigitalIOWD = new DigitalIO();

                if (DigitalIOWD.DataContext == null)
                {
                    return;
                }
                else
                {
                    var digitalIOVM = DigitalIOWD.DataContext as DigitalIOVM;
                    digitalIOVM.out6a = false;
                    digitalIOVM.out5a = true;
                    digitalIOVM.out4a = false;
                    digitalIOVM.out7 = false;
                }

                //check trang thai dong co
                AllCheckServoIsOn();

                //ve Home Z
                FindHomePoint(0, 1, 2, 3, 100, 50, 20, 1, 100, 100);
                WaitFindHomePoint(2, 3);      

            });
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
                MoveMotorByJog(1, 1, 5);
            });
            MoveJogXMinus = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MoveMotorByJog(1, -1, 5);
            });
            MoveJogYPlus = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MoveMotorByJog(0, 1, 5);
            });
            MoveJogYMinus = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MoveMotorByJog(0, -1, 5);
            });
            MoveJogZPlus = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MoveMotorByJog(2, 1, 50);
            });
            MoveJogZMinus = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MoveMotorByJog(2, -1, 50);
            });
            #endregion

            #region IO
            OnBangtaiCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DigitalIO DigitalIOWD = new DigitalIO();

                if (DigitalIOWD.DataContext == null)
                {
                    return;
                }
                else
                {
                    var digitalIOVM = DigitalIOWD.DataContext as DigitalIOVM;
                    digitalIOVM.out9 = true;
                }
            });
            OnCanVatCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DigitalIO DigitalIOWD = new DigitalIO();

                if (DigitalIOWD.DataContext == null)
                {
                    return;
                }
                else
                {
                    var digitalIOVM = DigitalIOWD.DataContext as DigitalIOVM;
                    digitalIOVM.out6 = true;
                }
            });
            OnNangVatCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DigitalIO DigitalIOWD = new DigitalIO();

                if (DigitalIOWD.DataContext == null)
                {
                    return;
                }
                else
                {
                    var digitalIOVM = DigitalIOWD.DataContext as DigitalIOVM;
                    digitalIOVM.out7 = true;
                }
            });
            OnNutNhanCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DigitalIO DigitalIOWD = new DigitalIO();

                if (DigitalIOWD.DataContext == null)
                {
                    return;
                }
                else
                {
                    var digitalIOVM = DigitalIOWD.DataContext as DigitalIOVM;
                    digitalIOVM.out5 = true;
                }
            });


            OffBangtaiCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DigitalIO DigitalIOWD = new DigitalIO();

                if (DigitalIOWD.DataContext == null)
                {
                    return;
                }
                else
                {
                    var digitalIOVM = DigitalIOWD.DataContext as DigitalIOVM;
                    digitalIOVM.out9 = false;
                }
            });
            OffCanVatCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DigitalIO DigitalIOWD = new DigitalIO();

                if (DigitalIOWD.DataContext == null)
                {
                    return;
                }
                else
                {
                    var digitalIOVM = DigitalIOWD.DataContext as DigitalIOVM;
                    digitalIOVM.out6 = false;
                    digitalIOVM.out8a = false;
                }
            });
            OffNangVatCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DigitalIO DigitalIOWD = new DigitalIO();

                if (DigitalIOWD.DataContext == null)
                {
                    return;
                }
                else
                {
                    var digitalIOVM = DigitalIOWD.DataContext as DigitalIOVM;
                    digitalIOVM.out7 = false;
                }
            });
            OffNutNhanCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                DigitalIO DigitalIOWD = new DigitalIO();

                if (DigitalIOWD.DataContext == null)
                {
                    return;
                }
                else
                {
                    var digitalIOVM = DigitalIOWD.DataContext as DigitalIOVM;
                    digitalIOVM.out5 = false;
                }
            });
            #endregion

            #region init
            digitalIOWD = new DigitalIO();
            if (digitalIOWD.DataContext == null)
            {
                return;
            }
            else
            {
                digitalIOVM = digitalIOWD.DataContext as DigitalIOVM;

            }

            listMotionModule = new List<string>();

            //InitLibrary();
            AddAxisInfo();
            UpdateState();
            //check trang thai dong co
            AllCheckServoIsOn();
            

            TimerFollowSignalFromIO = new DispatcherTimer();
            TimerFollowSignalFromIO.Interval = TimeSpan.FromMilliseconds(100);
            TimerFollowSignalFromIO.Tick += TimerFollowSignalFromIO_Tick;
            TimerFollowSignalFromIO.Start();
            #endregion

            #region fix marking
            //kep
            OnkepCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                var ListToSend = new List<byte>();
                //%01-----%#= 
                var d = "%$";

                var c = Encoding.ASCII.GetBytes(d);

                foreach (var i in c)
                {
                    ListToSend.Add(i);
                }
                ListToSend.Add(56);
                for (int i = 0; i < 6; i++)
                {
                    ListToSend.Add(0);
                }
                foreach (var i in c)
                {
                    ListToSend.Add(i);
                }
                var arrSend = ListToSend.ToArray();
                ScreenWorkVM.COM_Marking.Write(arrSend, 0, arrSend.Length);
            });
            OffkepCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                var ListToSend = new List<byte>();
                //%01-----%#= 
                var d = "%$";

                var c = Encoding.ASCII.GetBytes(d);

                foreach (var i in c)
                {
                    ListToSend.Add(i);
                }
                ListToSend.Add(104);
                for (int i = 0; i < 6; i++)
                {
                    ListToSend.Add(0);
                }
                foreach (var i in c)
                {
                    ListToSend.Add(i);
                }
                var arrSend = ListToSend.ToArray();
                ScreenWorkVM.COM_Marking.Write(arrSend, 0, arrSend.Length);
            });
            //x
            JogMarkingXPlusCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                var ListToSend = new List<byte>();
                //%01-----%#= 
                var d = "%$";

                var c = Encoding.ASCII.GetBytes(d);

                foreach (var i in c)
                {
                    ListToSend.Add(i);
                }
                ListToSend.Add(8);
                for (int i = 0; i < 6; i++)
                {
                    ListToSend.Add(0);
                }
                foreach (var i in c)
                {
                    ListToSend.Add(i);
                }
                var arrSend = ListToSend.ToArray();
                ScreenWorkVM.COM_Marking.Write(arrSend, 0, arrSend.Length);
            });
            JogMarkingXMinusCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                var ListToSend = new List<byte>();
                //%01-----%#= 
                var d = "%$";

                var c = Encoding.ASCII.GetBytes(d);

                foreach (var i in c)
                {
                    ListToSend.Add(i);
                }
                ListToSend.Add(16);
                for (int i = 0; i < 6; i++)
                {
                    ListToSend.Add(0);
                }
                foreach (var i in c)
                {
                    ListToSend.Add(i);
                }
                var arrSend = ListToSend.ToArray();
                ScreenWorkVM.COM_Marking.Write(arrSend, 0, arrSend.Length);
            });
            JogMarkingXStopCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                var ListToSend = new List<byte>();
                //%01-----%#= 
                var d = "%$";

                var c = Encoding.ASCII.GetBytes(d);

                foreach (var i in c)
                {
                    ListToSend.Add(i);
                }
                ListToSend.Add(0);
                for (int i = 0; i < 6; i++)
                {
                    ListToSend.Add(0);
                }
                foreach (var i in c)
                {
                    ListToSend.Add(i);
                }
                var arrSend = ListToSend.ToArray();
                ScreenWorkVM.COM_Marking.Write(arrSend, 0, arrSend.Length);
            });
            JogMarkingXSaveCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                var ListToSend = new List<byte>();
                //%01-----%#= 
                var d = "%$";

                var c = Encoding.ASCII.GetBytes(d);

                foreach (var i in c)
                {
                    ListToSend.Add(i);
                }
                ListToSend.Add(128);
                for (int i = 0; i < 6; i++)
                {
                    ListToSend.Add(0);
                }
                foreach (var i in c)
                {
                    ListToSend.Add(i);
                }
                var arrSend = ListToSend.ToArray();
                ScreenWorkVM.COM_Marking.Write(arrSend, 0, arrSend.Length);
            });

            //y
            JogMarkingYPlusCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                var ListToSend = new List<byte>();
                //%01-----%#= 
                var d = "%$";

                var c = Encoding.ASCII.GetBytes(d);

                foreach (var i in c)
                {
                    ListToSend.Add(i);
                }
                ListToSend.Add(32);
                for (int i = 0; i < 6; i++)
                {
                    ListToSend.Add(0);
                }
                foreach (var i in c)
                {
                    ListToSend.Add(i);
                }
                var arrSend = ListToSend.ToArray();
                ScreenWorkVM.COM_Marking.Write(arrSend, 0, arrSend.Length);
            });
            JogMarkingYMinusCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                var ListToSend = new List<byte>();
                //%01-----%#= 
                var d = "%$";

                var c = Encoding.ASCII.GetBytes(d);

                foreach (var i in c)
                {
                    ListToSend.Add(i);
                }
                ListToSend.Add(64);
                for (int i = 0; i < 6; i++)
                {
                    ListToSend.Add(0);
                }
                foreach (var i in c)
                {
                    ListToSend.Add(i);
                }
                var arrSend = ListToSend.ToArray();
                ScreenWorkVM.COM_Marking.Write(arrSend, 0, arrSend.Length);
            });
            JogMarkingYStopCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                var ListToSend = new List<byte>();
                //%01-----%#= 
                var d = "%$";

                var c = Encoding.ASCII.GetBytes(d);

                foreach (var i in c)
                {
                    ListToSend.Add(i);
                }
                ListToSend.Add(0);
                for (int i = 0; i < 6; i++)
                {
                    ListToSend.Add(0);
                }
                foreach (var i in c)
                {
                    ListToSend.Add(i);
                }
                var arrSend = ListToSend.ToArray();
                ScreenWorkVM.COM_Marking.Write(arrSend, 0, arrSend.Length);
            });
            JogMarkingYSaveCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                var ListToSend = new List<byte>();
                //%01-----%#= 
                var d = "%$";

                var c = Encoding.ASCII.GetBytes(d);

                foreach (var i in c)
                {
                    ListToSend.Add(i);
                }
                ListToSend.Add(24);
                for (int i = 0; i < 6; i++)
                {
                    ListToSend.Add(0);
                }
                foreach (var i in c)
                {
                    ListToSend.Add(i);
                }
                var arrSend = ListToSend.ToArray();
                ScreenWorkVM.COM_Marking.Write(arrSend, 0, arrSend.Length);
            });

            //in muc
            PrintMarkingCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                var ListToSend = new List<byte>();
                //%01-----%#= 
                var d = "%$";

                var c = Encoding.ASCII.GetBytes(d);

                foreach (var i in c)
                {
                    ListToSend.Add(i);
                }
                ListToSend.Add(96);
                for (int i = 0; i < 6; i++)
                {
                    ListToSend.Add(0);
                }
                foreach (var i in c)
                {
                    ListToSend.Add(i);
                }
                var arrSend = ListToSend.ToArray();
                ScreenWorkVM.COM_Marking.Write(arrSend, 0, arrSend.Length);
            });
            #endregion
        }
        private void TimerFollowSignalFromIO_Tick(object sender, EventArgs e)
        {        

            // kiem tra vi tri hien tai cua Z
                
            CAXM.AxmStatusGetCmdPos(m_lMoveMultiAxes[2], ref dCmdPos);
            feedBackPos3 = Convert.ToDouble(String.Format("{0:0.000}", dCmdPos));
            if (dCmdPos > 190 || digitalIOVM.out7 == true)
            {
                IsEnableButton = false;
            }
            else
            {
                IsEnableButton = true;
            }

               
            CAXM.AxmStatusGetCmdPos(m_lMoveMultiAxes[1], ref dCmdPosX);
            feedBackPos2 = Convert.ToDouble(String.Format("{0:0.000}", dCmdPosX));

                
            CAXM.AxmStatusGetCmdPos(m_lMoveMultiAxes[0], ref dCmdPosY);
            feedBackPos1 = Convert.ToDouble(String.Format("{0:0.000}", dCmdPosY));
            if(feedBackPos2 != Properties.Settings.Default.targetDefaultX || feedBackPos1 != Properties.Settings.Default.targetDefaultY)
            {
                IsEnableButtonXLN = false;
            }             
            else
            {
                IsEnableButtonXLN = true;
            }
        }
        #region func
        public void MoveMotorUTByPosition()
        {            

        }
        public void UpdateState()
        {          

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
                MovePos[lCount] = -27.5;
                MoveVel[lCount] = 20.000;
                MoveAcc[lCount] = 40;
                MoveDec[lCount] = 40;
                JogVel[lCount] = 100;
                JogAcc[lCount] = 40;
                JogDec[lCount] = 40;
                LinePos[lCount] = 100.000;
            }
            

            jogVelocity1 = JogVel[0]; 
            jogAccel1 = JogAcc[0]; 
            jogDecel1 = JogDec[0]; 
           

            
        }
        public void InitLibrary()
        {
            String szFilePath = "C:\\Users\\IPC-7132\\Desktop\\flieMot2508.mot";
            //++ AXL(AjineXtek Library)Bật và khởi tạo các bảng được gắn kết.
            if (CAXL.AxlOpen(7) != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
            {
               
                Mess6 = "Intialize Fail..!!";
            }
            if (CAXL.AxlOpen(7) != (uint)AXT_FUNC_RESULT.AXT_RT_OPEN_ALREADY)
            {
                var a = CAXL.AxlOpen(7);
                Mess6 = "READY";
            }
            //++ Thay đổi hàng loạt và áp dụng các giá trị cài đặt của bảng chuyển động với các giá trị cài đặt của tệp Mot được chỉ định.
            if (CAXM.AxmMotLoadParaAll(szFilePath) != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
            {
                //MessageBox.Show("Mot File Not Found.");
                Mess6 = "Mot File Not Found.";
                //Dispatcher.CurrentDispatcher.Invoke(() => { MessageBox.Show("Intialize Fail..!!"); });
            }
        }
        public void ClearPositionMotor()
        {
            uint lCount;

            for (lCount = 0; lCount < m_lMoveMultiAxesCount; lCount++)
                //CAXM.AxmStatusSetCmdPos(m_lMoveIntpAxes[lCount], 0);
                //CAXM.AxmStatusSetActPos(m_lMoveIntpAxes[lCount], 0);
                //++ Command위치와 Actual위치를 입력한 값으로 설정합니다.
                CAXM.AxmStatusSetPosMatch(m_lMoveMultiAxes[lCount], 0.0);
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
        public void FindHomePoint(int iHomeDir, uint duHomeSignal, uint step, uint range, double dVelFirst, double dVelSecond, double dVelThird, double dVelLast, double dAccFirst, double dAccSecond)
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
        public void MoveMotorByJog(int NoLAxis, double direction, double Vel)
        {
            uint duRetCode = 0;

            double JogVel = Vel * direction;

            double JogAcc = jogAccel1;

            double JogDec = jogDecel1;

            //++ Trục xác định được di chuyển theo hướng (+) với tốc độ / gia tốc / giảm tốc xác định.
            duRetCode = CAXM.AxmMoveVel(m_lMoveMultiAxes[NoLAxis], JogVel, JogAcc, JogDec);
            if (duRetCode != (uint)AXT_FUNC_RESULT.AXT_RT_SUCCESS)
                MessageBox.Show(String.Format("AxmMoveStartMultiPos return error[Code:{0:d}]", duRetCode));
        }
        #endregion
    }
}
