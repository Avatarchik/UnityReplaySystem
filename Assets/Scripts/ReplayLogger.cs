using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Logs input from a pair <see cref="PlayerInputController"/> instances.
/// </summary>
public class ReplayLogger : MonoBehaviour 
{
    [SerializeField]
    PlayerInputController playerA;

    [SerializeField]
    PlayerInputController playerB;

    private bool logging = false;

    public Queue<PlayerInput> inputQueueA;
    public Queue<PlayerInput> inputQueueB;

    void Awake()
    {
        inputQueueA = new Queue<PlayerInput>();
        inputQueueB = new Queue<PlayerInput>();
    }

    public void SetLogging(bool value)
    {
        logging = value;
    }

    // Input is not modified during fixed update—this makes it the best time to poll it
	void FixedUpdate () 
	{
        if (logging)
        {
            inputQueueA.Enqueue(playerA.input);
            inputQueueB.Enqueue(playerB.input);
        }
	}
}
