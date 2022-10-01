using System;
using System.Reflection;
using System.Text;

namespace destruct {
    class MemoryCleaner {
        public static void pcaClient_Clean( ) {
            try {
                DotNetScanMemory_SmoLL dn = new DotNetScanMemory_SmoLL( );
                string ToDeletePCA = $"TRACE,0000,0000,PcaClient,MonitorProcess,{ Assembly.GetEntryAssembly( ).Location },Time,0";
                string ArrayPCA = BitConverter.ToString( Encoding.ASCII.GetBytes( ToDeletePCA ) ).Replace( "-" , " " );
                IntPtr [ ] addressesPCA = dn.ScanArray( dn.GetPID( "explorer" ) , ArrayPCA );
                byte [ ] bufferPCA = new byte [ ArrayPCA.Length + 9 ];
                for ( int i = 0 ; i < addressesPCA.Length ; i++ ) {
                    dn.WriteArray( addressesPCA [ i ] , BitConverter.ToString( bufferPCA ).Replace( "-" , " " ) );
                }
            } catch { }
        }
        public static void StringClean( string toClean , string processName ) {
            try {
                DotNetScanMemory_SmoLL dn = new DotNetScanMemory_SmoLL( );
                string stringArray = toClean;
                string SearchArray = BitConverter.ToString( Encoding.ASCII.GetBytes( stringArray ) ).Replace( "-" , " " );
                IntPtr [ ] FindAddress = dn.ScanArray( dn.GetPID( processName ) , SearchArray );
                byte [ ] bufferArray = new byte [ SearchArray.Length + 9 ];
                for ( int i = 0 ; i < FindAddress.Length ; i++ ) {
                    dn.WriteArray( FindAddress [ i ] , BitConverter.ToString( bufferArray ).Replace( "-" , " " ) );
                }
            } catch { }
        }
    }
}
