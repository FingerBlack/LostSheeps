using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarSeed : Seed
{
    void Start()
    {
        base.Init();

    }

    void Update()
    {
        if(lifeTimer > 10.0f){
            Destroy(gameObject);
        }
    }
}
