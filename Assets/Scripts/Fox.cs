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
        lastPosition = transform.position;
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
        base.Walk();
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

    private void OnEnable()
    {
        EventsManager.OnForwardArrowTap += AddForwardCommand;
        EventsManager.OnRightArrowTap += AddRightCommand;
        EventsManager.OnLeftArrowTap += AddLeftCommand;
        EventsManager.OnRunButton += Walk;
    }
    private void OnDisable()
    {
        EventsManager.OnForwardArrowTap -= AddForwardCommand;
        EventsManager.OnRightArrowTap -= AddRightCommand;
        EventsManager.OnLeftArrowTap -= AddLeftCommand;
        EventsManager.OnRunButton -= Walk;
    }
}
