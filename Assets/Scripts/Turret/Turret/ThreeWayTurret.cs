using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeWayTurret : AttackTurret
{
    void Start()
    {   
        base.Init();

        // varying initialization (from Turret & AttackTurret)
        healthPoint = 50.0f;
        isCombinable = false;
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

    protected virtual void ShootEnemy()
    {
        // shoot every period of time
        shootTimer += Time.deltaTime;
        if(shootTimer > basicShootPeriod / (1.0f + bulletBuffTimer))
        {
            shootTimer = 0f;

            GameObject obj = Instantiate(bulletPrefab, transform.position + new Vector3(0f, 1.0f, 0.0f), Quaternion.identity, GameObject.Find("/Bullets").transform);
            Bullet BulletComponent = obj.GetComponent<Bullet>();
            GameObject obj1 = Instantiate(bulletPrefab, transform.position + new Vector3(0f, 1.0f, 0.0f), Quaternion.identity, GameObject.Find("/Bullets").transform);
            Bullet BulletComponent1 = obj1.GetComponent<Bullet>();
            GameObject obj2 = Instantiate(bulletPrefab, transform.position + new Vector3(0f, 1.0f, 0.0f), Quaternion.identity, GameObject.Find("/Bullets").transform);
            Bullet BulletComponent2 = obj2.GetComponent<Bullet>();

            //temp
            float x, y;
            //
            Vector3 direction = (targetEnemy.transform.position - transform.position + new Vector3(0f, -1f, 0f));
            Vector3 direction1 = (targetEnemy.transform.position - transform.position + new Vector3(0f, -1f, 0f));
            x = direction1.x;
            y = direction1.y;
            direction1.x = x * Mathf.Cos(Mathf.PI / 9) - y * Mathf.Sin(Mathf.PI / 9);
            direction1.y = x * Mathf.Sin(Mathf.PI / 9) + y * Mathf.Cos(Mathf.PI / 9);
            Vector3 direction2 = (targetEnemy.transform.position - transform.position + new Vector3(0f, -1f, 0f));
            x = direction2.x;
            y = direction2.y;
            direction2.x = x * Mathf.Cos(-Mathf.PI / 9) - y * Mathf.Sin(-Mathf.PI / 9);
            direction2.y = x * Mathf.Sin(-Mathf.PI / 9) + y * Mathf.Cos(-Mathf.PI / 9);
            BulletComponent.TargetPos = transform.position + direction.normalized * 1000.0f;
            BulletComponent.speed = bulletSpeed;
            BulletComponent1.TargetPos = transform.position + direction1.normalized * 1000.0f;
            BulletComponent1.speed = bulletSpeed;
            BulletComponent2.TargetPos = transform.position + direction2.normalized * 1000.0f;
            BulletComponent2.speed = bulletSpeed;
        }

        Vector3 currEnemyPos = targetEnemy.transform.position - transform.position;
        float enemyDist = currEnemyPos.magnitude;
        if (enemyDist > shootRange){
            targetEnemy = null;
        }
    }

    //======================================== variables ============================================================
    // Dont Initiat the Value here plz.
    // public GameObject target;
    // public Grid map;
    // public GameObject bullet;
    // public GameObject up;
    // public GameObject down;
    // public GameObject left;
    // public GameObject right;
    // private ContactFilter2D filter; // Collider Detect Tools.
    // private List<Collider2D> results;// Collider Detect Tools.
    // private float shootTimer;
    // public float shootPeriod;
    // public float targetRange;
    // public float bulletSpeed;
    // public float HP;
    // public UnityEngine.Rendering.Universal.Light2D light;
    // public Vector3Int gridPosition;
    // private Vector3 bulletOffset = new Vector3(0.5f, 1.0f, 0.0f);
    // public float bulletPeriodBuff;
    // //=============================================================================================================

    // // Start is called before the first frame update
    // void Start()
    // {
        
    //     light=GetComponent<UnityEngine.Rendering.Universal.Light2D>();
    //     HP = 50f;
    //     target = null;
    //     bulletPeriodBuff=0f;
    //     map = GameObject.Find("Grid").GetComponent<Grid>();
    //     gridPosition = map.WorldToCell(transform.position);
    //     targetRange = 5f;
    //     bulletSpeed = 10f;
    //     shootPeriod = 1.0f;
    //     shootTimer = 0f;
    //     filter = new ContactFilter2D().NoFilter(); //initiate the Collider Detect Tools.
    //     results = new List<Collider2D>(); //initiate the Collider Detect Tools.
    //     StartCoroutine(TargetEnemy());
    //     //StartCoroutine(CheckNeighbors());
    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     if(bulletPeriodBuff>0.01f)
    //         bulletPeriodBuff-=0.01f;
    //     if (target)
    //     {
    //         // shoot every period of time
    //         shootTimer += Time.deltaTime;
    //         if (shootTimer > shootPeriod/(1f+bulletPeriodBuff))
    //         {
    //             shootTimer = 0f;

    //             GameObject obj = Instantiate(bullet, transform.position + new Vector3(0f, 1.0f, 0.0f), Quaternion.identity, GameObject.Find("/Bullets").transform);
    //             Bullet BulletComponent = obj.GetComponent<Bullet>();
    //             GameObject obj1 = Instantiate(bullet, transform.position + new Vector3(0f, 1.0f, 0.0f), Quaternion.identity, GameObject.Find("/Bullets").transform);
    //             Bullet BulletComponent1 = obj1.GetComponent<Bullet>();
    //             GameObject obj2 = Instantiate(bullet, transform.position + new Vector3(0f, 1.0f, 0.0f), Quaternion.identity, GameObject.Find("/Bullets").transform);
    //             Bullet BulletComponent2 = obj2.GetComponent<Bullet>();

    //             //temp
    //             float x, y;
    //             //
    //             Vector3 direction = (target.transform.position - transform.position + new Vector3(0f, -1f, 0f));
    //             Vector3 direction1 = (target.transform.position - transform.position + new Vector3(0f, -1f, 0f));
    //             x = direction1.x;
    //             y = direction1.y;
    //             direction1.x = x * Mathf.Cos(Mathf.PI / 9) - y * Mathf.Sin(Mathf.PI / 9);
    //             direction1.y = x * Mathf.Sin(Mathf.PI / 9) + y * Mathf.Cos(Mathf.PI / 9);
    //             Vector3 direction2 = (target.transform.position - transform.position + new Vector3(0f, -1f, 0f));
    //             x = direction2.x;
    //             y = direction2.y;
    //             direction2.x = x * Mathf.Cos(-Mathf.PI / 9) - y * Mathf.Sin(-Mathf.PI / 9);
    //             direction2.y = x * Mathf.Sin(-Mathf.PI / 9) + y * Mathf.Cos(-Mathf.PI / 9);
    //             BulletComponent.TargetPos = transform.position + direction.normalized * 1000.0f;
    //             BulletComponent.speed = bulletSpeed;
    //             BulletComponent1.TargetPos = transform.position + direction1.normalized * 1000.0f;
    //             BulletComponent1.speed = bulletSpeed;
    //             BulletComponent2.TargetPos = transform.position + direction2.normalized * 1000.0f;
    //             BulletComponent2.speed = bulletSpeed;
    //         }
    //         Vector3 currEnemyPos = target.transform.position - transform.position;
    //         float enemyDist = currEnemyPos.magnitude;
    //         if (enemyDist > targetRange)
    //         {
    //             target = null;
    //         }
    //     }
    // }

    // private IEnumerator CheckNeighbors()
    // {

    //     while (true)
    //     {
    //         gridPosition = map.WorldToCell(transform.position);
    //         up = GetBox(map.GetCellCenterWorld(gridPosition + new Vector3Int(0, 1, 0)));
    //         down = GetBox(map.GetCellCenterWorld(gridPosition + new Vector3Int(0, -1, 0)));
    //         left = GetBox(map.GetCellCenterWorld(gridPosition + new Vector3Int(-1, 0, 0)));
    //         right = GetBox(map.GetCellCenterWorld(gridPosition + new Vector3Int(1, 0, 0)));
    //         if (up && up.transform.childCount == 1)
    //         {
    //             if (up.transform.GetChild(0).gameObject.TryGetComponent<PlantCherry>(out PlantCherry upCherry))
    //             {
    //                 if (down && down.transform.childCount == 1)
    //                 {
    //                     if (down.transform.GetChild(0).gameObject.TryGetComponent<PlantCherry>(out PlantCherry downCherry))
    //                     {
    //                         upCherry.target = transform.parent.gameObject;
    //                         downCherry.target = transform.parent.gameObject;
                            
    //                     }
    //                 }
    //             }
    //         }
    //         if (left && left.transform.childCount == 1)
    //         {
    //             if (left.transform.GetChild(0).gameObject.TryGetComponent<PlantCherry>(out PlantCherry leftCherry))
    //             {
    //                 if (right && right.transform.childCount == 1)
    //                 {
    //                     if (right.transform.GetChild(0).gameObject.TryGetComponent<PlantCherry>(out PlantCherry rightCherry))
    //                     {
    //                         leftCherry.target = transform.parent.gameObject;
    //                         rightCherry.target = transform.parent.gameObject;
                            
    //                     }
    //                 }
    //             }
    //         }
    //         yield return null;
    //     }
    // }
    // private IEnumerator TargetEnemy()
    // {
    //     while (true)
    //     {
    //         Physics2D.OverlapCircle(transform.position, targetRange, filter, results);
    //         foreach (Collider2D result in results)
    //         {
    //             if (result.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
    //             {
    //                 if (!target)
    //                 {
    //                     target = result.gameObject;
    //                     continue;
    //                 }
    //                 float dis1 = Vector3.Distance(transform.position, target.transform.position);
    //                 float dis2 = Vector3.Distance(transform.position, result.gameObject.transform.position);
    //                 if (dis2 < dis1)
    //                 {
    //                     target = result.gameObject;
    //                 }
    //             }
    //         }
    //         yield return null;
    //     }
    // }
    // private GameObject GetBox(Vector3 position)
    // {
    //     Physics2D.OverlapCircle(position, 0.1f, filter, results);
    //     foreach (Collider2D result in results)
    //     {
    //         if (result.gameObject.TryGetComponent<Box>(out Box box))
    //         {
    //             return result.gameObject;
    //         }
    //     }
    //     return null;
    // }

}
