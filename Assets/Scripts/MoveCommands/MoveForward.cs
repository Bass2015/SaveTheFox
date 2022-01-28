using UnityEngine;

public class MoveForward : MoveCommand
{
    public MoveForward() : base()
    {
        
    }
    protected override void InitMovement()
    {
        this.movement = Vector3.forward * movementDistance;
    }
}
