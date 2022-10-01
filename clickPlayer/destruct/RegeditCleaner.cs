using Microsoft.Win32;

namespace destruct {
    class RegeditCleaner {
        public static void Init( ) {
            try {
                CurrentUserCleanRegedit( "SOFTWARE\\MICROSOFT\\WINDOWS\\SHELL\\" );
                CurrentUserCleanRegedit( "SOFTWARE\\MICROSOFT\\WINDOWS\\SHELLNOROAM\\" );
                CurrentUserCleanRegedit( "SOFTWARE\\MICROSOFT\\WINDOWS\\CURRENTVERSION\\Explorer\\COMDLG32\\" );
                CurrentUserCleanRegedit( "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\UserAssist" );
                CurrentUserCleanRegedit( "SOFTWARE\\CLASSES\\LOCAL SETTINGS\\SOFTWARE\\MICROSOFT\\WINDOWS\\SHELL\\" );
                CurrentUserCleanRegedit( "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\FeatureUsage\\AppSwitched" );
                CurrentUserCleanRegedit( "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Internet Settings\\5.0\\Cache\\History\\" );
                CurrentUserCleanRegedit( "SOFTWARE\\MICROSOFT\\WINDOWS NT\\CURRENTVERSION\\APPCOMPATFLAGS\\COMPATIBILITY ASSISTANT\\" );
                CurrentUserCleanRegedit( "SOFTWARE\\Classes\\Local Settings\\Software\\Microsoft\\Windows\\CurrentVersion\\AppContainer\\Storage\\" );
                CurrentUserCleanRegedit( "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\ComDlg32\\OpenSavePidlMRU\\*" );
                CurrentUserCleanRegedit( "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\ComDlg32\\OpenSavePidlMRU\\exe" );
                CurrentUserCleanRegedit( "Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\ComDlg32\\LastVisitedPidlMRU" );
                LocalMachineCleanRegedit( "SYSTEM\\ControlSet001\\Services\\bam\\State\\" );
            } catch { }
        }
        public static void CurrentUserCleanRegedit( string path ) {
            using ( RegistryKey key = Registry.CurrentUser.OpenSubKey( path , true ) ) {
                foreach ( string c in key.GetSubKeyNames( ) ) {
                    try {
                        RegistryKey pkey = key.OpenSubKey( c );
                        pkey.DeleteSubKeyTree( c );
                    } catch { }
                }
            }
        }

        public static void LocalMachineCleanRegedit( string path ) {
            using ( RegistryKey key = Registry.LocalMachine.OpenSubKey( path , true ) ) {
                foreach ( string c in key.GetSubKeyNames( ) ) {
                    try {
                        RegistryKey pkey = key.OpenSubKey( c );
                        pkey.DeleteSubKeyTree( c );
                    } catch { }
                }
            }
        }
    }
}
