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
    private Vector3Int origCellPos;
    // Vector3 origCellCenterWorldPos;
    // Vector3 origCellWorldPos;
    // private Collider2D coll;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Box Initiate");

        floorGrid = GameObject.Find("Grid").GetComponent<Grid>();

        // coll = gameObject.GetComponent<Collider2D>();
        // Debug.Log(collider.transform.position);
        // Debug.Log(transform.position);

        origCellPos = floorGrid.WorldToCell(transform.position);
        Debug.Log(origCellPos);
        // origCellWorldPos = floorGrid.CellToLocal(origCellPos);
        // origCellCenterWorldPos = floorGrid.GetCellCenterWorld(origCellPos);
        // Debug.Log(origCellWorldPos);
        // Debug.Log(origCellCenterWorldPos);
        // transform.position = origCellCenterWorldPos;
        transform.position = floorGrid.GetCellCenterWorld(origCellPos);;
    }

    // Update is called once per frame
    void Update()
    {   
        Vector3Int direction=new Vector3Int(0,0,0);
        if(!isMoving&&(action=="move")){
            if(playerDirection=="West"){
                direction=new Vector3Int(-1,0,0);
            }else if(playerDirection=="East"){
                direction=new Vector3Int(1,0,0);
            }else if(playerDirection=="North"){
                direction=new Vector3Int(0,1,0);       
            }else if(playerDirection=="South"){
                direction=new Vector3Int(0,-1,0);
            }
            StartCoroutine(SlowMove(direction));
            action="None";

            direction=new Vector3Int(0,0,0);
        }   
    }

    // this is the move function exposed to other class
    // public void Move(Vector3Int direction)
    // {
    //     // 2 different type of move effect, QuickMove moves in 1 frame, SlowMove moves continuously
    //     // QuickMove(direction);
    //     StartCoroutine(SlowMove(direction));
    // }
    // use translate to move box in 1 frame
    private void QuickMove(Vector3Int direction)
    {
        Debug.Log("Start Move");
        Vector3Int targetCellPos = origCellPos + direction;
        Vector3 targetPos = floorGrid.GetCellCenterWorld(targetCellPos);
        
        // Debug.Log(Physics2D.OverlapBox(targetPos, new Vector2(0.5f, 0.25f), 0f));
        if(!Physics2D.OverlapBox(targetPos, new Vector2(0.3f, 0.1f), 0f))
        {
            origCellPos = targetCellPos;
            transform.Translate(targetPos - transform.position);
            Debug.Log(targetPos);
            // Debug.Log(Physics2D.OverlapBox(targetPos, new Vector2(0.4f, 0.2f), 0f, blockLayer));
            // Debug.Log("Something in target area!");
        }
    }

    // use IEnumerator to move continuously to target position
    private IEnumerator SlowMove(Vector3Int direction)
    {
        Vector3Int targetCellPos = origCellPos + direction;
        Vector3 targetPos = floorGrid.GetCellCenterWorld(targetCellPos);
        ContactFilter2D filter = new ContactFilter2D().NoFilter();
        List<Collider2D> results = new List<Collider2D>();
        int length=Physics2D.OverlapBox(targetPos, new Vector2(0.3f, 0.1f), 0f,filter,results);
        Debug.Log(results);
        bool isOccupied=false;
        foreach( Collider2D result in results)
        {
            
            if(result.gameObject.TryGetComponent<Box>(out Box box)){
                isOccupied=true;
                break;
            }else if(result.gameObject.TryGetComponent<Enemy>(out Enemy enemy)){
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

            isMoving = false;    
                    // box.playerDirection=direction;
                    // box.action="move";
        }
        


           
        
        
    }
    // a simple collider test, stop move when hit enemy 

}
