using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public Vector3 targetPos;
    public float speed;
    protected float maintainTime; 
    protected float timeCount;
    protected ContactFilter2D filter; // Collider Detect Tools.
    protected List<Collider2D> results;// Collider Detect Tools.


    protected virtual void Init()
    {
        // initialize here will override instantiate from turret
        //TargetPos = Vector3.zero;
        //speed = 0.0f;
        maintainTime = 10.0f;
        timeCount = 0.0f;
        filter = new ContactFilter2D().NoFilter(); //initiate the Collider Detect Tools.
        results = new List<Collider2D>(); //initiate the Collider Detect Tools.
    }

    protected virtual void CheckCollideEnemy()
    {
        Physics2D.OverlapCircle(transform.position, 0.1f, filter, results);
        foreach( Collider2D result in results)
        {
            if(result.gameObject.TryGetComponent<Enemy>(out Enemy enemy)){
                enemy.healthPoint -= 1.0f;
                Destroy(gameObject);
                break;
            }
        }
    }
}
