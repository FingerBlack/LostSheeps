using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedSpawner : MonoBehaviour
{
    public float timeCount;
    public float timePeirod;
    public GameObject peaSeeds;
    public GameObject peaSeed;
    public GameObject cherrySeeds;
    public GameObject cherrySeed;
    public GameObject player;
    public float range;
    public float rangeSize;
    public CanvasManager canvasManager;
    void Start()
    {   
        canvasManager=GameObject.Find("Canvas").GetComponent<CanvasManager>();
        player=GameObject.Find("Player");
        timeCount=0;
        timePeirod=1f;
        peaSeeds=GameObject.Find("PeaSeeds");
        cherrySeeds=GameObject.Find("CherrySeeds");
        peaSeed=Resources.Load("Prefabs/PeaSeed") as GameObject;
        cherrySeed=Resources.Load("Prefabs/CherrySeed") as GameObject;
        range=2f;
        rangeSize=2f;
    }

    // Update is called once per frame
    void Update()
    {   
        if(!canvasManager.ifStart){
            return;
        }
        timeCount+=Time.deltaTime;
        
        if(timeCount>timePeirod){
            timeCount=0;
            if(peaSeeds.transform.childCount>10){
                return;
            } 
            float radius=Random.Range(range,range+rangeSize);
            float angle=Random.Range(0,2*Mathf.PI); 
            float x = radius* Mathf.Cos( angle);
            float y = radius * Mathf.Sin( angle);

            Instantiate(peaSeed,player.transform.position+new Vector3(x,y,0f), Quaternion.identity,peaSeeds.transform);
            if(cherrySeeds.transform.childCount>10){
                return;
            } 
            radius=Random.Range(range,range+rangeSize);
            angle=Random.Range(0,2*Mathf.PI); 
            x = radius* Mathf.Cos( angle);
            y = radius * Mathf.Sin( angle);

            Instantiate(cherrySeed,player.transform.position+new Vector3(x,y,0f), Quaternion.identity,cherrySeeds.transform);
            
        }
    }
}
