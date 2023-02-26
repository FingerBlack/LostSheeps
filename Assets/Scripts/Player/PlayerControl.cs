using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Proyecto26;
public class PlayerControl : MonoBehaviour
{
    //======================================== variables ============================================================
    // Dont Initiat the Value here plz.
    private Grid floorGrid; // Grid Map
    public GameObject plant;
    public GameObject pea;  // Plant Kind
    public GameObject cherry;  // Plant Kind
    public GameObject horiBox;
    public GameObject vertBox;
    
    public int seedNumber;
    public float horiInput;
    public float vertInput;
    public float horiDis;
    public float vertDis;
    public float horiSpeed;
    public float vertSpeed;
    public float dX;
    public float dY;
    public float HP;
    public float timeToMove;
    public string action;
    public bool isMoving;
    public string attackedBy;

    private ContactFilter2D filter; // Collider Detect Tools.
    private List<Collider2D> results;// Collider Detect Tools.
    
    public Vector3Int playerDirection; // 0 up; 1 up-right; 2 right; 3 down-right; 4 down; 5 down-left; 6 left; 7 up-left;

    public CanvasManager canvasManager;
    public Vector3Int playerGridPos;
    public Vector3Int targetrGridPos;
    public Vector3 targetWorldPos;


    //=============================================================================================================
    // Start is called before the first frame update
    void Start()
    {      
        canvasManager=GameObject.Find("Canvas").GetComponent<CanvasManager>();
        isMoving=false;
        action="None";
        timeToMove=0.2f;
        seedNumber=0; //initiate the peaNumber.
       
        horiSpeed =5f; //Remember to Set the Speed on Start 
        vertSpeed =2.5f;  //Remember to Set the Speed on Start 
        filter = new ContactFilter2D().NoFilter(); //initiate the Collider Detect Tools.
        results = new List<Collider2D>(); //initiate the Collider Detect Tools.
        
        pea=Resources.Load("Prefabs/Turret/Turret") as GameObject; //Load Plant pea
        cherry=Resources.Load("Prefabs/Buff/Radar") as GameObject; //Load Plant cherry
        plant=pea;   
        HP=1000f; // Set HP 
        floorGrid = GameObject.Find("Grid").GetComponent<Grid>(); // initate the Map
        playerGridPos = floorGrid.WorldToCell(transform.position); //Find the Player position in GridSpace
        transform.position=floorGrid.GetCellCenterWorld(playerGridPos);
        playerDirection = new Vector3Int(0, -1, 0); //Set default direction
        dX=0f; //initiate the direction
        dY=-1f; //initiate the direction
        targetrGridPos = playerGridPos + new Vector3Int(0, -1, 0); //initiate the target position in Grid Space
        targetWorldPos=floorGrid.GetCellCenterWorld(targetrGridPos); //initiate the target position in World Space
    }
    //=============================================================================================================
    // Update is called once per frame
    void Update()
    {   
        if(!canvasManager.ifStart){
            return;
        }
        // Movement Input
        horiInput = Input.GetAxis("Horizontal");
        vertInput = Input.GetAxis("Vertical");
        horiSpeed =5f; //Remember to Set the Speed on Start 
        vertSpeed =2.5f;  //Remember to Set the Speed on Start 
    //=============================================================================================================
    // Hp
        if(HP<0){
            transform.eulerAngles=new Vector3(0, 0, 90f);
            canvasManager.ifRestart=true;
            PlayingStats.onLevelFail();

            PlayingStats.deathCount(attackedBy);
        }
    //=============================================================================================================
    // Facing direction;
        if (horiInput > 0) 
        {   

            playerDirection = new Vector3Int(1, 0, 0);
            targetrGridPos = playerGridPos + new Vector3Int(1, 0, 0);
            targetWorldPos = floorGrid.GetCellCenterWorld(targetrGridPos); // Set the target position in grid space;
            
        }
        else if(horiInput < 0)
        {

            playerDirection =new Vector3Int(-1, 0, 0);
            targetrGridPos = playerGridPos + new Vector3Int(-1, 0, 0);
            targetWorldPos = floorGrid.GetCellCenterWorld(targetrGridPos);

        }
        else if(vertInput>0)
        {
            playerDirection =new Vector3Int(0, 1, 0);
            targetrGridPos = playerGridPos + new Vector3Int(0, 1, 0);
            targetWorldPos = floorGrid.GetCellCenterWorld(targetrGridPos);
            
        }else if(vertInput<0){
            playerDirection =new Vector3Int(0, -1, 0);
            targetrGridPos = playerGridPos + new Vector3Int(0, -1, 0);
            targetWorldPos = floorGrid.GetCellCenterWorld(targetrGridPos);

        }

    //=============================================================================================================
    // All Input setting are here, learn these code and expand these codes in the future.
    //=============================================================================================================
        

        playerGridPos = floorGrid.WorldToCell(transform.position); //Find the Player position in Grid Space

        if(horiInput!=0){
            if(!isMoving){
                StartCoroutine(SlowMove(new Vector3Int(horiInput>0?1:-1,0,0)));
                action="None";
            }   
            //}            
        }else
        if(vertInput!=0){
            if(!isMoving){
                StartCoroutine(SlowMove(new Vector3Int(0,vertInput>0?1:-1,0)));
                action="None";
            }             
        }

        
        //=============================================================================================================
        // Other Input
        if(Input.GetKeyDown(KeyCode.Space)){  // Whatif press the F.
            
            Physics2D.OverlapCircle(targetWorldPos, 0.1f,filter, results);
            foreach( Collider2D result in results)
            {
                if(result.gameObject.TryGetComponent<Box>(out Box box)){
                    box.direction=playerDirection;
                    box.action="move";
                }
                // if(result.gameObject.TryGetComponent<Wall>(out Wall wall)){
                //     wall.direction=playerDirection;
                //     wall.action="move";
                // }
            }
        }
        if(Input.GetKeyDown(KeyCode.E)){ // Whatif press the E.
            Physics2D.OverlapCircle(targetWorldPos, 0.1f,filter, results);
            foreach( Collider2D result in results)
            {
                if(result.gameObject.TryGetComponent<Box>(out Box box)){
                    if(box.transform.childCount==0){
                        if(plant==pea&&seedNumber>0){
                            GameObject obj=Instantiate(plant, result.gameObject.transform.position-new Vector3(0f,0.001f,0f),Quaternion.identity,result.gameObject.transform);
                            seedNumber-=1;
                            PlayingStats.plantCount(NamingConstant.Plant1);
                            box.CheckNeighbors();
                            //Debug.Log(NamingConstant.Plant1);
                        }
                        if(plant==cherry&&seedNumber>0){
                            GameObject obj=Instantiate(plant, result.gameObject.transform.position-new Vector3(0f,0.001f,0f),Quaternion.identity,result.gameObject.transform);
                            seedNumber-=1;
                            PlayingStats.plantCount(NamingConstant.Plant2);
                            box.CheckNeighbors();
                            //Debug.Log(NamingConstant.Plant2);
                        }
                    }
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha1)){  //Whatif press the #1.
            plant=pea;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){ //Whatif press the #2.
            plant=cherry;

        }
        //=============================================================================================================
        // Collider for detect seeds.
        //=============================================================================================================
        Physics2D.OverlapCircle(transform.position, 0.2f,filter, results);
        foreach( Collider2D result in results)
        {
            if(result.gameObject.TryGetComponent<Seed>(out Seed seed)){
                seedNumber+=1;
                Destroy(result.gameObject);
            }
            if(result.gameObject.TryGetComponent<Seed>(out Seed cherrySeed)){
                seedNumber+=1;
                Destroy(result.gameObject);
            }
        }
    }
    private IEnumerator SlowMove(Vector3Int direction)
    {
        Vector3Int targetCellPos = playerGridPos + direction;
        Vector3 targetPos = floorGrid.GetCellCenterWorld(targetCellPos);
        ContactFilter2D filter = new ContactFilter2D().NoFilter();
        List<Collider2D> results = new List<Collider2D>();
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
            }else if(result.gameObject.TryGetComponent<Enemy>(out Enemy enemy)){
                isOccupied=true;
                break;
            }else if(result.gameObject.TryGetComponent<Wall>(out Wall wall)){
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
            playerGridPos = targetCellPos;
            isMoving = false;    
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
