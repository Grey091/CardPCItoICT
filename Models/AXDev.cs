using System.Runtime.InteropServices;

public class CAXDEV
{
    // Use Board Number and find Board Address
    [DllImport("AXL.dll")] public static extern uint  AxlGetBoardAddress(int lBoardNo, ref uint upBoardAddress);
    // Use Board Number and find Board ID
    [DllImport("AXL.dll")] public static extern uint  AxlGetBoardID(int lBoardNo, ref uint upBoardID);
    // Use Board Number and find Board Version
    [DllImport("AXL.dll")] public static extern uint  AxlGetBoardVersion(int lBoardNo, ref uint upBoardVersion);
    // Use Board Number/Module Position and find Module ID
    [DllImport("AXL.dll")] public static extern uint  AxlGetModuleID(int lBoardNo, int lModulePos, ref uint upModuleID);
    // Use Board Number/Module Position and find Module Version
    [DllImport("AXL.dll")] public static extern uint  AxlGetModuleVersion(int lBoardNo, int lModulePos, ref uint upModuleVersion);
    
    [DllImport("AXL.dll")] public static extern uint AxlGetModuleNodeInfo(int nBoardNo, int nModulePos, ref uint upNetNo, ref uint upNodeAddr);

    // Only for PCI-R1604[RTEX]
    // Writing user data to embedded flash memory
    // lPageAddr(0 ~ 199)
    // lByteNum(1 ~ 120)
    // Note) Delay time is required for completing writing operation to Flash(Max. 17mSec)
    [DllImport("AXL.dll")] public static extern uint AxlSetDataFlash(int lBoardNo, int lPageAddr, int lBytesNum, ref uint upSetData);
    // Reading datas from embedded flash memory
    // lPageAddr(0 ~ 199)
    // lByteNum(1 ~ 120)
    [DllImport("AXL.dll")] public static extern uint AxlGetDataFlash(int lBoardNo, int lPageAddr, int lBytesNum, ref uint upGetData);

    // Use Board Number/Module Position and find AIO Module Number
    [DllImport("AXL.dll")] public static extern uint  AxaInfoGetModuleNo(int lBoardNo, int lModulePos, ref int lpModuleNo);
    // Use Board Number/Module Position and find DIO Module Number
    [DllImport("AXL.dll")] public static extern uint  AxdInfoGetModuleNo(int lBoardNo, int lModulePos, ref int lpModuleNo);

