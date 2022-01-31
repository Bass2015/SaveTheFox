using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : CharacterMovement
{
    [SerializeField] private List<BoardTile> tilesPath;
    Coroutine highlighting;
    bool hasReset;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider>();
        if (tilesPath != null && tilesPath.Count > 0)
        {
            foreach (var tile in tilesPath)
            {
                tilePositions.Add(tile.transform.position);
            }
        }
        startPosition = transform.position;

    }

    protected override void Walk()
    {
        if (highlighting != null)
        {
            StopCoroutine(highlighting); 
        }
        walkingCoroutine = StartCoroutine("WalkCoroutine");
       // base.Walk();

    }
    // Update is called once per frame
    void Update()
    {
        
    }
    protected  IEnumerator WalkCoroutine()
    {
        foreach (var tilePos in tilePositions)
        {
            if (CheckNextTile(tilePos - transform.position))
            {
                break;
            }
            yield return StartCoroutine("MoveToTile", tilePos);
        }
    }
    protected bool CheckNextTile(Vector3 direction)
    {
        RaycastHit hit;
        _collider.enabled = false;
        direction = new Vector3(direction.x, 1, direction.z);
        Ray ray = new Ray(transform.position, direction);
        if (Physics.Raycast(ray, out hit, 2.5f))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                InteractWithObstacle(hit.collider.gameObject, direction);
                return true;
            }
        }
        _collider.enabled = true;
        return false;
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
        EventsManager.OnResetButton += OnReset;

    }
    private void OnDisable()
    {
        EventsManager.OnRunButton -= Walk;
        EventsManager.OnGameStarted -= HighlightPath;
        EventsManager.OnResetButton -= OnReset;

    }

    protected override void OnReset()
    {
        StopAllCoroutines();
        base.OnReset();
    }

    protected override void InteractWithObstacle(GameObject obstacle, Vector3 direction)
    {
        StopCoroutine(walkingCoroutine);
    }

    protected override void InitVariables()
    {
        base.InitVariables();
    }
}
