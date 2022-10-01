using System;
using System.IO;
using System.Security.Cryptography;

namespace checks {
    class HashCheck {
        public static string GetFileHash( string file ) {
            using ( FileStream stream = File.OpenRead( file ) ) {
                SHA256Managed sha = new SHA256Managed( );
                byte [ ] checksum = sha.ComputeHash( stream );
                return BitConverter.ToString( checksum ).Replace( "-" , string.Empty );
            }
        }

    }
}



