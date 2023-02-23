using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedFloor : MonoBehaviour
{
    // Start is called before the first frame update
    private Grid floorGrid;
    void Start()
    {
        floorGrid = GameObject.Find("Grid").GetComponent<Grid>();
        transform.position = floorGrid.GetCellCenterWorld(floorGrid.WorldToCell(transform.position));
    }


}
