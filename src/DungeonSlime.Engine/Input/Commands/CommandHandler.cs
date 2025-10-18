using System.Collections.Generic;

using Microsoft.Xna.Framework.Input;
namespace DungeonSlime.Engine.Input.Commands;

public class CommandHandler
{

    public List<Command<KeyboardState, Keys>> KeyboardCommands { get; private set; }
    public List<Command<MouseState, MouseButton>> MouseCommands { get; private set; }
    public List<Command<GamePadState, Buttons>> GamePadCommands { get; private set; }

    public CommandHandler()
    {
        KeyboardCommands = new();
        MouseCommands = new();
        GamePadCommands = new();
    }

    public void RegisterKeyboardCommand(Command<KeyboardState, Keys> command)
    {
        if (!KeyboardCommands.Contains(command))
            KeyboardCommands.Add(command);
    }
    public void RegisterMouseCommand(Command<MouseState, MouseButton> command)
    {
        if (!MouseCommands.Contains(command))
            MouseCommands.Add(command);
    }
    public void RegisterGamePadCommand(Command<GamePadState, Buttons> command)
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