    // IPCOMMAND Setting at an appoint axis
    [DllImport("AXL.dll")] public static extern uint  AxmSetCommand(int lAxisNo, uint sCommand);
    // 8bit IPCOMMAND Setting at an appoint axis
    [DllImport("AXL.dll")] public static extern uint  AxmSetCommandData08(int lAxisNo, uint sCommand, uint uData);
    // Get 8bit IPCOMMAND at an appoint axis
    [DllImport("AXL.dll")] public static extern uint  AxmGetCommandData08(int lAxisNo, uint sCommand, ref uint upData);
    // 16bit IPCOMMAND Setting at an appoint axis
    [DllImport("AXL.dll")] public static extern uint  AxmSetCommandData16(int lAxisNo, uint sCommand, uint uData);
    // Get 16bit IPCOMMAND at an appoint axis
    [DllImport("AXL.dll")] public static extern uint  AxmGetCommandData16(int lAxisNo, uint sCommand, ref uint upData);
    // 24bit IPCOMMAND Setting at an appoint axis
    [DllImport("AXL.dll")] public static extern uint  AxmSetCommandData24(int lAxisNo, uint sCommand, uint uData);
    // Get 24bit IPCOMMAND at an appoint axis
    [DllImport("AXL.dll")] public static extern uint  AxmGetCommandData24(int lAxisNo, uint sCommand, ref uint upData);
    // 32bit IPCOMMAND Setting at an appoint axis
    [DllImport("AXL.dll")] public static extern uint  AxmSetCommandData32(int lAxisNo, uint sCommand, uint uData);
    // Get 32bit IPCOMMAND at an appoint axis
    [DllImport("AXL.dll")] public static extern uint  AxmGetCommandData32(int lAxisNo, uint sCommand, ref uint upData);
    // QICOMMAND Setting at an appoint axis
    [DllImport("AXL.dll")] public static extern uint  AxmSetCommandQi(int lAxisNo, uint sCommand);
    // 8bit QICOMMAND Setting at an appoint axis
    [DllImport("AXL.dll")] public static extern uint  AxmSetCommandData08Qi(int lAxisNo, uint sCommand, uint uData);
    // Get 8bit QICOMMAND at an appoint axis
    [DllImport("AXL.dll")] public static extern uint  AxmGetCommandData08Qi(int lAxisNo, uint sCommand, ref uint upData);
    // 16bit QICOMMAND Setting at an appoint axis
    [DllImport("AXL.dll")] public static extern uint  AxmSetCommandData16Qi(int lAxisNo, uint sCommand, uint uData);
    // Get 16bit QICOMMAND at an appoint axis
    [DllImport("AXL.dll")] public static extern uint  AxmGetCommandData16Qi(int lAxisNo, uint sCommand, ref uint upData);
    // 24bit QICOMMAND Setting at an appoint axis
    [DllImport("AXL.dll")] public static extern uint  AxmSetCommandData24Qi(int lAxisNo, uint sCommand, uint uData);
    // Get 24bit QICOMMAND at an appoint axis
    [DllImport("AXL.dll")] public static extern uint  AxmGetCommandData24Qi(int lAxisNo, uint sCommand, ref uint upData);
    // 32bit QICOMMAND Setting at an appoint axis
    [DllImport("AXL.dll")] public static extern uint  AxmSetCommandData32Qi(int lAxisNo, uint sCommand, uint uData);
    // Get 32bit QICOMMAND at an appoint axis
    [DllImport("AXL.dll")] public static extern uint  AxmGetCommandData32Qi(int lAxisNo, uint sCommand, ref uint upData);
    // Get Port Data at an appoint axis - IP
    [DllImport("AXL.dll")] public static extern uint  AxmGetPortData(int lAxisNo,  uint wOffset, ref uint upData);
    // Port Data Setting at an appoint axis - IP
    [DllImport("AXL.dll")] public static extern uint  AxmSetPortData(int lAxisNo, uint wOffset, uint dwData);
    // Get Port Data at an appoint axis - QI
    [DllImport("AXL.dll")] public static extern uint  AxmGetPortDataQi(int lAxisNo, uint byOffset, ref uint wData);
    // Port Data Setting at an appoint axis - QI
    [DllImport("AXL.dll")] public static extern uint  AxmSetPortDataQi(int lAxisNo, uint byOffset, uint wData);

