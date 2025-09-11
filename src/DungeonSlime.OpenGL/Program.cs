using System;
using System.Diagnostics;

Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
Trace.AutoFlush = true;

using var game = new DungeonSlime.DungeonSlimeGame();
game.Run();