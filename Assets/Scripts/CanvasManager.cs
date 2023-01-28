using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            GetComponent<Canvas>().enabled=false;
            GameObject.Find("Main Camera").GetComponent<cameraControl>().enabled=true;
        }
    }
}
