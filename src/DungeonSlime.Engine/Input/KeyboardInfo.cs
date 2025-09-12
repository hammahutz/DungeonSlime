using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace DungeonSlime.Engine.Input;

public class KeyboardInfo : InputInfo<KeyboardState, Keys>
{
    protected override KeyboardState GetState => Keyboard.GetState();

    public override bool IsDown(Keys key) => CurrentState.IsKeyDown(key);
    public override bool IsUp(Keys key) => CurrentState.IsKeyUp(key);
    public override bool WasJustPressed(Keys key) => CurrentState.IsKeyDown(key) && PreviousState.IsKeyUp(key);
    public override bool WasJustReleased(Keys key) => CurrentState.IsKeyUp(key) && PreviousState.IsKeyDown(key);

}