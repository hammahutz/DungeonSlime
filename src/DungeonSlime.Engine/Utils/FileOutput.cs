using System;

namespace DungeonSlime.Engine.Utils;

public class FileOutput : ILogOutput
{
    private readonly string _filePath;
    public FileOutput(string filePath) => _filePath = filePath;

    public void Write(LogLevel level, string message)
    {
        var logEntry = $"{message}{Environment.NewLine}";
        System.IO.File.AppendAllText(_filePath, logEntry);
    }
}