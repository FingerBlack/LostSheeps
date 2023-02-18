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
    private GameObject plant;
    private string plantKind;
    void Start()
    { 
        floorGrid = GameObject.Find("Grid").GetComponent<Grid>();
        plant=transform.GetChild(0).gameObject;
        if(plant.TryGetComponent<PlantPea>(out PlantPea plantPea)){
            plantKind="PlantPea";
        }else if(plant.TryGetComponent<PlantCherry>(out PlantCherry plantCherry)){
            plantKind="PlantCherry";   
        }

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
        ContactFilter2D filter = new ContactFilter2D().NoFilter();
        List<Collider2D> results = new List<Collider2D>();
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
            if(plantKind=="PlantPea"){
                 PlantPea plantPea=plant.GetComponent<PlantPea>().CheckNeighbors();
                 plantPea.check
            }
            isMoving = false;    
        }     
    }
}