    // Set the script at an appoint axis.  - IP
    // sc    : Script number (1 - 4)
    // event : Define an event SCRCON to happen.
    //         Set event, a number of axis, axis which the event happens, event content 1, 2 attribute
    // cmd   : Define a selection SCRCMD however we change any content
    // data  : Selection to change any Data.    
    [DllImport("AXL.dll")] public static extern uint  AxmSetScriptCaptionIp(int lAxisNo, int sc, uint uEvent, uint data);
    // Return the script at an appoint axis.  - IP
    [DllImport("AXL.dll")] public static extern uint  AxmGetScriptCaptionIp(int lAxisNo, int sc, ref uint uEvent, ref uint data);
    // Set the script at an appoint axis.  - QI
    // sc    : Script number (1 - 4)
    // event : Define an event SCRCON to happen.
    //         Set event, a number of axis, axis which the event happens, event content 1, 2 attribute
    // cmd   : Define a selection SCRCMD however we change any content
    // data  : Selection to change any Data.
    [DllImport("AXL.dll")] public static extern uint  AxmSetScriptCaptionQi(int lAxisNo, int sc, uint uEvent, uint cmd, uint data);
    // Return the script at an appoint axis. - QI
    [DllImport("AXL.dll")] public static extern uint  AxmGetScriptCaptionQi(int lAxisNo, int sc, ref uint uEvent, ref uint cmd, ref uint data);
    // Clear orders a script inside Queue Index at an appoint axis
    // uSelect IP. 
    // uSelect(0): Script Queue Index Clear.
    //        (1): Caption Queue Index Clear.
    // uSelect QI. 
    // uSelect(0): Script Queue 1 Index Clear.
    //        (1): Script Queue 2 Index Clear.
    [DllImport("AXL.dll")] public static extern uint  AxmSetScriptCaptionQueueClear(int lAxisNo, uint uSelect);
    // Return Index of a script inside Queue at an appoint axis.
    // uSelect IP
    // uSelect(0): Read Script Queue Index
    //        (1): Read Caption Queue Index
    // uSelect QI. 
    // uSelect(0): Read Script Queue 1 Index
    //        (1): Read Script Queue 2 Index
    [DllImport("AXL.dll")] public static extern uint  AxmGetScriptCaptionQueueCount(int lAxisNo, ref uint updata, uint uSelect);
    // Return Data number of a script inside Queue at an appoint axis.
    // uSelect IP
    // uSelect(0): Read Script Queue Data
    //        (1): Read Caption Queue Data
    // uSelect QI.
    // uSelect(0): Read Script Queue 1 Data
    //        (1): Read Script Queue 2 Data
    [DllImport("AXL.dll")] public static extern uint  AxmGetScriptCaptionQueueDataCount(int lAxisNo, ref uint updata, uint uSelect);
    // Read an inside data.
    [DllImport("AXL.dll")] public static extern uint  AxmGetOptimizeDriveData(int lAxisNo, double dMinVel, double dVel, double dAccel, double  dDecel, ref uint wRangeData, ref uint wStartStopSpeedData, ref uint wObjectSpeedData, ref uint wAccelRate, ref uint wDecelRate);
    // Setting up confirmes the register besides within the board by Byte.
    [DllImport("AXL.dll")] public static extern uint  AxmBoardWriteByte(int lBoardNo, uint wOffset, uint byData);
    [DllImport("AXL.dll")] public static extern uint  AxmBoardReadByte(int lBoardNo, uint wOffset, ref uint byData);
    // Setting up confirmes the register besides within the board by Word.
    [DllImport("AXL.dll")] public static extern uint  AxmBoardWriteWord(int lBoardNo, uint wOffset, uint wData);
    [DllImport("AXL.dll")] public static extern uint  AxmBoardReadWord(int lBoardNo, uint wOffset, ref uint wData);
    // Setting up confirmes the register besides within the board by DWord.
    [DllImport("AXL.dll")] public static extern uint  AxmBoardWriteDWord(int lBoardNo, uint wOffset, uint dwData);
    [DllImport("AXL.dll")] public static extern uint  AxmBoardReadDWord(int lBoardNo, uint wOffset, ref uint dwData);
    // Setting up confirmes the register besides within the Module by Byte.
    [DllImport("AXL.dll")] public static extern uint  AxmModuleWriteByte(int lBoardNo, int lModulePos, uint wOffset, uint byData);
    [DllImport("AXL.dll")] public static extern uint  AxmModuleReadByte(int lBoardNo, int lModulePos, uint wOffset, ref uint byData);
    // Setting up confirmes the register besides within the Module by Word.
    [DllImport("AXL.dll")] public static extern uint  AxmModuleWriteWord(int lBoardNo, int lModulePos, uint wOffset, uint wData);
    [DllImport("AXL.dll")] public static extern uint  AxmModuleReaWord(int lBoardNo, int lModulePos, uint wOffset, ref uint wData);
    // Setting up confirmes the register besides within the Module by DWord.
    [DllImport("AXL.dll")] public static extern uint  AxmModuleWriteDWord(int lBoardNo, int lModulePos, uint wOffset, uint dwData);
    [DllImport("AXL.dll")] public static extern uint  AxmModuleReadDWord(int lBoardNo, int lModulePos, uint wOffset, ref uint dwData);
    // Set EXCNT (Pos = Unit)
    [DllImport("AXL.dll")] public static extern uint  AxmStatusSetActComparatorPos(int lAxisNo, double dPos);
    // Return EXCNT (Positon = Unit)
    [DllImport("AXL.dll")] public static extern uint  AxmStatusGetActComparatorPos(int lAxisNo, ref double dpPos);
    // Set INCNT (Pos = Unit)
    [DllImport("AXL.dll")] public static extern uint  AxmStatusSetCmdComparatorPos(int lAxisNo, double dPos);
    // Return INCNT (Pos = Unit)
    [DllImport("AXL.dll")] public static extern uint  AxmStatusGetCmdComparatorPos(int lAxisNo, ref double dpPos);

//=========== Append function. =========================================================================================================
    // Increase a straight line interpolation at speed to the infinity.
    // Must put the distance speed rate.
    [DllImport("AXL.dll")] public static extern uint  AxmLineMoveVel(int lCoord, double dVel, double dAccel, double dDecel);

//========= Sensor drive API( Read carefully: Available only PI , No function in QI) =========================================================================
    // Set mark signal( used in sensor positioning drive)
    [DllImport("AXL.dll")] public static extern uint  AxmSensorSetSignal(int lAxisNo, uint uLevel);
    // Verify mark signal( used in sensor positioning drive)
    [DllImport("AXL.dll")] public static extern uint  AxmSensorGetSignal(int lAxisNo, ref uint upLevel);
    // Verify mark signal( used in sensor positioning drive)state
    [DllImport("AXL.dll")] public static extern uint  AxmSensorReadSignal(int lAxisNo, ref uint upStatus);
    // Drive API which moves from edge detection of sensor input pin during velocity mode driving as much as specified position, then stop. Applied motion is started upon the start of API, and escapes from the API after the motion is completed.
    [DllImport("AXL.dll")] public static extern uint  AxmSensorMovePos(int lAxisNo, double dPos, double dVel, double dAccel, double dDecel, int lMethod);
    // Drive API which moves from edge detection of sensor input pin during velocity mode driving as much as specified position, then stop. Applied motion is started upon the start of API, then escapes from the API immediately without waiting until the motion is completed.
    [DllImport("AXL.dll")] public static extern uint  AxmSensorStartMovePos(int lAxisNo, double dPos, double dVel, double dAccel, double dDecel, int lMethod);
    
