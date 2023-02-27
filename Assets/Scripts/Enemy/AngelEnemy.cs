using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelEnemy : Enemy
{
    void Start()
    {
        base.Init();

        // properties
        healthPoint = 5.0f;
        attackDamage = 40.0f;
        attackSpeed = 1.0f;
        attackRange = 0.4f;
        normalSpeed = 2.5f;
        slowedSpeed = normalSpeed * 0.3f;
        slowDuration = 5.0f;
        currentSpeed = normalSpeed;
    }

    void Update()
    {   
        // check if game start
        if(!canvasManager.ifStart){
            return;
        }

        // check if alive
        if(healthPoint <= 0){
            Destroy(gameObject);
        }

        // check slowed 
        if(isSlowed){
            base.CheckSlowed();
        }

        base.TryAttackPlayer();

        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, currentSpeed * Time.deltaTime);
    }
}
