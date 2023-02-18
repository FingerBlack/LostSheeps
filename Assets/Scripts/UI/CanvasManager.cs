using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
public class CanvasManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject homePanel;
    public GameObject valuePanel;
    public GameObject ui;
    public GameObject cardPanel;
    public GameObject cardPanel1;
    public GameObject nextLevel;
    public GameObject restart;
    public float timeCount;
    private PlayerControl playerControl;
    public bool ifStart;
    public bool ifEnd; 
    public bool ifRestart; 
    private Button startButton;
    void Start()
    {   
        timeCount=0f;
        ifStart=false;
        ifEnd=false;
        homePanel=transform.GetChild(0).gameObject;
        startButton=homePanel.transform.GetChild(0).gameObject.GetComponent<Button>();
        startButton.onClick.AddListener(OnClick);
        valuePanel=transform.GetChild(1).gameObject;
        ui=valuePanel.transform.GetChild(0).gameObject;
        cardPanel=transform.GetChild(2).gameObject;
        cardPanel1=transform.GetChild(3).gameObject;
        nextLevel=transform.GetChild(4).gameObject;
        restart=transform.GetChild(5).gameObject;
        playerControl=GameObject.Find("Player").GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {   
        if(timeCount>299.5f){
            ifEnd=true;
        }
        if(ifStart)
            timeCount+=Time.deltaTime;
        if(ifEnd){
            ifStart=false;
            nextLevel.SetActive(true);
            valuePanel.SetActive(false);
            cardPanel.SetActive(false);
            cardPanel1.SetActive(false);
        }else if(ifRestart){
            ifStart=false;
            restart.SetActive(true);
            valuePanel.SetActive(false);
            cardPanel.SetActive(false);
            cardPanel1.SetActive(false);
        }
        // if (Input.anyKey)
        // {
        //     homePanel.SetActive(false);
        //     valuePanel.SetActive(true);
        //     cardPanel.SetActive(true);
        //     cardPanel1.SetActive(true);
        // }
        if(playerControl.plant==playerControl.pea){
            cardPanel.GetComponent<UnityEngine.UI.Image>().color= new Color(1f,1f,1f,1f);
            cardPanel1.GetComponent<UnityEngine.UI.Image>().color= new Color(0.5f,0.5f,0.5f,1f);
        }else if(playerControl.plant==playerControl.cherry){
            cardPanel.GetComponent<UnityEngine.UI.Image>().color= new Color(0.5f,0.5f,0.5f,1f);
            cardPanel1.GetComponent<UnityEngine.UI.Image>().color= new Color(1f,1f,1f,1f);
        }
        TMP_Text uiDisplay = ui.GetComponent<TMP_Text>();
        // TMP_Text peaSeedNumberDisplay = peaSeedNumber.GetComponent<TMP_Text>();
        // TMP_Text cherrySeedNumberDisplay = cherrySeedNumber.GetComponent<TMP_Text>();
        uiDisplay.text="HP: "+ playerControl.HP.ToString()+"      Timer: "+TimeSpan.FromSeconds(300f-timeCount).ToString(@"mm\:ss")
        +"      Turrent Component: "+playerControl.peaNumber.ToString()
        +"      Radar Component: "+playerControl.cherryNumber.ToString();
    }
    void OnClick(){
        //Debug.Log("Press the Button");
        homePanel.SetActive(false);
        valuePanel.SetActive(true);
        cardPanel.SetActive(true);
        cardPanel1.SetActive(true);
        ifStart=true;
    }
}
