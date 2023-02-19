using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantPea : MonoBehaviour
{   
    //======================================== variables ============================================================
    // Dont Initiat the Value here plz.
    public GameObject targetEnemy;
    public GameObject target;
    public float speed;
    public Grid map;
    public GameObject bullet;
    public GameObject up;
    public GameObject down;
    public GameObject left;
    public GameObject right;
    private ContactFilter2D filter; // Collider Detect Tools.
    private List<Collider2D> results;// Collider Detect Tools.
    private float shootTimer;
    public float basicShootPeriod;
    public float targetRange;
    public float bulletSpeed;
    public float HP;
    public Vector3Int gridPosition;
    public float bulletPeriodBuff;
    public GameObject transferGameObject1;
    public GameObject transferGameObject2 ;
    public GameObject transferGameObject3 ; 
    private Vector3 bulletOffset = new Vector3(0.5f, 1.0f, 0.0f);
    private int transfer;
    //private bool isTransfer;
    private GameObject targetObject1;
    private GameObject targetObject2;
    private GameObject box;
    private Box boxComponent;

    //=============================================================================================================

    // Start is called before the first frame update
    void Start()
    {   
        //isTransfer=false;
        speed=1f;
        HP=50f;
        box=transform.parent.gameObject;
        boxComponent=box.GetComponent<Box>();
        transfer=0;
        bulletPeriodBuff=0f;
        targetEnemy = null;
        map = GameObject.Find("Grid").GetComponent<Grid>();
        gridPosition=map.WorldToCell(transform.position);
        targetRange=5f;
        bulletSpeed=10f;
        basicShootPeriod=1.0f;
        shootTimer=0f;
        filter = new ContactFilter2D().NoFilter(); //initiate the Collider Detect Tools.
        results = new List<Collider2D>(); //initiate the Collider Detect Tools.
        StartCoroutine(TargetEnemy());
        
    }

    // Update is called once per frame
    void Update()
    {   
        gridPosition=map.WorldToCell(transform.position);
        up = GetBox(map.GetCellCenterWorld(gridPosition+new Vector3Int(0,1,0)));
        down = GetBox(map.GetCellCenterWorld(gridPosition+new Vector3Int(0,-1,0)));
        left = GetBox(map.GetCellCenterWorld(gridPosition+new Vector3Int(-1,0,0)));
        right = GetBox(map.GetCellCenterWorld(gridPosition+new Vector3Int(1,0,0)));
        if(target){
            float dis=Vector3.Distance(transform.parent.transform.position,target.transform.position);
            // if(dis<0.1f){
            //     target.GetComponent<Box>().HP=100f;
            //     target.transform.GetChild(0).gameObject.GetComponent<PlantPea>().HP+=100f;
            //     target.transform.GetChild(0).gameObject.GetComponent<PlantPea>().basicShootPeriod/=2f;
            //     //target.transform.GetChild(0).gameObject.GetComponent<PlantPea>().bulletSpeed*=2f;
            //     // Destroy(transform.parent.gameObject);
            // }
            transform.parent.position=Vector3.MoveTowards(transform.parent.position,target.transform.position,speed*Time.deltaTime);
            
        }
        if(transfer!=0){
            float dis=Vector3.Distance(transform.parent.transform.position,targetObject1.transform.position);
            if(dis<0.01f){
                if(transfer==1){

                    PlayingStats.comboCount(NamingConstant.Combo1);

                    Instantiate(transferGameObject1,transform.position,Quaternion.identity,transform.parent);
                }   
                if(transfer==2){
                    
                    Instantiate(transferGameObject2,transform.position,Quaternion.identity,transform.parent);
                }
                if(transfer==3){
                    
                    Instantiate(transferGameObject3,transform.position,Quaternion.identity,transform.parent);
                }
                Destroy(targetObject1.gameObject);
                Destroy(targetObject2.gameObject);
                Destroy(gameObject);
            }
            
        }       
        if(targetEnemy)
        {


            // shoot every period of time
            shootTimer += Time.deltaTime;
            if(shootTimer > basicShootPeriod/(1f+bulletPeriodBuff))
            {
                shootTimer =0f;

                GameObject obj = Instantiate(bullet, transform.position+new Vector3(0f, 1.0f, 0.0f),Quaternion.identity,GameObject.Find("/Bullets").transform);
                Bullet BulletComponent=obj.GetComponent<Bullet>();
                Vector3 direction=( targetEnemy.transform.position- transform.position+new Vector3(0f,-1f,0f));
                BulletComponent.TargetPos=transform.position + direction.normalized * 1000.0f;    
                BulletComponent.speed=bulletSpeed;
            }

            Vector3 currEnemyPos = targetEnemy.transform.position - transform.position;
            float enemyDist = currEnemyPos.magnitude;
            if (enemyDist > targetRange)
            {
                targetEnemy = null;
            }
        }

    }
    private int _Check_Neighbors(GameObject neighbor){ // 0 no plants, 1 plantpea, 2 plantcherry
        int flag=0;
        if(neighbor&&neighbor.transform.childCount==1){
            GameObject neighborPlant=neighbor.transform.GetChild(0).gameObject;

            if(neighborPlant.TryGetComponent<PlantCherry>(out PlantCherry plantCherry)){
                if(plantCherry.target){
                    return 0;
                }else{
                    return 1;
                }

            }
            if(neighborPlant.TryGetComponent<PlantPea>(out PlantPea plantPea)){
                if(plantPea.target){
                    return 0;
                }else{
                    return 2;
                }
            }
        }
        return 0;
    }
    private int _Check_Neighbors_Combine(GameObject neighbor1,GameObject neighbor2){ // 0 no plants, 1 plantpea, 2 plantcherry
        int cherryCount=0;
        int peaCount=1;
        int check1=_Check_Neighbors(neighbor1);
        if(check1==1){
            cherryCount++;
        }else if(check1==2){
            peaCount++;
        }else{
            return 0;
        }
        int check2=_Check_Neighbors(neighbor2);
        if(check2==1){
            cherryCount++;
        }else if(check2==2){
            peaCount++;
        }else{
            return 0;
        }
        if(peaCount==3){
            transfer=1;        
        }
        if(peaCount==2&&cherryCount==1){
            transfer=2;        
        }
        if(peaCount==1&&cherryCount==2){
            transfer=3;       
        }

        targetObject1=neighbor1;
        targetObject2=neighbor2;
        if(check1==1){
            neighbor1.transform.GetChild(0).gameObject.GetComponent<PlantCherry>().target=transform.parent.gameObject;
        }else if(check1==2){
            neighbor1.transform.GetChild(0).gameObject.GetComponent<PlantPea>().target=transform.parent.gameObject;
        }
        if(check2==1){
            neighbor2.transform.GetChild(0).gameObject.GetComponent<PlantCherry>().target=transform.parent.gameObject;
        }else if(check2==2){
            neighbor2.transform.GetChild(0).gameObject.GetComponent<PlantPea>().target=transform.parent.gameObject;
        }
        return 1;
    }
    public bool CheckNeighbors(){   
        
        

        if(!target&&transfer==0){
            if(_Check_Neighbors_Combine(up,down)==0){
                if(_Check_Neighbors_Combine(left,right)==0){
                    return false;
                }
                
            }
        }

        return true;
        
    }
    private IEnumerator TargetEnemy(){   
        while(true){
            Physics2D.OverlapCircle(transform.position, targetRange,filter, results);
            foreach( Collider2D result in results)
            {
                if(result.gameObject.TryGetComponent<Enemy>(out Enemy enemy)){
                    if(!targetEnemy){
                        targetEnemy=result.gameObject;
                        continue;
                    }
                    float dis1=Vector3.Distance(transform.position,targetEnemy.transform.position);
                    float dis2=Vector3.Distance(transform.position,result.gameObject.transform.position);
                    if(dis2<dis1){
                        targetEnemy=result.gameObject;
                    }
                }
                if(result.gameObject.TryGetComponent<Enemy2>(out Enemy2 enemy2)){
                    if(!targetEnemy){
                        targetEnemy=result.gameObject;
                        continue;
                    }
                    float dis1=Vector3.Distance(transform.position,targetEnemy.transform.position);
                    float dis2=Vector3.Distance(transform.position,result.gameObject.transform.position);
                    if(dis2<dis1){
                        targetEnemy=result.gameObject;
                    }
                }               
            }
            yield return null; 
        }
    }
    private GameObject GetBox(Vector3 position){
        Physics2D.OverlapCircle(position, 0.1f,filter, results);
        foreach( Collider2D result in results)
        {
            if(result.gameObject.TryGetComponent<Box>(out Box box)){
                return result.gameObject;
            }
        }
        return null;
    }
}
