using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public int HP=100;
    public GameObject Player;
    public float movementSpeed=0.05f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position=Vector3.MoveTowards(transform.position,Player.transform.position,movementSpeed*Time.deltaTime);
    }
}
