using System.Collections.Generic;
namespace DungeonSlime.Engine.Input.Commands;

public class CommandHandler
{

    public List<KeyboardCommand> KeyboardCommands { get; private set; }
    public List<MouseCommand> MouseCommands { get; private set; }
    public List<GamePadCommand> GamePadCommands { get; private set; }

    public CommandHandler()
    {
        KeyboardCommands = new List<KeyboardCommand>();
        MouseCommands = new List<MouseCommand>();
        GamePadCommands = new List<GamePadCommand>();
    }

    public void RegisterKeyboardCommand(KeyboardCommand command)
    {
        if (!KeyboardCommands.Contains(command))
            KeyboardCommands.Add(command);
    }
    public void RegisterMouseCommand(MouseCommand command)
    {
        if (!MouseCommands.Contains(command))
            MouseCommands.Add(command);
    }
    public void RegisterGamePadCommand(GamePadCommand command)
    {
        if (!GamePadCommands.Contains(command))
            GamePadCommands.Add(command);
    }

    public void Update(KeyboardInfo keyboard, MouseInfo mouse, GamePadInfo[] gamePads)
    {
        foreach (var command in KeyboardCommands)
        {
            command.ExecuteIfTriggered(keyboard);
        }
        foreach (var command in MouseCommands)
        {
            command.ExecuteIfTriggered(mouse);
        }
        foreach (var command in GamePadCommands)
        {
            foreach (var gamePad in gamePads)
            {
                if (gamePad is null || !gamePad.IsConnected)
                    continue;
                command.ExecuteIfTriggered(gamePad);
            }
        }
    }

}