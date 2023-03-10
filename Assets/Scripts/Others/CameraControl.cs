
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
   

public class CameraControl : MonoBehaviour
{
    public GameObject player;
    
    public float speed=10f;
     public CanvasManager canvasManager;
    // Start is called before the first frame update
    void Start()
    {
        canvasManager=GameObject.Find("Canvas").GetComponent<CanvasManager>();
        player=GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {   
        if(!canvasManager.ifStart){
            return;
        }
        Vector3 camOffset = new Vector3(0,0,-10);
        Vector3 playerOffset=new Vector3(player.transform.position.x,player.transform.position.y,-10f);
        transform.position =Vector3.MoveTowards(transform.position,playerOffset,speed*Time.deltaTime) ;
    }
    
}