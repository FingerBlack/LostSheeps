using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public Grid floorGrid;
    Vector3Int cellGridPosition;
    Vector3 cellCenterLocalPosition;
    Vector3 cellLocalPosition;
    Vector3 cellCenterWorldPosition;
    Vector3 cellWorldPosition;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Box Initiate");
        
        // GridLayout gridLayout = transform.parent.GetComponent<GridLayout>();
        // cellGridPosition = floorGrid.LocalToCell(transform.localPosition);
        // Debug.Log(cellGridPosition);
        // cellLocalPosition = floorGrid.CellToLocal(cellGridPosition);
        // cellCenterLocalPosition = floorGrid.GetCellCenterLocal(cellGridPosition);
        // Debug.Log(cellLocalPosition);
        // Debug.Log(cellCenterLocalPosition);
        // Debug.Log(transform.localPosition);
        // transform.localPosition = cellCenterLocalPosition;
        // Debug.Log();

        cellGridPosition = floorGrid.WorldToCell(transform.position);
        Debug.Log(cellGridPosition);
        cellWorldPosition = floorGrid.CellToLocal(cellGridPosition);
        cellCenterWorldPosition = floorGrid.GetCellCenterWorld(cellGridPosition);
        Debug.Log(cellWorldPosition);
        Debug.Log(cellCenterWorldPosition);
        Debug.Log(transform.position);
        transform.position = cellCenterWorldPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
