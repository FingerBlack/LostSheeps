using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class Text1 : MonoBehaviour
{
    // Start is called before the first frame update
    public CanvasManager canvasManager;
    void Start()
    {
        canvasManager=GameObject.Find("Canvas").GetComponent<CanvasManager>();
    }

    // Update is called once per frame
    void Update()
    {
        TMP_Text display = GetComponent<TMP_Text>();
        // TMP_Text peaSeedNumberDisplay = peaSeedNumber.GetComponent<TMP_Text>();
        // TMP_Text cherrySeedNumberDisplay = cherrySeedNumber.GetComponent<TMP_Text>();
        display.text="You have been Top "+ ((100-canvasManager.timeCount/3f+1)).ToString("#.00")+"%.";
    }
    void OnClick(){
        
    }
}
