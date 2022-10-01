using System;
using System.IO;
using System.Windows.Forms;
using Tommy;
using helpers;
using static helpers.Variables;

namespace config {
    class Parser {
        public static void ParseTOML( ) {
            using ( StreamReader reader = File.OpenText( "config.toml" ) ) {
                TomlTable table = TOML.Parse( reader );
                var key = table [ "clicker" ] [ "keybind" ].AsString;
                var invcccliiicck = table [ "clicker" ] [ "inventory" ].AsBoolean;
                var jitterenablee = table [ "shake" ] [ "enabled" ].AsBoolean;
                var chanchevalparse = table [ "shake" ] [ "chance" ].AsInteger;
                var xvaluee = table [ "shake" ] [ "X" ].AsInteger;
                var yvaluee = table [ "shake" ] [ "Y" ].AsInteger;
                keyz = key.ToString( );
                invcliccattt = invcccliiicck.ToString( ).ToUpper( );
                xvalue = xvaluee.ToString( );
                yvalue = yvaluee.ToString( );
                chanchevalueparss = chanchevalparse.ToString( );
                jitterenable = jitterenablee.ToString( ).ToUpper( );

            }
            coc = keyz.ToUpper( );
            MouseHelper.clickerKeybind = ( Keys ) Enum.Parse( typeof( Keys ) , coc );
            chanchevalue = int.Parse( chanchevalueparss );
            x = int.Parse( yvalue );
            y = int.Parse( xvalue );
        }
    }
}
