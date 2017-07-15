using UnityEngine;

/// <summary>
/// Pulls data from a <see cref="ReplayLogger"/> and plugs the input into 
/// a pair of <see cref="TankController"/> instances.
/// </summary>
public class ReplayInputController : MonoBehaviour 
{
    [SerializeField]
    ReplayLogger logger;

    [SerializeField]
    TankController playerA;

    [SerializeField]
    TankController playerB;

    [SerializeField]
    float slowTimeAmount = 0.25f;

    [SerializeField]
    float slowTimeDelay = 1;

    [SerializeField]
    GameObject replayHUD;

    private bool pulling = false;
    private bool timeSlowed = false;

    private float replayStartTime;
    private float matchDuration;

    public void Begin(float matchDuration)
    {
        pulling = true;

        playerA.Reset();
        playerB.Reset();

        replayStartTime = Time.time;

        this.matchDuration = matchDuration;

        Camera.main.GetComponent<VHSEffect>().enabled = true;

        replayHUD.SetActive(true);
    }

    void SlowTime()
    {
        Time.timeScale = slowTimeAmount;
    }

	void FixedUpdate () 
	{
		if (pulling)
        {
            if (logger.inputQueueA.Count > 0)
            {
                playerA.input = logger.inputQueueA.Dequeue();
                playerB.input = logger.inputQueueB.Dequeue();
            }

            if (!timeSlowed && Time.time - replayStartTime > matchDuration - slowTimeDelay)
            {
                SlowTime();
                timeSlowed = true;
            }
        }
	}
}
