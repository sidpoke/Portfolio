using System;

namespace SidUtils.Machinery.FSM;

/// <summary>
/// A message struct used by <see cref="StateMachine{TState,TTrigger}"/> to inform about conditions.
/// </summary>
public readonly struct StateTransition<TState, TTrigger> 
    where TState : Enum
    where TTrigger : Enum
{
    public TState Source { get; }
    public TState Destination { get; }
    public TTrigger Trigger { get; }

    public StateTransition(TState source, TTrigger trigger, TState destination)
    {
        Source = source;
        Trigger = trigger;
        Destination = destination;
    }

    public override string ToString()
    {
        return $"{Source} --({Trigger})--> {Destination}";
    }
}