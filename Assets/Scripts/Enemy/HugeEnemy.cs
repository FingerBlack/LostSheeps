using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HugeEnemy : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        base.Init();

        // properties
        healthPoint = 50.0f;
        attackDamage = 40.0f;
        attackSpeed = 1.0f;
        attackRange = 0.4f;
        normalSpeed = 1.0f;
        slowedSpeed = 0.2f;
        slowDuration = 5.0f;
        currentSpeed = normalSpeed;
    }

    // Update is called once per frame
    void Update()
    {   
        // check if game start
        if(!canvasManager.ifStart)
        {
            return;
        }

        // check if alive
        if(healthPoint <= 0)
        {
            Destroy(gameObject);
        }

        // check slowed 
        if(isSlowed)
        {
            base.CheckSlowed();
        }

        base.TryAttackPlayer();

        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, currentSpeed * Time.deltaTime);
    }
}
