using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    //public Vector3 Localposition;
    public float timeCount;
    public float timePeirod;
    public GameObject spawners;
    //public GameObject enemy;
    public GameObject player;
    public float range;
    public float rangeSize;
    private int level;
    private CanvasManager canvasManager;
    private List<int> totalExistEnemiesLevel=new List<int>{ 10, 20, 30,40,50,60};
    private List<int> totalEnemiesLevel=new List<int>{ 3, 4, 5,6,7,8};
    private List<float> rateLevel=new List<float>{ 10f, 5f, 3f,2f,1f};
    private List<float> timeLevel=new List<float>{ 4f, 3f, 2f,1f,1f};

    public GameObject enemySpawner;
    void Start()
    {   
        canvasManager=GameObject.Find("Canvas").GetComponent<CanvasManager>();
        level=0;
        player=GameObject.Find("Player");
        timeCount=0;
        timePeirod=4f;
        spawners=GameObject.Find("Spawners");
        //enemy=Resources.Load("Prefabs/Enemy") as GameObject;
        range=10f;
        rangeSize=5f;
    }

    // Update is called once per frame
    void Update()
    {   
        if(!canvasManager.ifStart){
            return;
        }
        level=(int)(canvasManager.timeCount/30f);
        timeCount+=Time.deltaTime;
        timePeirod=timeLevel[level];
        //float enemySpeed=enemySpeedLevel[level];
        if(timeCount>timePeirod){
            timeCount=0;

            float radius=Random.Range(range,range+rangeSize);
            float angle=Random.Range(0,2*Mathf.PI); 
            float x = radius* Mathf.Cos( angle);
            float y = radius * Mathf.Sin( angle);

            Instantiate(enemySpawner,player.transform.position + new Vector3(x,y,0f), Quaternion.identity,spawners.transform);
            //enemy.GetComponent<Enemy>().currentSpeed = enemySpeed;
            enemySpawner.GetComponent<Spawner>().totalEnemies=totalEnemiesLevel[level];
            enemySpawner.GetComponent<Spawner>().totalExistNumber=totalExistEnemiesLevel[level];
            enemySpawner.GetComponent<Spawner>().spawnRate=rateLevel[level];
        }
    }
}
