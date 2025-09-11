using System;
using System.IO;

namespace DungeonSlime.Engine.Utils;

public class FileOutput : ILogOutput
{
    private readonly string _filePath;
    public FileOutput(params string[] filePath)
    {
        _filePath = Path.Combine(filePath);
        if (!Directory.Exists(Path.GetDirectoryName(_filePath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);
        }
    }

    public void Write(LogLevel level, string message)
    {
        var logEntry = $"{message}{Environment.NewLine}";
        System.IO.File.AppendAllText(_filePath, logEntry);
    }
}