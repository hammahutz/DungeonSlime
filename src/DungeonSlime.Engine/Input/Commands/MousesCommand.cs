using System;

using Microsoft.Xna.Framework.Input;

namespace DungeonSlime.Engine.Input.Commands;

public class MouseCommand : Command<MouseState, MouseButton>
{
    public MouseCommand(MouseButton button, InputTrigger trigger, Action action) : base(button, trigger, action){}
}
