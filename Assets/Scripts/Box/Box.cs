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
    public SpriteRenderer spriteRenderer;
    public Sprite targetedBox;
    public Sprite normalBox;
    // public GameObject targetedLayer;
    void Start()
    { 
        // targetedLayer.SetActive(false);
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
        PlayingStats.pushCount();
        Vector3Int targetCellPos = origCellPos + direction;
        Vector3 targetPos = floorGrid.GetCellCenterWorld(targetCellPos);

        Physics2D.OverlapCircle(targetPos, 0.1f,filter,results);
        bool isOccupied=false;
        foreach( Collider2D result in results)
        {   
            if(result.isTrigger){
                continue;
            }
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
            CheckNeighbors();
            isMoving = false;    
        }     
    }
    public void CheckNeighbors(){
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

        if(plant.TryGetComponent<SingleTurret>(out SingleTurret singleTurret)){
            plantKind = "turret";
        }
        if(plant.TryGetComponent<BuffTurret>(out BuffTurret buffTurret)){
            plantKind = "radar";   
        }
        if(plantKind == "turret"){
            if(singleTurret.CheckCombiantion()){
                return true;
            }
        }else if(plantKind == "radar"){
            if(buffTurret.CheckCombiantion()){
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

    public void setTargeted(bool targeted){
        if(targeted){
            // targetedLayer.SetActive(true);
            spriteRenderer.sprite=targetedBox;
        } else{
            // targetedLayer.SetActive(false);
            spriteRenderer.sprite=normalBox;
        }
    }
}
