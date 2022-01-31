using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fox : CharacterMovement
{
    List<MoveCommand> commands = new List<MoveCommand>();
    Vector3 lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider>();
        lastPosition = transform.position;
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FillPositionsList()
    {
        tilePositions.Add(lastPosition);
        foreach (var command in commands)
        {
            lastPosition = lastPosition + command.Movement;
            tilePositions.Add(lastPosition);
        }
    }

    protected override void Walk()
    {
        FillPositionsList();
        walkingCoroutine = StartCoroutine("WalkCoroutine");
       // base.Walk();
    }
    protected  IEnumerator WalkCoroutine()
    {
        foreach (var tilePos in tilePositions)
        {
            CheckNextTile(tilePos - transform.position);
            yield return StartCoroutine("MoveToTile", tilePos);
        }
    }

    protected void CheckNextTile(Vector3 direction)
    {
        RaycastHit hit;
        _collider.enabled = false;
        Ray ray = new Ray(transform.position, direction);
        if (Physics.Raycast(ray, out hit, 2.5f))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                EventsManager.PlayerPushing(hit.collider.gameObject, direction);
            }
        }
        _collider.enabled = true;
    }

    void AddForwardCommand()
    {
        commands.Add(new MoveForward());
    }
    void AddRightCommand()
    {
        commands.Add(new MoveRight());
    }
    void AddLeftCommand()
    {
        commands.Add(new MoveLeft());
    }

    protected override void OnReset()
    {
        commands.Clear();
        tilePositions.Clear();
        lastPosition = startPosition;
        base.OnReset();
    }

    private void OnEnable()
    {
        EventsManager.OnForwardArrowTap += AddForwardCommand;
        EventsManager.OnRightArrowTap += AddRightCommand;
        EventsManager.OnLeftArrowTap += AddLeftCommand;
        EventsManager.OnRunButton += Walk;
        EventsManager.OnResetButton += OnReset;

    }
    private void OnDisable()
    {
        EventsManager.OnForwardArrowTap -= AddForwardCommand;
        EventsManager.OnRightArrowTap -= AddRightCommand;
        EventsManager.OnLeftArrowTap -= AddLeftCommand;
        EventsManager.OnRunButton -= Walk;
        EventsManager.OnResetButton -= OnReset;

    }

    protected override void InteractWithObstacle(GameObject obstacle, Vector3 direction)
    {
        print("InteractingFox");
        EventsManager.PlayerPushing(obstacle, direction);
    }
}
