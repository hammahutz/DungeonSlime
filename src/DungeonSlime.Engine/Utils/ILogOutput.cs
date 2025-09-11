namespace DungeonSlime.Engine.Utils;

public interface ILogOutput
{
    void Write(LogLevel level, string message);
}