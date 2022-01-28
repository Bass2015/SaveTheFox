using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
   
   
    public float timeBetweenTiles;
    protected List<Vector3> tilePositions = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    protected virtual void Walk()
    {
        StartCoroutine("WalkCoroutine");
    }

    IEnumerator WalkCoroutine()
    {
        foreach (var tilePos in tilePositions)
        {
            yield return StartCoroutine("MoveToTile", tilePos);
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
    

    }

    private void OnDisable()
    {
        EventsManager.OnRunButton -= Walk;
       
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
