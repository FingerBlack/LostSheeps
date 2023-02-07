using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
    
    public int peaNumber;
    public int cherryNumber;
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
    private ContactFilter2D filter; // Collider Detect Tools.
    private List<Collider2D> results;// Collider Detect Tools.
    
    public Vector3Int playerDirection; // 0 up; 1 up-right; 2 right; 3 down-right; 4 down; 5 down-left; 6 left; 7 up-left;


    public Vector3Int playerGridPos;
    public Vector3Int targetrGridPos;
    public Vector3 targetWorldPos;


    //=============================================================================================================
    // Start is called before the first frame update
    void Start()
    {      
        isMoving=false;
        action="None";
        timeToMove=0.2f;
        peaNumber=3; //initiate the peaNumber.
        cherryNumber=2; ////initiate the cherryNumber.
        horiSpeed =5f; //Remember to Set the Speed on Start 
        vertSpeed =2.5f;  //Remember to Set the Speed on Start 
        filter = new ContactFilter2D().NoFilter(); //initiate the Collider Detect Tools.
        results = new List<Collider2D>(); //initiate the Collider Detect Tools.
        
        pea=Resources.Load("Prefabs/Pea") as GameObject; //Load Plant pea
        cherry=Resources.Load("Prefabs/Cherry") as GameObject; //Load Plant cherry
        plant=pea;   
        HP=10000f; // Set HP 
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
        // Movement Input
        horiInput = Input.GetAxis("Horizontal");
        vertInput = Input.GetAxis("Vertical");
        horiSpeed =5f; //Remember to Set the Speed on Start 
        vertSpeed =2.5f;  //Remember to Set the Speed on Start 
    //=============================================================================================================
    // Hp
        if(HP<0){
            Destroy(gameObject); 
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
        if(Input.GetKeyDown(KeyCode.F)){  // Whatif press the F.
            
            Physics2D.OverlapCircle(targetWorldPos, 0.1f,filter, results);
            foreach( Collider2D result in results)
            {
                if(result.gameObject.TryGetComponent<Box>(out Box box)){
                    box.direction=playerDirection;
                    box.action="move";
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.E)){ // Whatif press the E.
            Physics2D.OverlapCircle(targetWorldPos, 0.1f,filter, results);
            foreach( Collider2D result in results)
            {
                if(result.gameObject.TryGetComponent<Box>(out Box box)){
                    if(box.transform.childCount==0){
                        if(plant==pea&&peaNumber>0){
                            GameObject obj=Instantiate(plant, result.gameObject.transform.position-new Vector3(0f,0.001f,0f),Quaternion.identity,result.gameObject.transform);
                            peaNumber-=1;
                        }
                        if(plant==cherry&&cherryNumber>0){
                            GameObject obj=Instantiate(plant, result.gameObject.transform.position-new Vector3(0f,0.001f,0f),Quaternion.identity,result.gameObject.transform);
                            cherryNumber-=1;
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
            if(result.gameObject.TryGetComponent<PeaSeed>(out PeaSeed peaSeed)){
                peaNumber+=1;
                Destroy(result.gameObject);
            }
            if(result.gameObject.TryGetComponent<CherrySeed>(out CherrySeed cherrySeed)){
                cherryNumber+=1;
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
