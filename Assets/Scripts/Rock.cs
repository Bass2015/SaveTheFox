using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Obstacle
{
    private void Start()
    {
        startPosition = transform.position;
    }
    protected override void Move(Vector3 direction)
    {
        StartCoroutine("MoveToTile", transform.position + direction);
    }
    protected override void OnReset()
    {
        base.OnReset();
    }
       
}
