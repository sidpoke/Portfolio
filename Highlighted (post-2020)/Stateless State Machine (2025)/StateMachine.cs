using System;
using System.Collections.Generic;

namespace SidUtils.Machinery.FSM;

/// <summary>
/// A generic stateless state machine with support for hierarchical states
/// and signal-based triggering. Inspired by Gerold Schneider's FSM.
/// </summary>
public class StateMachine<TState, TTrigger> 
    where TState : Enum 
    where TTrigger : Enum
{
    // Current state of the FSM
    public TState InitialState { get; private set; }
    
    // Current state of the FSM
    public TState PreviousState { get; private set; }
    
    // Current state of the FSM
    public TState CurrentState { get; private set; }
    
    /// <summary>
    /// Invoked on state transition.
    /// </summary>
    public event Action<StateTransition<TState, TTrigger>> OnTransition;
    
    /// <summary>
    /// Invoked on state transition failure.
    /// </summary>
    public event Action<string> OnError;
    
    // Dictionary to hold state configurations
    private readonly Dictionary<TState, StateConfig<TState, TTrigger>> _stateConfigurations = new();
    
    /// <summary>
    /// Constructs a StateMachine, the initial state is required.
    /// </summary>
    /// <param name="initialState">Must be of <typeparamref name="TState"/></param>
    public StateMachine(TState initialState)
    {
        InitialState = initialState;
        CurrentState = initialState;
    }
    
    /// <summary>
    /// Configures a specific state.
    /// </summary>
    public StateConfig<TState, TTrigger> Configure(TState state)
    {
        if (_stateConfigurations.TryGetValue(state, out var config))
        {
            return config;
        }
        
        config = new StateConfig<TState, TTrigger>(this, state);
        _stateConfigurations.TryAdd(state, config);
        return config;
    }

    /// <summary>
    /// Fires a trigger, attempting a state transition.
    /// Signals drive the core logic.
    /// </summary>
    /// <param name="trigger"></param>
    public void Fire(TTrigger trigger)
    {
        // Validate if the trigger exists
        if (!_stateConfigurations.TryGetValue(CurrentState, out var config))
        {
            OnError?.Invoke($"No configuration found for current state: { CurrentState }");
            return;
        }

        // Return early if the configuration ignores the trigger.
        if (config.CheckIgnored(trigger))
        {
            return;
        }

        // Checks if the transition is internal and executes the internal action.
        if (config.CheckInternal(trigger, out Action action))
        {
            action();
            return;
        }
        
        // Validate if the trigger has a valid transition in the current state or parent hierarchy
        if (!config.TryGetTransition(trigger, out (Func<bool> predicate, Func<TState> state) destinationState))
        {
            OnError?.Invoke($"No valid transition for trigger '{ trigger }' from state '{ CurrentState }'.");
            return;
        }
        
        // Validate destination state
        if (destinationState.state == null)
        {
            OnError?.Invoke($"Destination state is null for trigger '{ trigger }' in state '{ CurrentState }'.");
            return;
        }
        
        // Validate predicate of the destination state
        if (destinationState.predicate != null && !destinationState.predicate.Invoke())
        {
            OnError?.Invoke($"Predicate prevented transition for trigger '{trigger}' from state '{ CurrentState }'.");
            return;
        }
        
        // Notify about the successful state transition
        OnTransition?.Invoke(new StateTransition<TState, TTrigger>(CurrentState, trigger, destinationState.state.Invoke()));
        
        // Exit current state
        InvokeOnExit(CurrentState);

        // Save the previous state.
        PreviousState = CurrentState;
        
        // Transition to destination state
        CurrentState = destinationState.state.Invoke();

        // Enter the new state
        InvokeOnEntry(CurrentState);
    }
    
    /// <summary>
    /// Calls the entry Action of the provided state.
    /// </summary>
    /// <param name="state">The state to call.</param>
    private void InvokeOnEntry(TState state)
    {
        if (_stateConfigurations.TryGetValue(state, out var config))
        {
            config.InvokeEntryAction();
        }
    }

    /// <summary>
    /// Calls the entry Action of the provided state.
    /// </summary>
    /// <param name="state">The state to call.</param>
    private void InvokeOnExit(TState state)
    {
        if (_stateConfigurations.TryGetValue(state, out var config))
        {
            config.InvokeExitAction();
        }
    }
}