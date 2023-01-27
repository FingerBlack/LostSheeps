using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class PlayerController : MonoBehaviour

{
    
    private Tilemap groundTilemap;

    private PlayerMove controls;

    private void Awake()
    {
        controls = new PlayerMove();
        
        

    }
    private void OnEnable()
    {controls.Enable();

    }
    
    private void OnDisable()
    {
        controls.Disable();
    }
    
    void Start()
    {controls.Box.Move.performed += ctx =>
        {
            transform.Translate(ctx.ReadValue<Vector2>());

        };

    }
    
    private void Move(Vector2 direction)
    {
        
            transform.position += (Vector3)direction;
    }
    private bool CanMove (Vector2 direction)

    {
        return true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
