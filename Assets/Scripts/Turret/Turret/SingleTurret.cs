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
        
        // call this only if it's combinable
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

        if(bulletBuffTimer > 0.01f){
            buff.SetActive(true);
            sprite.color=new Color(1f,190f/255f,190f/255f,1f);
            bulletBuffTimer -= 0.01f;
        }else{
            sprite.color=new Color(1f,1f,1f,1f);
            buff.SetActive(false);
        }

        //light2D.intensity = bulletBuffTimer;

        // call this only if it's combinable
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
