using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterMovement : MovingThing 
{
    protected List<Vector3> tilePositions = new List<Vector3>();
    protected Coroutine walkingCoroutine;
    protected Collider _collider;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;

    }

    protected virtual void InitVariables()
    {
        _collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    protected override void OnReset()
    {
        base.OnReset();
    }
    protected virtual void Walk()
    {
   //    walkingCoroutine = StartCoroutine("WalkCoroutine");
    }

    //protected virtual IEnumerator WalkCoroutine()
    //{
    //    foreach (var tilePos in tilePositions)
    //    {
    //        CheckNextTile(transform.position - tilePos);
    //        yield return StartCoroutine("MoveToTile", tilePos);
    //    }
    //}

    //protected void CheckNextTile(Vector3 direction)
    //{
    //    RaycastHit hit;
    //    _collider.enabled = false;
    //    Ray ray = new Ray(transform.position, direction);
    //    if (Physics.Raycast(ray, out hit, 2.5f))
    //    {
    //        if (hit.collider.CompareTag("Obstacle"))
    //            print("ObstacleHit");
    //            InteractWithObstacle(hit.collider.gameObject, direction);
                
    //    }
    //    _collider.enabled = true;
    //}

    protected abstract void InteractWithObstacle(GameObject obstacle, Vector3 direction);

    private void OnEnable()
    {
        EventsManager.OnRunButton += Walk;
        EventsManager.OnResetButton += OnReset;

    }

    private void OnDisable()
    {
        EventsManager.OnRunButton -= Walk;
        EventsManager.OnResetButton -= OnReset;

    }


}
