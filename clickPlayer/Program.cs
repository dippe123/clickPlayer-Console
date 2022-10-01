using checks;
using helpers;
using System;
using System.IO;
using System.Net;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading.Tasks;
using Console = Colorful.Console;

using static helpers.ConsoleHelper;
using static config.Parser;
using static destruct.Destruct;
using static helpers.Variables;

namespace clickPlayer {
    class Program {
        private static void Main( string [ ] args ) {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var mutex = new Mutex( false , "AAA" );
            if ( !mutex.WaitOne( 0 , false ) )
                return;
            InitChecks.Checks( );
            WinAPI.TimerSet( );
            Console.CursorVisible = false;
            DisableSelection( );
            Logo( );
            var random = new Random( );
            Console.Title = "";
            Console.CursorVisible = false;
            if ( !File.Exists( "clicks.txt" ) || !File.Exists( "config.toml" ) ) {
                Console.Clear( );
                Logo( );
                Console.ForegroundColor = Color.FromArgb( 255 , 0 , 0 );
                printCustom( "info" , "unable to load the program" );
                Thread.Sleep( 2000 );
                Environment.Exit( -1 );
            }
            Console.Clear( );
            Logo( );
            ParseTOML( );
            DisableSelection( );
            Console.ForegroundColor = Color.FromArgb( 199 , 199 , 74 );
            printCustom( "instance" , "waiting minecraft window..." );
            while ( true ) {
                if ( Locator.GetMinecraftWindow( ) ) {
                    Console.Clear( );
                    Logo( );
                    Console.ForegroundColor = Color.FromArgb( 160 , 160 , 160 );
                    printCustom( "instance" , Locator.GetName( ) );
                    break;
                }
                Thread.Sleep( 150 );
            }
            var clickTimer = new Stopwatch( );
            bool toggled = true;
            bool firstclick = false;
            lineCount = 0;
            clickCount = 0;
            lines = File.ReadAllLines( "clicks.txt" );
            lineCount = File.ReadAllLines( "clicks.txt" ).Length;
            Task.Run( ( ) => {
                while ( true ) {
                    if ( MouseHelper.isKeyDown( Keys.End ) ) InitDestruct( ); Thread.Sleep( 100 );
                    if ( MouseHelper.isKeyDown( Keys.Insert ) ) WinAPI.ShowWindow( WinAPI.GetConsoleWindow( ) , SW_HIDE ); Thread.Sleep( 100 );
                    if ( MouseHelper.isKeyDown( MouseHelper.clickerKeybind ) ) toggled = !toggled; Thread.Sleep( 100 );
                }
            } );
            if ( lineCount % 2 >= 1 ) {
                clickCount++;
                clickCount = random.Next( 1 , lineCount / random.Next( 3 , 5 ) );
                Console.ForegroundColor = Color.FromArgb( 160 , 160 , 160 );
                printCustom( "playback" , $"loaded {lineCount} delays, starting at point: {clickCount}" );
                clickCount++;
            } else {
                clickCount = random.Next( 1 , lineCount / random.Next( 3 , 5 ) );
                Console.ForegroundColor = Color.FromArgb( 160 , 160 , 160 );
                printCustom( "playback" , $"loaded {lineCount} delays, starting at point: {clickCount}" );
                clickCount++;
            }
            Task.Run( ( ) => {
                while ( true ) {
                    if ( WinAPI.GetAsyncKeyState( Keys.LButton ) >= 0 ) { firstclick = true; clickTimer.Stop( ); };
                    if ( firstclick ) { clickTimer.Reset( ); clickTimer.Start( ); firstclick = false; }
                    if ( Locator.GetMinecraftWindow( ) && !InventoryClick.isInventoryEnabled( ) && WinAPI.GetAsyncKeyState( Keys.LButton ) < 0 ) {
                        if ( !firstclick && clickTimer.ElapsedMilliseconds > random.Next( 75 , 100 ) && toggled ) {
                            while ( clickCount >= lineCount ) {
                                clickCount = random.Next( 1 , lineCount / random.Next( 3 , 5 ) );
                                if ( clickCount % 2 >= 1 ) clickCount++;
                            }
                            MouseHelper.sendClicks( );
                        }
                    }
                }
            } );
        }
    }
}
