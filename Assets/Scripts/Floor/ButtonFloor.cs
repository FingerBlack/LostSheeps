using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFloor : MonoBehaviour
{
    private Grid floorGrid;
    private ContactFilter2D filter; // Collider Detect Tools.
    private List<Collider2D> results;// Collider Detect Tools.
    public GameObject wallsList;
    public GameObject spawnerList;
    void Start()
    { 
        filter = new ContactFilter2D().NoFilter(); //initiate the Collider Detect Tools.
        results = new List<Collider2D>(); //initiate the Collider Detect Tools.
        floorGrid = GameObject.Find("Grid").GetComponent<Grid>();
        transform.position = floorGrid.GetCellCenterWorld(floorGrid.WorldToCell(transform.position));
    }

    // Update is called once per frame
    void Update()
    {  
        Physics2D.OverlapCircle(transform.position, 0.1f,filter, results);
        foreach( Collider2D result in results)
        {
            if(result.gameObject.TryGetComponent<Box>(out Box box)){
                wallsList.SetActive(false);
                if(spawnerList){
                    foreach(Transform s in spawnerList.transform){
                        s.gameObject.GetComponent<Spawner>().enabled=true;
                    }
                }

                break;
            }
//            if(result.gameObject.TryGetComponent<PlayerControl>(out PlayerControl playerControl)){
//                wallsList.SetActive(false);
//            }
            // if(result.gameObject.TryGetComponent<Wall>(out Wall wall)){
            //     wall.direction=playerDirection;
            //     wall.action="move";
            // }
        }
    }

    // use IEnumerator to move continuously to target position
}
