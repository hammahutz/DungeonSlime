using System;

namespace DungeonSlime.Engine.Input.Commands;

public class Command<TState, TInput>
    where TState : struct
    where TInput : Enum
{
    private readonly TInput _input;
    private readonly InputTrigger _trigger;
    private readonly Action _action;

    public Command(TInput input, InputTrigger trigger, Action action)
    {
        _input = input;
        _trigger = trigger;
        _action = action ?? throw new ArgumentNullException(nameof(action));
    }


    public Command(TInput input, InputTrigger trigger, ICommand command)
    {
        _input = input;
        _trigger = trigger;
        _action = command.Execute;
    }

    public void ExecuteIfTriggered(InputInfo<TState, TInput> state)
    {
        bool shouldExecute = _trigger switch
        {
            InputTrigger.Down => state.IsDown(_input),
            InputTrigger.Up => state.IsUp(_input),
            InputTrigger.JustPressed => state.WasJustPressed(_input),
            InputTrigger.JustReleased => state.WasJustReleased(_input),
            _ => false
        };

        if (shouldExecute)
        {
            _action();
        }
    }
}
