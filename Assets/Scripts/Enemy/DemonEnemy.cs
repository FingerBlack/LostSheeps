using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonEnemy : Enemy
{
    private float timeSinceLastSet;
    public bool debug;
    [SerializeField] private float wanderInterval = 0.0f;
    void Start()
    {
        base.Init();

        // properties
        timeSinceLastSet=setDestinationInterval;
        healthPoint = maxHealthPoint;
        attackDamage = 40.0f;
        attackSpeed = 1.0f;
        attackRange = 0.4f;
        normalSpeed = 2.5f;
        slowedSpeed = normalSpeed * 0.3f;
        slowDuration = 5.0f;
        currentSpeed = normalSpeed;
        chasingRange = 5.0f;
        enemyAgent.SetDestination(transform.position);
    }

    // Update is called once per frame
    private float setDestinationInterval = 0.1f;




    // Update is called once per frame
    void Update()
    {
        wanderInterval -= Time.deltaTime;
        timeSinceLastSet += Time.deltaTime;

        if (timeSinceLastSet >= setDestinationInterval)
        {
            timeSinceLastSet = 0.0f;
            float distanceFromPlayer = (transform.position - player.transform.position).magnitude;

            if (distanceFromPlayer < chasingRange && HasValidPathToDestination(enemyAgent, player.transform.position))
            {
                wanderInterval = 3.0f;
                isWandering = false;
                enemyAgent.SetDestination(player.transform.position);
            }
            else
            {
                if (distanceFromPlayer >= chasingRange && !isWandering && !HasValidPathToDestination(enemyAgent, player.transform.position))
                {
                    if (wanderInterval <= 0.0f)
                    {
                        wanderInterval = 3.0f;
                        base.wanderAround(3.0f);
                        //enemyAgent.isStopped = false;
                    }
                    enemyAgent.isStopped = false;
                }
                else if (distanceFromPlayer >= chasingRange && isWandering)
                {
                    if (wanderInterval <= 0.0f)
                    {
                        wanderInterval = 3.0f;
                        base.wanderAround(3.0f);
                        
                    }
                    enemyAgent.isStopped = false;
                }
                else
                {
                    if (HasValidPathToDestination(enemyAgent, player.transform.position))
                    {
                        enemyAgent.SetDestination(player.transform.position);
                        enemyAgent.isStopped = false;
                    }
                    else
                    {
                        isWandering = true;

                        if (wanderInterval <= 0.0f)
                        {
                            wanderInterval = 3.0f;
                            base.wanderAround(3.0f);
                            
                        }
                        enemyAgent.isStopped = false;
                    }
                }
            }
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
    bool HasValidPathToDestination(UnityEngine.AI.NavMeshAgent navMeshAgent,Vector3 destination)
    {
        UnityEngine.AI.NavMeshPath path = new UnityEngine.AI.NavMeshPath();
        bool hasPath = navMeshAgent.CalculatePath(destination, path);

        if (!hasPath || path.status == UnityEngine.AI.NavMeshPathStatus.PathPartial)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}


