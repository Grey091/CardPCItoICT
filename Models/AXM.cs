/****************************************************************************
*****************************************************************************
**
** File Name
** ---------
**
** AXM.CS
**
** COPYRIGHT (c) AJINEXTEK Co., LTD
**
*****************************************************************************
*****************************************************************************
**
** Description
** -----------
** Ajinextek Motion Library Header File
** 
**
*****************************************************************************
*****************************************************************************
**
** Source Change Indices
** ---------------------
**
** (None)
**
*****************************************************************************
*****************************************************************************
**
** Website
** ---------------------
**
** http://www.ajinextek.com
**
*****************************************************************************
*****************************************************************************
*/

using System.Runtime.InteropServices;

public class CAXM
{
    
//========== Board and module verification API(Info) - Information =================================================================================

    // Return board number, module position and module ID of relevant axis. 
    [DllImport("AXL.dll")] public static extern uint AxmInfoGetAxis(int nAxisNo, ref int npBoardNo, ref int npModulePos, ref uint upModuleID);
    // Return whether the motion module exists.
    [DllImport("AXL.dll")] public static extern uint AxmInfoIsMotionModule(ref uint upStatus);
    // Return whether relevant axis is valid.
    [DllImport("AXL.dll")] public static extern uint AxmInfoIsInvalidAxisNo(int nAxisNo);
    // Return whether relevant axis status.     
    [DllImport("AXL.dll")] public static extern uint AxmInfoGetAxisStatus(int nAxisNo);
    // number of RTEX Products, return number of valid axis installed in system.
    [DllImport("AXL.dll")] public static extern uint AxmInfoGetAxisCount(ref int npAxisCount);
    // Return the first axis number of relevant board/module 
    [DllImport("AXL.dll")] public static extern uint AxmInfoGetFirstAxisNo(int nBoardNo, int nModulePos, ref int npAxisNo);
    // Return the first axis number of relevant board
    [DllImport("AXL.dll")] public static extern uint AxmInfoGetBoardFirstAxisNo(int lBoardNo, int lModulePos, ref int lpAxisNo);
    
//========= virtual axis function ============================================================================================    
    // Set virtual axis.
    [DllImport("AXL.dll")] public static extern uint AxmVirtualSetAxisNoMap(int nRealAxisNo, int nVirtualAxisNo);
    // Return the set virtual channel(axis) number. 
    [DllImport("AXL.dll")] public static extern uint AxmVirtualGetAxisNoMap(int nRealAxisNo, ref int npVirtualAxisNo);
    // Set multi-virtual axes. 
    [DllImport("AXL.dll")] public static extern uint AxmVirtualSetMultiAxisNoMap(int nSize, ref int npRealAxesNo, ref int npVirtualAxesNo);
    // Return the set multi-virtual channel(axis) number.
    [DllImport("AXL.dll")] public static extern uint AxmVirtualGetMultiAxisNoMap(int nSize, ref int npRealAxesNo, ref int npVirtualAxesNo);
    // Reset the virtual axis setting.
    [DllImport("AXL.dll")] public static extern uint AxmVirtualResetAxisMap();

//========= API related interrupt ======================================================================================
    // Call-back API method has the advantage which can be advised the event most fast timing as the call-back API is called immediately when the event occurs, but  
    // the main processor shall be congested until the call-back API is completed. 
    // i.e, it shall be carefully used when there is any work loaded in the call-bak API. 
    // Event method monitors if interrupt occurs continuously by using thread, and when interrupt is occurs 
    // it manages, and even though this method has disadvantage which system resource is occupied by thread , 
    // it can detect interrupt most quickly and manage it. 
    // It is not used a lot in general, but used when quick management of interrupt is the most concern. 
    // Event method is operated using specific thread which monitors the occurrence of event separately from main processor , so 
    // it able to use the resources efficiently in multi-processor system and expressly recommendable method. 

    // Window message or call back API is used for getting the interrupt message. 
    // (message handle, message ID, call back API, interrupt event)
    //    hWnd    : use to get window handle and window message. Enter NULL if it is not used.
    //    wMsg    : message of window handle, enter 0 if is not used or default value is used. 
    //    proc    : API pointer to be called when interrupted, enter NULL if not use 
    //    pEvent  : Event handle when event method is used. 
    [DllImport("AXL.dll")] public static extern uint AxmInterruptSetAxis(int nAxisNo, uint hWnd, uint uMessage, CAXHS.AXT_INTERRUPT_PROC pProc, ref uint pEvent);
    
    // Set whether to use interrupt of set axis or not. 
    // Set interrupt in the relevant axis/ verification
     // uUse : use or not use => DISABLE(0), ENABLE(1)
    [DllImport("AXL.dll")] public static extern uint AxmInterruptSetAxisEnable(int nAxisNo, uint uUse);
    // Return whether to use interrupt of set axis or not
    [DllImport("AXL.dll")] public static extern uint AxmInterruptGetAxisEnable(int nAxisNo, ref uint upUse);

    //Read relevant information when interrupt is used in event method
    [DllImport("AXL.dll")] public static extern uint AxmInterruptRead(ref int npAxisNo, ref uint upFlag);
    
    // Return interrupt flag value of relevant axis.
    [DllImport("AXL.dll")] public static extern uint AxmInterruptReadAxisFlag(int nAxisNo, int nBank, ref uint upFlag);

    // Set whether the interrupt set by user to specific axis occurs or not
    // lBank         : Enable to set interrupt bank number(0 - 1).
    // uInterruptNum : Enable to set interrupt number by setting bit number( 0 - 31 ).
    [DllImport("AXL.dll")] public static extern uint AxmInterruptSetUserEnable(int nAxisNo, int lBank, uint uInterruptNum);
    
    // Verify whether the interrupt set by user of specific axis occurs or not
    [DllImport("AXL.dll")] public static extern uint AxmInterruptGetUserEnable(int nAxisNo, int lBank, ref uint upInterruptNum);
    
    
//======== set motion parameter ===========================================================================================================================================================
    // If file is not loaded by AxmMotLoadParaAll, set default parameter in initial parameter setting. 
    // Apply to all axes which is being used in PC equally. Default parameters are as below. 
    // 00:AXIS_NO.             =0       01:PULSE_OUT_METHOD.    =4      02:ENC_INPUT_METHOD.    =3     03:INPOSITION.          =2
    // 04:ALARM.               =0       05:NEG_END_LIMIT.       =0      06:POS_END_LIMIT.       =0     07:MIN_VELOCITY.        =1
    // 08:MAX_VELOCITY.        =700000  09:HOME_SIGNAL.         =4      10:HOME_LEVEL.          =1     11:HOME_DIR.            =-1
    // 12:ZPHASE_LEVEL.        =1       13:ZPHASE_USE.          =0      14:STOP_SIGNAL_MODE.    =0     15:STOP_SIGNAL_LEVEL.   =0
    // 16:HOME_FIRST_VELOCITY. =10000   17:HOME_SECOND_VELOCITY.=10000  18:HOME_THIRD_VELOCITY. =2000  19:HOME_LAST_VELOCITY.  =100
    // 20:HOME_FIRST_ACCEL.    =40000   21:HOME_SECOND_ACCEL.   =40000  22:HOME_END_CLEAR_TIME. =1000  23:HOME_END_OFFSET.     =0
    // 24:NEG_SOFT_LIMIT.      =0.000   25:POS_SOFT_LIMIT.      =0      26:MOVE_PULSE.          =1     27:MOVE_UNIT.           =1
    // 28:INIT_POSITION.       =1000    29:INIT_VELOCITY.       =200    30:INIT_ACCEL.          =400   31:INIT_DECEL.          =400
    // 32:INIT_ABSRELMODE.     =0       33:INIT_PROFILEMODE.    =4

    // 00=[AXIS_NO             ]: axis (start from 0axis)
    // 01=[PULSE_OUT_METHOD    ]: Pulse out method TwocwccwHigh = 6
    // 02=[ENC_INPUT_METHOD    ]: disable = 0   1 multiplication = 1  2 multiplication = 2  4 multiplication = 3, for replacing the direction of splicing (-).1 multiplication = 11  2 multiplication = 12  4 multiplication = 13
    // 03=[INPOSITION          ], 04=[ALARM     ], 05,06 =[END_LIMIT   ]  : 0 = A contact 1= B contact 2 = not use. 3 = keep current mode
    // 07=[MIN_VELOCITY        ]: START VELOCITY
    // 08=[MAX_VELOCITY        ]: command velocity which driver can accept. Generally normal servo is 700k
    // Ex> screw : 20mm pitch drive: 10000 pulse motor: 400w
    // 09=[HOME_SIGNAL         ]: 4 - Home in0 , 0 :PosEndLimit , 1 : NegEndLimit // refer _HOME_SIGNAL.
    // 10=[HOME_LEVEL          ]: : 0 = A contact 1= B contact 2 = not use. 3 = keep current mode
    // 11=[HOME_DIR            ]: HOME DIRECTION 1:+direction, 0:-direction
    // 12=[ZPHASE_LEVEL        ]: : 0 = A contact 1= B contact 2 = not use. 3 = keep current mode
    // 13=[ZPHASE_USE          ]: use of Z phase. 0: not use , 1: - direction, 2: +direction 
    // 14=[STOP_SIGNAL_MODE    ]: ESTOP, mode in use of SSTOP  0:slowdown stop, 1:emergency stop 
    // 15=[STOP_SIGNAL_LEVEL   ]: ESTOP, SSTOP use level. : 0 = A contact 1= B contact 2 = not use. 3 = keep current mode 
    // 16=[HOME_FIRST_VELOCITY ]: 1st move velocity 
    // 17=[HOME_SECOND_VELOCITY]: velocity after detecting 
    // 18=[HOME_THIRD_VELOCITY ]: the last velocity 
    // 19=[HOME_LAST_VELOCITY  ]: velocity for index detecting and detail detecting 
    // 20=[HOME_FIRST_ACCEL    ]: 1st acceleration, 21=[HOME_SECOND_ACCEL   ] : 2nd acceleration 
    // 22=[HOME_END_CLEAR_TIME ]: queue time to set origin detecting Enc value,  23=[HOME_END_OFFSET] : move as much as offset after detecting of origin.
    // 24=[NEG_SOFT_LIMIT      ]: - not use if set same as SoftWare Limit , 25=[POS_SOFT_LIMIT ]: - not use if set same as SoftWare Limit.
    // 26=[MOVE_PULSE          ]: amount of pulse per driver revolution               , 27=[MOVE_UNIT  ]: travel distance per driver revolution :screw Pitch
    // 28=[INIT_POSITION       ]: initial position when use agent , user can use optionally
    // 29=[INIT_VELOCITY       ]: initial velocity when use agent, user can use optionally
    // 30=[INIT_ACCEL          ]: initial acceleration when use agent, user can use optionally
    // 31=[INIT_DECEL          ]: initial deceleration when use agent, user can use optionally
    // 32=[INIT_ABSRELMODE     ]: absolute(0)/relative(1) set position
    // 33=[INIT_PROFILEMODE    ]: set profile mode in (0 - 4) 
    //                            '0': symmetry Trapezode, '1': asymmetric Trapezode, '2': symmetry Quasi-S Curve, '3':symmetry S Curve, '4':asymmetric S Curve
    
