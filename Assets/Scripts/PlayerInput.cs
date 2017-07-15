using UnityEngine;

public struct PlayerInput 
{
    public int horizontalMove;
    public int verticalMove;
    public int rotation;
    public bool fireDown;

    public void Clear()
    {
        horizontalMove = verticalMove = rotation = 0;

        fireDown = false;
    }
}
