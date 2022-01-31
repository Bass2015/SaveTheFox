using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandsUI : MonoBehaviour
{
    [SerializeField]
    GameObject arrowImage;

    Vector3 currentSpawnPos;

    Stack<GameObject> activeArrows = new Stack<GameObject>();
    Stack<GameObject> inactiveArrows = new Stack<GameObject>();
    
    private void Start()
    {
        currentSpawnPos = transform.position;
    }
    enum Direction
    {
        Forward, Left, Right
    }
    void SpawnArrow(Quaternion rot)
    {
        GameObject newArrow;
        if(inactiveArrows.Count == 0)
        {
            newArrow = GameObject.Instantiate(arrowImage, this.transform);
        }
        else
        {
            newArrow = inactiveArrows.Pop();
            newArrow.SetActive(true);
        }
        activeArrows.Push(newArrow);
        newArrow.transform.position = currentSpawnPos;
        newArrow.transform.rotation = rot;
        currentSpawnPos += Vector3.right * 50;
    }

    void NewForwardCommand()
    {
        SpawnArrow(Quaternion.identity);
    }
    void NewLeftCommand()
    {
        Vector3 rot = new Vector3(0, 0, 90);
        SpawnArrow(Quaternion.Euler(rot));
    }
    void NewRightCommand()
    {
        Vector3 rot = new Vector3(0, 0, -90);
        SpawnArrow(Quaternion.Euler(rot));
    }
    void OnReset()
    {
        for (int i = 0; i < activeArrows.Count + 1; i++)
        {
            GameObject arrow = activeArrows.Pop();
            arrow.SetActive(false);
            inactiveArrows.Push(arrow);
        }
               
        currentSpawnPos = transform.position;
    }

    private void OnEnable()
    {
        EventsManager.OnForwardArrowTap += NewForwardCommand;
        EventsManager.OnRightArrowTap += NewRightCommand;
        EventsManager.OnLeftArrowTap += NewLeftCommand;
        EventsManager.OnResetButton += OnReset;

    }
    private void OnDisable()
    {
        EventsManager.OnForwardArrowTap -= NewForwardCommand;
        EventsManager.OnRightArrowTap -= NewRightCommand;
        EventsManager.OnLeftArrowTap -= NewLeftCommand;
        EventsManager.OnResetButton -= OnReset;

    }

}
