using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //======================================== variables ============================================================
    // Dont Initiat the Value here plz.
    public float HP;
    public GameObject Player;
    public float movementSpeed;
    public float Damage;
    public int isSlowed;
    public float slowDuration;
    public float slowedTime;

    //=============================================================================================================
    // Start is called before the first frame update
    void Start()
    {
        HP=5f;
        Damage=10f;
        movementSpeed=0.5f;
        Player=GameObject.Find("Player");
        isSlowed = 0;
        slowDuration = 5;
        slowedTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSlowed==1 )
        {
            if(slowedTime < slowDuration)
            {
                movementSpeed = 0.3f;
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
        if(distance<0.25f){
            Player.GetComponent<PlayerControl>().HP-=Damage;

        }

        transform.position=Vector3.MoveTowards(transform.position,Player.transform.position,movementSpeed*Time.deltaTime);
    }
}
