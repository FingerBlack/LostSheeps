using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfPressE : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject box;
    private Box boxComponent;
   
    void Start()
    {
        boxComponent=box.GetComponent<Box>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(boxComponent.transform.childCount!=0){
            Destroy(gameObject);
        }
    }
}
