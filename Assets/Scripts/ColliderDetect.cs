using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDetect : MonoBehaviour
{
    // Start is called before the first frame update
    public string direction;
    private ContactFilter2D filter;
    private List<Collider2D> results;
    private Collider2D coll;
    public GameObject Plant;
    public GameObject bbox;
    void Start()
    {
        filter = new ContactFilter2D().NoFilter();
        results = new List<Collider2D>();
        coll=GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {   
        bool isOccupied=false;
        int length=Physics2D.OverlapCollider(coll,filter,results);
        if (length>0){
            
            foreach(Collider2D result in results)
            {   
                // if(result.name!="Plants"&&result.name!="Player")
                //     Debug.Log(length+" "+ result.name);
                if(result.gameObject.TryGetComponent<Box>(out Box box)){
                    isOccupied=true;
                    if(Input.GetKeyDown(KeyCode.F)){
                        box.playerDirection=direction;
                        box.action="move";
            
                    }else if(Input.GetKeyDown(KeyCode.E)&&result.gameObject.transform.childCount==0){
                        GameObject obj=Instantiate(Plant, result.gameObject.transform.position+new Vector3(0f,-0.15f,0f),Quaternion.identity,result.gameObject.transform);
                    }
                    // box.playerDirection=direction;
                    // box.action="move";
                }else if(result.gameObject.TryGetComponent<Enemy>(out Enemy enemy)){
                    isOccupied=true;
                    // if(Input.GetKeyDown(KeyCode.F)){
                    //     box.playerDirection=direction;
                    //     box.action="move";
            
                    // }else if(Input.GetKeyDown(KeyCode.E)&&result.gameObject.transform.childCount==0){
                    //     GameObject obj=Instantiate(Plant, result.gameObject.transform.position+new Vector3(0f,-0.15f,0f),Quaternion.identity,result.gameObject.transform);
                    // }
                    // box.playerDirection=direction;
                    // box.action="move";
                    continue;
                }

            }
            

        }
        if(!isOccupied&&Input.GetKeyDown(KeyCode.Q)){
                GameObject obj=Instantiate(bbox,transform.position,Quaternion.identity,GameObject.Find("/Boxes").transform);
            }
    }
    // void OnTriggerStay2D(Collider2D Other){
    //      if(Collider2D.OverlapCollider()){
    //         Debug.Log("Move");
    //         if(Input.GetKeyDown(KeyCode.F)){
    //             box.playerDirection=direction;
    //             box.action="Move";
            
    //         }


    //     }   

        
    // }
}
