using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombinable
{
    protected GameObject parentObject {get; set;}
    [Tooltip("move towards combination target if exists")]
    public GameObject combinationTarget {get; set;}
    public float combinationSpeed {get; set;}

    [Tooltip("combination neighbor 1")]
    protected GameObject targetObject1 {get; set;}
    [Tooltip("combination neighbor 2")]
    protected GameObject targetObject2 {get; set;}

    [Tooltip("possible tranform turrets")]
    public GameObject[] transferGameObject {get; set;}

    // the plant combination will change into
    protected enum TransferCode { None = 0, LargeRadar = 1, ThreeWayTurret = 2, SlowTurret = 3 };
    protected TransferCode transferCode {get; set;}

    // enum code for readability
    protected enum PlantCode { NoPlant = 0, Turret = 1, Radar = 2 };

    protected void InitCombinable();
    /*{
        combinationSpeed = 1.0f;
        transferCode = TransferCode.None;
    }*/

    // ============================== combination methods ==============================
    protected PlantCode CheckNeighbors(GameObject neighbor); // 0 no plants, 1 radar, 2 Turret
    /*{ 
        if(neighbor && neighbor.transform.childCount == 1){
            GameObject neighborPlant = neighbor.transform.GetChild(0).gameObject;

            if(neighborPlant.TryGetComponent<PlantCherry>(out PlantCherry radar)){
                if(radar.combinationTarget){
                    return PlantCode.NoPlant;
                }
                else{
                    return PlantCode.Turret;
                }

            }
            if(neighborPlant.TryGetComponent<SingleTurret>(out SingleTurret singleTurret)){
                if(singleTurret.combinationTarget){
                    return PlantCode.NoPlant;
                }
                else{
                    return PlantCode.Turret;
                }
            }
        }
        return 0;
    }*/

    // combination methods
    protected bool CheckNeighborsCombine(GameObject neighbor1, GameObject neighbor2);
    /*{
        int radarCount = 0;
        int turretCount = 1;

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
            transferCode = TransferCode.LargeRadar;      
        } else if(turretCount==2 && radarCount==1){
            transferCode = TransferCode.ThreeWayTurret;
        } else if(turretCount==1 && radarCount==2){
            transferCode = TransferCode.SlowTurret;
        }


        targetObject1 = neighbor1;
        targetObject2 = neighbor2;

        neighbor1.transform.GetChild(0).gameObject.GetComponent<ICombinable>().combinationTarget = parentObject;
        neighbor2.transform.GetChild(0).gameObject.GetComponent<ICombinable>().combinationTarget = parentObject;

        return true;
    }*/

    protected bool CheckCombiantion();
    /*{ 
        if(!combinationTarget && transferCode == TransferCode.None){
            if(CheckNeighborsCombine(upBox, downBox) == false && CheckNeighborsCombine(leftBox, rightBox) == false){
                return false;
            }
        }

        return true;
    }*/

    protected void TryCreateUpgradeTurret();
    /*{
        /*float dis = Vector3.Distance(transform.parent.transform.position, targetObject1.transform.position);
        if(dis < 0.01f){
            if(transferCode == TransferCode.LargeRadar){
                PlayingStats.comboCount(transferGameObject[0].name);
                Instantiate(transferGameObject[0], transform.position, Quaternion.identity, transform.parent);
            }   
            if(transferCode== TransferCode.ThreeWayTurret){
                PlayingStats.comboCount(transferGameObject[1].name);
                Instantiate(transferGameObject[1], transform.position, Quaternion.identity, transform.parent);
            }
            if(transferCode== TransferCode.SlowTurret){
                PlayingStats.comboCount(transferGameObject[2].name);
                Instantiate(transferGameObject[2], transform.position, Quaternion.identity, transform.parent);
            }

            Destroy(targetObject1.gameObject);
            Destroy(targetObject2.gameObject);
            Destroy(gameObject);
        }
    }*/
}
