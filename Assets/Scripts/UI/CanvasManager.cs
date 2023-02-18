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
    public GameObject occupiedFloors;
    public float timeCount;
    private PlayerControl playerControl;
    public bool ifStart;
    public bool ifEnd; 
    public bool ifRestart; 
    public bool ifTimeCount; 
    public float timeNeed;
    private Button startButton;

    void Start()
    {   
        //timeNeed=300f;
        timeCount=0f;
        //set timeNeed
        ifTimeCount=false;
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
        occupiedFloors=GameObject.Find("OccupiedFloors");
    }

    // Update is called once per frame
    void Update()
    {   
        
        if(timeCount>timeNeed-0.2f){
            ifEnd=true;
        }
        if(ifStart){
            foreach(Transform i in occupiedFloors.transform){
                if(i.gameObject.GetComponent<OccupiedFloor>().isOccupied){
                    timeCount+=Time.deltaTime;
                    break;
                }
            }
        }
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
        uiDisplay.text="HP: "+ playerControl.HP.ToString()+"      Timer: "+TimeSpan.FromSeconds(timeNeed-timeCount).ToString(@"mm\:ss")
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
