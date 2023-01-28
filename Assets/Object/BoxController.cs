using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoxController : MonoBehaviour
{
    [SerializeField]
    private Tilemap groundTilemap;
    [SerializeField]
    private Tilemap colTilemap;

    private BoxMove controls;
    // Start is called before the first frame update
    private void Awake()
    {
        controls = new BoxMove();
    }
    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();

    }
    void Start()
    {
        controls.Box.Move.performed += ctx => Move(ctx.ReadValue<Vector2>());
    }
    private void Move(Vector2 direction)

    {
        if (CanMove(direction))
        {
            transform.position += (Vector3)direction;
        }

    }

    private bool CanMove(Vector2 direction)
    {
        Vector3Int gridPosition = groundTilemap.WorldToCell(transform.position + (Vector3)direction);
        if (!groundTilemap.HasTile(gridPosition))
            return false;
        return true;
    }
    

}
