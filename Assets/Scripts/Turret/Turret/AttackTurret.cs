using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class AttackTurret : Turret
{
    // ============================== variables ==============================
    [Tooltip("shooting at targetEnemy if exists")]
    public GameObject targetEnemy;
    public GameObject bulletPrefab;
    public GameObject bulletPrefabNormal;
    public GameObject bulletPrefabSlow;
    public GameObject bulletPrefabFrozen;
    protected Vector3 bulletOffset;
    [Tooltip("basic shooting cooldown")]
    public float basicShootPeriod;
    public float shootTimer;
    public float shootRange;
    public float bulletSpeed;
    [Tooltip("timer used for count down buff time")]
    public float bulletBuffTimer;
    public GameObject buff;
    public CanvasManager canvasManager;
    protected SpriteRenderer spriteRenderer;
    // change bullte type
    protected BulletType bulletType;

    [Tooltip("sprites of each direction, up -> upleft -> left -> downleft ... -> upright")]
    [SerializeField] protected Sprite[] directionalSprites;

    // ============================== general methods ==============================
    // general initialization, call this function first in Start() then modify varying variables
    protected override void Init()
    {
        base.Init();
        spriteRenderer = GetComponent<SpriteRenderer>();
        buff=transform.GetChild(0).gameObject;
        canvasManager = GameObject.Find("Canvas").GetComponent<CanvasManager>();
        // varying initialization
        bulletOffset = new Vector3(0f, 1.0f, 0.0f);
        basicShootPeriod = 1.0f;
        shootRange = 5.0f;
        bulletSpeed = 10.0f;

        // fixed initialization
        targetEnemy = null;
        shootTimer = basicShootPeriod;
        bulletBuffTimer = 0.0f;
        bulletType = BulletType.Normal;
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
                float dis1 = Vector3.Distance(transform.position, targetEnemy.transform.position);
                float dis2 = Vector3.Distance(transform.position, result.gameObject.transform.position);
                if(dis2 < dis1){
                    targetEnemy = result.gameObject;
                }
            }
        } 
    }

    protected virtual void ShootEnemy()
    {
        changeDirection();

        // shoot every period of time
        if(!canvasManager.ifStart){
            return ;
        }
        shootTimer += Time.deltaTime;
        if(shootTimer > basicShootPeriod / (1.0f + bulletBuffTimer))
        {
            shootTimer = 0.0f;

            // generate bullet
            // if(bulletType == BulletType.Normal) bulletPrefab = bulletPrefabNormal;
            // else if(bulletType == BulletType.Slow) bulletPrefab = bulletPrefabSlow;
            // else if(bulletType == BulletType.Frozen) bulletPrefab = bulletPrefabFrozen;
            GameObject obj = Instantiate(bulletPrefab, transform.position + bulletOffset, Quaternion.identity, GameObject.Find("/Bullets").transform);
            Bullet bulletComponent = obj.GetComponent<Bullet>();
            Vector3 direction = (targetEnemy.transform.position - transform.position - bulletOffset);

            // setup bullet properties
            bulletComponent.targetPos = transform.position + direction.normalized * 1000.0f;
            bulletComponent.speed = bulletSpeed;
            bulletComponent.source = String.Copy(GetType().Name);
            
            
        }

        // check if enemy is in range
        Vector3 enemyDistance = targetEnemy.transform.position - transform.position;
        if (enemyDistance.magnitude > shootRange){
            targetEnemy = null;
        }
    }

    public virtual void buffBullet(BulletType bulletType0){
        // Debug.Log("slow!!");
        bulletType = bulletType0;
        if(bulletType == BulletType.Slow) bulletPrefab = bulletPrefabSlow;
        else if(bulletType == BulletType.Normal) bulletPrefab = bulletPrefabNormal;
        if(bulletType == BulletType.Frozen) bulletPrefab = bulletPrefabFrozen;
    }

    protected virtual void changeDirection(){
        // change turret sprite to face enemy
        Vector2[] directions = { Vector2.up, new Vector2(-0.707f, 0.707f), 
                                 Vector2.left, new Vector2(-0.707f, -0.707f), 
                                 Vector2.down, new Vector2(0.707f, -0.707f), 
                                 Vector2.right, new Vector2(0.707f, 0.707f) };
        float maxVal = 0.0f;
        int destinationIndex = 0;
        Vector2 facing = targetEnemy.transform.position - transform.position;
        for(int i=0;i<8;i++){
            float dotProduct = Vector2.Dot(directions[i], facing);
            if(dotProduct > maxVal){
                maxVal = dotProduct;
                destinationIndex = i;
            }
        }

        spriteRenderer.sprite = directionalSprites[destinationIndex];
    }
}