    // load .mot file which is saved as AxmMotSaveParaAll. Optional modification is available by user. 
    [DllImport("AXL.dll")] public static extern uint AxmMotLoadParaAll(string szFilePath);
    // Save all parameter for all current axis by axis. Save as .mot file. Load file by using  AxmMotLoadParaAll. 
    [DllImport("AXL.dll")] public static extern uint AxmMotSaveParaAll(string szFilePath);
    
    // In parameter 28 - 31, user sets by using this API in the program. 
    [DllImport("AXL.dll")] public static extern uint AxmMotSetParaLoad(int nAxisNo, double InitPos, double InitVel, double InitAccel, double InitDecel);    
    // In parameter 28 - 31, user verifys by using this API in the program.
    [DllImport("AXL.dll")] public static extern uint AxmMotGetParaLoad(int nAxisNo, ref double InitPos, ref double InitVel, ref double InitAccel, ref double InitDecel);    

    //uMethod  0 :OneHighLowHigh, 1 :OneHighHighLow, 2 :OneLowLowHigh, 3 :OneLowHighLow, 4 :TwoCcwCwHigh
    //         5 :TwoCcwCwLow,    6 :TwoCwCcwHigh,   7 :TwoCwCcwLow,   8 :TwoPhase,      9 :TwoPhaseReverse
    //    OneHighLowHigh          = 0x0,        // 1 pulse method, PULSE(Active High), forward direction(DIR=Low)  / reverse direction(DIR=High)
    //    OneHighHighLow          = 0x1,        // 1 pulse method, PULSE(Active High), forward direction (DIR=High) / reverse direction (DIR=Low)
    //    OneLowLowHigh           = 0x2,        // 1 pulse method, PULSE(Active Low), forward direction (DIR=Low)  / reverse direction (DIR=High)
    //    OneLowHighLow           = 0x3,        // 1 pulse method, PULSE(Active Low), forward direction (DIR=High) / reverse direction (DIR=Low)
    //    TwoCcwCwHigh            = 0x4,        // 2 pulse method, PULSE(CCW: reverse direction),  DIR(CW: forward direction),  Active High
    //    TwoCcwCwLow             = 0x5,        // 2 pulse method, PULSE(CCW: reverse direction),  DIR(CW: forward direction),  Active Low
    //    TwoCwCcwHigh            = 0x6,        // 2 pulse method, PULSE(CW: forward direction),   DIR(CCW: reverse direction), Active High
    //    TwoCwCcwLow             = 0x7,        // 2 pulse method, PULSE(CW: forward direction),   DIR(CCW: reverse direction), Active Low
    //    TwoPhase                = 0x8,        // 2 phase (90' phase difference),  PULSE lead DIR(CW: forward direction), PULSE lag DIR(CCW: reverse direction)
    //    TwoPhaseReverse         = 0x9         // 2 phase(90' phase difference),  PULSE lead DIR(CCW: Forward diredtion), PULSE lag DIR(CW: Reverse direction)
    // Set the pulse output method of specific axis.
    [DllImport("AXL.dll")] public static extern uint AxmMotSetPulseOutMethod(int nAxisNo, uint uMethod);
    // Return the setting of pulse output method of specific axis. 
    [DllImport("AXL.dll")] public static extern uint AxmMotGetPulseOutMethod(int nAxisNo, ref uint upMethod);

    // Set the Encoder input method including the setting of increase direction of actual count of specific axis. 
    //    ObverseUpDownMode       = 0x0,        // Forward direction Up/Down
    //    ObverseSqr1Mode         = 0x1,        // Forward direction 1 multiplication
    //    ObverseSqr2Mode         = 0x2,        // Forward direction 2 multiplication
    //    ObverseSqr4Mode         = 0x3,        // Forward direction 4 multiplication
    //    ReverseUpDownMode       = 0x4,        // Reverse direction Up/Down
    //    ReverseSqr1Mode         = 0x5,        // Reverse direction 1 multiplication
    //    ReverseSqr2Mode         = 0x6,        // Reverse direction 2 multiplication
    //    ReverseSqr4Mode         = 0x7         // Reverse direction 4 multiplication
    [DllImport("AXL.dll")] public static extern uint AxmMotSetEncInputMethod(int nAxisNo, uint uMethod);
    // Return the Encoder input method including the setting of increase direction of actual count of specific axis.
    [DllImport("AXL.dll")] public static extern uint AxmMotGetEncInputMethod(int nAxisNo, ref uint upMethod);

    // If you want to set specified velocity unit in RPM(Revolution Per Minute),
    // ex>    calculate rpm :
    // 4500 rpm ?
    // When unit/ pulse = 1 : 1, then it becomes pulse per sec, and
    // if you want to set at 4500 rpm , then  4500 / 60 sec : 75 revolution / 1sec
    // The number of pulse per 1 revolution of motor shall be known. This can be know by detecting of Z phase in Encoder. 
    // If 1 revolution:1800 pulse,  75 x 1800 = 135000 pulses are required. 
    // Operate by input Unit = 1, Pulse = 1800 into AxmMotSetMoveUnitPerPulse.
    // Caution : If it is controlled with rpm, velocity and acceleration will be changed to rpm unit as well. 

    // Set the travel distance of specific axis per pulse. 
    [DllImport("AXL.dll")] public static extern uint AxmMotSetMoveUnitPerPulse(int nAxisNo, double dUnit, int nPulse);
    // Return the travel distance of specific axis per pulse.
    [DllImport("AXL.dll")] public static extern uint AxmMotGetMoveUnitPerPulse(int nAxisNo, ref double dpUnit, ref int npPulse);

    // Set deceleration starting point detecting method to specific axis. 
    // AutoDetect 0x0 : automatic acceleration/deceleration.
    // RestPulse  0x1 : manual acceleration/deceleration."
    [DllImport("AXL.dll")] public static extern uint AxmMotSetDecelMode(int nAxisNo, uint uMethod);
    // Return the deceleration starting point detecting method of specific axis.
    [DllImport("AXL.dll")] public static extern uint AxmMotGetDecelMode(int nAxisNo, ref uint upMethod);
    
    // Set remain pulse to the specific axis in manual deceleration mode.
    [DllImport("AXL.dll")] public static extern uint AxmMotSetRemainPulse(int nAxisNo, uint uData);
    // Return remain pulse of the specific axis in manual deceleration mode.
    [DllImport("AXL.dll")] public static extern uint AxmMotGetRemainPulse(int nAxisNo, ref uint upData);

    // Set maximum velocity to the specific axis in uniform velocity movement API. 
    [DllImport("AXL.dll")] public static extern uint AxmMotSetMaxVel(int nAxisNo, double dVel);
    // Return maximum velocity of the specific axis in uniform velocity movement API
    [DllImport("AXL.dll")] public static extern uint AxmMotGetMaxVel(int nAxisNo, ref double dpVel);

    // Set travel distance calculation mode of specific axis.
    //uAbsRelMode : POS_ABS_MODE '0' - absolute coordinate system
    //              POS_REL_MODE '1' - relative coordinate system
    [DllImport("AXL.dll")] public static extern uint AxmMotSetAbsRelMode(int nAxisNo, uint uAbsRelMode);
    // Return travel distance calculation mode of specific axis.
    [DllImport("AXL.dll")] public static extern uint AxmMotGetAbsRelMode(int nAxisNo, ref uint upAbsRelMode);

    //Set move velocity profile mode of specific axis. 
    //ProfileMode : SYM_TRAPEZOIDE_MODE  '0' - symmetry Trapezode
    //              ASYM_TRAPEZOIDE_MODE '1' - asymmetric Trapezode
    //              QUASI_S_CURVE_MODE   '2' - symmetry Quasi-S Curve
    //              SYM_S_CURVE_MODE     '3' - symmetry S Curve
    //              ASYM_S_CURVE_MODE    '4' - asymmetric S Curve
    [DllImport("AXL.dll")] public static extern uint AxmMotSetProfileMode(int nAxisNo, uint uProfileMode);
    // Return move velocity profile mode of specific axis.
    [DllImport("AXL.dll")] public static extern uint AxmMotGetProfileMode(int nAxisNo, ref uint upProfileMode);

    //Set acceleration unit of specific axis.
    //AccelUnit : UNIT_SEC2  '0' ? use unit/sec2 for the unit of acceleration/deceleration
    //            SEC        '1' - use sec for the unit of acceleration/deceleration
    [DllImport("AXL.dll")] public static extern uint AxmMotSetAccelUnit(int nAxisNo, uint uAccelUnit);
    // Return acceleration unit of specific axis.
    [DllImport("AXL.dll")] public static extern uint AxmMotGetAccelUnit(int nAxisNo, ref uint upAccelUnit);

    // Set initial velocity to the specific axis.
    [DllImport("AXL.dll")] public static extern uint AxmMotSetMinVel(int nAxisNo, double dMinVelocity);
    // Return initial velocity of the specific axis.
    [DllImport("AXL.dll")] public static extern uint AxmMotGetMinVel(int nAxisNo, ref double dpMinVelocity);

    // Set acceleration jerk value of specific axis.[%].
    [DllImport("AXL.dll")] public static extern uint AxmMotSetAccelJerk(int nAxisNo, double dAccelJerk);
    // Return acceleration jerk value of specific axis.
    [DllImport("AXL.dll")] public static extern uint AxmMotGetAccelJerk(int nAxisNo, ref double dpAccelJerk);

    // Set deceleration jerk value of specific axis.[%].
    [DllImport("AXL.dll")] public static extern uint AxmMotSetDecelJerk(int nAxisNo, double dDecelJerk);
    // Return deceleration jerk value of specific axis.
    [DllImport("AXL.dll")] public static extern uint AxmMotGetDecelJerk(int nAxisNo, ref double dpDecelJerk);
    [DllImport("AXL.dll")] public static extern uint AxmMotSetProfilePriority(int nAxisNo, uint uPriority);
    [DllImport("AXL.dll")] public static extern uint AxmMotGetProfilePriority(int nAxisNo, ref uint upPriority);

//=========== Setting API related in/output signal ================================================================================

    // Set Z phase level of specific axis.
    // uLevel : LOW(0), HIGH(1)
    [DllImport("AXL.dll")] public static extern uint AxmSignalSetZphaseLevel(int nAxisNo, uint uLevel);
    // Return Z phase level of specific axis.
    [DllImport("AXL.dll")] public static extern uint AxmSignalGetZphaseLevel(int nAxisNo, ref uint upLevel);

    // Set output level of Servo-On signal of specific axis.
    // uLevel : LOW(0), HIGH(1)
    [DllImport("AXL.dll")] public static extern uint AxmSignalSetServoOnLevel(int nAxisNo, uint uLevel);
    // Return output level of Servo-On signal of specific axis.
    [DllImport("AXL.dll")] public static extern uint AxmSignalGetServoOnLevel(int nAxisNo, ref uint upLevel);

    // Set output level of Servo-Alarm Reset signal of specific axis.
    // uLevel : LOW(0), HIGH(1)
    [DllImport("AXL.dll")] public static extern uint AxmSignalSetServoAlarmResetLevel(int nAxisNo, uint uLevel);
    // Return output level of Servo-Alarm Reset signal of specific axis.
    [DllImport("AXL.dll")] public static extern uint AxmSignalGetServoAlarmResetLevel(int nAxisNo, ref uint upLevel);

