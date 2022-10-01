using helpers;
using System;
using System.Diagnostics;

namespace checks {
    class DllCheck {
        public static void PatchLoadLibraryW( ) {
            IntPtr KernelModule = WinAPI.GetModuleHandle( "kernelbase.dll" );
            IntPtr LoadLibraryW = WinAPI.GetProcAddress( KernelModule , "LoadLibraryW" );
            byte [ ] HookedCode = { 0xC2 , 0x04 , 0x00 };
            WinAPI.WriteProcessMemory( Process.GetCurrentProcess( ).Handle , LoadLibraryW , HookedCode , 3 , 0 );
        }
    }
}

