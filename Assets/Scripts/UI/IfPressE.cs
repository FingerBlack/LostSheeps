using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfPressE : MonoBehaviour
{
    // Start is called before the first frame update
    
    public List<GameObject> boxList;
    private Box boxComponent;
    public GameObject switchPrompt;

    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < boxList.Count; i++)
        {
            boxComponent = boxList[i].GetComponent<Box>();
            if(boxComponent.transform.childCount!=0){
                switchPrompt.SetActive(true);
                Destroy(gameObject);
                
            }
           
        }

        
    }
}