    [DllImport("AXL.dll")] public static extern uint AxmHomeGetStepTrace(int nAxisNo, ref uint upStepCount, ref uint upMainStepNumber, ref uint upStepNumber, ref uint upStepBranch);
//======= Additive home search (Applicable to PI-N804/404  only) =================================================================================
    // Set home setting parameters of axis specified by user. (Use exclusive-use register for QI chip).
    // uZphasCount : Z phase count after home completion. (0 - 15)
    // lHomeMode   : Home setting mode( 0 - 12)
    // lClearSet   : Select use of position clear and remaining pulse clear (0 - 3)
    //               0: No use of position clear, no use of remaining pulse clear
    //               1: use of position clear, no use of remaining pulse clear
    //               2: No use of position clear, use of remaining pulse clear
    //               3: use of position clear, use of remaining pulse clear
    // dOrgVel : Set Org  Speed related home
    // dLastVel: Set Last Speed related home
    [DllImport("AXL.dll")] public static extern uint  AxmHomeSetConfig(int lAxisNo, uint uZphasCount, int lHomeMode, int lClearSet, double dOrgVel, double dLastVel, double dLeavePos);
    // Return home setting parameters of axis specified by user.
    [DllImport("AXL.dll")] public static extern uint  AxmHomeGetConfig(int lAxisNo, ref uint upZphasCount, ref int lpHomeMode, ref int lpClearSet, ref double dpOrgVel, ref double dpLastVel, ref double dpLeavePos); //KKJ(070215)
    // Start home search of axis specified by user
    // Set when use lHomeMode : Set 0 - 5 (Start search after Move Return.)
    // If lHomeMode -1is used as it is, the setting is done as used in HomeConfig.
    // Move direction      : CW when Vel value is positive, CCW when negative.
    [DllImport("AXL.dll")] public static extern uint  AxmHomeSetMoveSearch(int lAxisNo, double dVel, double dAccel, double dDecel);
    // Start home return of axis specified by user.
    // Set when lHomeMode is used: set 0 - 12  
    // If lHomeMode -1is used as it is, the setting is done as used in HomeConfig.
    // Move direction      : CW when Vel value is positive, CCW when negative.
    [DllImport("AXL.dll")] public static extern uint  AxmHomeSetMoveReturn(int lAxisNo, double dVel, double dAccel, double dDecel);
    // Home separation of axis specified by user is started. 
    // Move direction      : CW when Vel value is positive, CCW when negative.
    [DllImport("AXL.dll")] public static extern uint  AxmHomeSetMoveLeave(int lAxisNo, double dVel, double dAccel, double dDecel);
    // User start home search of multi-axis specified by user. 
    // Set when use lHomeMode : Set 0 - 5 (Start search after Move Return.)
    // If lHomeMode -1is used as it is, the setting is done as used in HomeConfig.
    // Move direction      : CW when Vel value is positive, CCW when negative.
    [DllImport("AXL.dll")] public static extern uint  AxmHomeSetMultiMoveSearch(int lArraySize, ref int lpAxesNo, ref double dpVel, ref double dpAccel, ref double dpDecel);
    //Set move velocity profile mode of specific coordinate system. 
    // (caution : Available to use only after axis mapping)
    //ProfileMode : '0' - symmetry Trapezoid
    //              '1' - asymmetric Trapezoid
    //              '2' - symmetry Quasi-S Curve
    //              '3' - symmetry S Curve
    //              '4' - asymmetric S Curve
    [DllImport("AXL.dll")] public static extern uint  AxmContiSetProfileMode(int lCoord, uint uProfileMode);
    // Return move velocity profile mode of specific coordinate system.
    [DllImport("AXL.dll")] public static extern uint  AxmContiGetProfileMode(int lCoord, ref uint upProfileMode);
    [DllImport("AXL.dll")] public static extern uint  AxmMoveProfilePos(int lAxisNo, int lProfileSize, ref double dpPos, ref double dpMinVel, ref double dpVel, ref double dpAccel, ref double dpDecel);

