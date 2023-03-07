using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ThreeWayTurret : AttackTurret
{
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
        if(shootTimer > basicShootPeriod / (1.0f + bulletBuffTimer))
        {
            shootTimer = 0.0f;

            GameObject obj = null;
            Bullet bulletComponent = null;
            Vector3 direction = targetEnemy.transform.position - transform.position - bulletOffset;
            float x = direction.x, y = direction.y;
            float[] shootAngles = {-Mathf.PI / 9, -Mathf.PI / 15, 0, Mathf.PI / 15, Mathf.PI / 9};

            for(int i=0;i<5;i++){
                // generate bullet
                obj = Instantiate(bulletPrefab, transform.position + bulletOffset, Quaternion.identity, GameObject.Find("/Bullets").transform);
                bulletComponent = obj.GetComponent<Bullet>();

                // adjust angles
                direction.x = x * Mathf.Cos(shootAngles[i]) - y * Mathf.Sin(shootAngles[i]);
                direction.y = x * Mathf.Sin(shootAngles[i]) + y * Mathf.Cos(shootAngles[i]);

                // setup bullet properties
                bulletComponent.targetPos = transform.position + direction.normalized * 1000.0f;
                bulletComponent.speed = bulletSpeed;
                bulletComponent.source = String.Copy(GetType().Name);
            }
        }

        // check if enemy is in range
        Vector3 enemyDistance = targetEnemy.transform.position - transform.position;
        if (enemyDistance.magnitude > shootRange){
            targetEnemy = null;
        }
    }
}
