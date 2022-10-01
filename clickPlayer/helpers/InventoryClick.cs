using System.Runtime.InteropServices;
using static helpers.Variables;
using static helpers.WinAPI;
using static helpers.WinStructs;

namespace helpers {
    class InventoryClick {
        public static bool isInventoryEnabled( ) {
            CURSORINFO cursorInfo = new CURSORINFO( );
            cursorInfo.cbSize = Marshal.SizeOf( cursorInfo );
            if ( GetCursorInfo( out cursorInfo ) ) {
                int mousehandle_int = ( int ) cursorInfo.hCursor;
                if ( mousehandle_int > 50000 & mousehandle_int < 100000 && invcliccattt != "TRUE" )
                    return true;
                else
                    return false;
            }
            return false;
        }
    }
}
