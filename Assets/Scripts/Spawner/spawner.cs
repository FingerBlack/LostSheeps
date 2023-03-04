using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject spawnObject;
    public float spawnRate;
    public int totalNumber;
    private float spawnTimer;
    public CanvasManager canvasManager;
    private Grid floorGrid;
    private GameObject enemies;
    // Start is called before the first frame update
    void Start()
    {   
        floorGrid = GameObject.Find("Grid").GetComponent<Grid>();
        transform.position = floorGrid.GetCellCenterWorld(floorGrid.WorldToCell(transform.position));
        canvasManager=GameObject.Find("Canvas").GetComponent<CanvasManager>();
        spawnTimer = 0;
        enemies=GameObject.Find("/Enemies");
    }

    // Update is called once per frame
    void Update()
    {   
        if(!canvasManager.ifStart){
            return;
        }
        if (spawnObject != null)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnRate)
            {
                spawnTimer = 0;
                if(enemies.transform.childCount<=totalNumber){

                    Instantiate(spawnObject, transform.position, Quaternion.identity, enemies.transform);
                }
            }

        }
    }
}