    //========== Reading group for input interrupt occurrence flag
    // Reading the interrupt occurrence state by bit unit in specified input contact module and Offset position of Interrupt Flag Register
    [DllImport("AXL.dll")] public static extern uint  AxdiInterruptFlagReadBit(int lModuleNo, int lOffset, ref uint upValue);
    // Reading the interrupt occurrence state by byte unit in specified input contact module and Offset position of Interrupt Flag Register
    [DllImport("AXL.dll")] public static extern uint  AxdiInterruptFlagReadByte(int lModuleNo, int lOffset, ref uint upValue);
    // Reading the interrupt occurrence state by word unit in specified input contact module and Offset position of Interrupt Flag Register
    [DllImport("AXL.dll")] public static extern uint  AxdiInterruptFlagReaByte(int lModuleNo, int lOffset, ref uint upValue);
    // Reading the interrupt occurrence state by double word unit in specified input contact module and Offset position of Interrupt Flag Register
    [DllImport("AXL.dll")] public static extern uint  AxdiInterruptFlagReadWord(int lModuleNo, int lOffset, ref uint upValue);
    // Reading the interrupt occurrence state by bit unit in entire input contact module and Offset position of Interrupt Flag Register
    [DllImport("AXL.dll")] public static extern uint  AxdiInterruptFlagRead(int lOffset, ref uint upValue);

//========= API related log ==========================================================================================    
    // This API sets or resets in order to monitor the API execution result of set axis in EzSpy. 
    // uUse : use or not use => DISABLE(0), ENABLE(1)
    [DllImport("AXL.dll")] public static extern uint  AxmLogSetAxis(int lAxisNo, uint uUse);
    // This API verifies if the API execution result of set axis is monitored in EzSpy. 
    [DllImport("AXL.dll")] public static extern uint  AxmLogGetAxis(int lAxisNo, ref uint upUse);
    //==Log
    // Set whether execute log output on EzSpy of specified module
    [DllImport("AXL.dll")] public static extern uint  AxdLogSetModule(int lModuleNo, uint uUse);
    // Verify whether execute log output on EzSpy of specified module
    [DllImport("AXL.dll")] public static extern uint  AxdLogGetModule(int lModuleNo, ref uint upUse);
    //Set whether to log output to EzSpy of specified input channel
    [DllImport("AXL.dll")] public static extern uint  AxaiLogSetChannel(int lChannelNo, uint uUse);
    //Verify whether to log output to EzSpy of specified input channel
    [DllImport("AXL.dll")] public static extern uint  AxaiLogGetChannel(int lChannelNo, ref uint upUse);
    //Set whether to log output in EzSpy of specified output channel
    [DllImport("AXL.dll")] public static extern uint  AxaoLogSetChannel(int lChannelNo, uint uUse);
    //Verify whether log output is done in EzSpy of specified output channel.
    [DllImport("AXL.dll")] public static extern uint  AxaoLogGetChannel(int lChannelNo, ref uint upUse);

