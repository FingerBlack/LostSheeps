using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OccupiedFloor : MonoBehaviour
{
    private Grid floorGrid;
    private ContactFilter2D filter; // Collider Detect Tools.
    private List<Collider2D> results;// Collider Detect Tools.
    // public GameObject wallsList;
    public bool isOccupied;
    void Start()
    {   
        isOccupied=false;
        filter = new ContactFilter2D().NoFilter(); //initiate the Collider Detect Tools.
        results = new List<Collider2D>(); //initiate the Collider Detect Tools.
        floorGrid = GameObject.Find("Grid").GetComponent<Grid>();
        transform.position = floorGrid.GetCellCenterWorld(floorGrid.WorldToCell(transform.position));
    }

    // Update is called once per frame
    void Update()
    {   
        isOccupied=false;
        Physics2D.OverlapCircle(transform.position, 0.1f,filter, results);
        foreach( Collider2D result in results)
        {

            if(result.gameObject.TryGetComponent<PlayerControl>(out PlayerControl playerControl)){
                isOccupied=true;
                break;
            }
            // if(result.gameObject.TryGetComponent<Wall>(out Wall wall)){
            //     wall.direction=playerDirection;
            //     wall.action="move";
            // }
        }
    }

    // use IEnumerator to move continuously to target position
}
