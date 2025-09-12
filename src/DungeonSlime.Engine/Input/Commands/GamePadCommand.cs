using System;

using Microsoft.Xna.Framework.Input;

namespace DungeonSlime.Engine.Input.Commands;

public class GamePadCommand : Command<GamePadState, Buttons>
{
    public GamePadCommand(Buttons button, InputTrigger trigger, Action action) : base(button, trigger, action) { }
}
