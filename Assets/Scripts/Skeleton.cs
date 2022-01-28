using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : CharacterMovement
{
    [SerializeField] private List<BoardTile> tilesPath;
    Coroutine highlighting;

    // Start is called before the first frame update
    void Start()
    {
        
        if (tilesPath != null && tilesPath.Count > 0)
        {
            foreach (var tile in tilesPath)
            {
                tilePositions.Add(tile.transform.position);
            }
        }
    }

    protected override void Walk()
    {
        if (highlighting != null)
        {
            StopCoroutine(highlighting); 
        }
        base.Walk();

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void HighlightPath()
    {
        highlighting = StartCoroutine("HighlightCo");
    }

    IEnumerator HighlightCo()
    {
        while (true)
        {
            foreach (var tile in tilesPath)
            {
                tile.Highlight();
                yield return new WaitForSeconds(0.25f);
            }
            foreach (var tile in tilesPath)
            {
                tile.Highlight();
            }
            yield return new WaitForSeconds(0.25f);
        }
    }

    private void OnEnable()
    {
        EventsManager.OnRunButton += Walk;
        EventsManager.OnGameStarted += HighlightPath;
    }
    private void OnDisable()
    {
        EventsManager.OnRunButton -= Walk;
        EventsManager.OnGameStarted -= HighlightPath;
    }
}
