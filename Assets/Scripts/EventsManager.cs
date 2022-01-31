using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventsManager : MonoBehaviour
{
    public delegate void GameEvent();
    public static event GameEvent OnGameStarted = delegate { };
    public static event GameEvent OnRunButton = delegate { };
    public static event GameEvent OnResetButton = delegate { };
    public static event GameEvent OnForwardArrowTap = delegate { };
    public static event GameEvent OnRightArrowTap = delegate { };
    public static event GameEvent OnLeftArrowTap = delegate { };

    public delegate void PlayerPushingEvent(GameObject obst, Vector3 direction);
    public static event PlayerPushingEvent OnPlayerPushing = delegate { };
    
 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            OnGameStarted();
    }

    public void RunButtonClicked()
    {
        OnRunButton();
    }
    public void ResetButtonClicked()
    {
        OnResetButton();
    }
    public void ForwardClicked()
    {
        OnForwardArrowTap();
    }
    public void LeftClicked()
    {
        OnLeftArrowTap();
    }
    public void RightClicked()
    {
        OnRightArrowTap();
    }

    public static void PlayerPushing(GameObject obstacle, Vector3 direction)
    {
        print("Push Event");
        OnPlayerPushing(obstacle, direction);
    }
}
