using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MoveCommand
{
    public MoveLeft() : base()
    {

    }
    protected override void InitMovement()
    {
        this.movement = Vector3.left * movementDistance;
    }
}
