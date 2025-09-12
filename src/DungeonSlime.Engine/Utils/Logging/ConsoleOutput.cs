using System;

namespace DungeonSlime.Engine.Utils.Logging;

public class ConsoleOutput : ILogOutput
{
    public void Write(LogLevel level, string message)
    {
        var originalColor = Console.ForegroundColor;

        Console.ForegroundColor = level switch
        {
            LogLevel.Trace => ConsoleColor.Gray,
            LogLevel.Info => ConsoleColor.Green,
            LogLevel.Debug => ConsoleColor.Blue,
            LogLevel.Warning => ConsoleColor.Yellow,
            LogLevel.Error => ConsoleColor.Red,
            LogLevel.Critical => ConsoleColor.Magenta,
            _ => originalColor
        };

        Console.WriteLine(message);
        Console.ForegroundColor = originalColor;
    }
}