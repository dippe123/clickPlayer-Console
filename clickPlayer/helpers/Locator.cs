using System.Text;

namespace helpers {
    class Locator {
        public static bool GetMinecraftWindow( ) {
            if ( GetName( ).Contains( "Lunar" ) || GetName( ).Contains( "Badlion" ) || GetName( ).Contains( "Minecraft" ) || GetName( ).Contains( "Cheatbreaker" ) )
                return true;
            else
                return false;
        }
        public static string GetName( ) {
            StringBuilder stringBuilder = new StringBuilder( 256 );
            WinAPI.GetWindowText( WinAPI.GetForegroundWindow( ) , stringBuilder , stringBuilder.Capacity );
            return stringBuilder.ToString( );
        }
    }
}
