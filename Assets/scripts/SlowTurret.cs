using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTurret : MonoBehaviour
{
    //======================================== variables ============================================================
    // Dont Initiat the Value here plz.
    public GameObject target;
    public Grid map;
    public GameObject bullet;
    public GameObject up;
    public GameObject down;
    public GameObject left;
    public GameObject right;
    private ContactFilter2D filter; // Collider Detect Tools.
    private List<Collider2D> results;// Collider Detect Tools.
    private float shootTimer;
    public float shootPeriod;
    public float targetRange;
    public float bulletSpeed;
    public float HP;
    public Vector3Int gridPosition;
    private Vector3 bulletOffset = new Vector3(0.5f, 1.0f, 0.0f);
    //=============================================================================================================

    // Start is called before the first frame update
    void Start()
    {
        HP = 50f;
        target = null;
        map = GameObject.Find("Grid").GetComponent<Grid>();
        gridPosition = map.WorldToCell(transform.position);
        targetRange = 5f;
        bulletSpeed = 6f;
        shootPeriod = 1.0f;
        shootTimer = 0f;
        filter = new ContactFilter2D().NoFilter(); //initiate the Collider Detect Tools.
        results = new List<Collider2D>(); //initiate the Collider Detect Tools.
        StartCoroutine(TargetEnemy());
        StartCoroutine(CheckNeighbors());
    }

    // Update is called once per frame
    void Update()
    {

        if (target)
        {
            // shoot every period of time
            shootTimer += Time.deltaTime;
            if (shootTimer > shootPeriod)
            {
                shootTimer = 0f;

                GameObject obj = Instantiate(bullet, transform.position + new Vector3(0f, 1.0f, 0.0f), Quaternion.identity, GameObject.Find("/Bullets").transform);
                SlowBullet BulletComponent = obj.GetComponent<SlowBullet>();
                Vector3 direction = (target.transform.position - transform.position + new Vector3(0f, -1f, 0f));
                BulletComponent.TargetPos = transform.position + direction.normalized * 1000.0f;
                BulletComponent.speed = bulletSpeed;
            }
        }
    }

    private IEnumerator CheckNeighbors()
    {

        while (true)
        {
            gridPosition = map.WorldToCell(transform.position);
            up = GetBox(map.GetCellCenterWorld(gridPosition + new Vector3Int(0, 1, 0)));
            down = GetBox(map.GetCellCenterWorld(gridPosition + new Vector3Int(0, -1, 0)));
            left = GetBox(map.GetCellCenterWorld(gridPosition + new Vector3Int(-1, 0, 0)));
            right = GetBox(map.GetCellCenterWorld(gridPosition + new Vector3Int(1, 0, 0)));
            if (up && up.transform.childCount == 1)
            {
                if (up.transform.GetChild(0).gameObject.TryGetComponent<PlantCherry>(out PlantCherry upCherry))
                {
                    if (down && down.transform.childCount == 1)
                    {
                        if (down.transform.GetChild(0).gameObject.TryGetComponent<PlantCherry>(out PlantCherry downCherry))
                        {
                            upCherry.target = transform.parent.gameObject;
                            downCherry.target = transform.parent.gameObject;
                        }
                    }
                }
            }
            if (left && left.transform.childCount == 1)
            {
                if (left.transform.GetChild(0).gameObject.TryGetComponent<PlantCherry>(out PlantCherry leftCherry))
                {
                    if (right && right.transform.childCount == 1)
                    {
                        if (right.transform.GetChild(0).gameObject.TryGetComponent<PlantCherry>(out PlantCherry rightCherry))
                        {
                            leftCherry.target = transform.parent.gameObject;
                            rightCherry.target = transform.parent.gameObject;
                        }
                    }
                }
            }
            yield return null;
        }
    }
    private IEnumerator TargetEnemy()
    {
        while (true)
        {
            Physics2D.OverlapCircle(transform.position, targetRange, filter, results);
            foreach (Collider2D result in results)
            {
                if (result.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
                {
                    if (!target)
                    {
                        target = result.gameObject;
                        continue;
                    }
                    float dis1 = Vector3.Distance(transform.position, target.transform.position);
                    float dis2 = Vector3.Distance(transform.position, result.gameObject.transform.position);
                    if (dis2 < dis1)
                    {
                        target = result.gameObject;
                    }
                }
            }
            yield return null;
        }
    }
    private GameObject GetBox(Vector3 position)
    {
        Physics2D.OverlapCircle(position, 0.1f, filter, results);
        foreach (Collider2D result in results)
        {
            if (result.gameObject.TryGetComponent<Box>(out Box box))
            {
                return result.gameObject;
            }
        }
        return null;
    }
}
