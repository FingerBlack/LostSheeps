using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    public GameObject hintE;
    public GameObject hintSpace;
    private bool touchedE;
    private bool touchedSpace;
    private bool inArea;

    // Start is called before the first frame update
    void Start()
    {
        hintE.SetActive(false);
        hintSpace.SetActive(false);
        touchedE = false;
        touchedSpace = false;
        inArea = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("enter");
        if(inArea && !touchedE && !touchedSpace && Input.GetKeyDown(KeyCode.E)){
            hintE.SetActive(false);
            hintSpace.SetActive(true);
            touchedE = true;
        }

        if(inArea && touchedE && !touchedSpace && Input.GetKeyDown(KeyCode.Space)){
            hintSpace.SetActive(false);
            touchedSpace = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("enter");
        if(other.tag == "Player")
        {   
            inArea = true;
            if(!touchedE && !touchedSpace){
                hintE.SetActive(true);
                hintSpace.SetActive(false);
            } else if(touchedE && !touchedSpace){
                hintE.SetActive(false);
                hintSpace.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Player")
        {   
            inArea = false;
        }
    }
}