    // Set whether to use Inposition signal of specific axis and signal input level.
    // uLevel : LOW(0), HIGH(1), UNUSED(2), USED(3)    
    [DllImport("AXL.dll")] public static extern uint AxmSignalSetInpos(int nAxisNo, uint uUse);
    // Return whether to use Inposition signal of specific axis and signal input level.
    [DllImport("AXL.dll")] public static extern uint AxmSignalGetInpos(int nAxisNo, ref uint upUse);
    // Return inposition signal input mode of specific axis. 
    [DllImport("AXL.dll")] public static extern uint AxmSignalReadInpos(int nAxisNo, ref uint upStatus);

    // Set whether to use emergency stop or not against to alarm signal input and set signal input level of specific axis. 
    // uLevel : LOW(0), HIGH(1), UNUSED(2), USED(3)
    [DllImport("AXL.dll")] public static extern uint AxmSignalSetServoAlarm(int nAxisNo, uint uUse);
    // Return whether to use emergency stop or not against to alarm signal input and set signal input level of specific axis.
    [DllImport("AXL.dll")] public static extern uint AxmSignalGetServoAlarm(int nAxisNo, ref uint upUse);
    // Return input level of alarm signal of specific axis. 
    [DllImport("AXL.dll")] public static extern uint AxmSignalReadServoAlarm(int nAxisNo, ref uint upStatus);
    
    // Set whether to use end limit sensor of specific axis and input level of signal. 
    // Available to set of slow down stop or emergency stop when end limit sensor is input.
    // uStopMode: EMERGENCY_STOP(0), SLOWDOWN_STOP(1)
    // uPositiveLevel, uNegativeLevel : LOW(0), HIGH(1), UNUSED(2), USED(3)
    [DllImport("AXL.dll")] public static extern uint AxmSignalSetLimit(int nAxisNo, uint uStopMode, uint uPositiveLevel, uint uNegativeLevel);
    // Return whether to use end limit sensor of specific axis , input level of signal and stop mode for signal input. 
    [DllImport("AXL.dll")] public static extern uint AxmSignalGetLimit(int nAxisNo, ref uint upStopMode, ref uint upPositiveLevel, ref uint upNegativeLevel);
    // Return the input state of end limit sensor of specific axis. 
    [DllImport("AXL.dll")] public static extern uint AxmSignalReadLimit(int nAxisNo, ref uint upPositiveStatus, ref uint upNegativeStatus);
    
    // Set whether to use software limit, count to use and stop method of specific axis.
    // uUse       : DISABLE(0), ENABLE(1)
    // uStopMode  : EMERGENCY_STOP(0), SLOWDOWN_STOP(1)
    // uSelection : COMMAND(0), ACTUAL(1)
    // Caution: When software limit is set in advance by using above API in origin detecting and is moving, if the detecting of origin is stopped during detecting, it becomes DISABLE.
    [DllImport("AXL.dll")] public static extern uint AxmSignalSetSoftLimit(int nAxisNo, uint uUse, uint uStopMode, uint uSelection, double dPositivePos, double dNegativePos);
    // Return whether to use software limit, count to use and stop method of specific axis.
    [DllImport("AXL.dll")] public static extern uint AxmSignalGetSoftLimit(int nAxisNo, ref uint upUse, ref uint upStopMode, ref uint upSelection, ref double dpPositivePos, ref double dpNegativePos);
    [DllImport("AXL.dll")] public static extern uint AxmSignalReadSoftLimit(int nAxisNo, ref uint upPositiveStatus, ref uint upNegativeStatus);
    
    // Set the stop method of emergency stop(emergency stop/slowdown stop) ,or whether to use or not.
    // uStopMode  : EMERGENCY_STOP(0), SLOWDOWN_STOP(1)
    // uLevel : LOW(0), HIGH(1), UNUSED(2), USED(3)
    [DllImport("AXL.dll")] public static extern uint AxmSignalSetStop(int nAxisNo, uint uStopMode, uint uLevel);
    // Return the stop method of emergency stop(emergency stop/slowdown stop) ,or whether to use or not.
    [DllImport("AXL.dll")] public static extern uint AxmSignalGetStop(int nAxisNo, ref uint upStopMode, ref uint upLevel);
    // Return input state of emergency stop.
    [DllImport("AXL.dll")] public static extern uint AxmSignalReadStop(int nAxisNo, ref uint upStatus);
    
    // Output the Servo-On signal of specific axis.
    // uOnOff : FALSE(0), TRUE(1) ( The case of universal 0 output)
    [DllImport("AXL.dll")] public static extern uint AxmSignalServoOn(int nAxisNo, uint uOnOff);
    // Return the output state of Servo-On signal of specific axis. 
    [DllImport("AXL.dll")] public static extern uint AxmSignalIsServoOn(int nAxisNo, ref uint upOnOff);

    // Output the Servo-Alarm Reset signal of specific axis.
    // uOnOff : FALSE(0), TRUE(1) (The case of universal 1 output)
    [DllImport("AXL.dll")] public static extern uint AxmSignalServoAlarmReset(int nAxisNo, uint uOnOff);
    
    // Set universal output value.
    // uValue : Hex Value 0x00
    [DllImport("AXL.dll")] public static extern uint AxmSignalWriteOutput(int nAxisNo, uint uValue);
    // Return universal output value.
    [DllImport("AXL.dll")] public static extern uint AxmSignalReadOutput(int nAxisNo, ref uint upValue);
    
    // lBitNo : Bit Number(0 - 4)
    // uOnOff : FALSE(0), TRUE(1)
    // Set universal output values by bit.
    [DllImport("AXL.dll")] public static extern uint AxmSignalWriteOutputBit(int nAxisNo, int nBitNo, uint uOn);
    // Return universal output values by bit.
    [DllImport("AXL.dll")] public static extern uint AxmSignalReadOutputBit(int nAxisNo, int nBitNo, ref uint upOn);

    // Return universal input value in Hex value.
    [DllImport("AXL.dll")] public static extern uint AxmSignalReadInput(int nAxisNo, ref uint upValue);
    
    // lBitNo : Bit Number(0 - 4)
    // Return universal input value by bit. 
    [DllImport("AXL.dll")] public static extern uint AxmSignalReadInputBit(int nAxisNo, int nBitNo, ref uint upOn);
    
    [DllImport("AXL.dll")] public static extern uint AxmSignalSetFilterBandwidth(int nAxisNo, uint uSignal, double dBandwidthUsec);

    //========== API which verifies the state during motion moving and after moving. ============================================================
    // Return pulse output state of specific axis. 
    // (Status of move)"
    [DllImport("AXL.dll")] public static extern uint AxmStatusReadInMotion(int nAxisNo, ref uint upStatus);

    //  Return move pulse counter value of specific axis after start of move. 
    //  (pulse count value)"
    [DllImport("AXL.dll")] public static extern uint AxmStatusReadDrivePulseCount(int nAxisNo, ref int npPulse);
    
    // Return DriveStatus register (status of in-motion) of specific Axis. 
    // Caution: All Motion Product is different Hardware bit signal. Let's refer Manual and AXHS.xxx
    
    [DllImport("AXL.dll")] public static extern uint AxmStatusReadMotion(int nAxisNo, ref uint upStatus);
    
    // Return EndStatus(status of stop) register of specific axis.
    // Caution: All Motion Product is different Hardware bit signal. Let's refer Manual and AXHS.xxx
    
    [DllImport("AXL.dll")] public static extern uint AxmStatusReadStop(int nAxisNo, ref uint upStatus);
    
    // Return Mechanical Signal Data(Current mechanical signal status)of specific axis.
    // Caution: All Motion Product is different Hardware bit signal. Let's refer Manual and AXHS.xxx
    [DllImport("AXL.dll")] public static extern uint AxmStatusReadMechanical(int nAxisNo, ref uint upStatus);
    
    // Read current move velocity oo specific axis.
    [DllImport("AXL.dll")] public static extern uint AxmStatusReadVel(int nAxisNo, ref double dpVelocity);
    
    // Return the error between Command Pos and Actual Pos of specific axis.
    [DllImport("AXL.dll")] public static extern uint AxmStatusReadPosError(int nAxisNo, ref double dpError);
    
    // Verify the travel(traveled) distance to the final drive.
    [DllImport("AXL.dll")] public static extern uint AxmStatusReadDriveDistance(int nAxisNo, ref double dpUnit);

    // Set use the Position information Type of specific Axis. 
    // uPosType  : Select Position Information Type (Actual position / Command position)
    //    POSITION_LIMIT '0' - Normal action, In all round action.
    //    POSITION_BOUND '1' - Position cycle type, dNegativePos ~ dPositivePos Range
    // Caution(PCI-Nx04)
    // - BOUNT설정시 카운트 값이 Max값을 초과 할 때 Min값이되며 반대로 Min값을 초과 할 때 Max값이 된다.
    // - 다시말해 현재 위치값이 설정한 값 밖에서 카운트 될 때는 위의 Min, Max값이 적용되지 않는다.
    [DllImport("AXL.dll")] public static extern uint AxmStatusSetPosType(int nAxisNo, uint uPosType, double dPositivePos, double dNegativePos);
    // Return the Position Information Type of of specific axis.
    [DllImport("AXL.dll")] public static extern uint AxmStatusGetPosType(int nAxisNo, ref uint upPosType, ref double dpPositivePos, ref double dpNegativePos);
    // Set absolute encoder origin offset Position of specific axis. [Only for PCI-R1604-MLII]
    [DllImport("AXL.dll")] public static extern uint AxmStatusSetAbsOrgOffset(int nAxisNo, double dOrgOffsetPos);

    // Set the actual position of specific axis. 
    [DllImport("AXL.dll")] public static extern uint AxmStatusSetActPos(int nAxisNo, double dPos);

    // Return the actual position of specific axis.
    [DllImport("AXL.dll")] public static extern uint AxmStatusGetActPos(int nAxisNo, ref double dpPos);

    // Set command position of specific axis.
    [DllImport("AXL.dll")] public static extern uint AxmStatusSetCmdPos(int nAxisNo, double dPos);

    // Return command position of specific axis.
    [DllImport("AXL.dll")] public static extern uint AxmStatusGetCmdPos(int nAxisNo, ref double dpPos);

    // Set command position and actual position of specific axis
    // Only RTEX use
    [DllImport("AXL.dll")] public static extern uint AxmStatusSetPosMatch(int nAxisNo, double dPos);

    [DllImport("AXL.dll")] public static extern uint AxmStatusReadMotionInfo(int nAxisNo, ref MOTION_INFO MI);

    // Network
    [DllImport("AXL.dll")] public static extern uint AxmStatusRequestServoAlarm(int nAxisNo);    
    [DllImport("AXL.dll")] public static extern uint AxmStatusReadServoAlarm(int nAxisNo, uint uReturnMode, ref uint upAlarmCode);
    [DllImport("AXL.dll")] public static extern uint AxmStatusGetServoAlarmString(int nAxisNo, uint uAlarmCode, int nAlarmStringSize, byte[] szAlarmString);
    [DllImport("AXL.dll")] public static extern uint AxmStatusRequestServoAlarmHistory(int nAxisNo);
    [DllImport("AXL.dll")] public static extern uint AxmStatusReadServoAlarmHistory(int nAxisNo, uint uReturnMode, ref int npCount, ref uint upAlarmCode);
    [DllImport("AXL.dll")] public static extern uint AxmStatusClearServoAlarmHistory(int nAxisNo);

//======== API related home. =============================================================================================
    
