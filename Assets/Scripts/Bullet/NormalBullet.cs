using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : Bullet
{
    void Start()
    {   
        base.Init();
    }

    void Update()
    {
        TimeCount += Time.deltaTime;
        if(TimeCount > MaintainTime){
            Destroy(gameObject);
        }
        
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, TargetPos, speed * Time.deltaTime);

        base.CheckCollideEnemy();
    }
}


        