using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfPressSpace : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject box;
    private Box boxComponent;
    private Vector3 position;
    void Start()
    {
        boxComponent=box.GetComponent<Box>();
        position=boxComponent.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(boxComponent.transform.position!=position){
            Destroy(gameObject);
        }
    }
}
