using UnityEngine;
using System.Collections;

/// <summary>
/// Polls keyboard input during Update, caching it for FixedUpdate. Input cache is cleared 
/// after each FixedUpdate
/// </summary>
public class PlayerInputController : MonoBehaviour 
{
    [SerializeField]
    TankController target;

    [SerializeField]
    KeyCode up;

    [SerializeField]
    KeyCode down;

    [SerializeField]
    KeyCode right;

    [SerializeField]
    KeyCode left;

    [SerializeField]
    KeyCode rotatePositive;

    [SerializeField]
    KeyCode rotateNegative;

    [SerializeField]
    KeyCode fire;

    public PlayerInput input;

    private bool hasFixedUpdateRun;

    void Awake()
    {
        hasFixedUpdateRun = false;

        input = new PlayerInput();
    }

    void FixedUpdate()
    {
        hasFixedUpdateRun = true;
    }

    void Update () 
	{
        /*
         * Input is polled in Update, but is queried in FixedUpdate to allow the game to
         * run entirely on a fixed timestep. This ensures that the game is deterministic,
         * allowing the ability to create a "replay" by re-running all player inputs
         */

        if (hasFixedUpdateRun)
        {
            input.Clear();
            hasFixedUpdateRun = false;
        }

        /*
         * It's likely Update will run more than once in between FixedUpdates, meaning that multiple
         * frames of input will be sampled in Update, but executed over a single frame in FixedUpdate.
         * Multiple frames of input must therefore be "flattened" into a single PlayerInput structure
         * for FixedUpdate to query. How you want to do this may vary depending on your game's needs.
         * I use an approach where "action" overrides "inaction", but not vice versa: i.e., if a player
         * presses down the fire key in one frame but does not press it in the next, the initial press
         * is still maintained into the FixedUpdate
         */

		if (Input.GetKey(up))
            input.verticalMove = 1;
        else if (Input.GetKey(down))
            input.verticalMove = -1;

        if (Input.GetKey(right))
            input.horizontalMove = 1;
        else if (Input.GetKey(left))
            input.horizontalMove = -1;

        if (Input.GetKey(rotatePositive))
            input.rotation = 1;
        else if (Input.GetKey(rotateNegative))
            input.rotation = -1;

        if (Input.GetKeyDown(fire))
            input.fireDown = true;

        target.input = input;
    }
}
