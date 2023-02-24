using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTurret : AttackTurret
{
    // Start is called before the first frame update
    void Start()
    {   
        base.Init();

        // varying initialization (from Turret & AttackTurret)
        healthPoint = 50.0f;
        isCombinable = true;
        base.CombinableInit();

        bulletOffset = new Vector3(0f, 1.0f, 0.0f);
        basicShootPeriod = 1.0f;
        shootRange = 5.0f;
        bulletSpeed = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {   
        base.GetLocalPosition();

        if(bulletBuffTimer > 0.01f)
            bulletBuffTimer -= 0.01f;

        light2D.intensity = bulletBuffTimer;

        if(isCombinable){
            if(combinationTarget){
                float dis = Vector3.Distance(transform.parent.transform.position, combinationTarget.transform.position);
                transform.parent.position = Vector3.MoveTowards(transform.parent.position, combinationTarget.transform.position, combinationSpeed * Time.deltaTime);
            }

            if(transferCode != TransferCode.None){
                base.TryCreateUpgradeTurret();
            }
        }

        base.TargetEnemy();

        if(targetEnemy){
            ShootEnemy();
        }
    }
}
