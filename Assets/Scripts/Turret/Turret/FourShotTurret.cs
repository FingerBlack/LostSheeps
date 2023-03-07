using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using System;
public class FourShotTurret : AttackTurret
{
    [Tooltip("hold targetEnemy direction until attack over")]
    protected Vector3 targetEnemyPosition;
    [SerializeField] protected bool isRapidFiring;
    [Tooltip("indicate whether four shot has ended")]
    [SerializeField] protected bool hasFiringEnded;
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
        targetEnemyPosition = Vector3.zero;
        isRapidFiring = false;
        hasFiringEnded = true;
        rapidFireRate = 0.1f;
        rapidFireNumber = 5;
        numFireinRapid = 0;
    }

    // Update is called once per frame
    void Update()
    {   
        base.GetLocalPosition();

        if(bulletBuffTimer > 0.01f){
            buff.SetActive(true);
            sprite.color=new Color(1f,190f/255f,190f/255f,1f);
            bulletBuffTimer -= 0.01f;
        }else{
            sprite.color=new Color(1f,1f,1f,1f);
            buff.SetActive(false);
        }
        //light2D.intensity = bulletBuffTimer;

        base.TargetEnemy();

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
        else if (isRapidFiring && hasFiringEnded){
            hasFiringEnded = false;
            targetEnemyPosition = targetEnemy.transform.position;
            StartCoroutine("FourShots");
        }

        // check if enemy is in range
        Vector3 enemyDistance = targetEnemy.transform.position - transform.position;
        if (enemyDistance.magnitude > shootRange){
            targetEnemy = null;
        }
    }

    protected IEnumerator FourShots()
    {
        ++numFireinRapid;

        // generate bullet
        GameObject obj = Instantiate(bulletPrefab, transform.position + bulletOffset, Quaternion.identity, GameObject.Find("/Bullets").transform);
        Bullet bulletComponent = obj.GetComponent<Bullet>();
        Vector3 direction = (targetEnemyPosition - transform.position - bulletOffset);
        
        // setup bullet properties
        bulletComponent.targetPos = transform.position + direction.normalized * 1000.0f;
        bulletComponent.speed = bulletSpeed;
        bulletComponent.source = String.Copy(GetType().Name);


        if (numFireinRapid < rapidFireNumber){
            yield return new WaitForSeconds(rapidFireRate);
            StartCoroutine("FourShots");
        }
        else{
            isRapidFiring = false;
            hasFiringEnded = true;
            numFireinRapid = 0;
            shootTimer = 0.0f;
        }
    }
}