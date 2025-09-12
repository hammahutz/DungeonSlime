using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace DungeonSlime.Engine.Input;

public class GamePadInfo : InputInfo<GamePadState, Buttons>
{
    private TimeSpan _vibrationTimeRemaining = TimeSpan.Zero;
    public PlayerIndex PlaterIndex { get; }


    public bool IsConnected => CurrentState.IsConnected;
    public bool IsVibrating => _vibrationTimeRemaining > TimeSpan.Zero;

    public Vector2 LeftThumbStick => CurrentState.ThumbSticks.Left;
    public Vector2 RightThumbStick => CurrentState.ThumbSticks.Right;
    public float LeftTrigger => CurrentState.Triggers.Left;
    public float RightTrigger => CurrentState.Triggers.Right;

    protected override GamePadState GetState => GamePad.GetState(PlaterIndex);

    public GamePadInfo(PlayerIndex playerIndex)
    {
        PlaterIndex = playerIndex;
        PreviousState = new GamePadState();
        CurrentState = GamePad.GetState(PlaterIndex);
    }


    protected override void UpdateState(GameTime gameTime)
    {
        if (_vibrationTimeRemaining > TimeSpan.Zero)
        {
            _vibrationTimeRemaining -= gameTime.ElapsedGameTime;
            if (_vibrationTimeRemaining <= TimeSpan.Zero)
            {
                _vibrationTimeRemaining = TimeSpan.Zero;
                StopVibration();
            }
        }
    }

    public override bool IsDown(Buttons button) => CurrentState.IsButtonDown(button);
    public override bool IsUp(Buttons button) => CurrentState.IsButtonUp(button);
    public override bool WasJustPressed(Buttons button) => CurrentState.IsButtonDown(button) && PreviousState.IsButtonUp(button);
    public override bool WasJustReleased(Buttons button) => CurrentState.IsButtonUp(button) && PreviousState.IsButtonDown(button);



    public void StartVibration(float motor, TimeSpan duration)
    {
        GamePad.SetVibration(PlaterIndex, motor, motor);
        _vibrationTimeRemaining = duration;
    }

    public void StartVibration(float leftMotor, float rightMotor, TimeSpan duration)
    {
        GamePad.SetVibration(PlaterIndex, leftMotor, rightMotor);
        _vibrationTimeRemaining = duration;
    }

    private void StopVibration() => GamePad.SetVibration(PlaterIndex, 0f, 0f);

}