using System;

using Microsoft.Xna.Framework.Input;

namespace DungeonSlime.Engine.Input.Commands;

public class KeyboardCommand : Command<KeyboardState, Keys>
{
    public KeyboardCommand(Keys key, InputTrigger trigger, Action action) : base(key, trigger, action){}
}
