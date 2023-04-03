using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfPressSpace : MonoBehaviour
{
    // Start is called before the first frame update
    private Grid floorGrid;    
    public GameObject box;
    private Box boxComponent;
    private Vector3Int position;
    public GameObject buttonFloorPrompt;

    void Start()
    {
        buttonFloorPrompt.SetActive(false);
        floorGrid =GameObject.Find("Grid").GetComponent<Grid>();
        boxComponent=box.GetComponent<Box>();
        position=floorGrid.WorldToCell(boxComponent.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(floorGrid.WorldToCell(boxComponent.transform.position)!=position){
            buttonFloorPrompt.SetActive(true);
            Destroy(gameObject);
        }
    }
}
