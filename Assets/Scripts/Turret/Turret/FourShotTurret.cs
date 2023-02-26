using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FourShotTurret : AttackTurret
{
    [SerializeField] protected bool isRapidFiring;
    [Tooltip("shot period in a single attack")]
    [SerializeField] protected float rapidFireRate;
    [Tooltip("total bullet number of a single attack")]
    [SerializeField] protected int rapidFireNumber;
    [Tooltip("bullets counter, stop shooting when this reaches rapidFireNumber")]
    [SerializeField] protected int numFireinRapid;

    void Start()
    {   
        base.Init();

        // varying initialization (from Turret & AttackTurret)
        healthPoint = 50.0f;
        isCombinable = false;

        bulletOffset = new Vector3(0f, 1.0f, 0.0f);
        basicShootPeriod = 1.0f;
        shootRange = 5.0f;
        bulletSpeed = 10.0f;

        // four shot turret only
        isRapidFiring = false;
        rapidFireRate = 0.1f;
        rapidFireNumber = 5;
        numFireinRapid = 0;
    }

    // Update is called once per frame
    void Update()
    {   
        base.GetLocalPosition();

        if(bulletBuffTimer > 0.01f)
            bulletBuffTimer -= 0.01f;

        light2D.intensity = bulletBuffTimer;

        if(!isRapidFiring){
            base.TargetEnemy();
        }

        if(targetEnemy){
            ShootEnemy();
        }
    }

    protected override void ShootEnemy()
    {
        // shoot every period of time
        shootTimer += Time.deltaTime;
        if(shootTimer > basicShootPeriod / (1.0f + bulletBuffTimer) && !isRapidFiring){
            shootTimer = 0.0f;
            isRapidFiring = true;
        }
        else if (shootTimer > rapidFireRate && isRapidFiring){
            shootTimer = 0.0f;
            if (numFireinRapid < rapidFireNumber)
            {
                numFireinRapid++;

                // generate bullet
                GameObject obj = Instantiate(bulletPrefab, transform.position + new Vector3(0f, 1.0f, 0.0f), Quaternion.identity, GameObject.Find("/Bullets").transform);
                Bullet bulletComponent = obj.GetComponent<Bullet>();
                Vector3 direction = (targetEnemy.transform.position - transform.position + new Vector3(0f, -1f, 0f));
                
                // setup bullet properties
                bulletComponent.targetPos = transform.position + direction.normalized * 1000.0f;
                bulletComponent.speed = bulletSpeed;
            }
            else{
                isRapidFiring = false;
                numFireinRapid = 0;
            }
        }

        // check if enemy is in range
        if(!isRapidFiring){
            Vector3 enemyDistance = targetEnemy.transform.position - transform.position;
            if (enemyDistance.magnitude > shootRange){
                targetEnemy = null;
            }
        }
    }
}