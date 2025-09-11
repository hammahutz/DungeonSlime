using System;
using System.Collections.Generic;

namespace DungeonSlime.Engine.Utils;

public static class Logger
{
    private static readonly object _lock = new();
    private static readonly List<ILogOutput> _outputs = new();
    public static LogLevel MinimumLogLevel { get; set; } = LogLevel.Trace;

    static Logger()
    {
        _outputs.Add(new ConsoleOutput());
        _outputs.Add(new FileOutput("log/game.log"));
    }
    public static void AddOutput(ILogOutput output)
    {
        lock (_lock)
        {
            _outputs.Add(output);
        }
    }

    private static void Log(LogLevel level, string message)
    {
        if (level > MinimumLogLevel) return;

        var logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";

        lock (_lock)
        {
            foreach (var output in _outputs)
            {
                output.Write(level, logMessage);
            }
        }
    }

    public static void Trace(string message) => Log(LogLevel.Trace, message);
    public static void Info(string message) => Log(LogLevel.Info, message);
    public static void Debug(string message) => Log(LogLevel.Debug, message);
    public static void Warning(string message) => Log(LogLevel.Warning, message);
    public static void Error(string message) => Log(LogLevel.Error, message);
    public static void Critical(string message) => Log(LogLevel.Critical, message);

}