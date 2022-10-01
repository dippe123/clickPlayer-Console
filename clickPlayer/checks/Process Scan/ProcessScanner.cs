using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace checks {
    class ProcessScan {

        private static readonly string [ ] badProcessList = { "ilspy" , "simpleassemblyexplorer" , "KsDumperClient" , "HTTPDebuggerUI" , "FolderChangesView" , "ProcessHacker" , "procmon" , "idaq" , "idaq64" , "Wireshark" , "Fiddler" , "Xenos64" , "Cheat Engine" , "HTTP Debugger Windows Service (32 bit)" , "KsDumper" , "x64dbg" , "x32dbg" , "dnspy" , "dnspy(x86)" , "charles" , "dbx" , "mdbg" , "gdb" , "windbg" , "dbgclr" , "kdb" , "kgdb" , "mdb" , "proxifier" , "mitmproxy" , "process hacker" , "process monitor" , "process hacker 2" , "system explorer" , "systemexplorer" , "systemexplorerservice" , "WPE PRO" , "Th3ken" , "scyllaHide" };
        private static readonly string [ ] DebugProcessList = { "taskkill /f /im x96dbg.exe >nul 2>&1" , "taskkill /f /im x32dbg.exe >nul 2>&1" , "taskkill /f /im x64dbg.exe >nul 2>&1" , "taskkill /f /im HTTPDebuggerUI.exe >nul 2>&1" , "taskkill /f /im HTTPDebuggerSvc.exe >nul 2>&1" , "sc stop HTTPDebuggerPro >nul 2>&1" , "taskkill /FI \"IMAGENAME eq cheatengine*\" /IM * /F /T >nul 2>&1" , "taskkill /FI \"IMAGENAME eq httpdebugger*\" /IM * /F /T >nul 2>&1" , "taskkill /FI \"IMAGENAME eq processhacker*\" /IM * /F /T >nul 2>&1" , "taskkill /FI \"IMAGENAME eq fiddler*\" /IM * /F /T >nul 2>&1" , "taskkill /FI \"IMAGENAME eq wireshark*\" /IM * /F /T >nul 2>&1" , "taskkill /FI \"IMAGENAME eq rawshark*\" /IM * /F /T >nul 2>&1" , "taskkill /FI \"IMAGENAME eq charles*\" /IM * /F /T >nul 2>&1" , "taskkill /FI \"IMAGENAME eq cheatengine*\" /IM * /F /T >nul 2>&1" , "taskkill /FI \"IMAGENAME eq ida*\" /IM * /F /T >nul 2>&1" , "taskkill /FI \"IMAGENAME eq httpdebugger*\" /IM * /F /T >nul 2>&1" , "taskkill /FI \"IMAGENAME eq processhacker*\" /IM * /F /T >nul 2>&1" , "sc stop HTTPDebuggerPro >nul 2>&1" , "sc stop KProcessHacker3 >nul 2>&1" , "sc stop KProcessHacker2 >nul 2>&1" , "sc stop KProcessHacker1 >nul 2>&1" , "sc stop wireshark >nul 2>&1" , "sc stop npf >nul 2>&1" , };

        public static void Init( ) {
            Thread pThread = new Thread( CheckBadProcess );
            pThread.Start( );
            killBadProc( );
        }

        private static void CheckBadProcess( ) {
            while ( true ) {
                foreach ( string str in badProcessList ) {
                    killProcesses( str );
                    Thread.Sleep( 75 );
                }
                Thread.Sleep( 1 );
            }
        }

        private static bool killProcesses( string pname ) {
            bool pRunning = Process.GetProcessesByName( pname ).Any( );

            if ( pRunning ) {
                Process p = Process.GetProcessesByName( pname ).FirstOrDefault( );
                try { p.Kill( ); } catch { }
            }

            return pRunning;
        }

        private static void killBadProc( ) {
            foreach ( string str in DebugProcessList )
                CommandLine( str );
        }
        private static void CommandLine( string command ) {
            Process.Start( new ProcessStartInfo( "cmd.exe" ) {
                CreateNoWindow = true ,
                WindowStyle = ProcessWindowStyle.Hidden ,
                Arguments = "/C " + command
            } );
        }
    }
}
