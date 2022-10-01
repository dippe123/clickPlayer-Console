using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static helpers.Variables;
using static helpers.WinAPI;
using static helpers.WinStructs;

namespace helpers {
    class ShakeHelper {
        public static void Jitter( int jittervalueX , int jittervalueY ) {
            Random r = new Random( );
            int x = ( int ) Math.Round( Cursor.Position.X * 0.002 + Convert.ToInt32( jittervalueX / 10 / 2 ) );
            int x_ = ( int ) Math.Round( Cursor.Position.X * 0.003 + Convert.ToInt32( jittervalueX / 10 / 2 ) );
            int y = ( int ) Math.Round( Cursor.Position.Y * 0.002 + Convert.ToInt32( jittervalueY / 10 / 2 ) );
            int y_ = ( int ) Math.Round( Cursor.Position.Y * 0.003 + Convert.ToInt32( jittervalueY / 10 / 2 ) );
            if ( jitterenable == "TRUE" && !isCursorVisible( ) )
                Cursor.Position = checked(new Point( Cursor.Position.X + r.Next( -1 * x , x_ ) , Cursor.Position.Y + r.Next( -1 * y , y_ ) ));
        }
        private static bool isCursorVisible( ) {
            CURSORINFO cursorInfo = new CURSORINFO( );
            cursorInfo.cbSize = Marshal.SizeOf( cursorInfo );
            if ( GetCursorInfo( out cursorInfo ) ) {
                int mousehandle_int = ( int ) cursorInfo.hCursor;
                if ( mousehandle_int > 50000 & mousehandle_int < 100000 )
                    return true;
                else
                    return false;
            }
            return false;
        }
        public static void SendJitter( ) {
            Random rnd = new Random( );
            if ( rnd.Next( 0 , 100 ) <= chanchevalue )
                Jitter( x , y );
        }
    }
}
