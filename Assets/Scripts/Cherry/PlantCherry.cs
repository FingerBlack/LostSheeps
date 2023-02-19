using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantCherry : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    public bool ifmove;
    public float speed;
    public Grid map;
    public GameObject up;
    public GameObject down;
    public GameObject left;
    public GameObject right;
    public GameObject upRight;
    public GameObject downRight;
    public GameObject upLeft;
    public GameObject downLeft;
    private ContactFilter2D filter; // Collider Detect Tools.
    private List<Collider2D> results;// Collider Detect Tools.
    public Vector3Int gridPosition;
    private int transfer;
    public GameObject transferGameObject1;
    public GameObject transferGameObject2 ;
    public GameObject transferGameObject3 ; 
    private GameObject targetObject1;
    private GameObject targetObject2;
    private GameObject box;
    private Box boxComponent;
    private float buffValue;
    void Start()
    {   
        target=null;
        buffValue=0.3f;
        box=transform.parent.gameObject;
        boxComponent=box.GetComponent<Box>();
        map = GameObject.Find("Grid").GetComponent<Grid>();
        speed=1f;
        transfer=0;
        filter = new ContactFilter2D().NoFilter(); //initiate the Collider Detect Tools.
        results = new List<Collider2D>(); //initiate the Collider Detect Tools.
        CheckNeighbors();
    }

    // Update is called once per frame
    void Update()
    {   
        gridPosition=map.WorldToCell(transform.position);
        upRight = GetBox(map.GetCellCenterWorld(gridPosition+new Vector3Int(1,1,0)));
        downRight = GetBox(map.GetCellCenterWorld(gridPosition+new Vector3Int(1,-1,0)));
        upLeft = GetBox(map.GetCellCenterWorld(gridPosition+new Vector3Int(-1,1,0)));
        downLeft = GetBox(map.GetCellCenterWorld(gridPosition+new Vector3Int(-1,-1,0)));
        up = GetBox(map.GetCellCenterWorld(gridPosition+new Vector3Int(0,1,0)));
        down = GetBox(map.GetCellCenterWorld(gridPosition+new Vector3Int(0,-1,0)));
        left = GetBox(map.GetCellCenterWorld(gridPosition+new Vector3Int(-1,0,0)));
        right = GetBox(map.GetCellCenterWorld(gridPosition+new Vector3Int(1,0,0)));
        AddBuff(upRight);
        AddBuff(downRight);
        AddBuff(upLeft);
        AddBuff(downLeft);
        AddBuff(up);
        AddBuff(down);
        AddBuff(left);
        AddBuff(right);
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
                    PlayingStats.comboCount(transferGameObject1.name);
                    Instantiate(transferGameObject1,transform.position,Quaternion.identity,transform.parent);
                }   
                if(transfer==2){
                    PlayingStats.comboCount(transferGameObject2.name);
                    Instantiate(transferGameObject2,transform.position,Quaternion.identity,transform.parent);
                }
                if(transfer==3){
                    PlayingStats.comboCount(transferGameObject3.name);
                    Instantiate(transferGameObject3,transform.position,Quaternion.identity,transform.parent);
                }
                Destroy(targetObject1.gameObject);
                Destroy(targetObject2.gameObject);
                Destroy(gameObject);
            }
            
        }
    }
    void AddBuff(GameObject g){
        if(g&&g.transform.childCount==1){
            if(g.transform.GetChild(0).gameObject.TryGetComponent<PlantPea>(out PlantPea plantPea)){
                if(plantPea.bulletPeriodBuff<buffValue)
                    plantPea.bulletPeriodBuff=buffValue;

                
            }
            if(g.transform.GetChild(0).gameObject.TryGetComponent<SlowTurret>(out SlowTurret slowTurret)){
                if(slowTurret.bulletPeriodBuff<buffValue)
                    slowTurret.bulletPeriodBuff=buffValue;

                
            }
            if(g.transform.GetChild(0).gameObject.TryGetComponent<ThreeWayTurret>(out ThreeWayTurret threeWayTurret)){
                if(threeWayTurret.bulletPeriodBuff<buffValue)
                    threeWayTurret.bulletPeriodBuff=buffValue;

                
            }
            if(g.transform.GetChild(0).gameObject.TryGetComponent<FourShotTurret>(out FourShotTurret fourShotTurret)){
                if(fourShotTurret.bulletPeriodBuff<buffValue)
                    fourShotTurret.bulletPeriodBuff=buffValue;

                
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
        int cherryCount=1;
        int peaCount=0;
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
        if(cherryCount==3){
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
        
        
        //Debug.Log("0.3");
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
