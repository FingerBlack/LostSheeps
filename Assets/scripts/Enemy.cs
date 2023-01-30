using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public float HP=100f;
    public GameObject Player;
    public float movementSpeed=2.0f;
    void Start()
    {
        HP=100;
        //movementSpeed=2.0f;
        Player=GameObject.Find("/Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position=Vector3.MoveTowards(transform.position,Player.transform.position,movementSpeed*Time.deltaTime);
        if(HP<0){
            Destroy(gameObject);
        }
        float distance=Vector3.Distance(transform.position,Player.transform.position);
        if(distance<0.25f){
            Player.GetComponent<playerControl>().HP-=10;

        }


    }
}
