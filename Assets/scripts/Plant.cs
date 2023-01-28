using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public Enemy target;

    public GameObject bullet;

    private float shootTimer = 0.0f;
    public float shootPeriod = 1.0f;

    private Vector3 bulletOffset = new Vector3(0.5f, 1.0f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        target = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(target)
        {
            // shoot every period of time
            shootTimer += Time.deltaTime;
            if(shootTimer > shootPeriod)
            {
                shootTimer -= shootPeriod;

                GameObject obj = Instantiate(bullet, transform.position+new Vector3(0f, 1.0f, 0.0f),Quaternion.identity,GameObject.Find("/Bullets").transform);
                Bullet BulletComponent=obj.GetComponent<Bullet>();
                // target.transform.position+=new Vector3(0f,-1f,0f);
                Vector3 direction=( target.transform.position- transform.position+new Vector3(0f,-1f,0f));
                
                BulletComponent.TargetPos=transform.position + direction.normalized * 1000.0f;    
                BulletComponent.speed=5;
                //obj.transform.position += bulletOffset;

                //Debug.Log("Shoot");
                // bullets should update itself?
            }
        }
        else
        {
            // reset timer if there is no enemy
            shootTimer=0.0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // if no target and a new enemy appear inside the range, target on it 
        //Debug.Log(collider.name);
        if(collider.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            if(target==null){
                target=enemy;
                return;
            }

            float Dis1=Vector3.Distance(transform.position,enemy.transform.position);
            float Dis2=Vector3.Distance(transform.position,target.transform.position);
            if(Dis1<Dis2)
                target = enemy;
            Debug.Log("target");
        }
    }
}
