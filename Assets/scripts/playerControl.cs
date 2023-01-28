using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class playerControl : MonoBehaviour
{
    //======================================== variables ============================================================
    //movement var
    public float horiInput;
    public float vertInput;
    public float horiSpeed = 5;
    public float vertSpeed = 5;
    public float dX;
    public float dY;
    public float HP;
    private GameObject ColliderDetect;
    private ColliderDetect colliderdetect;
    // 0 up; 1 up-right; 2 right; 3 down-right; 4 down; 5 down-left; 6 left; 7 up-left;
    public string playerDirection;

    //tilemap var
    public Vector2 playerPos;
    public Vector3Int playerGridPos;
    public Vector3Int targetrGridPos;
    public Tilemap map;


    //=============================================================================================================

    // Start is called before the first frame update

    void Start()
    {       
        HP=100;
        ColliderDetect=GameObject.Find("/Player/ColliderDetect");
        colliderdetect=ColliderDetect.GetComponent<ColliderDetect>();
        playerPos.x = transform.position.x;
        playerPos.y = transform.position.y;
        playerGridPos = map.WorldToCell(playerPos);
        playerDirection = "South";
        targetrGridPos = playerGridPos + new Vector3Int(0, -1, 0);
        Vector3 WorldtargetGridPos=map.CellToWorld(targetrGridPos);
        ColliderDetect.transform.position=new Vector3(WorldtargetGridPos.x+0.27f,WorldtargetGridPos.y+0.14f,WorldtargetGridPos.z);
        colliderdetect.direction=playerDirection;
    }

    // Update is called once per frame
    void Update()
    {


        //movement
        if(HP<0){
            Destroy(gameObject);
        }
        horiInput = Input.GetAxis("Horizontal");
        vertInput = Input.GetAxis("Vertical");
        dX = horiSpeed * Time.deltaTime  * horiInput;
        dY = vertSpeed * Time.deltaTime  * vertInput;
        transform.Translate(dX * Vector2.right);
        transform.Translate(dY * Vector2.up);

        //player position to grid
        playerPos.x = transform.position.x;
        playerPos.y = transform.position.y;
        playerGridPos = map.WorldToCell(playerPos);

        //lock rotation
        transform.rotation = Quaternion.Euler(0, 0, 0);


        //facing direction;
        if (dX > 0)
        {   

            playerDirection = "East";
            targetrGridPos = playerGridPos + new Vector3Int(1, 0, 0);
            Vector3 WorldtargetGridPos=map.CellToWorld(targetrGridPos);
            ColliderDetect.transform.position=new Vector3(WorldtargetGridPos.x+0.22f,WorldtargetGridPos.y+0.13f,WorldtargetGridPos.z);
            colliderdetect.direction=playerDirection;
        }
        else if(dX < 0)
        {

            playerDirection = "West";
            targetrGridPos = playerGridPos + new Vector3Int(-1, 0, 0);
            Vector3 WorldtargetGridPos=map.CellToWorld(targetrGridPos);
            ColliderDetect.transform.position=new Vector3(WorldtargetGridPos.x+0.30f,WorldtargetGridPos.y+0.13f,WorldtargetGridPos.z);
            colliderdetect.direction=playerDirection;
        }
        else if(dY>0)
        {
            playerDirection = "North";
            targetrGridPos = playerGridPos + new Vector3Int(0, 1, 0);
            Vector3 WorldtargetGridPos=map.CellToWorld(targetrGridPos);
            ColliderDetect.transform.position=new Vector3(WorldtargetGridPos.x+0.27f,WorldtargetGridPos.y+0.08f,WorldtargetGridPos.z);
            colliderdetect.direction=playerDirection;
        }else if(dY<0){
            playerDirection = "South";
            targetrGridPos = playerGridPos + new Vector3Int(0, -1, 0);
            Vector3 WorldtargetGridPos=map.CellToWorld(targetrGridPos);
            ColliderDetect.transform.position=new Vector3(WorldtargetGridPos.x+0.27f,WorldtargetGridPos.y+0.14f,WorldtargetGridPos.z);
            colliderdetect.direction=playerDirection;
        }else{
            //playerDirection = "None";
            targetrGridPos = playerGridPos;
            //ColliderDetect.transform.position=transform.position;
            colliderdetect.direction=playerDirection;
        }



    }

 
}
