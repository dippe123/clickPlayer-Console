using System;
using System.Windows.Forms;
using static helpers.Variables;
using static helpers.WinAPI;

namespace helpers {
    class MouseHelper {
        public static Keys clickerKeybind;
        public static bool isKeyDown( Keys key ) {
            byte [ ] result = BitConverter.GetBytes( GetAsyncKeyState( ( short ) key ) );
            return result [ 0 ] == 1;
        }
        private static void customPause( double millisecs ) {
            DateTime Tthen = DateTime.Now;
            do {
                Application.DoEvents( );
            } while ( Tthen.AddMilliseconds( millisecs ) > DateTime.Now );
        }
        public static void sendClicks( ) {
            PostMessage( GetForegroundWindow( ) , 0x0201 , 0 , 0 );
            ShakeHelper.SendJitter( );
            customPause( ( int ) Math.Round( double.Parse( lines [ clickCount++ ] ) ) );
            PostMessage( GetForegroundWindow( ) , 0x0202 , 0 , 0 );
            ShakeHelper.SendJitter( );
            customPause( ( int ) Math.Round( double.Parse( lines [ clickCount++ ] ) ) );
        }
    }
}
