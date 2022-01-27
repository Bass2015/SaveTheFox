using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] protected List<BoardTile> tilesPath;
    protected Coroutine highlighting;
    public float timeBetweenTiles;

    List<Vector3> tilePositions;
    // Start is called before the first frame update
    void Start()
    {
        tilePositions = new List<Vector3>();
        if (tilesPath != null && tilesPath.Count > 0)
        {
            foreach (var tile in tilesPath)
            {
                tilePositions.Add(tile.transform.position);
            } 
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    

    private void Walk()
    {
        StopCoroutine(highlighting);
        StartCoroutine("WalkCoroutine");
    }

    IEnumerator WalkCoroutine()
    {
        foreach (var tilePos in tilePositions)
        {
            yield return StartCoroutine("MoveToTile", tilePos);
        }
    }
    private void HighlightPath()
    {
        if (tilesPath != null && tilesPath.Count > 0)
        {
            highlighting = StartCoroutine("HighlightCo"); 
        }
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
    IEnumerator MoveToTile(Vector3 nextTile)
    {
        float percentage = 0;
        float elapsedTime = 0;
        Vector3 newPosition = new Vector3(nextTile.x, transform.position.y, nextTile.z);
        while (percentage < 1)
        {
            transform.position = Vector3.Lerp(transform.position, newPosition, EaseInOut(percentage));
            elapsedTime += Time.deltaTime;
            percentage = elapsedTime / timeBetweenTiles;
            yield return null;
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

    float EaseInOut(float x)
    {
        if(x < 0.5f)
        {
            x = 16 * x * x * x * x * x;
        }
        else
        {
            double value = -2 * (double)x + 2;
            x = 1 - (float)Math.Pow(value, (double)x) / 2;
        }
        return x;
    }
}
