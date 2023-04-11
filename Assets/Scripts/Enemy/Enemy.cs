using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    // ============================== variables ==============================
    public float maxHealthPoint;
    public float healthPoint;
    public Vector3 localPosition;
    [SerializeField] protected float attackDamage;
    [Tooltip("enemy attacks every attackSpeed seconds")]
    [SerializeField] protected float attackSpeed;
    [Tooltip("timer used for checking if enemy could attack")]
    [SerializeField] protected float attackCoolDown;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float currentSpeed;
    [SerializeField] protected float normalSpeed;
    [SerializeField] protected float slowedSpeed;
    // set frozen speed
    [SerializeField] protected float frozenSpeed;
    public bool isSlowed;
    public bool isFrozen;
    [Tooltip("slow effect period")]
    [SerializeField] protected float slowDuration;
    [Tooltip("timer used for check slowed status")]
    public float slowedTime;
    // frozen time and duration
    [SerializeField] protected float frozenDuration;
    public float frozenTime;
    [SerializeField] protected GameObject player;
    [SerializeField] protected CanvasManager canvasManager;
    public Image hpImage;
    protected NavMeshAgent enemyAgent;
    [SerializeField] protected bool isWandering;
    [SerializeField] protected Vector3 wanderDestination;
    [Tooltip("enemy will start chasing player within this range")]
    [SerializeField] public float chasingRange;

    // ============================== general methods ==============================
    // general initialization, call this function first in Start() then modify varying variables
    protected virtual void Init()
    {
        // varying inititalization (should be replaced)
        //healthPoint = maxHealthPoint;
        attackDamage = 40.0f;
        localPosition=transform.position;
        attackSpeed = 1.0f;
        attackRange = 0.4f;
        normalSpeed = 2.0f;
        slowedSpeed = 0.2f;
        slowDuration = 5.0f;
        // frozen varibales
        frozenSpeed = 0.0f;
        frozenDuration = 1.0f;
        //chasingRange = 5.0f;

        // fixed initialization
        attackCoolDown = 0.0f;
        currentSpeed = 0.2f;
        isSlowed = false;
        slowedTime = 0.0f;

        player=GameObject.Find("Player");
        canvasManager=GameObject.Find("Canvas").GetComponent<CanvasManager>();

        enemyAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        enemyAgent.updateRotation = false; // 禁用旋转
        enemyAgent.updateUpAxis=false;

        isWandering = true;
    }

    // in Update(), if this enemy is slowed, call this method
    protected virtual void CheckSlowed()
    {
        if(slowedTime < slowDuration){

            enemyAgent.speed=slowedSpeed;
            currentSpeed = slowedSpeed;
            slowedTime += Time.deltaTime;
        }
        else{
            slowedTime = 0;
            isSlowed = false;
            enemyAgent.speed = normalSpeed;
        }
    }

    // in Update(), if this enemy is frozen, call this method
    protected virtual void CheckFrozen()
    {
        if(frozenTime < frozenDuration){

            enemyAgent.speed=frozenSpeed;
            currentSpeed = frozenSpeed;
            frozenTime += Time.deltaTime;
        }
        else{
            frozenTime = 0;
            isFrozen = false;
            enemyAgent.speed = normalSpeed;
        }
    }

    // call this method in Update to deal damage to player
    protected virtual void TryAttackPlayer()
    {
        if(attackCoolDown > 0){
            attackCoolDown -= Time.deltaTime;
        }

        float distance = Vector3.Distance(transform.position, player.transform.position);
        if(distance < attackRange && attackCoolDown <= 0){
            // attack
            player.GetComponent<PlayerControl>().currentHp -= attackDamage;
        
            player.GetComponent<PlayerControl>().attackedBy = GetType().Name;

            attackCoolDown = attackSpeed;
        }
    }

    protected virtual void wanderAround(float range){

        wanderDestination = new Vector3(localPosition.x + Random.Range(-range, range), localPosition.y + Random.Range(-range, range),0);
        enemyAgent.SetDestination(wanderDestination);
    }
}