    // Set home sensor level of specific axis. 
    // uLevel : LOW(0), HIGH(1)
    [DllImport("AXL.dll")] public static extern uint AxmHomeSetSignalLevel(int nAxisNo, uint uLevel);
    // Return home sensor level of specific axis.
    [DllImport("AXL.dll")] public static extern uint AxmHomeGetSignalLevel(int nAxisNo, ref uint upLevel);
    // Verify current home signal input status. Hoe signal can be set by user optionally by using AxmHomeSetMethod API. 
    // upStatus : OFF(0), ON(1)
    [DllImport("AXL.dll")] public static extern uint AxmHomeReadSignal(int nAxisNo, ref uint upStatus);
    
    // Parameters related to origin detecting must be set in order to detect origin of relevant axis. 
    // If the initialization is done properly by using MotionPara setting file, no separate setting is required.  
    // In the setting of origin detecting method, direction of detecting proceed, signal to be used for origin, active level of origin sensor and detecting/no detecting of encoder Z phase are set. 
    // Caution : When the level is set wrong, it may operate + direction even though ? direction is set, and may cause problem in finding home.
    // (Refer the guide part of AxmMotSaveParaAll for detail information. )
    // Use AxmSignalSetHomeLevel for home level.
    // HClrTim : HomeClear Time : Queue time for setting origin detecting Encoder value. 
    // HmDir(Home direction): DIR_CCW(0): - direction    , DIR_CW(1) = + direction   // HOffset ? traveled distance after detecting of origin. 
    // uZphas: Set whether to detect of encoder Z phase after completion of the 1st detecting of origin. 
    // HmSig : PosEndLimit(0) -> +Limit
    //         NegEndLimit(1) -> -Limit
    //         HomeSensor (4) -> origin sensor(universal input 0)    
    [DllImport("AXL.dll")] public static extern uint AxmHomeSetMethod(int nAxisNo,int nHmDir, uint uHomeSignal, uint uZphas, double dHomeClrTime, double dHomeOffset);
    // Return set parameters related to home.
    [DllImport("AXL.dll")] public static extern uint AxmHomeGetMethod(int nAxisNo,ref int nHmDir, ref uint uHomeSignal, ref uint uZphas, ref double dHomeClrTime, ref double dHomeOffset);

    // Set Home Search Fine tunning Method (Don't care Setting by default).
    // dHomeDogDistance[500 pulse]: Set Dog Length. (Unit = AxmMotSetMoveUnitPerPulse function Setting Unit)
    // lLevelScanTime[100msec]: Set level status confirmation scan time. (msec[1~1000])
    // dwFineSearchUse[USE]: Select find search method. (Default use 5Step, Select 0 use 3Step)
    // dwHomeClrUse[USE]: Set After Home Search Automatically Command/Actual Position Set 0 value of  specific axis.
    [DllImport("AXL.dll")] public static extern uint AxmHomeSetFineAdjust(int nAxisNo, double dHomeDogLength, uint lLevelScanTime, uint uFineSearchUse, uint uHomeClrUse);
    // Return set Home  Search fine tunning Method parameters related to home.
    [DllImport("AXL.dll")] public static extern uint AxmHomeGetFineAdjust(int nAxisNo, ref double dpHomeDogLength, ref uint lpLevelScanTime, ref uint upFineSearchUse, ref uint upHomeClrUse);


    // Detect through several steps in order to detect origin quickly and precisely. Now, set velocities to be used for each step.  
    // The time of origin detecting and the accuracy of origin detecting are decided by setting of these velocities. 
    // Set velocity of origin detecting of each axis by changing velocities of each step.  
    // (Refer the guide part of AxmMotSaveParaAll for detail information.)
    // API which sets velocity to be used in origin detecting. 
    // [dVelFirst]- 1st move velocity   [dVelSecond]- velocity after detecting   [dVelThird]- the last velocity  [dvelLast]- index detecting and in order to detect precisely. 
    // [dAccFirst]- 1st move acceleration [dAccSecond]-acceleration after detecting.  
    [DllImport("AXL.dll")] public static extern uint AxmHomeSetVel(int nAxisNo,double dVelFirst, double dVelSecond, double dVelThird, double dvelLast, double dAccFirst, double dAccSecond);
    // Return set velocity to be used in origin detecting. 
    [DllImport("AXL.dll")] public static extern uint AxmHomeGetVel(int nAxisNo, ref double dVelFirst, ref double dVelSecond, ref double dVelThird, ref double dvelLast, ref double dAccFirst, ref double dAccSecond);

    // Start to detect origin.
    // When origin detecting start API is executed, thread which will detect origin of relevant axis in the library is created automatically and it is automatically closed after carrying out of the origin detecting in sequence. 
    [DllImport("AXL.dll")] public static extern uint AxmHomeSetStart(int nAxisNo);
    // User sets the result of origin detecting optionally. 
    // When the detecting of origin is completed successfully by using origin detecting API, the result of detecting is set as HOME_SUCCESS.
    // This API enables user to set result optionally without execution of origin detecting. 
    // uHomeResult Setup
    // HOME_SUCCESS             = 0x01,       
    // HOME_SEARCHING           = 0x02,     
    // HOME_ERR_GNT_RANGE       = 0x10, // Gantry Home Range Over
    // HOME_ERR_USER_BREAK      = 0x11, // User Stop Command
    // HOME_ERR_VELOCITY        = 0x12, // Velocity is very slow and fast
    // HOME_ERR_AMP_FAULT       = 0x13, // Servo Drive Alarm 
    // HOME_ERR_NEG_LIMIT       = 0x14, // (+)Limit sensor check (-)dir during Motion
    // HOME_ERR_POS_LIMIT       = 0x15, // (-)Limit sensor check (+)dir during Motion
    // HOME_ERR_NOT_DETECT      = 0x16, // not detect User set signal
    // HOME_ERR_UNKNOWN         = 0xFF,
      
    [DllImport("AXL.dll")] public static extern uint AxmHomeSetResult(int nAxisNo, uint uHomeResult);
    // Return the result of origin detecting. 
    // Verify detecting result of origin detection API. When detecting of origin is started, it sets as HOME_SEARCHING, and if the detecting of origin is failed the reason of failure is set. Redo origin detecting after eliminating of failure reasons.
    [DllImport("AXL.dll")] public static extern uint AxmHomeGetResult(int nAxisNo, ref uint upHomeResult);
    // Return progress rate of origin detection.
    // Progress rate can be verified when origin detection is commenced. When origin detection is completed, return 100% whether success or failure. The success or failure of origin detection result can be verified by using GetHome Result API.
    // upHomeMainStepNumber : Progress rate of Main Step . 
    // In case of gentry FALSE upHomeMainStepNumber : When 0 , only selected axis is in proceeding, home progress rate is indicated upHomeStepNumber.
    // In case of gentry TRUE upHomeMainStepNumber : When 0, master home is in proceeding, master home progress rate is indicated upHomeStepNumber. 
    // In case of gentry TRUE upHomeMainStepNumber : When 10 , slave home is in proceeding, master home progress rate is indicated upHomeStepNumber .
    // upHomeStepNumber     : Indicate progress rate against to selected axis. 
    // In case of gentry FALSE  : Indicate progress rate against to selected axis only.
    // In case of gentry TRUE, progress rate is indicated by sequence of master axis and slave axis.
    [DllImport("AXL.dll")] public static extern uint AxmHomeGetRate(int nAxisNo, ref uint upHomeMainStepNumber, ref uint upHomeStepNumber);

    //========= Position move API ===============================================================================================================
    
    // If you want to set specified velocity unit in RPM(Revolution Per Minute),
    // ex>    calculate rpm :
    // 4500 rpm ?
    // When unit/ pulse = 1 : 1, then it becomes pulse per sec, and
    // if you want to set at 4500 rpm , then  4500 / 60 sec : 75 revolution / 1sec
    // The number of pulse per 1 revolution of motor shall be known. This can be know by detecting of Z phase in Encoder. 
    // If 1 revolution:1800 pulse,  75 x 1800 = 135000 pulses are required. 
    // Operate by input Unit = 1, Pulse = 1800 into AxmMotSetMoveUnitPerPulse.
    
    // Travel up to set distance or position.
    // It moves by set velocity and acceleration up to the position set by absolute coordinates/ relative coordinates of specific axis. 
    // Velocity profile is set in AxmMotSetProfileMode API. 
    // It separates from API at the timing of pulse output start.
    [DllImport("AXL.dll")] public static extern uint AxmMoveStartPos(int nAxisNo, double dPos, double dVel, double dAccel, double dDecel);

    // Travel up to set distance or position.
    // It moves by set velocity and acceleration up to the position set by absolute coordinates/ relative coordinates of specific axis.
    // Velocity profile is set in AxmMotSetProfileMode API
    // It separates from API at the timing of pulse output finish.
    [DllImport("AXL.dll")] public static extern uint AxmMovePos(int nAxisNo, double dPos, double dVel, double dAccel, double dDecel);

    // Move by set velocity.
    // It maintain velocity mode move by velocity and acceleration  set against to specific axis. 
    // It separates from API at the timing of pulse output start.
    // It moves toward to CW direction when Vel value is positive, CCW when negative.
    [DllImport("AXL.dll")] public static extern uint AxmMoveVel(int nAxisNo, double dVel, double dAccel, double dDecel);

    // It maintain velocity mode move by velocity and acceleration  set against to specific multi-axis.
    // It separates from API at the timing of pulse output start.
    // It moves toward to CW direction when Vel value is positive, CCW when negative.
    [DllImport("AXL.dll")] public static extern uint AxmMoveStartMultiVel(int lArraySize, int[] lpAxesNo, double[] dVel, double[] dAccel, double[] dDecel);

    [DllImport("AXL.dll")] public static extern uint AxmMoveStartMultiVelEx(int lArraySize, int[] lpAxesNo, double[] dpVel, double[] dpAccel, double[] dpDecel, uint uSyncMode);

    [DllImport("AXL.dll")] public static extern uint AxmMoveStartLineVel(int lArraySize, int[] lpAxesNo, double[] dpDis, double dVel, double dAccel, double dDecel);
    

    // API which detects Edge of specific Input signal and makes emergency stop or slowdown stop. 
    // lDetect Signal : Select input signal to detect . 
    // lDetectSignal  : PosEndLimit(0), NegEndLimit(1), HomeSensor(4), EncodZPhase(5), UniInput02(6), UniInput03(7)
    // Signal Edge    : Select edge direction of selected input signal (rising or falling edge).
    //                  SIGNAL_DOWN_EDGE(0), SIGNAL_UP_EDGE(1)
    // Move direction : CW when Vel value is positive, CCW when negative.
    // SignalMethod   : EMERGENCY_STOP(0), SLOWDOWN_STOP(1)
    // Caution: When SignalMethod is used as EMERGENCY_STOP(0), acceleration/deceleration is ignored and it is accelerated to specific velocity and emergency stop. 
    //          lDetectSignal is PosEndLimit , in case of searching NegEndLimit(0,1) active status of signal level is detected.
    [DllImport("AXL.dll")] public static extern uint AxmMoveSignalSearch(int nAxisNo, double dVel, double dAccel, int nDetectSignal, int nSignalEdge, int nSignalMethod);
    