    //Verify whether to firmware version designated RTEX board.
    [DllImport("AXL.dll")] public static extern uint  AxlGetFirmwareVersion(int nBoardNo, ref char szVersion);
    //Sent to firmware designated board.
    [DllImport("AXL.dll")] public static extern uint  AxlSetFirmwareCopy(int nBoardNo, ref ushort wData, ref ushort wCmdData);
    //Execute Firmware update to designated board. 
    [DllImport("AXL.dll")] public static extern uint  AxlSetFirmwareUpdate(int nBoardNo);
    //Verify whether currently RTEX status Initialized.
    [DllImport("AXL.dll")] public static extern uint  AxlCheckStatus(int nBoardNo, ref uint dwStatus);
    //To Initialized currently RTEX on designated board.
    [DllImport("AXL.dll")] public static extern uint  AxlInitRtex(int nBoardNo, uint dwOption);
    //Execute universal command designated axis of board.
    [DllImport("AXL.dll")] public static extern uint  AxlRtexUniversalCmd(int nBoardNo, ushort wCmd, ushort wOffset, ref ushort wData);
    //Execute RTEX communication command designated axis.
    [DllImport("AXL.dll")] public static extern uint  AxmRtexSlaveCmd(int nAxisNo, uint dwCmdCode, uint dwTypeCode, uint dwIndexCode, uint dwCmdConfigure, uint dwValue);
    //Verify whether Result of RTEX communication command designated axis.
    [DllImport("AXL.dll")] public static extern uint  AxmRtexGetSlaveCmdResult(int nAxisNo, ref uint dwIndex, ref uint dwValue);
    //Verify whether RTEX status information designated axis.
    [DllImport("AXL.dll")] public static extern uint  AxmRtexGetAxisStatus(int nAxisNo, ref uint dwStatus);
    //Verify whether RTEX communication return information designated axis.(Actual position, Velocity, Torque)
    [DllImport("AXL.dll")] public static extern uint  AxmRtexGetAxisReturnData(int nAxisNo,  ref uint dwReturn1, ref uint dwReturn2, ref uint dwReturn3);
    //Verify whether currently status information of RTEX slave axis.(mechanical, Inposition and etc)
    [DllImport("AXL.dll")] public static extern uint  AxmRtexGetAxisSlaveStatus(int nAxisNo,  ref uint dwStatus);        
    [DllImport("AXL.dll")] public static extern uint  AxmSetAxisCmd(int nAxisNo, ref uint tagCommand);
    [DllImport("AXL.dll")] public static extern uint  AxmGetAxisCmdResult(int nAxisNo, ref uint tagCommand);
	
