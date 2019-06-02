﻿using System;

namespace Rogue.Monogame
{
    public static class Program
    {
        private static bool CompileDatabase => true;

        [STAThread]
        static void Main()
        {
            if (CompileDatabase)
                Rogue.DataAccess.Program.Main(new string[0]);

            using (var game = new XNADrawClient())
                game.Run();
        }
    }
}