    // API which detects signal set in specific axis and travels in order to save the position. 
    // In case of searching acting API to select and find desired signal, save the position and read the value using AxmGetCapturePos. 
    // Signal Edge   : Select edge direction of selected input signal. (rising or falling edge).
    //                 SIGNAL_DOWN_EDGE(0), SIGNAL_UP_EDGE(1)
    // Move direction      : CW when Vel value is positive, CCW when negative.
    // SignalMethod  : EMERGENCY_STOP(0), SLOWDOWN_STOP(1)
    // lDetect Signal: Select input signal to detect edge .SIGNAL_DOWN_EDGE(0), SIGNAL_UP_EDGE(1)
    // lDetectSignal : PosEndLimit(0), NegEndLimit(1), HomeSensor(4), EncodZPhase(5), UniInput02(6), UniInput03(7)
    //                 Select the motion action which COMMON(0) or SOFTWARE(0) by upper 8bit. Only for SMP Board(PCIe-Rxx05-MLIII)
    // lTarget       : COMMAND(0), ACTUAL(1)
    // Caution: When SignalMethod is used as EMERGENCY_STOP(0), acceleration/deceleration is ignored and it is accelerated to specific velocity and emergency stop. 
    // lDetectSignal is PosEndLimit , in case of searching NegEndLimit(0,1) active status of signal level is detected.
    [DllImport("AXL.dll")] public static extern uint AxmMoveSignalCapture(int nAxisNo, double dVel, double dAccel, int nDetectSignal, int nSignalEdge, int nTarget, int nSignalMethod);
    // API which verifies saved position value in 'AxmMoveSignalCapture' API.
    [DllImport("AXL.dll")] public static extern uint AxmMoveGetCapturePos(int nAxisNo, ref double dpCapPos);

    // " API which travels up to set distance or position.
    // When execute API, it starts relevant motion action and escapes from API without waiting until motion is completed ”
    [DllImport("AXL.dll")] public static extern uint AxmMoveStartMultiPos(int nArraySize, int[] nAxisNo, double[] dPos, double[] dVel, double[] dAccel, double[] dDecel);
    
    // Travels up to the distance which sets multi-axis or position. 
    // It moves by set velocity and acceleration up to the position set by absolute coordinates of specific axis. specific axes.
    [DllImport("AXL.dll")] public static extern uint AxmMoveMultiPos(int nArraySize, int[] nAxisNo, double[] dPos, double[] dVel, double[] dAccel, double[] dDecel);

    // When execute API, it starts open-loop torque motion action of specific axis.(only for MLII, Sigma 5 servo drivers)
    // dTroque        : Percentage value(%) of maximum torque. (negative value : CCW, positive value : CW)
    // dVel           : Percentage value(%) of maximum velocity.
    // dwAccFilterSel : LINEAR_ACCDCEL(0), EXPO_ACCELDCEL(1), SCURVE_ACCELDECEL(2)
    // dwGainSel      : GAIN_1ST(0), GAIN_2ND(1)
    // dwSpdLoopSel   : PI_LOOP(0), P_LOOP(1)
    [DllImport("AXL.dll")] public static extern uint AxmMoveStartTorque(int lAxisNo, double dTorque, double dVel, uint dwAccFilterSel, uint dwGainSel, uint dwSpdLoopSel);

    // It stops motion during torque motion action.
    // it can be only applied for AxmMoveStartTorque API.
    [DllImport("AXL.dll")] public static extern uint AxmMoveTorqueStop(int lAxisNo, uint dwMethod);

    // To Move Set Position or distance
    // Absolute coordinates / position set to the coordinates relative to the set speed / acceleration rate to drive of specific Axis.
    // Velocity Profile is fixed Asymmetric trapezoid.
    // Accel/Decel Setting Unit is fixed Unit/Sec^2 
    // dAccel != 0.0 and dDecel == 0.0 일 경우 이전 속도에서 감속 없이 지정 속도까지 가속.
    // dAccel != 0.0 and dDecel != 0.0 일 경우 이전 속도에서 지정 속도까지 가속후 등속 이후 감속.
    // dAccel == 0.0 and dDecel != 0.0 일 경우 이전 속도에서 다음 속도까지 감속.

    // The following conditions must be satisfied.
    // dVel[1] == dVel[3] must be satisfied.
    // dVel [2] that can occur as a constant speed drive range is greater dPosition should be enough.
    // Ex) dPosition = 10000;
    // dVel[0] = 300., dAccel[0] = 200., dDecel[0] = 0.;    <== Acceleration
    // dVel[1] = 500., dAccel[1] = 100., dDecel[1] = 0.;    <== Acceleration
    // dVel[2] = 700., dAccel[2] = 200., dDecel[2] = 250.;  <== Acceleration, constant velocity, Deceleration
    // dVel[3] = 500., dAccel[3] = 0.,   dDecel[3] = 150.;  <== Deceleration
    // dVel[4] = 200., dAccel[4] = 0.,   dDecel[4] = 350.;  <== Deceleration
    // Exits API at the point that pulse out starts.
    [DllImport("AXL.dll")] public static extern uint AxmMoveStartPosWithList(int lAxisNo, double dPosition, double[] dpVel, double[] dpAccel, double[] dpDecel, int lListNum);

    // Is set by the distance to the target axis position or the position to increase or decrease the movement begins.
    // lEvnetAxisNo    : Start condition occurs axis.
    // dComparePosition: Conditions Occurrence Area of Start condition occurs axis.
    // uPositionSource : Set Conditions Occurrence Area of Start condition occurs axis => COMMAND(0), ACTUAL(1)
    // Cancellations after reservation AxmMoveStop, AxmMoveEStop, AxmMoveSStop use
    // Motion Axis and Start condition occurs axis must be In same group(case by 2V04 In same module).
    [DllImport("AXL.dll")] public static extern uint AxmMoveStartPosWithPosEvent(int lAxisNo, double dPos, double dVel, double dAccel, double dDecel, int lEventAxisNo, double dComparePosition, uint uPositionSource);

    // It slowdown stops by deceleration set for specific axis.
    // dDecel : Deceleration value when stop. 
    [DllImport("AXL.dll")] public static extern uint AxmMoveStop(int nAxisNo, double dDecel);
    [DllImport("AXL.dll")] public static extern uint AxmMoveStopEx(int lAxisNo, double dDecel);
    // Stops specific axis emergently .
    [DllImport("AXL.dll")] public static extern uint AxmMoveEStop(int nAxisNo);
    // Stops specific axis slow down. 
    [DllImport("AXL.dll")] public static extern uint AxmMoveSStop(int nAxisNo);

//========= Overdrive API ============================================================================
    // Overdrives position.
    // Adjust number of specific pulse before the move of specific axis is finished. 
    // Cautions : In here when put in the position subjected to overdrive, as the travel distance is put into the position value of relative form,
    //            position must be put in as position value of relative form.
    [DllImport("AXL.dll")] public static extern uint AxmOverridePos(int nAxisNo, double dOverridePos);

    // Set the maximum velocity subjected to overdirve before velocity overdriving of specific axis. 
       // Caution : If the velocity overdrive is done 5 times, the max velocity shall be set among them.  
    [DllImport("AXL.dll")] public static extern uint AxmOverrideSetMaxVel(int nAxisNo, double dOverrideMaxVel);

    // Overdrive velocity.
    // Set velocity variably during the move of specific axis. (Make sure to set variably during the motion.)
    // Caution: Before use of AxmOverrideVel API, set the maximum velocity which can be set by AxmOverrideMaxVel
    // EX> If velocity overdrive two times, 
    // 1. Set the highest velocity among the two as the highest setting velocity of AxmOverrideMaxVel.
    // 2. Set the AxmOverrideVel  variably with the velocity in the moving of AxmMoveStartPos execution specific axis (including move API all) as the first velocity. 
     // 3. Set the AxmOverrideVel  variably with the velocity in the moving of specific axis (including move API all) as the 2nd velocity.
    [DllImport("AXL.dll")] public static extern uint AxmOverrideVel(int nAxisNo, double dOverrideVelocity);
    
    // Overdrive velocity.
    // Set velocity variably during the move of specific axis. (Make sure to set variably during the motion.)
    // Caution: Before use of AxmOverrideAccelVelDecel API, set the maximum velocity which can be set by AxmOverrideMaxVel
    // EX> If velocity overdrive two times, 
    // 1. Set the highest velocity among the two as the highest setting velocity of AxmOverrideMaxVel.
    // 2. Set the AxmOverrideAccelVelDecel  variably with the velocity in the moving of AxmMoveStartPos execution specific axis (including move API all) as the first velocity. 
     // 3. Set the AxmOverrideAccelVelDecel  variably with the velocity in the moving of specific axis (including move API all) as the 2nd velocity.
    [DllImport("AXL.dll")] public static extern uint AxmOverrideAccelVelDecel(int nAxisNo, double dOverrideVelocity, double dMaxAccel, double dMaxDecel);
    
    // Velocity overdrive at certain timing. 
    // API which becomes overdrive at the position when a certain position point and velocity to be overdrived
    // lTarget : COMMAND(0), ACTUAL(1)
    [DllImport("AXL.dll")] public static extern uint AxmOverrideVelAtPos(int nAxisNo, double dPos, double dVel, double dAccel, double dDecel, double dOverridePos, double dOverrideVelocity, int nTarget);

    [DllImport("AXL.dll")] public static extern uint AxmOverrideVelAtMultiPos(int nAxisNo, double dPos, double dVel, double dAccel, double dDecel, int nArraySize, double[] dpOverridePos, double[] dpOverrideVel, int nTarget, uint uOverrideMode);
//========= Move API by master, slave gear ration. ===========================================================================

    // In Electric Gear mode, set gear ratio between master axis and slave axis. 
    // dSlaveRatio : Gear ratio of slave against to master axis ( 0 : 0% , 0.5 : 50%, 1 : 100%)
    [DllImport("AXL.dll")] public static extern uint AxmLinkSetMode(int nMasterAxisNo, int nSlaveAxisNo, double dSlaveRatio);
    // In Electric Gear mode, return gear ratio between master axis and slave axis.
    [DllImport("AXL.dll")] public static extern uint AxmLinkGetMode(int nMasterAxisNo, ref uint nSlaveAxisNo, ref double dpGearRatio);
    // Reset electric gear ration between Master axis and slave axis. 
    [DllImport("AXL.dll")] public static extern uint AxmLinkResetMode(int nMasterAxisNo);

//======== API related to gentry===========================================================================================================================================================
        // Motion module supports gentry move system control which two axes are linked mechanically.. 
    // When set master axis for gentry control by using this API, relevant slave axis synchronizes with master axis and moves.  
    // However after setting of gentry, even if any move command or stop command is directed, they are ignored
    // uSlHomeUse     : Whether to use home of slave axis or not ( 0 - 2)
    //             (0 : Search home of master axis without using home of slave axis.)
    //             (1 : Search home of master axis, slave axis. Compensating by applying slave dSlOffset value.)
    //             (2 : Search home of master axis, slave axis. No compensating by applying slave dSlOffset value.))
    // dSlOffset      : Offset value of slave axis
    // dSlOffsetRange : Set offset value range of slave axis.
    // Caution in use : When gentry is enabled, it is normal operation if the slave axis is verified as Inmotion by AxmStatusReadMotion API in its motion, and verified as True.  
 