    [DllImport("AXL.dll")] public static extern uint  AxlGetDpRamData(int nBoardNo, ushort wAddress, ref uint dwpRdData);     
    [DllImport("AXL.dll")] public static extern uint  AxlBoardReadDpramWord(int nBoardNo, ushort uOffset, ref uint upRdData);
    [DllImport("AXL.dll")] public static extern uint  AxlSetSendBoardCommand(int nBoardNo, uint uCommand, ref uint upSendData, uint uLength);
    [DllImport("AXL.dll")] public static extern uint  AxlGetResponseBoardCommand(int nBoardNo, ref uint upReadData);
    [DllImport("AXL.dll")] public static extern uint  AxmInfoGetFirmwareVersion(int nAxisNo, ref ushort ucaFirmwareVersion);
    [DllImport("AXL.dll")] public static extern uint  AxaInfoGetFirmwareVersion(int nModuleNo, ref ushort ucaFirmwareVersion);
    [DllImport("AXL.dll")] public static extern uint  AxdInfoGetFirmwareVersion(int nModuleNo, ref ushort ucaFirmwareVersion);

    //======== Only for PCI-R1604-MLII =========================================================================== .
    [DllImport("AXL.dll")] public static extern uint  AxmSetTorqFeedForward(int nAxisNo, uint uTorqFeedForward);
    [DllImport("AXL.dll")] public static extern uint  AxmGetTorqFeedForward(int nAxisNo, ref uint upTorqFeedForward);
    [DllImport("AXL.dll")] public static extern uint  AxmSetVelocityFeedForward(int nAxisNo, uint uVelocityFeedForward);
    [DllImport("AXL.dll")] public static extern uint  AxmGetVelocityFeedForward(int nAxisNo, ref uint upVelocityFeedForward);

    // Set Encoder type.
    // Default value : 0(TYPE_INCREMENTAL)
    // Setting range : 0 ~ 1
    // dwEncoderType : 0(TYPE_INCREMENTAL), 1(TYPE_ABSOLUTE).
    [DllImport("AXL.dll")]
    public static extern uint AxmSignalSetEncoderType(int lAxisNo, uint uEncoderType);

    // Verify Encoder type.
    [DllImport("AXL.dll")] public static extern uint AxmSignalGetEncoderType(int lAxisNo, ref uint upEncoderType);

    // For updating the slave firmware(only for RTEX-PM).
    //DWORD   __stdcall AxmSetSendAxisCommand(long lAxisNo, WORD wCommand, WORD* wpSendData, WORD wLength);

    //======== Only for PCI-R1604-RTEX, RTEX-PM============================================================== 
    // When Input Universal Input 2, 3, Set Jog move velocity
    // Set only once execute after all drive setting (Ex, PulseOutMethod, MoveUnitPerPulse etc..)
    [DllImport("AXL.dll")] public static extern uint AxmMotSetUserMotion(int lAxisNo, double dVelocity, double dAccel, double dDecel);

    // When Input Universal Input 2, 3, Set Jog move usage
    // Setting value :  0(DISABLE), 1(ENABLE)
    [DllImport("AXL.dll")] public static extern uint AxmMotSetUserMotionUsage(int lAxisNo, uint dwUsage);

    // Set Load/UnLoad Position to Automatically move use MPGP Input  
    [DllImport("AXL.dll")] public static extern uint AxmMotSetUserPosMotion(int lAxisNo, double dVelocity, double dAccel, double dDecel, double dLoadPos, double dUnLoadPos, uint dwFilter, uint dwDelay);

    // Set Usage Load/UnLoad Position to Automatically move use MPGP Input 
    // Setting value :  0(DISABLE), 1(Position function A), 2(Position function B)
    [DllImport("AXL.dll")] public static extern uint AxmMotSetUserPosMotionUsage(int lAxisNo, uint dwUsage);
    //======================================================================================================== 

