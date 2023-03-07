using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedSpawner : MonoBehaviour
{
    public float timeCount;
    public float timePeirod;
    public GameObject gears;
    public GameObject gear;
    public GameObject player;
    public PlayerControl playerControl;
    public float range;
    public float rangeSize;
    public CanvasManager canvasManager;
    void Start()
    {   
        canvasManager=GameObject.Find("Canvas").GetComponent<CanvasManager>();
        gears=GameObject.Find("Gears");
        playerControl=GameObject.Find("Player").GetComponent<PlayerControl>();
        timeCount=0;
        timePeirod=1f;
        range=3.5f;
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
            if(playerControl.seedNumber>10){
                return;
            } 
            float radius=Random.Range(0,range);
            float angle=Random.Range(0,2*Mathf.PI); 
            float x = radius* Mathf.Cos( angle);
            float y = radius * Mathf.Sin( angle);

            Instantiate(gear,transform.position+new Vector3(x,y,0f), Quaternion.identity,gears.transform);
            
            
        }
    }
}
