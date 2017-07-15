using System;
using UnityEngine;


/// <summary>
/// Uses the StateMachine to send out MonoBehaviour method signals
/// </summary>
public abstract class StateMachineMonoBehaviour : MonoBehaviour
{
    [SerializeField]
    protected bool debugGUI;

    [SerializeField]
    Vector2 debugGuiPosition;

    [SerializeField]
    string debugGuiTitle = "State Machine";

    [SerializeField]
    protected bool autoUpdate = false;

    [SerializeField]
    protected bool autoFixedUpdate = false;

    public StateMachine stateMachine { get; private set; }
    public float timeEnteredState { get; private set; }

    public event Action<string> OnNewState;

    protected virtual void Awake()
    {
        stateMachine = new StateMachine(this);

        stateMachine.OnStateChanged += OnStateChanged;
    }

    private void OnStateChanged()
    {
        timeEnteredState = Time.time;

        if (OnNewState != null)
            OnNewState(stateMachine.currentState.ToString());
    }

    protected virtual void OnGUI()
    {
        if (debugGUI)
        {
            GUI.Box(new Rect(debugGuiPosition.x, debugGuiPosition.y, 200, 50), debugGuiTitle);

            GUI.TextField(new Rect(debugGuiPosition.x + 10, debugGuiPosition.y + 20, 180, 20), string.Format("State: {0}", stateMachine.currentState));
        }
    }

    protected virtual void EarlyGlobalUpdate() { }
    protected virtual void LateGlobalUpdate() { }

    protected virtual void EarlyGlobalFixedUpdate() { }
    protected virtual void LateGlobalFixedUpdate() { }

    void Update()
    {
        if (autoUpdate)
            GlobalUpdate();
    }

    protected void GlobalUpdate()
    {
        EarlyGlobalUpdate();
        stateMachine.SendSignal<Void>("Update");
        LateGlobalUpdate();
    }

    void FixedUpdate()
    {
        if (autoFixedUpdate)
            GlobalFixedUpdate();
    }

    protected void GlobalFixedUpdate()
    {
        EarlyGlobalFixedUpdate();
        stateMachine.SendSignal<Void>("FixedUpdate");
        LateGlobalFixedUpdate();
    }
}
