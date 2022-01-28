using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveRight : MoveCommand
{
    public MoveRight() : base()
    {

    }
    protected override void InitMovement()
    {
        this.movement = Vector3.right * movementDistance;
    }
}
