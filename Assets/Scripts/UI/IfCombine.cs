using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfCombine : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject radar;
    private PlantCherry radarComponent;
    void Start()
    {
        radarComponent=radar.GetComponent<PlantCherry>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(radarComponent.gridPosition==new Vector3Int(3,-2,0)){
            Destroy(gameObject);
        }
    }
}
