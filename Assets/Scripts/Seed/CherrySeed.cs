using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherrySeed : MonoBehaviour
{
    // Start is called before the first frame update
    private float timeCount;
    void Start()
    {
        timeCount=0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeCount>10f){
            Destroy(gameObject);
        }
    }
}
