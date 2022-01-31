using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MovingThing
{
    protected abstract void Move(Vector3 direction);

    protected void OnPlayerPushingEvent(GameObject obs, Vector3 direction)
    {
        print("player is pushing");
        if (obs.Equals(this.gameObject))
        {
            Move(direction);
        }
    }
    private void OnEnable()
    {
        EventsManager.OnPlayerPushing += OnPlayerPushingEvent;
        EventsManager.OnResetButton += OnReset;

    }
    protected override void OnReset()
    {
        base.OnReset();
    }
    private void OnDisable()
    {
        EventsManager.OnPlayerPushing -= OnPlayerPushingEvent;
        EventsManager.OnResetButton -= OnReset;
    }
}
