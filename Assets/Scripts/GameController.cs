using UnityEngine;

public class GameController : StateMachineMonoBehaviour 
{
    [SerializeField]
    ReplayLogger logger;

    [SerializeField]
    TankHealth playerA;

    [SerializeField]
    TankHealth playerB;

    [SerializeField]
    ReplayInputController replay;

    [SerializeField]
    float replayDelay = 3f;

    enum GameStates { Playing, Aftermath, Replay }

    protected override void Awake()
    {
        base.Awake();

        stateMachine.currentState = GameStates.Playing;

        playerA.OnDeath += OnGameOver;
        playerB.OnDeath += OnGameOver;
    }

    private void OnGameOver()
    {
        stateMachine.SendSignal<Void>("OnGameOver");
    }

    Void Playing_EnterState()
    {
        logger.SetLogging(true);

        return Void.empty;
    }

    Void Playing_OnGameOver()
    {
        logger.SetLogging(false);

        playerA.GetComponent<PlayerInputController>().enabled = false;
        playerB.GetComponent<PlayerInputController>().enabled = false;

        playerA.GetComponent<TankController>().Halt();
        playerB.GetComponent<TankController>().Halt();

        stateMachine.currentState = GameStates.Aftermath;

        return Void.empty;
    }

    Void Aftermath_Update()
    {
        if (Time.time > timeEnteredState + replayDelay)
        {
            stateMachine.currentState = GameStates.Replay;
        }

        return Void.empty;
    }

    Void Replay_EnterState()
    {
        foreach (var shell in FindObjectsOfType<CannonShell>())
        {
            Destroy(shell.gameObject);
        }

        playerA.gameObject.SetActive(true);
        playerB.gameObject.SetActive(true);

        replay.Begin(Time.time - replayDelay);

        return Void.empty;
    }

    Void Replay_OnGameOver()
    {
        playerA.GetComponent<TankController>().Halt();
        playerB.GetComponent<TankController>().Halt();

        return Void.empty;
    }
}
