using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public GameObject testBox;
    public GameObject boxPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // sample test input to move box, w a s d for 4 directions
        if(Input.GetKeyDown(KeyCode.W))
        {
            testBox.GetComponent<Box>().Move(Vector3Int.up);
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            testBox.GetComponent<Box>().Move(Vector3Int.left);
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            testBox.GetComponent<Box>().Move(Vector3Int.down);
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            testBox.GetComponent<Box>().Move(Vector3Int.right);
        }

        // sample test to create a box on target position
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Instantiate(boxPrefab, new Vector3(1.2f, -3.7f, 0), Quaternion.identity);
        }
    }
}
