using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    // ============================== variables ==============================
    public float healthPoint;
    [SerializeField] protected float attackDamage;
    [Tooltip("enemy attacks every attackSpeed seconds")]
    [SerializeField] protected float attackSpeed;
    [Tooltip("timer used for checking if enemy could attack")]
    [SerializeField] protected float attackCoolDown;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float currentSpeed;
    [SerializeField] protected float normalSpeed;
    [SerializeField] protected float slowedSpeed;
    public bool isSlowed;
    [Tooltip("slow effect period")]
    [SerializeField] protected float slowDuration;
    [Tooltip("timer used for check slowed status")]
    public float slowedTime;
    [SerializeField] protected GameObject player;
    [SerializeField] protected CanvasManager canvasManager;
    
    // ============================== general methods ==============================
    // general initialization, call this function first in Start() then modify varying variables
    protected virtual void Init()
    {
        // varying inititalization (should be replaced)
        healthPoint = 5.0f;
        attackDamage = 40.0f;
        attackSpeed = 1.0f;
        attackRange = 0.4f;
        normalSpeed = 2.0f;
        slowedSpeed = 0.2f;
        slowDuration = 5.0f;

        // fixed initialization
        attackCoolDown = 0.0f;
        currentSpeed = 0.0f;
        isSlowed = false;
        slowedTime = 0.0f;

        player=GameObject.Find("Player");
        canvasManager=GameObject.Find("Canvas").GetComponent<CanvasManager>();
    }

    // in Update(), if this enemy is slowed, call this method
    protected virtual void CheckSlowed()
    {
        if(slowedTime < slowDuration){
            currentSpeed = slowedSpeed;
            slowedTime += Time.deltaTime;
        }
        else{
            slowedTime = 0;
            isSlowed = false;
            currentSpeed = normalSpeed;
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
            player.GetComponent<PlayerControl>().HP -= attackDamage;
        
            player.GetComponent<PlayerControl>().attackedBy = GetType().Name;

            attackCoolDown = attackSpeed;
        }
    }
}
