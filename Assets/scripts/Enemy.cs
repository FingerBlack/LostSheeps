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

    //=============================================================================================================
    // Start is called before the first frame update
    void Start()
    {
        HP=100f;
        Damage=10f;
        movementSpeed=0.5f;
        Player=GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        movementSpeed=0.5f;

        if(HP<0){
            Destroy(gameObject);
        }
        float distance=Vector3.Distance(transform.position,Player.transform.position);
        if(distance<0.25f){
            Player.GetComponent<PlayerControl>().HP-=Damage;

        }

        transform.position=Vector3.MoveTowards(transform.position,Player.transform.position,movementSpeed*Time.deltaTime);
    }
}
