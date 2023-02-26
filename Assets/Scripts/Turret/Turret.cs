using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public abstract class Turret : MonoBehaviour
{
    // ============================== variables ==============================
    public float healthPoint;
    protected Grid map;
    [Tooltip("position corresponds on map")]
    public Vector3Int gridPosition;
    protected GameObject upBox;
    protected GameObject downBox;
    protected GameObject leftBox;
    protected GameObject rightBox;
    private Box boxComponent;
    protected Light2D light2D;
    // collider detect tools
    protected ContactFilter2D filter; 
    // colliding list
    protected List<Collider2D> results;
    
    // ========== combination related variables ==========
    protected bool isCombinable;
    [Tooltip("move towards combination target if exists")]
    protected GameObject combinationTarget;
    protected float combinationSpeed;
    [Tooltip("combination neighbor 1")]
    private GameObject targetObject1;
    [Tooltip("combination neighbor 2")]
    private GameObject targetObject2;
    
    // the plant combination will change into
    protected enum TransferCode { None = 0, LargeRadar = 1, SlowTurret = 2, ThreeWayTurret = 3, FourShotTurret = 4 };
    protected TransferCode transferCode;

    // more readable
    protected enum PlantCode { NoPlant = 0, Turret = 1, Radar = 2 };

    [Tooltip("possible tranform object, initial in scene")]
    [SerializeField] protected GameObject[] transferGameObject;

    // ============================== general methods ==============================
    protected virtual void Init()
    {
        // varying initialization
        healthPoint = 50.0f;
        isCombinable = false;

        // fixed initialization
        map = GameObject.Find("Grid").GetComponent<Grid>();
        gridPosition = map.WorldToCell(transform.position);
        boxComponent = transform.parent.gameObject.GetComponent<Box>();
        light2D = GetComponent<Light2D>();
        filter = new ContactFilter2D().NoFilter();
        results = new List<Collider2D>();

        combinationTarget = null;
        combinationSpeed = 1.0f;
        targetObject1 = null;
        targetObject2 = null;
        
        transferCode = TransferCode.None;
    }

    // necessary initialization for combinable turrets
    protected virtual void CombinableInit()
    {
        transferGameObject = new GameObject[4] {
            Resources.Load("Prefabs/Buff/LargeRadar") as GameObject, 
            Resources.Load("Prefabs/Turret/SlowTurret") as GameObject,
            Resources.Load("Prefabs/Turret/ThreeWayTurret") as GameObject,
            Resources.Load("Prefabs/Turret/FourShotTurret") as GameObject
        };

        CheckCombiantion();
    }

    protected virtual void GetLocalPosition()
    {
        gridPosition = map.WorldToCell(transform.position);
        upBox = GetBox(map.GetCellCenterWorld(gridPosition + new Vector3Int(0, 1, 0)));
        downBox = GetBox(map.GetCellCenterWorld(gridPosition + new Vector3Int(0, -1, 0)));
        leftBox = GetBox(map.GetCellCenterWorld(gridPosition + new Vector3Int(-1, 0, 0)));
        rightBox = GetBox(map.GetCellCenterWorld(gridPosition + new Vector3Int(1, 0, 0)));
    }
     
    protected GameObject GetBox(Vector3 position){
        Physics2D.OverlapCircle(position, 0.1f, filter, results);
        foreach( Collider2D result in results)
        {
            if(result.gameObject.TryGetComponent<Box>(out Box box)){
                return result.gameObject;
            }
        }
        return null;
    }

    // ============================== combination methods ==============================
    protected virtual void TryCreateUpgradeTurret()
    {
        float dis = Vector3.Distance(transform.parent.transform.position, targetObject1.transform.position);

        if(dis < 0.01f){
            // instantiate with enum, -1 to fit position in array
            PlayingStats.comboCount(transferGameObject[(int)transferCode - 1].name);
            Instantiate(transferGameObject[(int)transferCode - 1], transform.position, Quaternion.identity, transform.parent);


            Destroy(targetObject1.gameObject);
            Destroy(targetObject2.gameObject);
            Destroy(gameObject);
        }
    }

    protected virtual PlantCode CheckNeighbors(GameObject neighbor) // 0 no plants, 1 radar, 2 Turret
    { 
        if(neighbor && neighbor.transform.childCount == 1){
            GameObject neighborPlant = neighbor.transform.GetChild(0).gameObject;

            if(neighborPlant.TryGetComponent<Radar>(out Radar radar)){
                // if already in a combination then pass
                if(radar.combinationTarget){
                    return PlantCode.NoPlant;
                }
                else{
                    return PlantCode.Radar;
                }

            }
            if(neighborPlant.TryGetComponent<SingleTurret>(out SingleTurret singleTurret)){
                // if already in a combination then pass
                if(singleTurret.combinationTarget){
                    return PlantCode.NoPlant;
                }
                else{
                    return PlantCode.Turret;
                }
            }
        }
        return 0;
    }

    protected virtual bool CheckNeighborsCombine(GameObject neighbor1, GameObject neighbor2)
    {
        int radarCount = GetComponent<Radar>() ?  1 : 0;
        int turretCount = GetComponent<SingleTurret>() ?  1 : 0;

        // get plant kind on neighbor 1
        PlantCode check1 = CheckNeighbors(neighbor1);
        if(check1 == PlantCode.Radar){
            radarCount++;
        } else if(check1 == PlantCode.Turret){
            turretCount++;
        } else{
            return false;
        }

        // get plant kind on neighbor 2
        PlantCode check2 = CheckNeighbors(neighbor2);
        if(check2 == PlantCode.Radar){
            radarCount++;
        } else if(check2 == PlantCode.Turret){
            turretCount++;
        } else{
            return false;
        }

        // get plant kind this combination will change into
        if(turretCount == 3){
            transferCode = TransferCode.FourShotTurret;      
        } else if(turretCount==2 && radarCount==1){
            transferCode = TransferCode.ThreeWayTurret;
        } else if(turretCount==1 && radarCount==2){
            transferCode = TransferCode.SlowTurret;
        } else if(radarCount == 3){
            transferCode = TransferCode.LargeRadar;
        } 
        /*else{
            Debug.Log("no combination");
        }*/


        targetObject1 = neighbor1;
        targetObject2 = neighbor2;

        neighbor1.transform.GetChild(0).gameObject.GetComponent<Turret>().combinationTarget = transform.parent.gameObject;
        neighbor2.transform.GetChild(0).gameObject.GetComponent<Turret>().combinationTarget = transform.parent.gameObject;

        return true;
    }

    public virtual bool CheckCombiantion()
    { 
        if(!combinationTarget && transferCode == TransferCode.None){
            if(CheckNeighborsCombine(upBox, downBox) == false && CheckNeighborsCombine(leftBox, rightBox) == false){
                return false;
            }
        }

        return true;
    }

    // ================================================================================
}
