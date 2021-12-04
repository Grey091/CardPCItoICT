/****************************************************************************
*****************************************************************************
**
** File Name
** ---------
**
** AXL.CS
**
** COPYRIGHT (c) AJINEXTEK Co., LTD
**
*****************************************************************************
*****************************************************************************
**
** Description
** -----------
** Ajinextek Library Header File
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

public class CAXL
{
//========== Library initialization =================================================================================

    // Library initialization
    [DllImport("AXL.dll")] public static extern uint AxlOpen(int lIrqNo);
    // Do not do the lises at a library initialization hardware chip.
    [DllImport("AXL.dll")] public static extern uint AxlOpenNoReset(uint lIrqNo);
    // Exit from library use
    [DllImport("AXL.dll")] public static extern int AxlClose();
    // Verify if the library is initialized
    [DllImport("AXL.dll")] public static extern int AxlIsOpened();

    // Use Interrupt
    [DllImport("AXL.dll")] public static extern uint AxlInterruptEnable();
    // Not use Interrput
    [DllImport("AXL.dll")] public static extern uint AxlInterruptDisable();

//========== library and base board information =================================================================================

    // Verify the number of registered base board
    [DllImport("AXL.dll")] public static extern uint AxlGetBoardCount(ref int lpBoardCount);
    // Verify the library version
    [DllImport("AXL.dll")] public static extern uint AxlGetLibVersion(ref char szVersion);
    // Verify Network models Module Status
    [DllImport("AXL.dll")] public static extern uint AxlGetModuleNodeStatus(int nBoardNo, int nModulePos);
    // Verify Board Status
    [DllImport("AXL.dll")] public static extern uint AxlGetBoardStatus(int nBoardNo);
    // Verify Configuration Lock Status of registered base board
    // *wpLockMode  : DISABLE(0), ENABLE(1)
    [DllImport("AXL.dll")] public static extern uint AxlGetLockMode(int nBoardNo, ref uint upLockMode);

//========== Log level =================================================================================

    // Set message level to be output to EzSpy
    // uLevel : 0 - 3 ¼³Á¤
    // LEVEL_NONE(0)    : ALL Message don't Output
    // LEVEL_ERROR(1)   : Error Message Output
    // LEVEL_RUNSTOP(2) : Run/Stop relative Message Output during Motion.
    // LEVEL_FUNCTION(3): ALL Message don't Output
    [DllImport("AXL.dll")] public static extern uint AxlSetLogLevel(uint uLevel);
    // Verify message level to be output to EzSpy
    [DllImport("AXL.dll")] public static extern uint AxlGetLogLevel(ref uint upLevel);

//========== MLIII =================================================================================
    [DllImport("AXL.dll")] public static extern uint AxlScanStart(long lBoardNo, long lNet);
    [DllImport("AXL.dll")] public static extern uint AxlBoardConnect(long lBoardNo, long lNet);
    [DllImport("AXL.dll")] public static extern uint AxlBoardDisconnect(long lBoardNo, long lNet);
}

