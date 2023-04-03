using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonEnemy : Enemy
{
    
    void Start()
    {
        base.Init();

        // properties
        healthPoint = maxHealthPoint;
        attackDamage = 40.0f;
        attackSpeed = 1.0f;
        attackRange = 0.4f;
        normalSpeed = 2.5f;
        slowedSpeed = normalSpeed * 0.3f;
        slowDuration = 5.0f;
        currentSpeed = normalSpeed;
        chasingRange = 5.0f;
    }

    // Update is called once per frame
    private float setDestinationInterval = 0.1f;
    private float timeSinceLastSet;

    [SerializeField] private float wanderInterval = 3.0f;

    // Update is called once per frame
    void Update()
    {
        wanderInterval -= Time.deltaTime;
        timeSinceLastSet += Time.deltaTime;
        if (timeSinceLastSet >= setDestinationInterval)
        {
            if(!enemyAgent.hasPath){
                enemyAgent.ResetPath();
            }

            if((transform.position - player.transform.position).magnitude < chasingRange){
                wanderInterval = 3.0f;
                isWandering = false;
                enemyAgent.SetDestination(player.transform.position);
            }
            else{
                if(!isWandering){
                    isWandering = true;
                    base.wanderAround();
                }
                else{
                    if(wanderInterval <= 0.0f){
                        wanderInterval = 3.0f;
                        base.wanderAround();
                    }
                }
            }

            timeSinceLastSet = 0.0f;
        }

        // check if game start
        if(!canvasManager.ifStart)
        {
            return;
        }

        hpImage.fillAmount = healthPoint / maxHealthPoint;

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
        //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, currentSpeed * Time.deltaTime);
    }
}


