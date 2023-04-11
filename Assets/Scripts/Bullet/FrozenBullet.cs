using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenBullet : Bullet
{
    void Start()
    {
        base.Init();
    }

    void Update()
    {
        timeCount += Time.deltaTime;
        if (timeCount > maintainTime){
            Destroy(gameObject);
        }

        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPos, speed * Time.deltaTime);

        CheckCollideEnemy();
    }

    protected override void CheckCollideEnemy()
    {
        Physics2D.OverlapCircle(transform.position, 0.1f, filter, results);
        foreach (Collider2D result in results)
        {
            if (result.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.healthPoint -= 1.0f;
                // set frozen status, make sure no conflict between slow and frozen
                enemy.isSlowed = false;
                enemy.slowedTime = 0;
                enemy.isFrozen = true;
                enemy.frozenTime = 0;
                
                PlayingStats.damageToEnemy(source, 1.0f.ToString(), enemy.GetType().Name);
                Destroy(gameObject);
                break;
            }
        }
    }
}
