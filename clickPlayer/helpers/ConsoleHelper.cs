using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Console = Colorful.Console;

namespace helpers {

    internal class ConsoleHelper {
        public const int MF_BYCOMMAND = 0x00000000;
        public const int SC_CLOSE = 0xF060;
        public const int SC_MINIMIZE = 0xF020;
        public const int SC_MAXIMIZE = 0xF030;
        public const int SC_SIZE = 0xF000;
        public const int SW_HIDE = 0;
        public const int SW_SHOW = 5;
        private const uint ENABLE_QUICK_EDIT = 0x0040;
        private const int STD_INPUT_HANDLE = -10;
        internal static bool DisableSelection( ) {
            IntPtr consoleHandle = WinAPI.GetStdHandle( STD_INPUT_HANDLE );
            uint consoleMode;
            if ( !WinAPI.GetConsoleMode( consoleHandle , out consoleMode ) ) {
                return false;
            }
            consoleMode &= ~ENABLE_QUICK_EDIT;
            if ( !WinAPI.SetConsoleMode( consoleHandle , consoleMode ) ) {
                return false;
            }
            return true;
        }
        public static void printCustom( string info , string text ) {
            Console.Write( $"[{info}] > " , Color.FromArgb( 213 , 216 , 224 ) );
            Console.WriteLine( text );
        }
        public static void writeCustom( string info , string text ) {
            Console.Write( $"[{info}] > " , Color.FromArgb( 213 , 216 , 224 ) );
            Console.Write( text );
        }
        public static Stack<char> pass = new Stack<char>( );
        public static string ReadPassword( char mask ) {
            const int ENTER = 13, BACKSP = 8, CTRLBACKSP = 127;
            int [ ] FILTERED = { 0 , 27 , 9 , 10 };

            char chr = ( char ) 0;

            while ( ( chr = System.Console.ReadKey( true ).KeyChar ) != ENTER ) {
                if ( chr == BACKSP ) {
                    if ( pass.Count > 0 ) {
                        System.Console.Write( "\b \b" );
                        pass.Pop( );
                    }
                } else if ( chr == CTRLBACKSP ) {
                    while ( pass.Count > 0 ) {
                        System.Console.Write( "\b \b" );
                        pass.Pop( );
                    }
                } else if ( FILTERED.Count( x => chr == x ) > 0 ) { } else {
                    pass.Push( chr );
                    System.Console.Write( mask );
                }
            }

            System.Console.WriteLine( );

            return new string( pass.Reverse( ).ToArray( ) );
        }
        public static string ReadPassword( ) {
            return ReadPassword( '*' );
        }
        public static void Logo( ) {
            Console.ForegroundColor = Color.FromArgb( 171 , 110 , 193 );
            Console.WriteLine( @"
                                        /\
                                       /  \  _   _ _ __ ___  _ __ __ _
                                      / /\ \| | | | '__/ _ \| '__/ _` |
                                     / ____ \ |_| | | | (_) | | | (_| |
                                    /_/    \_\__,_|_|  \___/|_|  \__,_| Player v1.9

" );
        }

    }
}