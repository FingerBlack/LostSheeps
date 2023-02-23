using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update
    //public Vector3 Localposition;
    public float timeCount;
    public float timePeirod;
    public GameObject enemies;
    public GameObject enemy;
    public GameObject player;
    public float range;
    public float rangeSize;
    private int level;
    private CanvasManager canvasManager;
    private List<float> hpSpeedLevel=new List<float>{ 5f, 10f, 15f,20f,25f,30f};
    private List<float> enemySpeedLevel=new List<float>{ 0.5f, 1f, 1.5f,2f,2.5f,5f};
    private List<float> spawnerSpeedLevel=new List<float>{ 4f, 3f, 2f,1f,0.5f};
    void Start()
    {   
        canvasManager=GameObject.Find("Canvas").GetComponent<CanvasManager>();
        level=0;
        player=GameObject.Find("Player");
        timeCount=0;
        timePeirod=4f;
        enemies=GameObject.Find("Enemies");
        enemy=Resources.Load("Prefabs/Enemy") as GameObject;
        range=10f;
        rangeSize=5f;
    }

    // Update is called once per frame
    void Update()
    {   
        if(!canvasManager.ifStart){
            return;
        }
        level=(int)(canvasManager.timeCount/60f);
        timeCount+=Time.deltaTime;
        timePeirod=spawnerSpeedLevel[level];
        float enemySpeed=enemySpeedLevel[level];
        if(timeCount>timePeirod){
            timeCount=0;
            if(enemies.transform.childCount>50){
                return;
            }
            float radius=Random.Range(range,range+rangeSize);
            float angle=Random.Range(0,2*Mathf.PI); 
            float x = radius* Mathf.Cos( angle);
            float y = radius * Mathf.Sin( angle);

            Instantiate(enemy,player.transform.position + new Vector3(x,y,0f), Quaternion.identity,enemies.transform);
            //enemy.GetComponent<Enemy>().currentSpeed = enemySpeed;
            enemy.GetComponent<Enemy>().healthPoint = hpSpeedLevel[level];
        }
    }
}
