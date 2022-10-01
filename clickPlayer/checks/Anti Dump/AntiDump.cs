using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using static helpers.WinAPI;

namespace checks {
    class AntiDumper {

        private static int [ ] words = new int [ ] { 0x8 , 0xC , 0x10 , 0x14 , 0x18 , 0x1C , 0x24 };
        private static int [ ] peheaderbytes = new int [ ] { 0x1A , 0x1B };
        private static int [ ] peheaderwords = new int [ ] { 0x4 , 0x16 , 0x18 , 0x40 , 0x42 , 0x44 , 0x46 , 0x48 , 0x4A , 0x4C , 0x5C , 0x5E };

        private static void RandomizePE( IntPtr address , int size ) {
            IntPtr sz = ( IntPtr ) size;
            IntPtr dwOld = default( IntPtr );
            VirtualProtect( address , sz , ( IntPtr ) 0x40 , ref dwOld );
            ZeroMemory( address , sz );
            IntPtr temp = default( IntPtr );
            VirtualProtect( address , sz , dwOld , ref temp );
        }

        public static void Init( ) {
            var process = Process.GetCurrentProcess( );
            var base_address = process.MainModule.BaseAddress;
            var dwpeheader = Marshal.ReadInt32( ( IntPtr ) ( base_address.ToInt32( ) + 0x3C ) );
            var wnumberofsections = Marshal.ReadInt16( ( IntPtr ) ( base_address.ToInt32( ) + dwpeheader + 0x6 ) );

            for ( int i = 0 ; i < peheaderwords.Length ; i++ )
                RandomizePE( ( IntPtr ) ( base_address.ToInt32( ) + dwpeheader + peheaderwords [ i ] ) , 2 );

            for ( int i = 0 ; i < peheaderbytes.Length ; i++ )
                RandomizePE( ( IntPtr ) ( base_address.ToInt32( ) + dwpeheader + peheaderbytes [ i ] ) , 1 );

            int x = 0, y = 0;

            while ( x <= wnumberofsections ) {
                if ( y == 0 )
                    RandomizePE( ( IntPtr ) ( base_address.ToInt32( ) + dwpeheader + 0xFA + ( 0x28 * x ) + 0x20 ) , 2 );

                y++;

                if ( y == words.Length ) { x++; y = 0; }
            }
        }
    }
}
