using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventsManager : MonoBehaviour
{
    public delegate void GameEvent();
    public static event GameEvent OnGameStarted = delegate { };
    public static event GameEvent OnRunButton = delegate { };
    public static event GameEvent OnForwardArrowTap = delegate { };
    public static event GameEvent OnRightArrowTap = delegate { };
    public static event GameEvent OnLeftArrowTap = delegate { };
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            OnGameStarted();
    }

    public void ButtonClicked()
    {
        OnRunButton();
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
}
