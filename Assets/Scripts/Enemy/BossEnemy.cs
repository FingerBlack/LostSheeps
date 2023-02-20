using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    //======================================== variables ============================================================
    // Dont Initiat the Value here plz.
    public float HP=50f;
    public GameObject Player;
    public float movementSpeed;
    public float Damage;
    public int isSlowed;
    public float slowDuration;
    public float slowedTime;
    public CanvasManager canvasManager;
    private float notSlowedSPeed;
    private float slowedSPeed;
    //=============================================================================================================
    // Start is called before the first frame update
    void Start()
    {
        canvasManager=GameObject.Find("Canvas").GetComponent<CanvasManager>();
        Damage=40f;
        //movementSpeed;
        Player=GameObject.Find("Player");
        isSlowed = 0;
        slowDuration = 5;
        slowedTime = 0;
        notSlowedSPeed = 0.5f;
        slowedSPeed = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {   
        // do nothing if game not start yet
        if(!canvasManager.ifStart){
            return;
        }
        // check if being slowed
        if (isSlowed==1 ){
            if(slowedTime < slowDuration)
            {
                movementSpeed = slowedSPeed;
                slowedTime += Time.deltaTime;
            }
            else{
                slowedTime = 0;
                isSlowed = 0;
            }
        }
        else{
            movementSpeed = notSlowedSPeed;
        }

        // destroy when killed
        if(HP<=0){
            Destroy(gameObject);
        }
        
        // attack player
        float distance=Vector3.Distance(transform.position,Player.transform.position);

        if(distance<0.4f){
            Player.GetComponent<PlayerControl>().HP-=Damage;
        }

        // move toward player
        transform.position=Vector3.MoveTowards(transform.position,Player.transform.position,movementSpeed*Time.deltaTime);
    }
}
