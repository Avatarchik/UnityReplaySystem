using System;
using System.Collections.Generic;

public sealed class StateMachine
{
    /// <summary>
    /// Sends an ExitState signal on the previous state when set, before sending an EnterState signal on the new state
    /// Does nothing if the current state is the same as the new state
    /// </summary>
    public Enum currentState
    {
        get
        {
            return _currentState;
        }

        set
        {
            if (currentState == value)
                return;

            if (currentState != null)
                SendSignal<Void>("ExitState");

            _currentState = value;
            SendSignal<Void>("EnterState");

            if (OnStateChanged != null)
                OnStateChanged();
        }
    }

    /// <summary>
    /// Called when the currentState is set to a new value
    /// </summary>
    public event Action OnStateChanged;

    private Enum _currentState;

    // Object that is sent all signals
    private readonly object receiver;

    // Cache all signal response delegates, for each signal, for each state
    private Dictionary<Enum, Dictionary<string, Delegate>> cache = new Dictionary<Enum, Dictionary<string, Delegate>>();

    /// <param name="receiver">Object that will receive all method callbacks</param>
    public StateMachine(object receiver)
    {
        this.receiver = receiver;
    }

    /// <summary>
    /// Triggers the response delegate on the receiver object for the specified signal. Delegate method name follows
    /// the form of stateName_signal
    /// </summary>
    /// <returns>The delegate's returned value, or the default of T if no delegate exists</returns>
    public T SendSignal<T>(string signal)
    {
        if (currentState == null)
        {
            throw new NullReferenceException("Current state of State Machine is null");
        }

        // Attempt to retrieve the current dictionary of signal responses for this state
        Dictionary<string, Delegate> lookup;
        if (!cache.TryGetValue(currentState, out lookup))
        {
            cache[currentState] = lookup = new Dictionary<string, Delegate>();
        }

        // Attempt to receive the delegate for this signal response, for this specific state
        Delegate signalResponse;
        if (!lookup.TryGetValue(signal, out signalResponse))
        {
            // Bind the method to the signal response, or null if the method does not exist
            signalResponse = Delegate.CreateDelegate(typeof(Func<T>), receiver, currentState + "_" + signal, false, false);

            // Cache the signal to avoid looking up repeatedly
            lookup[signal] = signalResponse;
        }

        if (signalResponse != null)
        {
            return (signalResponse as Func<T>)();
        }
        else
        {
            return default(T);
        }
    }
}

