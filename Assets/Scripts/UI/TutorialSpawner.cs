using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSpawner : MonoBehaviour
{
    private Grid map; // Grid Map

    private GameObject box; // Grid Map

    public Vector3Int gridPlayerPosition;



    // Start is called before the first frame update
    void Start()
    {
        map=GameObject.Find("Grid").GetComponent<Grid>();
        box=Resources.Load("Prefabs/Box") as GameObject;

        GenerateTutorialBoxes();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateTutorialBoxes(){
        Debug.Log("work");
        
        gridPlayerPosition=(GameObject.Find("Player").GetComponent<PlayerControl>().playerGridPos);

        Vector3 worldPos=map.GetCellCenterWorld(gridPlayerPosition + new Vector3Int(-1, 3, 0));
        Instantiate(box, worldPos, Quaternion.identity, GameObject.Find("Boxes").transform);
        worldPos=map.GetCellCenterWorld(gridPlayerPosition + new Vector3Int(1, 3, 0));
        Instantiate(box, worldPos, Quaternion.identity, GameObject.Find("Boxes").transform);

    }

    void GenerateTutorialEnemies(){

    }

    void GenerateTutorialSeeds(){

    }
}
