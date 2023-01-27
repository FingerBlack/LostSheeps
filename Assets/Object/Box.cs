using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public Grid floorGrid;
    Vector3Int cellGridPosition;
    Vector3 cellCenterWorldPosition;
    Vector3 cellWorldPosition;
    BoxCollider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Box Initiate");

        collider = gameObject.GetComponent<BoxCollider2D>();
        Debug.Log(collider.transform.position);
        Debug.Log(transform.position);

        cellGridPosition = floorGrid.WorldToCell(transform.position);
        // Debug.Log(cellGridPosition);
        cellWorldPosition = floorGrid.CellToLocal(cellGridPosition);
        cellCenterWorldPosition = floorGrid.GetCellCenterWorld(cellGridPosition);
        Debug.Log(cellWorldPosition);
        Debug.Log(cellCenterWorldPosition);
        transform.position = cellCenterWorldPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
