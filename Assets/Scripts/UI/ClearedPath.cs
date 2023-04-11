using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearedPath : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject entrance;
    SpriteRenderer sp;

    void Start()
    {
        sp = gameObject.GetComponent<SpriteRenderer>();
        sp.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        LevelFloor lf = entrance.GetComponent<LevelFloor>();
        if(lf.status == 2)
        {
            sp.enabled = true;
        }
        else{
            sp.enabled = false;
        }
    }
}
