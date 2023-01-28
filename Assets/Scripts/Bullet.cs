using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 TargetPos;
    public float speed=1.0f;
    public float MaintainTime=10000.0f;
    public float TimeCount=0.0f;
    void Start()
    {
        TimeCount=0;
        MaintainTime=10f;
    }

    // Update is called once per frame
    void Update()
    {
        TimeCount+=Time.deltaTime;
        if(TimeCount>MaintainTime){
            Destroy(gameObject);
        }
        gameObject.transform.position=Vector3.MoveTowards(gameObject.transform.position,TargetPos,speed*Time.deltaTime);

    }
    void OnTriggerEnter2D(Collider2D col)
    {   
        Debug.Log(col);
        if( col.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {   

            enemy.HP-=10f;
            Destroy(gameObject);
        }
    }
}
