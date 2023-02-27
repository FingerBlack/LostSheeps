using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldEnemy : Enemy
{
    // ============================== variables ==============================
    protected bool hasShield;
    protected float unShieldedHealth;
    [SerializeField] protected Sprite demonEnemySprite;

    // ======================================================================
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

        // shield enemy only
        unShieldedHealth = 5.0f;
        hasShield = true;
    }

    // Update is called once per frame
    void Update()
    {   
        // check if game start
        if(!canvasManager.ifStart){
            return;
        }

        // check if alive
        if(healthPoint <= 0){
            if(hasShield){
                hasShield = false;
                healthPoint += unShieldedHealth;
                GetComponent<SpriteRenderer>().sprite = demonEnemySprite; 
            }
            else{
                Destroy(gameObject);
            }
        }

        // check slowed 
        if(isSlowed){
            base.CheckSlowed();
        }

        base.TryAttackPlayer();

        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, currentSpeed * Time.deltaTime);
    }
}
