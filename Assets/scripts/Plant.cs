using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    private Enemy target;

    public GameObject bullet;

    private float shootTimer = 0.0f;
    public float shootPeriod = 1.0f;

    private float bulletOffset = 1.0f;

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

                GameObject obj = Instantiate(bullet, transform, false);
                obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y + bulletOffset, obj.transform.position.z);;
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
        // if no target and a new enemy appear inside the range, target it 
        if(target==null && collider.TryGetComponent<Enemy>(out Enemy enemy))
        {
            target = enemy;
            Debug.Log("target");
        }
    }
}
