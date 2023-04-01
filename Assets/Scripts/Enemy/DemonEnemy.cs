using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonEnemy : Enemy
{
    // Start is called before the first frame update
    private UnityEngine.AI.NavMeshAgent agent;
    void Start()
    {
        base.Init();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false; // 禁用旋转
        agent.updateUpAxis=false;
        // properties
        healthPoint = maxHealthPoint;
        attackDamage = 40.0f;
        attackSpeed = 1.0f;
        attackRange = 0.4f;
        normalSpeed = 2.5f;
        slowedSpeed = normalSpeed * 0.3f;
        slowDuration = 5.0f;
        currentSpeed = normalSpeed;
    }

    // Update is called once per frame
    private float setDestinationInterval = 0.1f;
    private float timeSinceLastSet;

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSet += Time.deltaTime;
        if (timeSinceLastSet >= setDestinationInterval)
        {
            agent.SetDestination(player.transform.position);
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


