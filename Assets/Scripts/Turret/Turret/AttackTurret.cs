using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackTurret : Turret
{
    // ============================== variables ==============================
    [Tooltip("shooting at targetEnemy if exists")]
    public GameObject targetEnemy;
    public GameObject bulletPrefab;
    protected Vector3 bulletOffset;
    [Tooltip("basic shooting cooldown")]
    public float basicShootPeriod;
    private float shootTimer;
    public float shootRange;
    public float bulletSpeed;
    [Tooltip("timer used for count down buff time")]
    public float bulletBuffTimer;

    // ============================== general methods ==============================
    // general initialization, call this function first in Start() then modify varying variables
    protected override void Init()
    {
        base.Init();

        // varying initialization
        //bulletOffset = new Vector3(0.5f, 1.0f, 0.0f);
        bulletOffset = new Vector3(0f, 1.0f, 0.0f);
        basicShootPeriod = 1.0f;
        shootRange = 5.0f;
        bulletSpeed = 10.0f;

        // fixed initialization
        targetEnemy = null;
        shootTimer = 0f;
        bulletBuffTimer = 0.0f;
    }
    
    protected virtual void TargetEnemy(){   
        Physics2D.OverlapCircle(transform.position, shootRange, filter, results);
        foreach( Collider2D result in results)
        {
            if(result.gameObject.TryGetComponent<Enemy>(out Enemy enemy)){
                if(!targetEnemy){
                    targetEnemy = result.gameObject;
                    continue;
                }
                float dis1 = Vector3.Distance(transform.position,targetEnemy.transform.position);
                float dis2 = Vector3.Distance(transform.position,result.gameObject.transform.position);
                if(dis2 < dis1){
                    targetEnemy = result.gameObject;
                }
            }
        }
    }

    protected virtual void ShootEnemy()
    {
        // shoot every period of time
        shootTimer += Time.deltaTime;
        if(shootTimer > basicShootPeriod / (1.0f + bulletBuffTimer))
        {
            shootTimer = 0.0f;

            GameObject obj = Instantiate(bulletPrefab, transform.position + bulletOffset, Quaternion.identity, GameObject.Find("/Bullets").transform);
            Bullet bulletComponent = obj.GetComponent<Bullet>();
            Vector3 direction = (targetEnemy.transform.position - transform.position + new Vector3(0.0f, -1.0f, 0.0f));
            bulletComponent.TargetPos = transform.position + direction.normalized * 1000.0f;
            bulletComponent.speed = bulletSpeed;
        }

        Vector3 currEnemyPos = targetEnemy.transform.position - transform.position;
        float enemyDist = currEnemyPos.magnitude;
        if (enemyDist > shootRange){
            targetEnemy = null;
        }
    }
}
