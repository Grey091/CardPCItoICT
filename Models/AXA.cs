/****************************************************************************
*****************************************************************************
**
** File Name
** ---------
**
** AXA.CS
**
** COPYRIGHT (c) AJINEXTEK Co., LTD
**
*****************************************************************************
*****************************************************************************
**
** Description
** -----------
** Ajinextek Analog Library Header File
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

using System;
using System.Runtime.InteropServices;

public unsafe class CAXA
{
//========== Board and verification API group of module information =================================================================================
    //Verify if AIO module exists
    [DllImport("AXL.dll")] public static extern uint AxaInfoIsAIOModule(ref uint upStatus);
    
    //Verify AIO module number
    [DllImport("AXL.dll")] public static extern uint AxaInfoGetModuleNo(int lBoardNo, int lModulePos, ref int lpModuleNo);
    
    //Verify the number of AIO module
    [DllImport("AXL.dll")] public static extern uint AxaInfoGetModuleCount(ref int lpModuleCount);
    
    //Verify the number of input channels of specified module
    [DllImport("AXL.dll")] public static extern uint AxaInfoGetInputCount(int lModuleNo, ref int lpCount);
    
    //Verify the number of output channels of specified module
    [DllImport("AXL.dll")] public static extern uint AxaInfoGetOutputCount(int lModuleNo, ref int lpCount);
    
    //Verify the first channel number of specified module
    [DllImport("AXL.dll")] public static extern uint AxaInfoGetChannelNoOfModuleNo(int lModuleNo, ref int lpChannelNo);

    // Verify the first Input channel number of specified module (Inputmodule, Integration for input/output Module)
    [DllImport("AXL.dll")] public static extern uint AxaInfoGetChannelNoAdcOfModuleNo(int lModuleNo, ref int lpChannelNo);

    // Verify the first output channel number of specified module (Inputmodule, Integration for input/output Module)
    [DllImport("AXL.dll")] public static extern uint AxaInfoGetChannelNoDacOfModuleNo(int lModuleNo, ref int lpChannelNo);
    
    //Verify base board number, module position and module ID with specified module number
    [DllImport("AXL.dll")] public static extern uint AxaInfoGetModule(int lModuleNo, ref int lpBoardNo, ref int lpModulePos, ref uint upModuleID);

    //Verify Module status of specified module board
    [DllImport("AXL.dll")] public static extern uint AxaInfoGetModuleStatus(int lModuleNo);

//========== API group of input module information search ====================================================================================
    //Verify module number with specified input channel number
    [DllImport("AXL.dll")] public static extern uint AxaiInfoGetModuleNoOfChannelNo(int lChannelNo, ref int lpModuleNo);
    
    //Verify the number of entire channels of analog input module
    [DllImport("AXL.dll")] public static extern uint AxaiInfoGetChannelCount(ref int lpChannelCount);

//========== API group for setting and verifying of input module interrupt ============================================================
    //Use window message, callback API or event method in order to get event message into specified channel. Use for the time of collection action( refer AxaStartMultiChannelAdc ) of continuous data by H/W timer
    //(Timer Trigger Mode, External Trigger Mode)
    [DllImport("AXL.dll")] public static extern uint AxaiEventSetChannel(int lModuleNo, IntPtr hWnd, uint uMessage, CAXHS.AXT_INTERRUPT_PROC pProc, ref uint pEvent);
    
    //Set whether to use event in specified input channel
    //======================================================//
    // uUse     : DISABLE(0)    // Event Disable
    //          : ENABLE(1)     // Event Enable
    //======================================================//
    [DllImport("AXL.dll")] public static extern uint AxaiEventSetChannelEnable(int lChannelNo, uint uUse);
    
    //Verify whether to use event in specified input channel
    //======================================================//
    // *upUse   : DISABLE(0)    // Event Disable
    //          : ENABLE(1)     // Event Enable
    //======================================================//
    [DllImport("AXL.dll")] public static extern uint AxaiEventGetChannelEnable(int lChannelNo, ref uint upUse);
    
    //Set whether to use event in specified multiple input channels
    //======================================================//
    // uUse     : DISABLE(0)    // Event Disable
    //          : ENABLE(1)     // Event Enable
    //======================================================//
    [DllImport("AXL.dll")] public static extern uint AxaiEventSetMultiChannelEnable(int lSize, int[] lpChannelNo, uint uUse);
    
    //Set kind of event in specified input channel
    //======================================================//
    // uMask    : DATA_EMPTY(1)
    //          : DATA_MANY(2)
    //          : DATA_SMALL(3)
    //          : DATA_FULL(4)
    //======================================================//
    [DllImport("AXL.dll")] public static extern uint AxaiEventSetChannelMask(int lChannelNo, uint uMask);
    
    //Verify kind of event in specified input channel
    //======================================================//
    // *upMask  : DATA_EMPTY(1)
    //          : DATA_MANY(2)
    //          : DATA_SMALL(3)
    //          : DATA_FULL(4)
    //======================================================//
    [DllImport("AXL.dll")] public static extern uint AxaiEventGetChannelMask(int lChannelNo, ref uint upMask);
    
    //Set kind of event in specified multiple input channels
    //==============================================================================//
    // uMask    : DATA_EMPTY(1)
    //          : DATA_MANY(2)
    //          : DATA_SMALL(3)
    //          : DATA_FULL(4)
    //==============================================================================//
    [DllImport("AXL.dll")] public static extern uint AxaiEventSetMultiChannelMask(int lSize, int[] lpChannelNo, uint uMask);
    
    //Verify event occurrence position
    //==============================================================================//
    // *upMode  : AIO_EVENT_DATA_UPPER(1)
    //          : AIO_EVENT_DATA_LOWER(2)
    //          : AIO_EVENT_DATA_FULL(3)
    //          : AIO_EVENT_DATA_EMPTY(4)
    //==============================================================================//
    [DllImport("AXL.dll")] public static extern uint AxaiEventRead(ref int lpChannelNo, ref uint upMode);
    
    //Set interrupt mask of specified module. (SIO-AI4RB is not supportive.)
    //==================================================================================================//
    // uMask    : SCAN_END(1)
    //          : FIFO_HALF_FULL(2)
    //==================================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxaiInterruptSetModuleMask(int lModuleNo, uint uMask);
    
    //Verify interrupt mask of specified module
    //==================================================================================================//
    // *upMask  : SCAN_END(1)
    //          : FIFO_HALF_FULL(2)
    //==================================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxaiInterruptGetModuleMask(int lModuleNo, ref uint upMask);
    
//========== API group for setting and verifying of input module parameter ========================================================================
    //Set the input voltage range in specified input channel
    //==================================================================================================//
    // AXT_SIO_RAI8RB
    // dMinVolt : -10V Fix
    // dMaxVolt : 10V Fix
    //==================================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxaiSetRange(int lChannelNo, double dMinVolt, double dMaxVolt);
    
    //Verify the input voltage range in specified input channel
    //==================================================================================================//
    // AXT_SIO_RAI8RB
    // *dpMaxVolt : -10V Fix
    // *dpMaxVolt : 10V Fix
    //==================================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxaiGetRange(int lChannelNo, ref double dpMinVolt, ref double dpMaxVolt);

    //Set the allowed input voltage range in specified multiple input Modules
    //==================================================================================================//
    // lModuleNo   : Analog Module Number
    //
    // RTEX AI16F
    // Mode -5~+5  : dMinVolt = -5, dMaxVolt = +5
    // Mode -10~+10: dMinVolt = -10, dMaxVolt = +10
    //==================================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxaiSetRangeModule(int lModuleNo, double dMinVolt, double dMaxVolt);

    //Verify the input voltage range in specified input Module
    //==================================================================================================//
    // lModuleNo   : Analog Module Number
    //
    // RTEX AI16F
    // *dMinVolt   : -5V, -10V
    // *dMaxVolt   : +5V, +10V
    //==================================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxaiGetRangeModule(int lModuleNo, ref double dMinVolt, ref double dMaxVolt);

    //Set the allowed input voltage range in specified multiple input channels
    //==================================================================================================//
    // AXT_SIO_RAI8RB
    // dMinVolt   : -10V Fix
    // dMaxVolt   : 10V Fix
    //==================================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxaiSetMultiRange(int lSize, int[] lpChannelNo, double dMinVolt, double dMaxVolt);
    
    //Set trigger mode in the specified input module
    //==================================================================================================//
    // uTriggerMode : NORMAL_MODE(1) 
    //              : TIMER_MODE(2)
    //              : EXTERNAL_MODE(3)
    //==================================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxaiSetTriggerMode(int lModuleNo, uint uTriggerMode);
    
    //Verify trigger mode in the specified input module
    //==================================================================================================//
    // *upTriggerMode : NORMAL_MODE(1)
    //                : TIMER_MODE(2)
    //                : EXTERNAL_MODE(3)
    //==================================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxaiGetTriggerMode(int lModuleNo, ref uint upTriggerMode);
    
    //Set offset of specified input module by mVolt (mV) unit. Max -100~100mVolt
    //==================================================================================================//
    // dMiliVolt    : -100 ~ 100 
    //==================================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxaiSetModuleOffsetValue(int lModuleNo, double dMiliVolt);
    
    //Verify offset value of specified input module. mVolt unit(mV)
    //==================================================================================================//
    // *dpMiliVolt    : -100 ~ 100 
    //==================================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxaiGetModuleOffsetValue(int lModuleNo, ref double dpMiliVolt); 

//========== Software Timer (Normal Trigger Mode) group ===================================================================================
    //Software Trigger Mode API, Convert analog input value to A/D in the specified input channel by user , then return it in voltage value
    [DllImport("AXL.dll")] public static extern uint AxaiSwReadVoltage(int lChannelNo, ref double dpVolt);
    
    //Software Trigger Mode API, Return analog input value in digit value to specified input channel
    [DllImport("AXL.dll")] public static extern uint AxaiSwReadDigit(int lChannelNo, ref uint upDigit);
    
    //Software Trigger Mode API, Return analog input value in voltage value to specified multiple input channels
    [DllImport("AXL.dll")] public static extern uint AxaiSwReadMultiVoltage(int lSize, int[] lpChannelNo, double[] dpVolt);
    
    //Software Trigger Mode API, Return analog input value in digit value to specified multiple input channels
    [DllImport("AXL.dll")] public static extern uint AxaiSwReadMultiDigit(int lSize, int[] lpChannelNo, uint[] upDigit);

//========== Hardware Timer (Timer Trigger Mode + External Trigger Mode) group ===================================================================================
    //Hardware Trigger Mode API, Set setting value in order to use immediate mode in specified multiple channels
    [DllImport("AXL.dll")] public static extern uint AxaiHwSetMultiAccess(int lSize, int[] lpChannelNo, int[] lpWordSize);
    
    //Hardware Trigger Mode API, Convert A/D as much as number of specified, then return the voltage value
    [DllImport("AXL.dll")] public static unsafe extern uint AxaiHwStartMultiAccess(double*[] dpBuffer);
    
    //Set sampling interval to specified module by frequency unit(Hz)
    //==================================================================================================//
    // dSampleFreq    : 10 ~ 100000 
    //==================================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxaiHwSetSampleFreq(int lModuleNo, double dSampleFreq);
    
    //Verify the setting value of sampling interval to specified module by frequency unit(Hz)
    //==================================================================================================//
    // *dpSampleFreq    : 10 ~ 100000 
    //==================================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxaiHwGetSampleFreq(int lModuleNo, ref double dpSampleFreq);
    
    //Set sampling interval to specified module by time unit (uSec)
    //==================================================================================================//
    // dSamplePeriod    : 100000 ~ 1000000000
    //==================================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxaiHwSetSamplePeriod(int lModuleNo, double dSamplePeriod);
    
    //Verify setting value of sampling interval to specified module by time unit(uSec)
    //==================================================================================================//
    // *dpSamplePeriod    : 100000 ~ 1000000000
    //==================================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxaiHwGetSamplePeriod(int lModuleNo, ref double dpSamplePeriod);
    
    //Set control method when the buffer is full in specified input channel
    //==================================================================================================//
    // uFullMode    : NEW_DATA_KEEP(0)
    //              : CURR_DATA_KEEP(1)
    //==================================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxaiHwSetBufferOverflowMode(int lChannelNo, uint uFullMode);
    
    //Verify control method when the buffer is full in specified input channel
    //==================================================================================================//
    // *upFullMode  : NEW_DATA_KEEP(0)
    //              : CURR_DATA_KEEP(1)
    //==================================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxaiHwGetBufferOverflowMode(int lChannelNo, ref uint upFullMode);
    
    //control method when the buffer is full in specified multiple input channels
    //==================================================================================================//
    // uFullMode    : NEW_DATA_KEEP(0)
    //              : CURR_DATA_KEEP(1)
    //==================================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxaiHwSetMultiBufferOverflowMode(int lSize, int[] lpChannelNo, uint uFullMode);
    
    //Set the upper limit and lower limit of buffer in specified input channel
    [DllImport("AXL.dll")] public static extern uint AxaiHwSetLimit(int lChannelNo, int lLowLimit, int lUpLimit);
    
    //Verify the upper limit and lower limit of buffer in specified input channel
    [DllImport("AXL.dll")] public static extern uint AxaiHwGetLimit(int lChannelNo, ref int lpLowLimit, ref int lpUpLimit);
    
    //Set the upper limit and lower limit of buffer in multiple input channels
    [DllImport("AXL.dll")] public static extern uint AxaiHwSetMultiLimit(int lSize, ref int lpChannelNo, int lLowLimit, int lUpLimit);
    
    //Start A/D conversion using H/W timer in specified multiple channels
    [DllImport("AXL.dll")] public static extern uint AxaiHwStartMultiChannel(int lSize, int[] lpChannelNo, int lBuffSize);
    
    //After starting of A/D conversion in specified multiple channels, manage filtering as much as specified and return into voltage
    [DllImport("AXL.dll")] public static extern uint AxaiHwStartMultiFilter(int lSize, int[] lpChannelNo, int lFilterCount, int lBuffSize);
    
    //Stop continuous signal A/D conversion used H/W timer
    [DllImport("AXL.dll")] public static extern uint AxaiHwStopMultiChannel(int lModuleNo);
    
    //Inspect the numbers of data in memory buffer of specified input channel
    [DllImport("AXL.dll")] public static extern uint AxaiHwReadDataLength(int lChannelNo, ref int lpDataLength);
    
    //Read A/D conversion data used H/W timer in specified input channel by voltage value
    [DllImport("AXL.dll")] public static extern uint AxaiHwReadSampleVoltage(int lChannelNo, ref int lpSize, ref double dpVolt);
    
    //Read A/D conversion data used H/W timer in specified input channel by digit value
    [DllImport("AXL.dll")] public static extern uint AxaiHwReadSampleDigit(int lChannelNo, ref int lpSize, ref uint upDigit);

//========== API group of input module state check ===================================================================================
    //Inspect if there is no data in memory buffer of specified input channel
    //==================================================================================================//
    // *upEmpty      : FALSE(0)
    //               : TRUE(1)
    //==================================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxaiHwIsBufferEmpty(int lChannelNo, ref uint upEmpty);
    
    //Inspect if the data is more than the upper limit specified in memory buffer of specified input channel
    //==================================================================================================//
    // *upUpper      : FALSE(0)
    //               : TRUE(1)
    //==================================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxaiHwIsBufferUpper(int lChannelNo, ref uint upUpper);
    
    //Inspect if the data is less than the upper limit specified in memory buffer of specified input channel
    //==================================================================================================//
    // *upLower      : FALSE(0)
    //               : TRUE(1)
    //==================================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxaiHwIsBufferLower(int lChannelNo, ref uint upLower);

//==External Trigger Mode Function
    [DllImport("AXL.dll")] public static extern uint AxaiExternalStartADC(int lModuleNo, int lSize, ref int lpChannelPos);
    [DllImport("AXL.dll")] public static extern uint AxaiExternalStopADC(int lModuleNo);

    //==================================================================================================//
    // *dwpStatus       : FIFO_DATA_EXIST(0)
    //                  : FIFO_DATA_EMPTY(1)
    //                  : FIFO_DATA_HALF(2)
    //                  : FIFO_DATA_FULL(6)
    //==================================================================================================//    
    [DllImport("AXL.dll")] public static extern uint AxaiExternalReadFifoStatus(int lModuleNo, ref uint upStatus);
    [DllImport("AXL.dll")] public static extern uint AxaiExternalReadVoltage(int lModuleNo, int lSize, ref int lpChannelPos, int lDataSize, int lBuffSize, int lStartDataPos, ref double dpVolt, ref int lpRetDataSize, ref uint upStatus);
	
//========== API group of output module information search ========================================================================================
    //Verify module number with specified output channel number
    [DllImport("AXL.dll")] public static extern uint AxaoInfoGetModuleNoOfChannelNo(int lChannelNo, ref int lpModuleNo);
    
    //Verify entire number of channel of analog output module
    [DllImport("AXL.dll")] public static extern uint AxaoInfoGetChannelCount(ref int lpChannelCount);

//========== API group for output module setting and verification ========================================================================================
    //Set output voltage range in specified output channel
    //==================================================================================================//
    // AXT_SIO_RAO4RB
    // dMinVolt    : -10V Fix
    // dMaxVolt    : 10V Fix
    //==================================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxaoSetRange(int lChannelNo, double dMinVolt, double dMaxVolt);
    
    //Verify output voltage range in specified output channel
    //==================================================================================================//
    // AXT_SIO_RAO4RB
    // dMinVolt    : -10V Fix
    // dMaxVolt    : 10V Fix
    //==================================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxaoGetRange(int lChannelNo, ref double dpMinVolt, ref double dpMaxVolt);
    
    //Set output voltage range in specified multiple output channels
    //==================================================================================================//
    // AXT_SIO_RAO4RB
    // dMinVolt    : -10V Fix
    // dMaxVolt    : 10V Fix
    //==================================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxaoSetMultiRange(int lSize, int[] lpChannelNo, double dMinVolt, double dMaxVolt);
    
    //The Input voltage is output in specified output channel
    [DllImport("AXL.dll")] public static extern uint AxaoWriteVoltage(int lChannelNo, double dVolt);
    
    //The Input voltage is output in specified multiple output channel
    [DllImport("AXL.dll")] public static extern uint AxaoWriteMultiVoltage(int lSize, int[] lpChannelNo, double[] dpVolt);
    
    //Verify voltage which is output in specified output channel
    [DllImport("AXL.dll")] public static extern uint AxaoReadVoltage(int lChannelNo, ref double dpVolt);
    
    //Verify voltage which is output in specified multiple output channels
    [DllImport("AXL.dll")] public static extern uint AxaoReadMultiVoltage(int lSize, int[] lpChannelNo, double[] dpVolt);
}

