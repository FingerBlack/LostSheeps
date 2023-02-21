using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfPress2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            Destroy(gameObject);
        }
    }
}
