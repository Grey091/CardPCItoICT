/****************************************************************************
*****************************************************************************
**
** File Name
** ---------
**
** AXD.CS
**
** COPYRIGHT (c) AJINEXTEK Co., LTD
**
*****************************************************************************
*****************************************************************************
**
** Description
** -----------
** Ajinextek Digital Library Header File
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

public class CAXD
{
//========== Board and module information =================================================================================

    // Verify if DIO module exists
    [DllImport("AXL.dll")] public static extern uint AxdInfoIsDIOModule(ref uint upStatus);
    
    // Verify DIO in/output module number
    [DllImport("AXL.dll")] public static extern uint AxdInfoGetModuleNo(int lBoardNo, int lModulePos, ref int lpModuleNo);
    
    // Verify the number of DIO in/output module
    [DllImport("AXL.dll")] public static extern uint AxdInfoGetModuleCount(ref int lpModuleCount);
    
    // Verify the number of input contacts of specified module
    [DllImport("AXL.dll")] public static extern uint AxdInfoGetInputCount(int lModuleNo, ref int lpCount);
    
    // Verify the number of output contacts of specified module
    [DllImport("AXL.dll")] public static extern uint AxdInfoGetOutputCount(int lModuleNo, ref int lpCount);
    
    // Verify the base board number, module position and module ID with specified module number
    [DllImport("AXL.dll")] public static extern uint AxdInfoGetModule(int lModuleNo, ref int lpBoardNo, ref int lpModulePos, ref uint upModuleID);

    // Verify module status of specified module
    [DllImport("AXL.dll")] public static extern uint AxdInfoGetModuleStatus(int lModuleNo);

//========== Verification group for input interrupt setting =================================================================================
    // Use window message, callback API or event method in order to get interrupt message into specified module
    [DllImport("AXL.dll")] public static extern uint AxdiInterruptSetModule(int lModuleNo, IntPtr hWnd, uint uMessage, CAXHS.AXT_INTERRUPT_PROC pProc, ref uint pEvent);
    
    // Set whether to use interrupt of specified module
    //======================================================//
    // uUse      : DISABLE(0)    // Interrupt Disable
    //           : ENABLE(1)     // Interrupt Enable
    //======================================================//
    [DllImport("AXL.dll")] public static extern uint AxdiInterruptSetModuleEnable(int lModuleNo, uint uUse);
    
    // Verify whether to use interrupt of specified module
    //======================================================//
    // *upUse    : DISABLE(0)    // Interrupt Disable
    //           : ENABLE(1)     // Interrupt Enable
    //======================================================//
    [DllImport("AXL.dll")] public static extern uint AxdiInterruptGetModuleEnable(int lModuleNo, ref uint upUse);
    
    // Verify the position interrupt occurred
    [DllImport("AXL.dll")] public static extern uint AxdiInterruptRead(ref int lpModuleNo, ref uint upFlag);

//========== Input interrupt rising / Verification group for setting of interrupt occurrence in falling edge =================================================================================

    // Set the rising or falling edge value by bit unit in specified input contact module and Offset position of Interrupt Rising / Falling Edge register
    //===============================================================================================//
    // lModuleNo : Module Number
    // lOffset     : Offset location from input contact
    // uMode     : DOWN_EDGE(0)
    //           : UP_EDGE(1)
    // uValue    : DISABLE(0)
    //           : ENABLE(1)
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdiInterruptEdgeSetBit(int lModuleNo, int lOffset, uint uMode, uint uValue);
    
    // Set the rising or falling edge value by byte unit in specified input contact module and Offset position of Interrupt Rising / Falling Edge register
    //===============================================================================================//
    // lModuleNo : Module Number
    // lOffset     : Offset location from input contact
    // uMode     : DOWN_EDGE(0)
    //           : UP_EDGE(1)
    // uValue    : 0x00 ~ 0x0FF 
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdiInterruptEdgeSetByte(int lModuleNo, int lOffset, uint uMode, uint uValue);
    
    // Set the rising or falling edge value by word unit in specified input contact module and Offset position of Interrupt Rising / Falling Edge register
    //===============================================================================================//
    // lModuleNo : Module Number
    // lOffset     : Offset location from input contact
    // uMode     : DOWN_EDGE(0)
    //           : UP_EDGE(1)
    // uValue    : 0x00 ~ 0x0FFFF 
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdiInterruptEdgeSetWord(int lModuleNo, int lOffset, uint uMode, uint uValue);
    
    // Set the rising or falling edge value by double word unit in specified input contact module and Offset position of Interrupt Rising / Falling Edge register
    //===============================================================================================//
    // lModuleNo : Module Number
    // lOffset     : Offset location from input contact
    // uMode     : DOWN_EDGE(0)
    //           : UP_EDGE(1)
    // uValue    : 0x00 ~ 0x0FFFFFFFF
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdiInterruptEdgeSetDword(int lModuleNo, int lOffset, uint uMode, uint uValue);
    
    // Verify the rising or falling edge value by bit unit in specified input contact module and Offset position of Interrupt Rising / Falling Edge register
    //===============================================================================================//
    // lModuleNo : Module Number
    // lOffset     : Offset location from input contact
    // uMode     : DOWN_EDGE(0)
    //           : UP_EDGE(1)
    // *upValue     : DISABLE(0)
    //                : ENABLE(1)
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdiInterruptEdgeGetBit(int lModuleNo, int lOffset, uint uMode, ref uint upValue);
    
    // Verify the rising or falling edge value by byte unit in specified input contact module and Offset position of Interrupt Rising / Falling Edge register
    //===============================================================================================//
    // lModuleNo : Module Number
    // lOffset     : Offset location from input contact
    // uMode     : DOWN_EDGE(0)
    //           : UP_EDGE(1)
    // *upValue  : 0x00 ~ 0x0FF
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdiInterruptEdgeGetByte(int lModuleNo, int lOffset, uint uMode, ref uint upValue);
    
    // Verify the rising or falling edge value by word unit in specified input contact module and Offset position of Interrupt Rising / Falling Edge register
    //===============================================================================================//
    // lModuleNo : Module Number
    // lOffset     : Offset location from input contact
    // uMode     : DOWN_EDGE(0)
    //           : UP_EDGE(1)
    // *upValue  : 0x00 ~ 0x0FFFF
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdiInterruptEdgeGetWord(int lModuleNo, int lOffset, uint uMode, ref uint upValue);
    
    // Verify the rising or falling edge value by double word unit in specified input contact module and Offset position of Interrupt Rising / Falling Edge register
    //===============================================================================================//
    // lModuleNo : Module Number
    // lOffset     : Offset location from input contact
    // uMode     : DOWN_EDGE(0)
    //           : UP_EDGE(1)
    // *upValue  : 0x00 ~ 0x0FFFFFFFF
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdiInterruptEdgeGetDword(int lModuleNo, int lOffset, uint uMode, ref uint upValue);
    
    // Set the rising or falling edge value by bit unit in entire input contact module and Offset position of Interrupt Rising / Falling Edge register
    //===============================================================================================//
    // lOffset     : Offset location from input contact
    // uMode     : DOWN_EDGE(0)
    //           : UP_EDGE(1)
    // uValue    : DISABLE(0)
    //           : ENABLE(1)
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdiInterruptEdgeSet(int lOffset, uint uMode, uint uValue);
    
    // Verify the rising or falling edge value by bit unit in entire input contact module and Offset position of Interrupt Rising / Falling Edge register
    //===============================================================================================//
    // lOffset     : Offset location from input contact
    // uMode     : DOWN_EDGE(0)
    //           : UP_EDGE(1)
    // *upValue  : DISABLE(0)
    //           : ENABLE(1)
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdiInterruptEdgeGet(int lOffset, uint uMode, ref uint upValue);

//========== Verification group of input / output signal level setting =================================================================================
//==Verification group of input signal level setting
    // Set data level by bit unit in Offset position of specified input contact module
    //===============================================================================================//
    // lModuleNo : Module Number
    // lOffset     : Offset location from input contact
    // uLevel    : LOW(0)
    //           : HIGH(1)
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdiLevelSetInportBit(int lModuleNo, int lOffset, uint uLevel);
    
    // Set data level by byte unit in Offset position of specified input contact module
    //===============================================================================================//
    // lModuleNo : Module Number
    // lOffset     : Offset location from input contact
    // uLevel    : 0x00 ~ 0x0FF
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdiLevelSetInportByte(int lModuleNo, int lOffset, uint uLevel);
    
    // Set data level by word unit in Offset position of specified input contact module
    //===============================================================================================//
    // lModuleNo : Module Number
    // lOffset     : Offset location from input contact
    // uLevel    : 0x00 ~ 0x0FFFF
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdiLevelSetInportWord(int lModuleNo, int lOffset, uint uLevel);
    
    // Set data level by double word unit in Offset position of specified input contact module
    //===============================================================================================//
    // lModuleNo : Module Number
    // lOffset     : Offset location from input contact
    // uLevel    : 0x00 ~ 0x0FFFFFFFF
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdiLevelSetInportDword(int lModuleNo, int lOffset, uint uLevel);
    
    // Verify data level by bit unit in Offset position of specified input contact module
    //===============================================================================================//
    // lModuleNo : Module Number
    // lOffset     : Offset location from input contact
    // *upLevel  : LOW(0)
    //           : HIGH(1)
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdiLevelGetInportBit(int lModuleNo, int lOffset, ref uint upLevel);
    
    // Verify data level by byte unit in Offset position of specified input contact module
    //===============================================================================================//
    // lModuleNo : Module Number
    // lOffset     : Offset location from input contact
    // *upLevel  : 0x00 ~ 0x0FF
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdiLevelGetInportByte(int lModuleNo, int lOffset, ref uint upLevel);
    
    // Verify data level by word unit in Offset position of specified input contact module
    //===============================================================================================//
    // lModuleNo : Module Number
    // lOffset     : Offset location from input contact
    // *upLevel  : 0x00 ~ 0x0FFFF
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdiLevelGetInportWord(int lModuleNo, int lOffset, ref uint upLevel);
    
    // Verify data level by double word unit in Offset position of specified input contact module
    //===============================================================================================//
    // lModuleNo : Module Number
    // lOffset     : Offset location from input contact
    // *upLevel  : 0x00 ~ 0x0FFFFFFFF
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdiLevelGetInportDword(int lModuleNo, int lOffset, ref uint upLevel);
    
    // Set data level by bit unit in Offset position of entire input contact module
    //===============================================================================================//
    // lOffset     : Offset location from input contact
    // uLevel    : LOW(0)
    //           : HIGH(1)
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdiLevelSetInport(int lOffset, uint uLevel);
    
    // Verify data level by bit unit in Offset position of entire input contact module
    //===============================================================================================//
    // lOffset     : Offset location from input contact
    // *upLevel  : LOW(0)
    //           : HIGH(1)
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdiLevelGetInport(int lOffset, ref uint upLevel);
    
//==Verification group of output signal level setting
    // Set data level by bit unit in Offset position of specified output contact module
    //===============================================================================================//
    // lModuleNo : Module Number
    // lOffset     : Offset location from output contact
    // uLevel    : LOW(0)
    //           : HIGH(1)
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdoLevelSetOutportBit(int lModuleNo, int lOffset, uint uLevel);
    
    // Set data level by byte unit in Offset position of specified output contact module
    //===============================================================================================//
    // lModuleNo : Module Number
    // lOffset     : Offset location from output contact
    // uLevel    : 0x00 ~ 0x0FF
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdoLevelSetOutportByte(int lModuleNo, int lOffset, uint uLevel);
    
    // Set data level by word unit in Offset position of specified output contact module
    //===============================================================================================//
    // lModuleNo : Module Number
    // lOffset     : Offset location from output contact
    // uLevel    : 0x00 ~ 0x0FFFF
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdoLevelSetOutportWord(int lModuleNo, int lOffset, uint uLevel);
    
    // Set data level by double word unit in Offset position of specified output contact module
    //===============================================================================================//
    // lModuleNo : Module Number
    // lOffset     : Offset location from output contact
    // uLevel    : 0x00 ~ 0x0FFFFFFFF
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdoLevelSetOutportDword(int lModuleNo, int lOffset, uint uLevel);
    
    // Verify data level by bit unit in Offset position of specified output contact module
    //===============================================================================================//
    // lModuleNo : Module Number
    // lOffset     : Offset location from output contact
    // *upLevel  : LOW(0)
    //           : HIGH(1)
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdoLevelGetOutportBit(int lModuleNo, int lOffset, ref uint upLevel);
    
    // Verify data level by byte unit in Offset position of specified output contact module
    //===============================================================================================//
    // lModuleNo : Module Number
    // lOffset     : Offset location from output contact
    // uLevel    : 0x00 ~ 0x0FF
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdoLevelGetOutportByte(int lModuleNo, int lOffset, ref uint upLevel);
    
    // Verify data level by word unit in Offset position of specified output contact module
    //===============================================================================================//
    // lModuleNo : Module Number
    // lOffset     : Offset location from output contact
    // uLevel    : 0x00 ~ 0x0FFFF
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdoLevelGetOutportWord(int lModuleNo, int lOffset, ref uint upLevel);
    
    // Verify data level by double word unit in Offset position of specified output contact module
    //===============================================================================================//
    // lModuleNo : Module Number
    // lOffset     : Offset location from output contact
    // uLevel    : 0x00 ~ 0x0FFFFFFFF
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdoLevelGetOutportDword(int lModuleNo, int lOffset, ref uint upLevel);
    
    // Set data level by bit unit in Offset position of entire output contact module
    //===============================================================================================//
    // lOffset     : Offset location from output contact
    // uLevel    : LOW(0)
    //           : HIGH(1)
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdoLevelSetOutport(int lOffset, uint uLevel);
    
    // Verify data level by bit unit in Offset position of entire output contact module
    //===============================================================================================//
    // lOffset     : Offset location from output contact
    // *upLevel  : LOW(0)
    //           : HIGH(1)
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdoLevelGetOutport(int lOffset, ref uint upLevel);
//========== Input / Output signal reading / writing  =================================================================================
//==Output signal writing
    // Output data by bit unit in Offset position of entire output contact module
    //===============================================================================================//
    // lOffset     : Offset location from output contact
    // uLevel    : LOW(0)
    //           : HIGH(1)
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdoWriteOutport(int lOffset, uint uValue);
    
    // Output data by bit unit in Offset position of specified output contact module
    //===============================================================================================//
    // lModuleNo : Module Number
    // lOffset     : Offset location from output contact
    // uLevel    : LOW(0)
    //           : HIGH(1)
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdoWriteOutportBit(int lModuleNo, int lOffset, uint uValue);
    
    // Output data by byte unit in Offset position of specified output contact module
    //===============================================================================================//
    // lModuleNo : Module Number
    // lOffset     : Offset location from output contact
    // uValue    : 0x00 ~ 0x0FF
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdoWriteOutportByte(int lModuleNo, int lOffset, uint uValue);
    
    // Output data by word unit in Offset position of specified output contact module
    //===============================================================================================//
    // uValue    : 0x00 ~ 0x0FFFF
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdoWriteOutportWord(int lModuleNo, int lOffset, uint uValue);
    
    // Output data by double word unit in Offset position of specified output contact module
    //===============================================================================================//
    // lModuleNo : Module Number
    // lOffset     : Offset location from output contact
    // uValue    : 0x00 ~ 0x0FFFFFFFF
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdoWriteOutportDword(int lModuleNo, int lOffset, uint uValue);
//==Output signal reading    
    // Read data by bit unit in Offset position of entire output contact module
    //===============================================================================================//
    // lOffset     : Offset location from output contact
    // *upLevel  : LOW(0)
    //           : HIGH(1)
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdoReadOutport(int lOffset, ref uint upValue);
    
    // Read data by bit unit in Offset position of specified output contact module
    //===============================================================================================//
    // lModuleNo : Module Number
    // lOffset     : Offset location from output contact
    // *upLevel  : LOW(0)
    //           : HIGH(1)
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdoReadOutportBit(int lModuleNo, int lOffset, ref uint upValue);
    
    // Read data by byte unit in Offset position of specified output contact module
    //===============================================================================================//
    // lModuleNo : Module Number
    // lOffset     : Offset location from output contact
    // *upValue  : 0x00 ~ 0x0FF
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdoReadOutportByte(int lModuleNo, int lOffset, ref uint upValue);
    
    // Read data by word unit in Offset position of specified output contact module
    //===============================================================================================//
    // lModuleNo : Module Number
    // lOffset     : Offset location from output contact
    // *upValue  : 0x00 ~ 0x0FFFF
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdoReadOutportWord(int lModuleNo, int lOffset, ref uint upValue);
    
    // Read data by double word unit in Offset position of specified output contact module
    //===============================================================================================//
    // lModuleNo : Module Number
    // lOffset     : Offset location from output contact
    // *upValue  : 0x00 ~ 0x0FFFFFFFF
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdoReadOutportDword(int lModuleNo, int lOffset, ref uint upValue);
//==Input signal reading    
    // Read data level by bit unit in Offset position of entire input contact module
    //===============================================================================================//
    // lOffset     : Offset location from output contact
    // *upValue  : LOW(0)
    //           : HIGH(1)
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdiReadInport(int lOffset, ref uint upValue);
    
    // Read data level by bit unit in Offset position of specified input contact module
    //===============================================================================================//
    // lModuleNo : Module Number
    // lOffset     : Offset location from input contact
    // *upValue  : LOW(0)
    //           : HIGH(1)
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdiReadInportBit(int lModuleNo, int lOffset, ref uint upValue);
    
    // Read data level by byte unit in Offset position of specified input contact module
    //===============================================================================================//
    // lModuleNo : Module Number
    // lOffset     : Offset location from input contact
    // *upValue  : 0x00 ~ 0x0FF
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdiReadInportByte(int lModuleNo, int lOffset, ref uint upValue);
    
    // Read data level by wordt unit in Offset position of specified input contact module
    //===============================================================================================//
    // lModuleNo : Module Number
    // lOffset     : Offset location from input contact
    // *upValue  : 0x00 ~ 0x0FFFF
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdiReadInportWord(int lModuleNo, int lOffset, ref uint upValue);
    
    // Read data level by double word unit in Offset position of specified input contact module
    //===============================================================================================//
    // lModuleNo : Module Number
    // lOffset     : Offset location from input contact
    // *upValue  : 0x00 ~ 0x0FFFFFFFF
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdiReadInportDword(int lModuleNo, int lOffset, ref uint upValue);

    //== Only for MLII, M-Systems DIO(R7 series)
    // Read data level by bit unit in Offset position of specified extended input contact module
    //===============================================================================================//
    // lModuleNo   : Module number
    // lOffset     : Offset position by bit unit between input index.(0~15)
    // *upValue    : LOW(0)
    //             : HIGH(1)
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdReadExtInportBit(int lModuleNo, int lOffset, ref uint upValue);

    // Read data level by byte unit in Offset position of specified extended input contact module
    //===============================================================================================//
    // lModuleNo   : Module number
    // lOffset     : offset position by byte unit between input index.(0~1)
    // *upValue    : 0x00 ~ 0x0FF
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdReadExtInportByte(int lModuleNo, int lOffset, ref uint upValue);

    // Read data level by word unit in Offset position of specified extended input contact module
    //===============================================================================================//
    // lModuleNo   : Module number
    // lOffset     : offset position by word unit between input index.(0)
    // *upValue    : 0x00 ~ 0x0FFFF
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdReadExtInportWord(int lModuleNo, int lOffset, ref uint upValue);

    // Read data level by double word unit in Offset position of specified extended input contact module
    //===============================================================================================//
    // lModuleNo   : Module number
    // lOffset     : offset position by dword unit between input index.(0)
    // *upValue    : 0x00 ~ 0x00000FFFF
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdReadExtInportDword(int lModuleNo, int lOffset, ref uint upValue);

    // Read data level by bit unit in Offset position of specified extended output contact module
    //===============================================================================================//
    // lModuleNo   : Module number
    // lOffset     : Offset position by bit unit between output index.(0~15)
    // *upValue    : LOW(0)
    //             : HIGH(1)
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdReadExtOutportBit(int lModuleNo, int lOffset, ref uint upValue);

    // Read data level by byte unit in Offset position of specified extended output contact module
    //===============================================================================================//
    // lModuleNo   : Module number
    // lOffset     : Offset position by byte unit between output index.(0~1)
    // *upValue    : 0x00 ~ 0x0FF
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdReadExtOutportByte(int lModuleNo, int lOffset, ref uint upValue);

    // Read data level by word unit in Offset position of specified extended output contact module
    //===============================================================================================//
    // lModuleNo   : Module number
    // lOffset     : Offset position by word unit between output index.(0)
    // *upValue    : 0x00 ~ 0x0FFFF
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdReadExtOutportWord(int lModuleNo, int lOffset, ref uint upValue);

    // Read data level by double word unit in Offset position of specified extended output contact module
    //===============================================================================================//
    // lModuleNo   : Module number
    // lOffset     : Offset position by dword unit between output index.(0)
    // *upValue    : 0x00 ~ 0x0000FFFF
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdReadExtOutportDword(int lModuleNo, int lOffset, ref uint upValue);

    // Output data by bit unit in Offset position of specified extended output contact module
    //===============================================================================================//
    // lModuleNo   : Module number
    // lOffset     : Offset position by bit unit between output index.(0~15)
    // uValue      : LOW(0)
    //             : HIGH(1)
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdWriteExtOutportBit(int lModuleNo, int lOffset, uint uValue);

    // Output data by byte unit in Offset position of specified extended output contact module
    //===============================================================================================//
    // lModuleNo   : Module number
    // lOffset     : Offset position by byte unit between output index.(0~1)
    // uValue      : 0x00 ~ 0x0FF
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdWriteExtOutportByte(int lModuleNo, int lOffset, uint uValue);

    // Output data by word unit in Offset position of specified extended output contact module
    //===============================================================================================//
    // lModuleNo   : Module number
    // lOffset     : Offset position by word unit between output index.(0~1)
    // uValue      : 0x00 ~ 0x0FFFF
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdWriteExtOutportWord(int lModuleNo, int lOffset, uint uValue);

    // Output data by dword unit in Offset position of specified extended output contact module
    //==============================================================================================//
    // lModuleNo   : Module number
    // lOffset     : Offset position by dword unit between output index.(0)
    // uValue    : 0x00 ~ 0x00000FFFF
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdWriteExtOutportDword(int lModuleNo, int lOffset, uint uValue);

    // Set data level by bit unit in Offset position of specified extended input/output contact module
    //===============================================================================================//
    // lModuleNo   : Module number
    // lOffset     : Offset position by bit unit between input/output index.(0~15)
    // uLevel      : LOW(0)
    //             : HIGH(1)
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdLevelSetExtportBit(int lModuleNo, int lOffset, uint uLevel);

    // Set data level by byte unit in Offset position of specified extended input/output contact module
    //===============================================================================================//
    // lModuleNo   : Module number
    // lOffset     : Offset position by byte unit between input/output index.(0~1)
    // uLevel      : 0x00 ~ 0xFF
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdLevelSetExtportByte(int lModuleNo, int lOffset, uint uLevel);

    // Set data level by word unit in Offset position of specified extended input/output contact module
    //===============================================================================================//
    // lModuleNo   : Module number
    // lOffset     : Offset position by word unit between input/output index.(0)
    // uLevel      : 0x00 ~ 0xFFFF
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdLevelSetExtportWord(int lModuleNo, int lOffset, uint uLevel);

    // Set data level by dword unit in Offset position of specified extended input/output contact module
    //===============================================================================================//
    // lModuleNo   : Module number
    // lOffset     : Offset position by dword unit between input/output index.(0)
    // uLevel      : 0x00 ~ 0x0000FFFF
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdLevelSetExtportDword(int lModuleNo, int lOffset, uint uLevel);

    // Verify data level by bit unit in Offset position of specified extended input/output contact module
    //===============================================================================================//
    // lModuleNo   : Module number
    // lOffset     : Offset position by bit unit between input/output index.(0~15)
    // uLevel      : LOW(0)
    //             : HIGH(1)
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdLevelGetExtportBit(int lModuleNo, int lOffset, ref uint upLevel);

    // Verify data level by byte unit in Offset position of specified extended input/output contact module
    //===============================================================================================//
    // lModuleNo   : Module number
    // lOffset     : Offset position by byte unit between input/output index.(0~1)
    // uLevel      : 0x00 ~ 0xFF
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdLevelGetExtportByte(int lModuleNo, int lOffset, ref uint upLevel);

    // Verify data level by word unit in Offset position of specified extended input/output contact module
    //===============================================================================================//
    // lModuleNo   : Module number
    // lOffset     : Offset position by word unit between input/output index.(0)
    // uLevel      : 0x00 ~ 0xFFFF
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdLevelGetExtportWord(int lModuleNo, int lOffset, ref uint upLevel);

    // Verify data level by dword unit in Offset position of specified extended input/output contact module
    //===============================================================================================//
    // lModuleNo   : Module number
    // lOffset     : Offset position by dword unit between input/output index.(0)
    // uLevel      : 0x00 ~ 0x0000FFFF
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdLevelGetExtportDword(int lModuleNo, int lOffset, ref uint upLevel);

//========== Advanced API =================================================================================

    // Verify if the signal was switched from Off to On in Offset position of specified input contact module
    //===============================================================================================//
    // lModuleNo : Module Number
    // lOffset     : Offset location from input contact
    // *upValue  : FALSE(0)
    //           : TRUE(1)
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdiIsPulseOn(int lModuleNo, int lOffset, ref uint upValue);
    
    // Verify if the signal was switched from On to Off in Offset position of specified input contact module
    //===============================================================================================//
    // lModuleNo : Module Number
    // lOffset     : Offset location from input contact
    // *upValue  : FALSE(0)
    //           : TRUE(1)
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdiIsPulseOff(int lModuleNo, int lOffset, ref uint upValue);
    
    // Verify if the signal is maintained On state during the calling time as much as count in Offset position of specified input contact module
    //===============================================================================================//
    // lModuleNo : Module Number
    // lOffset     : Offset location from input contact
    // lCount    : 0 ~ 0x7FFFFFFF(2147483647)
    // *upValue  : FALSE(0)
    //           : TRUE(1)
    // lStart    : 1
    //           : 0
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdiIsOn(int lModuleNo, int lOffset, int lCount, ref uint upValue, int lStart);
    
    // Verify if the signal is maintained Off state during the calling time as much as count in Offset position of specified input contact module
    //===============================================================================================//
    // lModuleNo : Module Number
    // lOffset     : Offset location from input contact
    // lCount    : 0 ~ 0x7FFFFFFF(2147483647)
    // *upValue  : FALSE(0)
    //           : TRUE(1)
    // lStart    : 1
    //           : 0
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdiIsOff(int lModuleNo, int lOffset, int lCount, ref uint upValue, int lStart);
    
    // Maintain On state during mSec set in Offset position of specified output contact module, then turns Off
    //===============================================================================================//
    // lModuleNo : Module Number
    // lOffset     : Offset location from output contact
    // lCount    : 0 ~ 0x7FFFFFFF(2147483647)
    // lmSec     : 1 ~ 30000
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdoOutPulseOn(int lModuleNo, int lOffset, int lmSec);
    
    // Maintain Off state during mSec set in Offset position of specified output contact module, then turns On
    //===============================================================================================//
    // lModuleNo : Module Number
    // lOffset     : Offset location from output contact
    // lCount    : 0 ~ 0x7FFFFFFF(2147483647)
    // lmSec     : 1 ~ 30000
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdoOutPulseOff(int lModuleNo, int lOffset, int lmSec);
    
    // Toggling by numbers and interval which are set in Offset position of specified output contact module, then afterward maintaining the original output state
    //===============================================================================================//
    // lModuleNo : Module Number
    // lOffset     : Offset location from output contact
    // lInitState: Off(0)
    //           : On(1)
    // lmSecOn   : 1 ~ 30000
    // lmSecOff  : 1 ~ 30000
    // lCount    : 1 ~ 0x7FFFFFFF(2147483647)
    //           : -1
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdoToggleStart(int lModuleNo, int lOffset, int lInitState, int lmSecOn, int lmSecOff, int lCount);
    
    // Toggling by numbers and interval which are set in Offset position of specified output contact module, then afterward maintaining the original output state
    //===============================================================================================//
    // lModuleNo : Module Number
    // lOffset     : Offset location from output contact
    // uOnOff    : Off(0)
    //           : On(1)
    //===============================================================================================//
    [DllImport("AXL.dll")] public static extern uint AxdoToggleStop(int lModuleNo, int lOffset, uint uOnOff);

    [DllImport("AXL.dll")] public static extern uint AxdoSetNetworkErrorAct(int lModuleNo, uint dwSize, ref uint dwaSetValue);

    [DllImport("AXL.dll")] public static extern uint AxdSetContactNum(int lModuleNo, uint dwInputNum, uint dwOutputNum);

    [DllImport("AXL.dll")] public static extern uint AxdGetContactNum(int lModuleNo, ref uint dwpInputNum, ref uint dwpOutputNum);
}

