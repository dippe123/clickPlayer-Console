using System;
using System.Diagnostics;
using System.Reflection;

using static destruct.MemoryCleaner;
using static helpers.WinAPI;

namespace destruct {
    class Destruct {
        public static void InitDestruct( ) {
            try {
                DnsFlushResolverCache( );
                cmd( "fsutil usn createjournal m=1 a=1 c:" );
                RegeditCleaner.Init( );
                pcaClient_Clean( );
                StringClean( Assembly.GetEntryAssembly( ).Location , "explorer" );
                StringClean( "*.auroraclicker.xyz" , "lsass" );
                StringClean( "auroraclicker.xyz" , "lsass" );
                StringClean( "auroraclicker" , "lsass" );
                StringClean( "dippeware" , "lsass" );
                StringClean( "fsutil usn createjournal m=1 a=1 c:" , "diagtrack" );
                Environment.Exit( -1 );
            } catch { }
        }
        public static void cmd( string command ) {
            var process = Process.Start( new ProcessStartInfo( "cmd.exe" ) {
                CreateNoWindow = true ,
                WindowStyle = ProcessWindowStyle.Hidden ,
                Arguments = $"/C {command}"
            } );
            process.PriorityClass = ProcessPriorityClass.Idle;
            if ( process.HasExited )
                process.Dispose( );
        }
    }
}