    //                When slave axis is verified by AxmStatusReadMotion, if InMotion is False then Gantry Enable is not available, therefore verify whether alarm hits limit. 
    
    [DllImport("AXL.dll")] public static extern uint AxmGantrySetEnable(int nMasterAxisNo, int nSlaveAxisNo, uint uSlHomeUse, double dSlOffset, double dSlOffsetRange);

    // The method to know offset value of Slave axis.
    // A. Servo-On both master and slave.         
    // B. After set uSlHomeUse = 2 in AxmGantrySetEnableAPI, then search home by using  AxmHomeSetStart API. 
    // C. After search home, twisted offset value of master axis and slave axis can be seen by reading command value of master axis.
    // D. Read Offsetvalue, and put it into dSlOffset argument of AxmGantrySetEnable API.  
    // E. As dSlOffset value is the slave axis value against to master axis, put its value with reversed sign as ?dSlOffset. 
    // F. dSIOffsetRange means the range of Slave Offset, it is used to originate error when it is out of the specified range.    
    // G. If the offset has been input into AxmGantrySetEnable API, in AxmGantrySetEnable API,after  set uSlHomeUse = 1 then search home by using AxmHomeSetStart API.          
    
    // In gentry move, return parameter set by user. 
    [DllImport("AXL.dll")] public static extern uint AxmGantryGetEnable(int nMasterAxisNo, ref uint upSlHomeUse, ref double dpSlOffset, ref double dSlORange, ref uint uGatryOn);

    // Motion module releases gentry move system control which two axes are linked mechanically.
    [DllImport("AXL.dll")] public static extern uint AxmGantrySetDisable(int nMasterAxisNo, int nSlaveAxisNo);

    // Only for PCI-Rxx04-MLII.
    // Set Synchronous compensation of Gentry System.
    // lMasterGain, lSlaveGain : Set % value of Between the two axes about position deviation.
    // lMasterGain, lSlaveGain : If Set Input 0 Not Use. Default value : 0%
    [DllImport("AXL.dll")] public static extern uint AxmGantrySetCompensationGain(int lMasterAxisNo, int lMasterGain, int lSlaveGain);
    // Return Synchronous compensation of Gentry System.
    [DllImport("AXL.dll")] public static extern uint AxmGantryGetCompensationGain(int lMasterAxisNo, ref int lMasterGain, ref int lSlaveGain);

//====Regular interpolation API ============================================================================================================================================;

    // Do linear interpolate.
    // API which moves multi-axis linear interpolation by specifying start point and ending point. It escapes from API after commencing of move.
    // When it is used with AxmContiBeginNode and AxmContiEndNode, it becomes Save API in the queue which moves linear interpolation by specifying start point and ending point in the specified  coordinates system. 
    // For the continuous interpolation move of linear profile, save it in internal queue and start by using AxmContiStart API.
    [DllImport("AXL.dll")] public static extern uint AxmLineMove(int lCoord, double[] dPos, double dVel, double dAccel, double dDecel);

    // Do linear interpolate.(Software Core)
    // API which moves multi-axis linear interpolation by specifying start point and ending point. It escapes from API after commencing of move.
    // When it is used with AxmContiBeginNode and AxmContiEndNode, it becomes Save API in the queue which moves linear interpolation by specifying start point and ending point in the specified  coordinates system. 
    // For the continuous interpolation move of linear profile, save it in internal queue and start by using AxmContiStart API.
    [DllImport("AXL.dll")] public static extern uint AxmLineMoveEx2(int lCoord, double[] dpEndPos, double dVel, double dAccel, double dDecel);

    // Interpolate 2 axis arc
    // API which moves arc interpolation by specifying start point, ending point and center point. It escapes from API after commencing of move.
    // When it is used with AxmContiBeginNode and AxmContiEndNode, it becomes Save API in the arc interpolation queue which moves by specifying start point, ending point and center point in the specified  coordinates system. 
    // For the profile arc continuous interpolation move, save it internal queue and start by using AxmContiStart API.
    // lAxisNo = 2 axis array , dCenterPos = center point X,Y array , dEndPos = ending point X,Y array.
    // uCWDir   DIR_CCW(0): Counterclockwise direction,   DIR_CW(1) Clockwise direction

    [DllImport("AXL.dll")] public static extern uint AxmCircleCenterMove(int lCoord, int[] lAxisNo, double[] dCenterPos, double[] dEndPos, double dVel, double dAccel, double dDecel, uint uCWDir);
        
    // API which arc interpolation moves by specifying middle point and ending point. It escapes from API after commencing of move.
    // When it is used with AxmContiBeginNode and AxmContiEndNode, it becomes Save API in the arc interpolation queue which moves by specifying middle point and ending point in the specified  coordinates system. 
    // For the profile arc continuous interpolation move, save it internal queue and start by using AxmContiStart API.
    // lAxisNo = 2 axis array , dMidPos = middle point, X,Y array , dEndPos = ending point X,Y array, lArcCircle = arc(0), circle(1)
    [DllImport("AXL.dll")] public static extern uint AxmCirclePointMove(int lCoord, int[] lAxisNo, double[] dMidPos, double[] dEndPos, double dVel, double dAccel, double dDecel, int lArcCircle);
    
    // API which arc interpolation moves by specifying start point, ending point and radius. It escapes from API after commencing of move.
    // When it is used with AxmContiBeginNode and AxmContiEndNode, it becomes Save API in the arc interpolation queue which moves by specifying start point, ending point and radius in the specified  coordinates system. 
    // For the profile arc continuous interpolation move, save it internal queue and start by using AxmContiStart API.
    // lAxisNo = 2 axis array , dRadius = radius, dEndPos = ending point X,Y array , uShortDistance = small circle(0), large circle(1)
      // uCWDir   DIR_CCW(0): Counterclockwise direction,   DIR_CW(1) Clockwise direction
    [DllImport("AXL.dll")] public static extern uint AxmCircleRadiusMove(int lCoord, int[] lAxisNo, double dRadius, double[] dEndPos, double dVel, double dAccel, double dDecel, uint uCWDir, uint uShortDistance);
    
    // API which arc interpolation moves by specifying start point, revolution angle and radius. It escapes from API after commencing of move.
    // When it is used with AxmContiBeginNode and AxmContiEndNode, it becomes Save API in the arc interpolation queue which moves by specifying start point, revolution angle and radius in the specified  coordinates system. 
    // For the profile arc continuous interpolation move, save it internal queue and start by using AxmContiStart API.
    // lAxisNo = 2 axis array , dCenterPos = center point X,Y array , dAngle = angle.
    // uCWDir   DIR_CCW(0): Counterclockwise direction,   DIR_CW(1) Clockwise direction
    [DllImport("AXL.dll")] public static extern uint AxmCircleAngleMove(int lCoord, int[] lAxisNo, double[] dCenterPos, double dAngle, double dVel, double dAccel, double dDecel, uint uCWDir);
    
//==== continuous interpolation API ============================================================================================================================================;
    //Set continuous interpolation axis mapping in specific coordinates system.
    //(Start axis mapping number from 0)
    // Caution: In the axis mapping, the input must be started from smaller number. 
    //         In here, the axis of smallest number becomes master. 
    [DllImport("AXL.dll")] public static extern uint AxmContiSetAxisMap(int lCoord, uint lSize, int[] lpRealAxesNo);
    // Return continuous interpolation axis mapping in specific coordinates system.
    [DllImport("AXL.dll")] public static extern uint AxmContiGetAxisMap(int lCoord, ref uint lSize, ref int lpRealAxesNo);
        
    // Set continuous interpolation axis absolute/relative mode in specific coordinates system.
    // (Caution : available to use only after axis mapping)
    // St travel distance calculation mode of specific axis.
    //uAbsRelMode : POS_ABS_MODE '0' - absolute coordinate system
    //              POS_REL_MODE '1' - relative coordinate system
    [DllImport("AXL.dll")] public static extern uint AxmContiSetAbsRelMode(int lCoord, uint uAbsRelMode);
    // Return interpolation axis absolute/relative mode in specific coordinates system.
    [DllImport("AXL.dll")] public static extern uint AxmContiGetAbsRelMode(int lCoord, ref uint upAbsRelMode);

    // API which verifies whether internal Queue for the interpolation move is empty in specific coordinate system.
    [DllImport("AXL.dll")] public static extern uint AxmContiReadFree(int lCoord, ref uint upQueueFree);
    // API which verifies the number of interpolation move saved in internal Queue for the interpolation move in specific coordinate system 
    [DllImport("AXL.dll")] public static extern uint AxmContiReadIndex(int lCoord, ref int npQueueIndex);

    // API which deletes all internal Queue saved for the continuous interpolation move in specific coordinate system 
    [DllImport("AXL.dll")] public static extern uint AxmContiWriteClear(int lCoord);

    // Start registration of tasks to be executed in continuous interpolation at the specific  coordinate system. After call this API,
    // all motion tasks to be executed before calling of AxmContiEndNode API are not execute actual motion, but are registered as continuous interpolation motion, 
    // and when AxmContiStart API is called, then the registered motions execute actually
    [DllImport("AXL.dll")] public static extern uint AxmContiBeginNode(int lCoord);
    // Finish registration of tasks to be executed in continuous interpolation at the specific  coordinate system.
    [DllImport("AXL.dll")] public static extern uint AxmContiEndNode(int lCoord);

    // Start continuous interpolation.
    // if dwProfileset is  CONTI_NODE_VELOCITY(0), use continuous interpolation.
    //                     CONTI_NODE_MANUAL(1)  , set profile interpolation use. 
    //                     CONTI_NODE_AUTO(2)    , use auto-profile interpolation.
    [DllImport("AXL.dll")] public static extern uint AxmContiStart(int lCoord, uint dwProfileset, int lAngle);
    // API which verifies whether the continuous interpolation is moving in the specific coordinate system. 
    [DllImport("AXL.dll")] public static extern uint AxmContiIsMotion(int lCoord, ref uint upInMotion);

    // API which verifies the index number of the continuous interpolation that is being moving currently while the continuous interpolation is moving in the specific coordinate system. 
    [DllImport("AXL.dll")] public static extern uint AxmContiGetNodeNum(int lCoord, ref int npNodeNum);
    // API which verifies the total number of continuous interpolation index that were set in  the specific coordinate system. 
    [DllImport("AXL.dll")] public static extern uint AxmContiGetTotalNodeNum(int lCoord, ref int npNodeNum);
      
//====================trigger API ===============================================================================================================================
    // Set whether to use of trigger function, output level, position comparator, trigger signal delay time and trigger output mode ino specific axis.
    //  dTrigTime  : trigger output time 
    //             : 1usec - max 50msec ( set 1 - 50000)
    //  upTriggerLevel  : whether to use or not use     => LOW(0), HIGH(1), UNUSED(2), USED(3)
    //  uSelect         : Standard position to use    => COMMAND(0), ACTUAL(1)
    //  uInterrupt      : set interrupt        => DISABLE(0), ENABLE(1)
    
