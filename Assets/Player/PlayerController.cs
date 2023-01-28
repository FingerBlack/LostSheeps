using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour

{
    [SerializeField] private int speed = 5;
    private Vector2 movement;
    private Rigidbody2D rb;
    

    private void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }
    

    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
        

    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement *speed* Time.fixedDeltaTime);
    }
}
