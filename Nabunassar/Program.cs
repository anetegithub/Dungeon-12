﻿global using Geranium.Reflection;
global using Dungeon;
using Dungeon.Monogame.Runner;
using Dungeon.Monogame.Settings;
using Dungeon.VariableEditor;
using Nabunassar;

var cfg = DungeonGlobal.Init<Global>(true, true);

var monocfg = cfg.Get<MonogameSettings>("Monogame");
#if DEBUG
monocfg.IsDebug = true;
#endif

var client = new MonogameRunner(monocfg);

Debugger.IsEnabled=true;

DungeonGlobal.OnRun+=() =>
{
    Task.Run(() =>
    {
        while (true)
        {
            var value = Console.ReadLine();
            if (value=="debugger")
            {
                Debugger.IsEnabled=!Debugger.IsEnabled;
                continue;
            }

            if (value.IsNotEmpty() && value.Contains(' '))
            {                
                var keyvalue = value.Split(' ');
                Debugger.Set(keyvalue[0], keyvalue[1]);
            }
            else if (!value.Contains(' '))
            {
                Debugger.Get(value);
            }
        }
    });
};

if (cfg.VariableEditor)
{
    var form = new VariablesForm();
    form.WindowState = System.Windows.Forms.FormWindowState.Minimized;
    form.Show();
}

DungeonGlobal.Run(client);