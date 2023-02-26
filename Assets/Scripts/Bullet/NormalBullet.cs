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
        timeCount += Time.deltaTime;
        if(timeCount > maintainTime){
            Destroy(gameObject);
        }
        
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPos, speed * Time.deltaTime);

        base.CheckCollideEnemy();
    }
}


        