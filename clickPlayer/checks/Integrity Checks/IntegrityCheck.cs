using helpers;
using System;
using System.Diagnostics;

namespace checks {
    class IntegrityChecks {
        public static bool AssemblyJMP( ) {
            byte [ ] byteRead = new byte [ 1 ];
            byte [ ] mov = new byte [ 1 ] { 0x8B };
            bool IntegrityCheck = false;
            IntPtr CheckRemoteDebuggerPresentAddr = MemoryStructs.GetProcAddress( MemoryStructs.GetModuleHandle( "Kernel32.dll" ) , "CheckRemoteDebuggerPresent" );
            MemoryStructs.ReadProcessMemory( Process.GetCurrentProcess( ).Handle , CheckRemoteDebuggerPresentAddr , byteRead , 2 , out _ );
            if ( !ByteArrayCompare( byteRead , mov ) )
                IntegrityCheck = true;

            return IntegrityCheck;
        }
        private static bool ByteArrayCompare( byte [ ] b1 , byte [ ] b2 ) {
            return b1.Length == b2.Length && MemoryStructs.memcmp( b1 , b2 , b1.Length ) == 0;
        }
    }
}
