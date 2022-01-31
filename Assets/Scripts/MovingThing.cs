using System.Collections;
using UnityEngine;

public abstract class MovingThing : MonoBehaviour
{
    public float timeBetweenTiles;
    protected Vector3 startPosition;
    private void Start()
    {
        startPosition = transform.position;
    }


    protected IEnumerator MoveToTile(Vector3 nextTile)
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
    float EaseInOut(float x)
    {
        if (x < 0.5f)
        {
            x = 16 * x * x * x * x * x;
        }
        else
        {
            float value = -2 * x + 2;
            x = 1 - Mathf.Pow(value, x) / 2;
        }
        return x;
    }

    protected virtual void OnReset()
    {
        transform.position = startPosition;
    }

    private void OnEnable()
    {
        EventsManager.OnResetButton += OnReset;
    }

    private void OnDisable()
    {
        EventsManager.OnResetButton -= OnReset;
    }
}
