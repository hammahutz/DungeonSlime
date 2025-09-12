using System;

using Microsoft.Xna.Framework;

namespace DungeonSlime.Engine.Input;

public abstract class InputInfo<TState, TInput>
    where TState : struct
    where TInput : Enum
{
    public TState PreviousState { get; protected set; }
    public TState CurrentState { get; protected set; }
    protected abstract TState GetState { get; }


    public InputInfo()
    {
        PreviousState = new TState();
        CurrentState = GetState;
    }

    public void Update(GameTime gameTime)
    {
        PreviousState = CurrentState;
        CurrentState = GetState;
        UpdateState(gameTime);
    }
    protected virtual void UpdateState(GameTime gameTime) { }
    public abstract bool IsDown(TInput input);
    public abstract bool IsUp(TInput input);
    public abstract bool WasJustPressed(TInput input);
    public abstract bool WasJustReleased(TInput input);
}
