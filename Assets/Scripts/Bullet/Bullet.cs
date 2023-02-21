using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //======================================== variables ============================================================
    // Dont Initiat the Value here plz.
    public Vector3 TargetPos;
    public float speed;
    public float MaintainTime; 
    public float TimeCount;
    private ContactFilter2D filter; // Collider Detect Tools.
    private List<Collider2D> results;// Collider Detect Tools.
    //=============================================================================================================
    void Start()
    {   
        filter = new ContactFilter2D().NoFilter(); //initiate the Collider Detect Tools.
        results = new List<Collider2D>(); //initiate the Collider Detect Tools.
        // speed=1f;
        TimeCount=0;
        MaintainTime=10f;
        TimeCount=0f;
    }

    // Update is called once per frame
    void Update()
    {
        TimeCount+=Time.deltaTime;
        if(TimeCount>MaintainTime){
            Destroy(gameObject);
        }
        gameObject.transform.position=Vector3.MoveTowards(gameObject.transform.position,TargetPos,speed*Time.deltaTime);
        Physics2D.OverlapCircle(transform.position, 0.1f,filter, results);
        foreach( Collider2D result in results)
        {
            if(result.gameObject.TryGetComponent<Enemy>(out Enemy enemy)){
                enemy.HP-=1f;
                Destroy(gameObject);
            }
            if(result.gameObject.TryGetComponent<Enemy2>(out Enemy2 enemy2)){
                enemy2.HP-=1f;
                Destroy(gameObject);
            }
            if(result.gameObject.TryGetComponent<GhostEnemy>(out GhostEnemy ghostEnemy)){
                ghostEnemy.HP-=1f;
                Destroy(gameObject);
            }
            if(result.gameObject.TryGetComponent<Enemy1>(out Enemy1 enemy1)){
                enemy1.HP-=1f;
                Destroy(gameObject);
            }
            // if(result.gameObject.TryGetComponent<Wall>(out Wall wall)){
            //     //enemy.HP-=1f;
            //     Destroy(gameObject);
            // }
        }
    }

}
