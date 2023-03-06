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
    public float shootTimer;
    public float shootRange;
    public float bulletSpeed;
    [Tooltip("timer used for count down buff time")]
    public float bulletBuffTimer;
    public CanvasManager canvasManager;
    // ============================== general methods ==============================
    // general initialization, call this function first in Start() then modify varying variables
    protected override void Init()
    {
        base.Init();
        canvasManager=GameObject.Find("Canvas").GetComponent<CanvasManager>();
        // varying initialization
        bulletOffset = new Vector3(0f, 1.0f, 0.0f);
        basicShootPeriod = 1.0f;
        shootRange = 5.0f;
        bulletSpeed = 10.0f;

        // fixed initialization
        targetEnemy = null;
        shootTimer = basicShootPeriod;
        bulletBuffTimer = 0.0f;
    }
    
    protected virtual void TargetEnemy(){   
        if(!canvasManager.ifStart){
            return ;
        }
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

            // generate bullet
            GameObject obj = Instantiate(bulletPrefab, transform.position + bulletOffset, Quaternion.identity, GameObject.Find("/Bullets").transform);
            Bullet bulletComponent = obj.GetComponent<Bullet>();
            Vector3 direction = (targetEnemy.transform.position - transform.position - bulletOffset);

            // setup bullet properties
            bulletComponent.targetPos = transform.position + direction.normalized * 1000.0f;
            bulletComponent.speed = bulletSpeed;
        }

        // check if enemy is in range
        Vector3 enemyDistance = targetEnemy.transform.position - transform.position;
        if (enemyDistance.magnitude > shootRange){
            targetEnemy = null;
        }
    }
}
