using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private Grid floorGrid;
    public bool isMoving = false;
    public float timeToMove = 0.2f;
    public LayerMask blockLayer;
    public string playerDirection;
    public string action;
    public bool stand;
    public float HP;
    public Vector3Int direction;
    private Vector3Int origCellPos;
    private ContactFilter2D filter; // Collider Detect Tools.
    private List<Collider2D> results;// Collider Detect Tools.
    private string plantKind;
    void Start()
    { 
        floorGrid = GameObject.Find("Grid").GetComponent<Grid>();
        filter = new ContactFilter2D().NoFilter(); //initiate the Collider Detect Tools.
        results = new List<Collider2D>(); //initiate the Collider Detect Tools.

        origCellPos = floorGrid.WorldToCell(transform.position);
        transform.position = floorGrid.GetCellCenterWorld(origCellPos);
    }

    // Update is called once per frame
    void Update()
    {  
        if(!isMoving&&(action=="move")){
            StartCoroutine(SlowMove(direction));
            action="None";
        }   
    }

    // use IEnumerator to move continuously to target position
    private IEnumerator SlowMove(Vector3Int direction)
    {
        Vector3Int targetCellPos = origCellPos + direction;
        Vector3 targetPos = floorGrid.GetCellCenterWorld(targetCellPos);

        Physics2D.OverlapCircle(targetPos, 0.1f,filter,results);
        bool isOccupied=false;
        foreach( Collider2D result in results)
        {
            if(result.gameObject.TryGetComponent<Box>(out Box box)){
                isOccupied=true;
                break;
            }else if(result.gameObject.TryGetComponent<Wall>(out Wall wall)){
                isOccupied=true;
                break;
            }
            else if(result.gameObject.TryGetComponent<Enemy>(out Enemy enemy)){
                isOccupied=true;
                break;
            }                
        }
        if(!isOccupied){
            isMoving = true;
            float elapsedTime = 0;
            Vector3 origPos = transform.position;
            while(elapsedTime < timeToMove){
                transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime / timeToMove));
                elapsedTime += Time.deltaTime;
                yield return null;
            }            
            transform.position = targetPos;
            origCellPos = targetCellPos;
            if(!GetPlant(gameObject)){
               // Debug.Log("1");
                if(GetResult(floorGrid.GetCellCenterWorld(origCellPos+new Vector3Int(1,0,0)))){
               //     Debug.Log("2");
                }else if(GetResult(floorGrid.GetCellCenterWorld(origCellPos+new Vector3Int(-1,0,0)))){
               //     Debug.Log("3");
                }else if(GetResult(floorGrid.GetCellCenterWorld(origCellPos+new Vector3Int(0,1,0)))){
               //     Debug.Log("4");
                }else if(GetResult(floorGrid.GetCellCenterWorld(origCellPos+new Vector3Int(0,-1,0)))){
               //     Debug.Log("5");
                }
            }
            isMoving = false;    
        }     
    }
    private bool GetPlant(GameObject box){
        string plantKind="";
        if(box.transform.childCount==0){
            return false;
        }        
        GameObject plant=box.transform.GetChild(0).gameObject;
        // if(!plant){
        //     return false;
        // }

        if(plant.TryGetComponent<PlantPea>(out PlantPea plantPea)){
            plantKind="PlantPea";
        }
        if(plant.TryGetComponent<PlantCherry>(out PlantCherry plantCherry)){
            plantKind="PlantCherry";   
        }
        if(plantKind=="PlantPea"){
            //PlantPea plantPea=plant.GetComponent<PlantPea>();
            if(plantPea.CheckNeighbors()){
                //Debug.Log("0.1");
                return true;
            }
        }else if(plantKind=="PlantCherry"){
            //PlantCherry plantCherry=plant.GetComponent<PlantCherry>();
            if(plantCherry.CheckNeighbors()){
               // Debug.Log("0.2");
                return true;
            }
        }
        return false;
    }
    private bool GetResult(Vector3 position){
        Physics2D.OverlapCircle(position, 0.1f,filter, results);
        foreach( Collider2D result in results)
        {
            if(result.gameObject.TryGetComponent<Box>(out Box box)){
            
                return GetPlant(result.gameObject);
            }
        }
        return false;
    }
}