    // Set trigger signal delay time , trigger output level and trigger output method in specific axis. 
    [DllImport("AXL.dll")] public static extern uint AxmTriggerSetTimeLevel(int lAxisNo, double dTrigTime, uint uTriggerLevel, uint uSelect, uint uInterrupt);
        // Return trigger signal delay time , trigger output level and trigger output method to specific axis.
    [DllImport("AXL.dll")] public static extern uint AxmTriggerGetTimeLevel(int lAxisNo, ref double dTrigTime, ref uint uTriggerLevel, ref uint uSelect, ref uint uInterrupt);

    //  uMethod :   PERIOD_MODE   0x0 : cycle trigger method using trigger position value
    //              ABS_POS_MODE   0x1 : Trigger occurs at trigger absolute position,  absolute position method
    //  dPos : in case of cycle selection : the relevant position  for output by position and position. 
    // in case of absolute selection: The position on which to output, If same as this position then output goes out unconditionally. 
    [DllImport("AXL.dll")] public static extern uint AxmTriggerSetAbsPeriod(int nAxisNo, uint uMethod, double dPos);
    
    // Return whether to use of trigger function, output level, position comparator, trigger signal delay time and trigger output mode to specific axis.
    [DllImport("AXL.dll")] public static extern uint AxmTriggerGetAbsPeriod(int nAxisNo, ref uint upMethod, ref double dpPos);

    //  Output the trigger by regular interval from the starting position to the ending position specified by user. 
    [DllImport("AXL.dll")] public static extern uint AxmTriggerSetBlock(int nAxisNo, double dStartPos, double dEndPos, double dPeriodPos);
    // Read trigger setting value of 'AxmTriggerSetBlock' API.
    [DllImport("AXL.dll")] public static extern uint AxmTriggerGetBlock(int nAxisNo, ref double dpStartPos, ref double dpEndPos, ref double dpPeriodPos);
    // User outputs a trigger pulse.
    [DllImport("AXL.dll")] public static extern uint AxmTriggerOneShot(int nAxisNo);
    // User outputs a trigger pulse after several seconds. 
    [DllImport("AXL.dll")] public static extern uint AxmTriggerSetTimerOneshot(int nAxisNo, int mSec);
    // Output absolute position trigger infinite absolute position output.
    [DllImport("AXL.dll")] public static extern uint AxmTriggerOnlyAbs(int nAxisNo,int nTrigNum, ref double[] dTrigPos);
    // Reset trigger settings.
    [DllImport("AXL.dll")] public static extern uint AxmTriggerSetReset(int nAxisNo);

//======== CRC( Remaining pulse clear API) =====================================================================    

    //Level   : LOW(0), HIGH(1), UNUSED(2), USED(3) 
    //uMethod : Available to set the width of remaining pulse eliminating output signal pulse in 2 - 6.
    //          0: Don't care , 1: Don't care, 2: 500 uSec, 3:1 mSec, 4:10 mSec, 5:50 mSec, 6:100 mSec

    //Set whether to use CRC signal in specific axis and output level.
    [DllImport("AXL.dll")] public static extern uint AxmCrcSetMaskLevel(int nAxisNo, uint uLevel, uint uMethod);
        // Return whether to use CRC signal of specific axis and output level.
    [DllImport("AXL.dll")] public static extern uint AxmCrcGetMaskLevel(int nAxisNo, ref uint upLevel, ref uint upMethod);

    //uOnOff  : Whether to generate CRC signal to the Program or not.  (FALSE(0),TRUE(1))

    // Force to generate CRC signal to the specific axis.
    [DllImport("AXL.dll")] public static extern uint AxmCrcSetOutput(int nAxisNo, uint uOnOff);
    // Return whether to forcedly generate CRC signal of specific axis.
    [DllImport("AXL.dll")] public static extern uint AxmCrcGetOutput(int nAxisNo, ref uint upOnOff);


//====== MPG(Manual Pulse Generation) API ===========================================================

    // lInputMethod : Available to set 0-3. 0:OnePhase, 1:TwoPhase1, 2:TwoPhase2, 3:TwoPhase4
    // lDriveMode   : Available to set 0-5
    //                0 :MPG continuous mode ,1 :MPG PRESET mode (Travel up to specific pulse), 2 :COMMAND ABSOLUTE MPG PRESET mode 
    //                3 :ACTUAL ABSOLUTE MPG PRESET mode ,4 :COMMAND ABSOLUTE ZERO MPG PRESET mode, 5 :ACTUAL ABSOLUTE ZERO MPG PRESET mode 
    // MPGPos        : the travel distance by every MPG input signal

    // MPGdenominator: Divide value in MPG(Enter manual pulse generating device)movement
    // dMPGnumerator : Product value in MPG(Enter manual pulse generating device)movement
    // dwNumerator   : Available to set max(from 1 to  64) 
    // dwDenominator : Available to set max(from 1 to  4096)
    // dMPGdenominator = 4096 and MPGnumerator=1 mean that
    // the output is 1 by 1 pulse as 1:1 if one turn of MPG is 200pulse. 
    // If dMPGdenominator = 4096 and MPGnumerator=2 , then it means the output is 2 by 2 pulse as 1:2.  
    // In here, MPG PULSE = ((Numerator) * (Denominator)/ 4096 ) It’s the calculation which outputs to the inside of chip.

    // Set MPG input method, Drive move mode, travel distance, MPG velocity in specific axis.
    [DllImport("AXL.dll")] public static extern uint AxmMPGSetEnable(int nAxisNo, int nInputMethod, int nDriveMode, double dMPGPos, double dVel, double dAcc);
    // Return MPG input method, Drive move mode, travel distance, MPG velocity in specific axis.
    [DllImport("AXL.dll")] public static extern uint AxmMPGGetEnable(int nAxisNo, ref int npInputMethod, ref int npDriveMode, ref double dpMPGPos, ref double dpVel, ref double dpAcc);

    // Set the pulse ratio to travel per pulse in MPG drive move mode to specific axis.
    [DllImport("AXL.dll")] public static extern uint AxmMPGSetRatio(int nAxisNo, double dMPGnumerator, double dMPGdenominator);
    // Return the pulse ratio to travel per pulse in MPG drive move mode to specific axis.
    [DllImport("AXL.dll")] public static extern uint AxmMPGGetRatio(int nAxisNo, ref double dMPGnumerator, ref double dMPGdenominator);

    // Release MPG drive settings to specific axis.
    [DllImport("AXL.dll")] public static extern uint AxmMPGReset(int nAxisNo);
    
//======= Helical move ===========================================================================
    // API which moves helical interpolation by specifying start point, ending point and center point to specific coordinate system.
    // API which moves helical continuous interpolation by specifying start point, ending point and center point to specific coordinate system when it is used with AxmContiBeginNode and  AxmContiEndNode together.  
    // API which saves in internal Queue in order to move the arc continuous interpolation. It is started using AxmContiStart API. ( It is used with continuous interpolation API together)
    //dCenterPos = center point X,Y , dEndPos = ending point X,Y.
    // uCWDir   DIR_CCW(0): Counterclockwise direction,   DIR_CW(1) Clockwise direction
    [DllImport("AXL.dll")] public static extern uint AxmHelixCenterMove(int lCoord, double dCenterXPos, double dCenterYPos, double dEndXPos, double dEndYPos, double dZPos, double dVel, double dAccel, double dDecel, uint uCWDir);

    // API which moves helical interpolation by specifying start point, ending point and radius to specific coordinate system.
    // API which moves helical continuous interpolation by specifying middle point and ending point to specific coordinate system when it is used with AxmContiBeginNode and  AxmContiEndNode together.  
    // API which saves in internal Queue in order to move the arc continuous interpolation. It is started using AxmContiStart API. ( It is used with continuous interpolation API together)
    // dMidPos = middle point X,Y  , dEndPos = ending point X,Y
    [DllImport("AXL.dll")] public static extern uint AxmHelixPointMove(int lCoord, double dMidXPos, double dMidYPos, double dEndXPos, double dEndYPos, double dZPos, double dVel, double dAccel, double dDecel);

    // API which moves helical interpolation by specifying start point, ending point and radius to specific coordinate system.
    // API which moves helical continuous interpolation by specifying middle point ,ending point and radius to specific coordinate system when it is used with AxmContiBeginNode and  AxmContiEndNode together.  
    // API which saves in internal Queue in order to move the arc continuous interpolation. It is started using AxmContiStart API. ( It is used with continuous interpolation API together)
    // dRadius = radius, dEndPos = ending point X,Y  , uShortDistance = small circle(0), large circle(1)
    // uCWDir   DIR_CCW(0): Counterclockwise direction,   DIR_CW(1) Clockwise direction
    [DllImport("AXL.dll")] public static extern uint AxmHelixRadiusMove(int lCoord, double dRadius, double dEndXPos, double dEndYPos, double dZPos, double dVel, double dAccel, double dDecel, uint uCWDir, uint uShortDistance);
    
    // API which moves helical interpolation by specifying start point, revolution angle and radius to specific coordinate system.
    // API which moves helical continuous interpolation by specifying start point , revolution angle and radius to specific coordinate system when it is used with AxmContiBeginNode and  AxmContiEndNode together.
    // API which saves in internal Queue in order to move the arc continuous interpolation. It is started using AxmContiStart API. ( It is used with continuous interpolation API together)
    // dCenterPos = center point X,Y , dAngle = angle.
    // uCWDir   DIR_CCW(0): Counterclockwise direction,   DIR_CW(1) Clockwise direction
    [DllImport("AXL.dll")] public static extern uint AxmHelixAngleMove(int lCoord, double dCenterXPos, double dCenterYPos, double dAngle, double dZPos, double dVel, double dAccel, double dDecel, uint uCWDir);

//======== Spline move =========================================================================== 

    // It’s not used with AxmContiBeginNode and AxmContiEndNode together. 
    // API which moves spline continuous interpolation. API which saves in internal Queue in order to move the arc continuous interpolation. 
    //It is started using AxmContiStart API. ( It is used with continuous interpolation API together)

