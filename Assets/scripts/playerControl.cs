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

    // 0 up; 1 up-right; 2 right; 3 down-right; 4 down; 5 down-left; 6 left; 7 up-left;
    public int playerDirection = 2;

    //tilemap var
    public Vector2 playerPos;
    public Vector3Int playerGridPos;
    public Vector3Int targetrGridPos;
    public Tilemap map;


    //=============================================================================================================

    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        //movement
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
            if(dY > 0)
            {
                playerDirection = 1;
                targetrGridPos = playerGridPos + new Vector3Int(1,1,0);
}
            else if(dY == 0)
            {
                playerDirection = 2;
                targetrGridPos = playerGridPos + new Vector3Int(1, 0, 0);
            }
            else
            {
                playerDirection = 3;
                targetrGridPos = playerGridPos + new Vector3Int(1, -1, 0);
            }
        }
        else if(dX == 0)
        {
            if (dY > 0)
            {
                playerDirection = 0;
                targetrGridPos = playerGridPos + new Vector3Int(0, 1, 0);
            }
            else if (dY == 0)
            {
                // no change
            }
            else
            {
                playerDirection = 4;
                targetrGridPos = playerGridPos + new Vector3Int(0, -1, 0);
            }
        }
        else
        {
            if (dY > 0)
            {
                playerDirection = 7;
                targetrGridPos = playerGridPos + new Vector3Int(-1, 1, 0);
            }
            else if (dY == 0)
            {
                playerDirection = 6;
                targetrGridPos = playerGridPos + new Vector3Int(-1, 0, 0);
            }
            else
            {
                playerDirection = 5;
                targetrGridPos = playerGridPos + new Vector3Int(-1, -1, 0);
            }
        }



    }

 
}
