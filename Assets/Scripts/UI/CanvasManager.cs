using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject homePanel;
    public GameObject valuePanel;
    public GameObject componentCounterPanel;
    public GameObject componentCounterText;
    public GameObject ui;
    public GameObject cardPanel;
    public GameObject cardPanel1;
    public GameObject nextLevel;
    public GameObject restart;
    public GameObject menu;
    public GameObject occupiedFloors;
    public float timeCount;
    private PlayerControl playerControl;
    public bool ifStart;
    public bool ifEnd; 
    public bool ifRestart; 
    public bool ifTimeCount; 
    public float timeNeed;
    private Button startButton;
    private bool wasInCapture;
    public HomeCanvas homeCanvas;
    void Start()
    {   
        //timeNeed=300f;
        homeCanvas=GameObject.Find("HomeCanvas").GetComponent<HomeCanvas>();
        timeCount=0f;
        wasInCapture = false;
        //set timeNeed
        ifTimeCount =false;
        ifStart=false;
        ifEnd=false;
        homePanel=transform.GetChild(0).gameObject;
        startButton=homePanel.transform.GetChild(0).gameObject.GetComponent<Button>();
        startButton.onClick.AddListener(OnClick);
        valuePanel=transform.GetChild(1).gameObject;
        ui=valuePanel.transform.GetChild(1).gameObject;
        cardPanel=transform.GetChild(2).gameObject;
        cardPanel1=transform.GetChild(3).gameObject;
        nextLevel=transform.GetChild(4).gameObject;
        restart=transform.GetChild(5).gameObject;
        menu=transform.GetChild(6).gameObject;
        componentCounterPanel=transform.GetChild(7).gameObject;
        componentCounterText=componentCounterPanel.transform.GetChild(1).gameObject;
        playerControl=GameObject.Find("Player").GetComponent<PlayerControl>();
        occupiedFloors=GameObject.Find("OccupiedFloors");
    }

    // Update is called once per frame
    void Update()
    {   
        if(timeCount>timeNeed-0.2f){
            PlayingStats.onLevelSuccess();
            ifEnd=true;
        }
        if(ifStart){
            
            foreach (Transform i in occupiedFloors.transform){
                if(i.gameObject.GetComponent<OccupiedFloor>().isOccupied){
                    timeCount+=Time.deltaTime;
                    wasInCapture = true;
                    break;
                }
            }
            if (wasInCapture)
            {
                wasInCapture = false;
            }
            else
            {
                timeCount -= Mathf.Max(0f, timeCount - Time.deltaTime);
            }
        }
        
        if(ifEnd){//success
            int level=(int.Parse( SceneManager.GetActiveScene().name));
            homeCanvas.levels[level]=1;
            if(level+1<homeCanvas.levels.Count)
                if( homeCanvas.levels[level+1]==0)
                    homeCanvas.levels[level+1]=2;
            ifStart =false;
            nextLevel.SetActive(true);
            //valuePanel.SetActive(false);
            cardPanel.SetActive(false);
            cardPanel1.SetActive(false);
            menu.SetActive(false);
            componentCounterPanel.SetActive(false);
            
        }else if(ifRestart){//fail
            
            ifStart =false;
            restart.SetActive(true);
            //valuePanel.SetActive(false);
            cardPanel.SetActive(false);
            cardPanel1.SetActive(false);
            menu.SetActive(false);
            componentCounterPanel.SetActive(false);

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
        
        
        //TMP_Text uiDisplay = ui.GetComponent<TMP_Text>();
        // TMP_Text peaSeedNumberDisplay = peaSeedNumber.GetComponent<TMP_Text>();
        // TMP_Text cherrySeedNumberDisplay = cherrySeedNumber.GetComponent<TMP_Text>();
        //uiDisplay.text="Capture Time: "+TimeSpan.FromSeconds(Mathf.Min(timeNeed-timeCount, timeNeed)).ToString(@"mm\:ss")
        //+"      \nComponent: "+playerControl.seedNumber.ToString();

        TMP_Text componentAmount = componentCounterText.GetComponent<TMP_Text>();
        componentAmount.text="× " + playerControl.seedNumber.ToString();

    }
    void OnClick(){
        //Debug.Log("Press the Button");
        PlayingStats.onLevelStart();
        homePanel.SetActive(false);
        //valuePanel.SetActive(true);
        if(SceneManager.GetActiveScene().name!="Home"){
            cardPanel.SetActive(true);
            cardPanel1.SetActive(true);
            menu.SetActive(true);
            componentCounterPanel.SetActive(true);
        }
        
        ifStart=true;
    }
}
