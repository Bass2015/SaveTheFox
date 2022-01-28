using UnityEngine;

public abstract class MoveCommand 
{
    protected float movementDistance = 2.5f;

    protected Vector3 movement;

    public MoveCommand()
    {
        InitMovement();
    }

    public Vector3 Movement { get => movement;}

    protected abstract void InitMovement();
       
}
