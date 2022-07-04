using System;
using System.Collections.Generic;
using System.Linq;

namespace SidUtils.Machinery.FSM;

public class StateConfig<TState, TTrigger> 
    where TState : Enum 
    where TTrigger : Enum
{
    /// <summary>
    /// The StateMachine of this StateConfig.
    /// </summary>
    public StateMachine<TState, TTrigger> Machine { get; private set; }
    
    /// <summary>
    /// The State this StateConfig describes.
    /// </summary>
    public TState State { get; private set; }
    
    /// <summary>
    /// The parent of this state. May be null.
    /// </summary>
    public TState Parent { get; private set; }
    
    private readonly Dictionary<TTrigger, (Func<bool> predicate, Func<TState> state)> _transitions = new();
    private readonly HashSet<TTrigger> _ignoredTriggers = new();
    private readonly Dictionary<TTrigger, (Func<bool> predicate, Action action)> _internalTransition = new();
    
    private Action _onEntry = () => { }; 
    private Action _onExit = () => { };
    
    /// <summary>
    /// Condition predicate to always allow a transition.
    /// </summary>
    public Func<bool> Always => () => true;
    
    /// <summary>
    /// Creates a new state machine configuration.
    /// </summary>
    /// <remarks>This constructor is called automatically and should not be used directly.</remarks>
    public StateConfig ( StateMachine<TState, TTrigger> stateMachine, TState state ) {
        Machine = stateMachine;
        State = state;
        Parent = state;
    }
    
    /// <summary>
    /// Defines a transition for a specific trigger.
    /// </summary>
    public StateConfig<TState, TTrigger> Permit(TTrigger trigger, TState destinationState)
    {
        _transitions[trigger] = (Always, () => destinationState);
        return this;
    }
    
    /// <summary>
    /// Defines a transition with a dynamic state selector for a specific trigger.
    /// </summary>
    public StateConfig<TState, TTrigger> PermitDynamic(TTrigger trigger, Func<TState> stateSelector)
    {
        _transitions[trigger] = (Always, stateSelector);
        return this;
    }
    
    /// <summary>
    /// Defines a transition for a specific trigger if it fulfills a condition predicate.
    /// </summary>
    public StateConfig<TState, TTrigger> PermitIf(TTrigger trigger, TState destinationState, Func<bool> predicate)
    {
        _transitions[trigger] = (predicate, () => destinationState);
        return this;
    }
    
    /// <summary>
    /// Defines a transition with a dynamic state selector for a specific trigger if it fulfills a condition predicate.
    /// </summary>
    public StateConfig<TState, TTrigger> PermitDynamicIf(TTrigger trigger, Func<TState> stateSelector, Func<bool> predicate)
    {
        _transitions[trigger] = (predicate, stateSelector);
        return this;
    }

    /// <summary>
    /// Defines a transition that should be ignored.
    /// </summary>
    public StateConfig<TState, TTrigger> Ignore(TTrigger trigger)
    {
        _ignoredTriggers.Add(trigger);
        return this;
    }

    /// <summary>
    /// Defines a trigger to execute an internal transition with an action.
    /// </summary>
    public StateConfig<TState, TTrigger> InternalTransition(TTrigger trigger, Action action)
    {
        _internalTransition[trigger] = (Always, action);
        return this;
    }
    
    /// <summary>
    /// Defines a trigger to execute an internal transition with an action if it fulfills a condition predicate.
    /// </summary>
    public StateConfig<TState, TTrigger> InternalTransitionIf(TTrigger trigger, Action action, Func<bool> predicate)
    {
        _internalTransition[trigger] = (predicate, action);
        return this;
    }
    
    /// <summary>
    /// Only one action is allowed. Defines the entry action of the state.
    /// </summary>
    public StateConfig<TState, TTrigger> OnEntry(Action onEntry)
    {
        _onEntry = onEntry;
        return this;
    }

    /// <summary>
    /// Only one action is allowed. Defines the exit action of the state.
    /// </summary>
    public StateConfig<TState, TTrigger> OnExit(Action onExit)
    {
        _onExit = onExit;
        return this;
    }

    /// <summary>
    /// Defines this state as a substate of a parent state.
    /// </summary>
    public StateConfig<TState, TTrigger> SubstateOf(TState parentState)
    {
        Parent = parentState;
        return this;
    }

    /// <summary>
    /// Checks if the trigger is an internal transition.
    /// Also returns the internal Action to execute.
    /// </summary>
    public bool CheckInternal(TTrigger trigger, out Action internalAction)
    {
        internalAction = _internalTransition[trigger].action;
        return _internalTransition.ContainsKey(trigger);
    }
    
    /// <summary>
    /// Returns if a trigger is ignored by this state configuration.
    /// </summary>
    public bool CheckIgnored(TTrigger trigger)
    {
        return _ignoredTriggers.Contains(trigger);
    }
    
    /// <summary>
    /// Finds the transition for a specific trigger, searching parent-state hierarchy if necessary.
    /// </summary>
    public bool TryGetTransition(TTrigger trigger, out (Func<bool>, Func<TState>) destinationState, HashSet<TState> visited = null)
    {
        // Create a recursive Hashset to figure out if a parent state has already been visited
        visited ??= new HashSet<TState>();

        // Try adding the state to the visited hashset
        if (!visited.Add(State)) 
        {
            destinationState = default;
            return false;
        }

        // If the trigger has been found in this configuration, return its transition state
        if (_transitions.TryGetValue(trigger, out destinationState))
        {
            return true;
        }

        // Check if the parent has the transition
        if (Parent != null && Machine.Configure(Parent).TryGetTransition(trigger, out destinationState, visited))
        {
            return true;
        }

        // When no transition was found
        destinationState = default;
        return false;
    }
    
    /// <summary>
    /// Executes the on-entry action for this state.
    /// </summary>
    public void InvokeEntryAction()
    {
        _onEntry?.Invoke();
    }

    /// <summary>
    /// Executes the on-exit action for this state.
    /// </summary>
    public void InvokeExitAction()
    {
        _onExit?.Invoke();
    }
}