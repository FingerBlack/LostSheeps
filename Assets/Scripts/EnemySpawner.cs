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
    void Start()
    {   
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
        timeCount+=Time.deltaTime;
        
        if(timeCount>timePeirod){
            timeCount=0;
            if(enemies.transform.childCount>50){
                return;
            }
            float radius=Random.Range(range,range+rangeSize);
            float angle=Random.Range(0,2*Mathf.PI); 
            float x = radius* Mathf.Cos( angle);
            float y = radius * Mathf.Sin( angle);

            Instantiate(enemy,player.transform.position+new Vector3(x,y,0f), Quaternion.identity,enemies.transform);
            
        }
    }
}
