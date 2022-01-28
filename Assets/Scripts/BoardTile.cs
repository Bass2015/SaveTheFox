using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTile : MonoBehaviour
{
    Material defaultDirtMaterial;
    Material defaultGrassMaterial;
    public Material[] hlMats;
    MeshRenderer[] meshRends = new MeshRenderer[2];

    bool highlighted;
    
    // Start is called before the first frame update
    void Start()
    {
        meshRends[0] = gameObject.GetComponent<MeshRenderer>();
        meshRends[1] = transform.GetChild(0).GetComponent<MeshRenderer>();
        defaultDirtMaterial = meshRends[0].material;
        defaultGrassMaterial = meshRends[1].material;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
           // Highlight();
    }

    public void Highlight()
    {
        if (!highlighted)
        {
            for (int i = 0; i < meshRends.Length; i++)
            {
                meshRends[i].material = hlMats[i];
            }
            highlighted = true;
        }
        else
        {
            TurnOffHighlight();
        }
    }

    void TurnOffHighlight()
    {
        if (highlighted)
        {
            meshRends[0].material = defaultDirtMaterial;
            meshRends[1].material = defaultGrassMaterial;
            highlighted = false;
        }
    }

    private void OnEnable()
    {
        EventsManager.OnRunButton += TurnOffHighlight;
    }

    private void OnDisable()
    {
        EventsManager.OnRunButton -= TurnOffHighlight;
    }
}
