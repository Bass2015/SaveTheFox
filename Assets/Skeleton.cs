using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : CharacterMovement
{

    // Start is called before the first frame update
    void Start()
    {
        
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
        EventsManager.OnGameStarted += HighlightPath;
    }
    private void OnDisable()
    {
        EventsManager.OnGameStarted -= HighlightPath;

    }
}