    // lPosSize : Minimum more than 3 pieces.
    // Enter 0 as for dPoZ value when it is used with 2 axes. 
    // Enter 3 piece as for axis mapping and dPosZ value when it is used with 3axes.    
    [DllImport("AXL.dll")] public static extern uint AxmSplineWrite(int lCoord, int lPosSize, double[] dPosX, double[] dPosY, double dVel, double dAccel, double dDecel, double dPosZ, int lPointFactor);

//======== Compensation Table ====================================================================
    // API which set the parameters for using the geometric compensation feature (Mechatrolink-II products)
    // lNumEntry : minimum entries are 2, maximum entries are 512.
    // dStartPos : starting relative position to apply the compensation.
    // dpPosition, dpCorrection : arrays of position and correction values.
    // bRollOver : enable/disable the roll over feature if the table can not cover the motor travel distance.
    [DllImport("AXL.dll")] public static extern uint AxmCompensationSet(int nAxisNo, int nNumEntry, double dStartPos, ref double dpPosition, ref double dpCorrection, uint uRollOver);
    // Return the number of entry, start position, array of position for moving, array of correction for compensating, setting RollOver feature on specific axis.
    [DllImport("AXL.dll")] public static extern uint AxmCompensationGet(int nAxisNo, ref int npNumEntry, ref double dpStartPos, ref double dpPosition, ref double dpCorrection, ref uint upRollOver);
    // API which enable/disable the compensation feature.
    [DllImport("AXL.dll")] public static extern uint AxmCompensationEnable(int nAxisNo, uint uEnable);
    // Return the setting enable/disable the compensation feature on specific axis.
    [DllImport("AXL.dll")] public static extern uint AxmCompensationIsEnable(int nAxisNo, ref uint upEnable);
    // Return Current Position Compensation Value
    [DllImport("AXL.dll")] public static extern uint AxmCompensationGetCorrection(int lAxisNo, ref double dpCorrection);

//======== Electronic CAM ========================================================================
    // API which set the parameters for using the Ecam feature (Mechatrolink-II products)
    // lAxisNo : one slave axis only has one master axis.
    // lMasterAxis : one master axis can have more than one slave axis.
    // lNumEntry : minimum entries are 2, maximum entries are 512.
    // dMasterStartPos : starting relative position to apply Ecam on master axis.
    // dpMasterPos, dpSlavePos : arrays of position values on master and slave axis.  
    [DllImport("AXL.dll")] public static extern uint AxmEcamSet(int nAxisNo, int nMasterAxisNo, int nNumEntry, double dMasterStartPos, ref double dpMasterPos, ref double dpSlavePos);
    // Return the number of master axis, entries, start position of master axis, array of position on master axis, array of position on slave axis.
    [DllImport("AXL.dll")] public static extern uint AxmEcamGet(int nAxisNo, ref int npMasterAxisNo, ref int npNumEntry, ref double dpMasterStartPos, ref double dpMasterPos, ref double dpSlavePos);
    // API which enable the Ecam feature on slave axis.
    [DllImport("AXL.dll")] public static extern uint AxmEcamEnableBySlave(int nAxisNo, uint uEnable);
    // API which enable the Ecam feature on corresponding slave axes.    
    [DllImport("AXL.dll")] public static extern uint AxmEcamEnableByMaster(int nAxisNo, uint uEnable);
    // Return the setting enable/disable the Ecam feature on slave axis.
    [DllImport("AXL.dll")] public static extern uint AxmEcamIsSlaveEnable(int nAxisNo, ref uint upEnable);    
//--------------------------------------------------------------------------------------------------------------------------------

//======== Servo Status Monitor =====================================================================================
	// Set exception function of specific axis. (Only for MLII, Sigma-5)
    [DllImport("AXL.dll")] public static extern uint AxmStatusSetServoMonitor(int nAxisNo, uint uSelMon, double dActionValue, uint uAction);
    // Return exception function of specific axis. (Only for MLII, Sigma-5)
    [DllImport("AXL.dll")] public static extern uint AxmStatusGetServoMonitor(int nAxisNo, uint uSelMon, ref double dpActionValue, ref uint upAction);
    // Set exception function usage of specific axis. (Only for MLII, Sigma-5)
    [DllImport("AXL.dll")] public static extern uint AxmStatusSetServoMonitorEnable(int nAxisNo, uint uEnable);
    // Return exception function usage of specific axis. (Only for MLII, Sigma-5)
    [DllImport("AXL.dll")] public static extern uint AxmStatusGetServoMonitorEnable(int nAxisNo, ref uint upEnable);

    // Return exception function execution result Flag of specific axis. Auto reset after function execution. (Only for MLII, Sigma-5)
    [DllImport("AXL.dll")] public static extern uint AxmStatusReadServoMonitorFlag(int nAxisNo, uint uSelMon, ref uint upMonitorFlag, ref double dpMonitorValue);
    // Return exception function monitoring information of specific axis. (Only for MLII, Sigma-5)
    [DllImport("AXL.dll")] public static extern uint AxmStatusReadServoMonitorValue(int nAxisNo, uint uSelMon, ref double dpMonitorValue);

    // Set load ratio monitor function of specific axis. (Only for MLII, Sigma-5)
	// dwSelMon = 0 : Accumulated load ratio
	// dwSelMon = 1 : Regenerative load ratio
	// dwSelMon = 2 : Reference Torque load ratio
    [DllImport("AXL.dll")] public static extern uint AxmStatusSetReadServoLoadRatio(int lAxisNo, uint dwSelMon);
    // Return load ratio of specific axis. (Only for MLII, Sigma-5)
    [DllImport("AXL.dll")] public static extern uint AxmStatusReadServoLoadRatio(int lAxisNo, ref double dpMonitorValue);

//======== Only for PCI-R1604-RTEX ==================================================================================
	// Set RTEX A4Nx Scale Coefficient. (Only for RTEX, A4Nx)
    [DllImport("AXL.dll")] public static extern uint AxmMotSetScaleCoeff(int nAxisNo, int lScaleCoeff);
    // Return RTEX A4Nx Scale Coefficient. (Only for RTEX, A4Nx)
    [DllImport("AXL.dll")] public static extern uint AxmMotGetScaleCoeff(int nAxisNo, ref int lpScaleCoeff);
    // Edge detection of specific Input Signal that stop or slow down to stop the function.
    [DllImport("AXL.dll")] public static extern uint AxmMoveSignalSearchEx(int nAxisNo, double dVel, double dAccel, int nDetectSignal, int nSignalEdge, int nSignalMethod);
//-------------------------------------------------------------------------------------------------------------------

//======== Only for PCI-R1604-MLII ==================================================================================
	// Move to the set absolute position.
	// Velocity profile use Trapezoid.
	// Exits API at the point that pulse out starts.
	// Always position(include -position), Velocity, Accel/Deccel Change possible.
    [DllImport("AXL.dll")] public static extern uint AxmMoveToAbsPos(int lAxisNo, double dPos, double dVel, double dAccel, double dDecel);
    // Return current drive velocity of specific axis.
    [DllImport("AXL.dll")] public static extern uint AxmStatusReadVelEx(int lAxisNo, ref double dpVel);
//-------------------------------------------------------------------------------------------------------------------

//======== Only for PCI-R1604-SSCNETIIIH ==================================================================================
	// Set electric ratio. This parameter saved Non-volatile memory.
	// Default value(lNumerator : 4194304(2^22), lDenominator : 10000)
	// MR-J4-B is don't Setting electric ratio, Must be set from the host controller.
	// No.PA06, No.PA07 of Existing Pulse type Servo Driver(MR-J4-A)
    [DllImport("AXL.dll")] public static extern uint AxmMotSetElectricGearRatio(int lAxisNo, int lNumerator, int lDenominator);
    // Return electric ratio of specific axis.
    [DllImport("AXL.dll")] public static extern uint AxmMotGetElectricGearRatio(int lAxisNo, ref int lpNumerator, ref int lpDenominator);

	// Set limit torque value of specific axis.
	// Forward, Backward drive torque limit function.
	// Setting range 1 ~ 1000
	// 0.1% of the maximum torque are controlled.
	[DllImport("AXL.dll")] public static extern uint AxmMotSetTorqueLimit(int lAxisNo, double dbPluseDirTorqueLimit, double dbMinusDirTorqueLimit);

	// Return torque limit value of specific axis.
	[DllImport("AXL.dll")] public static extern uint AxmMotGetTorqueLimit(int lAxisNo, ref double dbpPluseDirTorqueLimit, ref double dbpMinusDirTorqueLimit);

    [DllImport("AXL.dll")] public static extern uint AxmOverridePosSetFunction(int lAxisNo, uint dwUsage, int lDecelPosRatio, double dReserved);

    [DllImport("AXL.dll")] public static extern uint AxmOverridePosGetFunction(int lAxisNo, ref uint dwpUsage, ref long lpDecelPosRatio, ref double dpReserved);    

//======== Only for PCIe-Rxx05-MLIII ==================================================================================
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoGetParameter(int lAxisNo, uint wNo, uint bSize, uint bMode, ref uint pbParam);
    [DllImport("AXL.dll")] public static extern uint AxmM3ServoSetParameter(int lAxisNo, uint wNo, uint bSize, uint bMode, ref uint pbParam);
    [DllImport("AXL.dll")] public static extern uint AxmServoCmdExecution(int lAxisNo, uint dwCommand, uint dwSize, ref uint pdwExcData);
//======== Only for SMP ===============================================================================================
    [DllImport("AXL.dll")] public static extern uint AxmSignalSetInposRange(int lAxisNo, double dInposRange);
    [DllImport("AXL.dll")] public static extern uint AxmSignalGetInposRange(int lAxisNo, ref double dpInposRange);
    [DllImport("AXL.dll")] public static extern uint AxmMotSetOverridePosMode(int lAxisNo, uint dwAbsRelMode);
    [DllImport("AXL.dll")] public static extern uint AxmMotGetOverridePosMode(int lAxisNo, ref uint dwpAbsRelMode);
    [DllImport("AXL.dll")] public static extern uint AxmMotSetOverrideLinePosMode(int lCoordNo, uint dwAbsRelMode);
    [DllImport("AXL.dll")] public static extern uint AxmMotGetOverrideLinePosMode(int lCoordNo, ref uint dwpAbsRelMode);

    [DllImport("AXL.dll")] public static extern uint AxmMoveStartPosEx(int lAxisNo, double dPos, double dVel, double dAccel, double dDecel, double dEndVel);
    [DllImport("AXL.dll")] public static extern uint AxmMovePosEx(int lAxisNo, double dPos, double dVel, double dAccel, double dDecel, double dEndVel);

    [DllImport("AXL.dll")] public static extern uint AxmMoveCoordStop(int lCoordNo, double dDecel); 
    [DllImport("AXL.dll")] public static extern uint AxmMoveCoordEStop(int lCoordNo);
    [DllImport("AXL.dll")] public static extern uint AxmMoveCoordSStop(int lCoordNo);

    [DllImport("AXL.dll")] public static extern uint AxmOverrideLinePos(int lCoordNo, ref double dpOverridePos);
    [DllImport("AXL.dll")] public static extern uint AxmOverrideLineVel(int lCoordNo, double dOverrideVel, ref double dpDistance);

    [DllImport("AXL.dll")] public static extern uint AxmOverrideLineAccelVelDecel(int lCoordNo, double dOverrideVelocity, double dMaxAccel, double dMaxDecel, ref double dpDistance);
    [DllImport("AXL.dll")] public static extern uint AxmOverrideAccelVelDecelAtPos(int lAxisNo, double dPos, double dVel, double dAccel, double dDecel, double dOverridePos, double dOverrideVel, double dOverrideAccel, double dOverrideDecel, int lTarget);

    [DllImport("AXL.dll")] public static extern uint AxmEGearSet(int lMasterAxisNo, int lSize, ref int lpSlaveAxisNo, ref double dpGearRatio);
    [DllImport("AXL.dll")] public static extern uint AxmEGearGet(int lMasterAxisNo, ref int lpSize, ref int lpSlaveAxisNo, ref double dpGearRatio);
    [DllImport("AXL.dll")] public static extern uint AxmEGearReset(int lMasterAxisNo);
    [DllImport("AXL.dll")] public static extern uint AxmEGearEnable(int lMasterAxisNo, uint dwEnable);
    [DllImport("AXL.dll")] public static extern uint AxmEGearIsEnable(int lMasterAxisNo, ref uint dwpEnable);    

    [DllImport("AXL.dll")] public static extern uint AxmMotSetEndVel(int lAxisNo, double dEndVelocity);
    [DllImport("AXL.dll")] public static extern uint AxmMotGetEndVel(int lAxisNo, ref double dpEndVelocity);
}
