using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantCherry : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    public bool ifmove;
    public float speed;
    void Start()
    {   
        target=null;
        speed=1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(target){
            float dis=Vector3.Distance(transform.parent.transform.position,target.transform.position);
            if(dis<0.1f){
                target.GetComponent<Box>().HP=100f;
                target.transform.GetChild(0).gameObject.GetComponent<PlantPea>().HP+=100f;
                target.transform.GetChild(0).gameObject.GetComponent<PlantPea>().shootPeriod/=1.1f;
                target.transform.GetChild(0).gameObject.GetComponent<PlantPea>().bulletSpeed*=2f;
                Destroy(transform.parent.gameObject);
            }
            transform.parent.position=Vector3.MoveTowards(transform.parent.position,target.transform.position,speed*Time.deltaTime);
            
        }
    }
}
