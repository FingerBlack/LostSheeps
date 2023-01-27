using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private Grid floorGrid;
    public bool isMoving = false;
    public LayerMask blockLayer;
    Vector3Int origCellPos;
    // Vector3 origCellCenterWorldPos;
    // Vector3 origCellWorldPos;
    BoxCollider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Box Initiate");

        floorGrid = GameObject.Find("Grid").GetComponent<Grid>();

        collider = gameObject.GetComponent<BoxCollider2D>();
        Debug.Log(collider.transform.position);
        Debug.Log(transform.position);

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
        
    }

    // this is the move function exposed to other class
    public void Move(Vector3Int direction)
    {
        QuickMove(direction);
    }
    // use translate to move box in 1 frame
    private void QuickMove(Vector3Int direction)
    {
        Debug.Log("Start Move");
        Vector3Int targetCellPos = origCellPos + direction;
        Vector3 targetPos = floorGrid.GetCellCenterWorld(targetCellPos);
        
        // Debug.Log(Physics2D.OverlapBox(targetPos, new Vector2(0.5f, 0.25f), 0f));
        if(!Physics2D.OverlapBox(targetPos, new Vector2(0.3f, 0.1f), 0f, blockLayer))
        {
            origCellPos = targetCellPos;
            transform.Translate(targetPos - transform.position);
            Debug.Log(targetPos);
            // Debug.Log(Physics2D.OverlapBox(targetPos, new Vector2(0.4f, 0.2f), 0f, blockLayer));
            // Debug.Log("Something in target area!");
        }
    }

    // a simple collider test, stop move when hit enemy 
    void OnTriggerEnter2D(Collider2D col)
    {
        // Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        // spriteMove = -0.1f;
        Debug.Log("Collid!");
    }
}
