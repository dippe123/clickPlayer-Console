using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace helpers {
    class WinAPI {
        [DllImport( "dnsapi.dll" , EntryPoint = "DnsFlushResolverCache" )] public static extern bool DnsFlushResolverCache( );
        [DllImport( "wininet.dll" , CharSet = CharSet.Auto )] public extern static bool InternetGetConnectedState( ref WinStructs.InternetConnectionState_e lpdwFlags , int dwReserved );
        [DllImport( "kernel32.dll" )] public static extern unsafe bool VirtualProtect( byte* lpAddress , int dwSize , uint flNewProtect , out uint lpflOldProtect );
        [DllImport( "ntdll.dll" , SetLastError = true )]  public static extern IntPtr NtSetInformationThread( IntPtr ThreadHandle , uint ThreadInfoClass , IntPtr ThreadInfo , uint ThreadInfoLength );
        [DllImport( "kernel32.dll" )] public static extern IntPtr OpenThread( uint dwDesiredAccess , bool bInheritHandle , uint dwThreadId );
        [DllImport( "kernel32.dll" , SetLastError = true )] public static extern bool CloseHandle( IntPtr Handle );
        [DllImport( "kernel32.dll" , SetLastError = true )] public static extern uint GetLastError( );
        [DllImport( "kernel32.dll" , SetLastError = true )] public static extern void OutputDebugStringA( string Text );
        [DllImport( "kernel32.dll" , SetLastError = true )] public static extern bool IsProcessCritical( IntPtr Handle , ref bool BoolToCheck );
        [DllImport( "kernel32.dll" , SetLastError = true )] public static extern IntPtr GetModuleHandle( string lib );

        [DllImport( "kernel32.dll" , SetLastError = true )] public static extern IntPtr GetProcAddress( IntPtr ModuleHandle , string Function );

        [DllImport( "kernel32.dll" , SetLastError = true )] public static extern bool WriteProcessMemory( IntPtr ProcHandle , IntPtr BaseAddress , byte [ ] Buffer , uint size , int NumOfBytes );

        [DllImport( "kernel32.dll" )] public static extern IntPtr ZeroMemory( IntPtr addr , IntPtr size );
        
        [DllImport( "kernel32.dll" )] public static extern IntPtr VirtualProtect( IntPtr lpAddress , IntPtr dwSize , IntPtr flNewProtect , ref IntPtr lpflOldProtect );

        [DllImport( "kernel32.dll" , SetLastError = true , ExactSpelling = true )]
        [return: MarshalAs( UnmanagedType.Bool )] public static extern bool CheckRemoteDebuggerPresent( IntPtr hProcess , [MarshalAs( UnmanagedType.Bool )] ref bool isDebuggerPresent );
        
        [DllImport( "kernel32.dll" , SetLastError = true , ExactSpelling = true )]
        [return: MarshalAs( UnmanagedType.Bool )] public static extern bool IsDebuggerPresent( );
        [DllImport( "ntdll.dll" , SetLastError = true , ExactSpelling = true )] internal static extern NTStatus NtQueryInformationProcess( [In] IntPtr ProcessHandle , [In] WinStructs.PROCESSINFOCLASS ProcessInformationClass , out IntPtr ProcessInformation , [In] int ProcessInformationLength , [Optional] out int ReturnLength );
        
        [DllImport( "ntdll.dll" , SetLastError = true , ExactSpelling = true )] internal static extern NTStatus NtClose( [In] IntPtr Handle );
        
        [DllImport( "ntdll.dll" , SetLastError = true , ExactSpelling = true )] internal static extern NTStatus NtRemoveProcessDebug( IntPtr ProcessHandle , IntPtr DebugObjectHandle );
        
        [DllImport( "ntdll.dll" , SetLastError = true , ExactSpelling = true )] internal static extern NTStatus NtSetInformationDebugObject( [In] IntPtr DebugObjectHandle , [In] WinStructs.DebugObjectInformationClass DebugObjectInformationClass , [In] IntPtr DebugObjectInformation , [In] int DebugObjectInformationLength , [Out][Optional] out int ReturnLength );
        
        [DllImport( "ntdll.dll" , SetLastError = true , ExactSpelling = true )] internal static extern NTStatus NtQuerySystemInformation( [In] WinStructs.SYSTEM_INFORMATION_CLASS SystemInformationClass , IntPtr SystemInformation , [In] int SystemInformationLength , [Out][Optional] out int ReturnLength );

        [DllImport( "user32" , CharSet = CharSet.Auto , SetLastError = true )] public static extern int GetWindowText( IntPtr hWnd , StringBuilder lpString , int cch );

        [DllImport( "ntdll.dll" , EntryPoint = "NtSetTimerResolution" )] public static extern void NtSetTimerResolution( uint DesiredResolution , bool SetResolution , ref uint CurrentResolution );

        [DllImport( "User32.dll" )] public static extern short GetAsyncKeyState( int vKey );
        [DllImport( "user32" , SetLastError = true )] public static extern short GetAsyncKeyState( Keys vKey );

        [DllImport( "user32.dll" )] public static extern bool PostMessage( IntPtr hWnd , uint Msg , int wParam , int lParam );

        [DllImport( "user32" , CharSet = CharSet.Ansi , SetLastError = true )] public static extern int GetKeyState( int vKey );

        [DllImport( "user32.dll" )] public static extern int DeleteMenu( IntPtr hMenu , int nPosition , int wFlags );

        [DllImport( "user32.dll" )] public static extern IntPtr GetSystemMenu( IntPtr hWnd , bool bRevert );

        [DllImport( "kernel32.dll" , ExactSpelling = true )] public static extern IntPtr GetConsoleWindow( );
        [DllImport( "user32.dll" )] public static extern IntPtr GetForegroundWindow( );

        [DllImport( "user32.dll" , SetLastError = true , CharSet = CharSet.Unicode )] public static extern int GetWindowThreadProcessId( IntPtr hWnd , out int lpdwProcessId );

        [DllImport( "user32.dll" )] public static extern bool HideWindow( IntPtr hWnd , int nCmdShow );

        [DllImport( "user32.dll" )] public static extern bool ShowWindow( IntPtr hWnd , int nCmdShow );

        [DllImport( "kernel32.dll" , SetLastError = true )] public static extern IntPtr GetStdHandle( int nStdHandle );

        [DllImport( "kernel32.dll" )] public static extern bool GetConsoleMode( IntPtr hConsoleHandle , out uint lpMode );

        [DllImport( "kernel32.dll" )] public static extern bool SetConsoleMode( IntPtr hConsoleHandle , uint dwMode );
        [DllImport( "user32.dll" )] public static extern bool GetCursorInfo( out WinStructs.CURSORINFO pci );
        public static void TimerSet( ) {
            uint DesiredResolution = 5000, CurrentResolution = 0;
            bool SetResolution = true;
            NtSetTimerResolution( DesiredResolution , SetResolution , ref CurrentResolution );
        }
    }
}