    //======== SIO-CN2CH, Only for absolute position trigger module(B0)======================================= 
    [DllImport("AXL.dll")] public static extern uint AxcKeWriteRamDataAddr(int lChannelNo, uint dwAddr, uint dwData);
    [DllImport("AXL.dll")] public static extern uint AxcKeReadRamDataAddr(int lChannelNo, uint dwAddr, ref uint dwpData);
    [DllImport("AXL.dll")] public static extern uint AxcKeResetRamDataAll(int lModuleNo, uint dwData);
    [DllImport("AXL.dll")] public static extern uint AxcTriggerSetTimeout(int lChannelNo, uint dwTimeout);
    [DllImport("AXL.dll")] public static extern uint AxcTriggerGetTimeout(int lChannelNo, ref uint dwpTimeout);
    [DllImport("AXL.dll")] public static extern uint AxcStatusGetWaitState(int lChannelNo, ref uint dwpState);
    [DllImport("AXL.dll")] public static extern uint AxcStatusSetWaitState(int lChannelNo, uint dwState);
    //======================================================================================================== 

    //======== Only for PCI-N804/N404, Sequence Motion ===========================================================
    // Set Axis Information of sequence Motion (min 1axis)
    // lSeqMapNo : Sequence Motion Index Point
    // lSeqMapSize : Number of axis
    // long* LSeqAxesNo : Number of arrary
    [DllImport("AXL.dll")] public static extern uint AxmSeqSetAxisMap(int lSeqMapNo, int lSeqMapSize, ref int lSeqAxesNo);
    [DllImport("AXL.dll")] public static extern uint AxmSeqGetAxisMap(int lSeqMapNo, ref int lSeqMapSize, ref int lSeqAxesNo);

    // Set Standard(Master)Axis of Sequence Motion.
    // By all means Set in AxmSeqSetAxisMap setting axis.
    [DllImport("AXL.dll")] public static extern uint AxmSeqSetMasterAxisNo(int lSeqMapNo, int lMasterAxisNo);

    // Notifies the library node start loading of Sequence Motion.
    [DllImport("AXL.dll")] public static extern uint AxmSeqBeginNode(int lSeqMapNo);

    // Notifies the library node end loading of Sequence Motion.
    [DllImport("AXL.dll")] public static extern uint AxmSeqEndNode(int lSeqMapNo);

    // Start Sequence Motion Move.
    [DllImport("AXL.dll")] public static extern uint AxmSeqStart(int lSeqMapNo, ref uint dwStartOption);

    // Set each profile node Information of Sequence Motion in Library.
    // if used 1axis Sequence Motion, Must be Set *dPosition one Array.
    [DllImport("AXL.dll")] public static extern uint AxmSeqAddNode(int lSeqMapNo, ref double dPosition, double dVelocity, double dAcceleration, double dDeceleration, double dNextVelocity);

    // Return Node Index number of Sequence Motion.
    [DllImport("AXL.dll")] public static extern uint AxmSeqGetNodeNum(int lSeqMapNo, ref int lCurNodeNo);

    // Return All node count of Sequence Motion.
    [DllImport("AXL.dll")] public static extern uint AxmSeqGetTotalNodeNum(int lSeqMapNo, ref int lTotalNodeCnt);

    // Return Sequence Motion drive status  of specific SeqMap.
    // dwInMotion : 0(Drive end), 1(In drive)
    [DllImport("AXL.dll")] public static extern uint AxmSeqIsMotion(int lSeqMapNo, ref uint dwInMotion);

    // Clear Sequence Motion Memory.
    [DllImport("AXL.dll")] public static extern uint AxmSeqWriteClear(int lSeqMapNo);

    // Stop sequence motion
    // dwStopMode : 0(EMERGENCY_STOP), 1(SLOWDOWN_STOP) 
    [DllImport("AXL.dll")] public static extern uint AxmSeqStop(int lSeqMapNo, uint dwStopMode);
    //======================================================================================================== 
}                                                 
  
  
  
