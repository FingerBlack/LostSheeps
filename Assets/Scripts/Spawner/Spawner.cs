using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{   
    
    public GameObject spawnObject;
    public float spawnRate;
    public int totalExistNumber;
    private float spawnTimer;
    public CanvasManager canvasManager;
    private Grid floorGrid;
    private GameObject enemies;
    public int totalEnemies=10;
    public int currentEnemies;
    public SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {   //totalEnemies=10;

        currentEnemies=0;
        floorGrid = GameObject.Find("Grid").GetComponent<Grid>();
        transform.position = floorGrid.GetCellCenterWorld(floorGrid.WorldToCell(transform.position));
        canvasManager=GameObject.Find("Canvas").GetComponent<CanvasManager>();
        spawnTimer = spawnRate;
        enemies=GameObject.Find("/Enemies");
        sprite=GetComponent<SpriteRenderer>();
    }
 
    // Update is called once per frame
    void Update()
    {   
                //Debug.Log(((float)totalEnemies-(float)currentEnemies)/(float)totalEnemies);
        if(!canvasManager.ifStart){
            return;
        }
        if(totalEnemies!=0)
            sprite.color=new Color(sprite.color.r,sprite.color.g,sprite.color.b,((float)totalEnemies-(float)currentEnemies)/(float)totalEnemies);

        if (spawnObject != null)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnRate)
            {
                spawnTimer = 0;
                if(currentEnemies<totalEnemies&&enemies.transform.childCount<totalExistNumber){
                    currentEnemies++;
                    Instantiate(spawnObject, transform.position, Quaternion.identity, enemies.transform);


                }
            }

        }
    }
}
