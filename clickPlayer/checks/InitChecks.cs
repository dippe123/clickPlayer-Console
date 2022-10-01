namespace checks {
    class InitChecks {
        public static void Checks( ) {
            AntiDumper.Init( );
            BetterAntiDump.BetterAntiDumper( );
            ProcessScan.Init( );
            IntegrityChecks.AssemblyJMP( );
            AntiDebugger.DebugInit( );
            AntiDebugger.TestOllyDbgExploit( );
            AntiDebugger.PatchingDbgUiRemoteBreakin( );
            AntiDebugger.HideThreadsFromDebugger( );
            DllCheck.PatchLoadLibraryW( );
            Emulation.Init( );
            Emulation.SandBoxCrash( );
        }
    }
}
