using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //======================================== variables ============================================================
    // Dont Initiat the Value here plz.
    public float HP=5f;
    public GameObject Player;
    public float movementSpeed;
    public float Damage;
    public int isSlowed;
    public float slowDuration;
    public float slowedTime;
    public CanvasManager canvasManager;
    //=============================================================================================================
    // Start is called before the first frame update
    void Start()
    {
        // HP=5f;
        canvasManager=GameObject.Find("Canvas").GetComponent<CanvasManager>();
        Damage=10f;
        //movementSpeed;
        Player=GameObject.Find("Player");
        isSlowed = 0;
        slowDuration = 5;
        slowedTime = 0;
    }

    // Update is called once per frame
    void Update()
    {   
        if(!canvasManager.ifStart){
            return;
        }
        if (isSlowed==1 )
        {
            if(slowedTime < slowDuration)
            {
                movementSpeed = 0.1f;
                slowedTime += Time.deltaTime;
            }
            else
            {
                slowedTime = 0;
                isSlowed = 0;
            }
        }
        else
        {
            movementSpeed = 0.7f;

        }


        if(HP<=0){
            Destroy(gameObject);
        }
        float distance=Vector3.Distance(transform.position,Player.transform.position);
        Debug.Log(distance);
        if(distance<0.2f){
            Player.GetComponent<PlayerControl>().HP-=Damage;

        }

        transform.position=Vector3.MoveTowards(transform.position,Player.transform.position,movementSpeed*Time.deltaTime);
    }
}
