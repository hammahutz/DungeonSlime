namespace DungeonSlime.Engine.Utils.Logging;

public interface ILogOutput
{
    void Write(LogLevel level, string message);
}