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
    public static float totalTimeinSafeZone;
    public GameObject navigation;

    private GameObject helpMenu;

    void Awake()
    {
        
        //helpMenu.SetActive(false);
    }
    void Start()
    {   
        //timeNeed=300f;
        helpMenu = transform.Find("HelpMenu1").gameObject;
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
        restart=transform.Find("Restart").gameObject;
        menu=transform.Find("menu").gameObject;
        componentCounterPanel=transform.Find("Component Counter").gameObject;
        componentCounterText=componentCounterPanel.transform.GetChild(1).gameObject;
        playerControl=GameObject.Find("Player").GetComponent<PlayerControl>();
        occupiedFloors=GameObject.Find("OccupiedFloors");
        //navigation.SetActive(false);

        OnClick();
    }


    void PauseGame()
    {
        Time.timeScale = 0f; // Set the time scale to zero to pause the game
    }

    void UnpauseGame()
    {
        Time.timeScale = 1f; // Set the time scale to one to unpause the game
    }
    

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (Time.timeScale == 0f)
            {
                UnpauseGame();
                helpMenu.SetActive(false);
            }
            else
            {
                
                PauseGame();
                helpMenu.SetActive(true);
                
            }
        }

        // if (ifStart)
        // {
        //     navigation.SetActive(true);
        // }
        // else
        // {
        //     navigation.SetActive(false);
        // }
        if (timeCount>timeNeed-0.2f){
            PlayingStats.onLevelSuccess();

            
             
                
            ifEnd =true;
        }
        if(ifStart){
            
            foreach (Transform i in occupiedFloors.transform){
                if(i.gameObject.GetComponent<OccupiedFloor>().isOccupied){
                    timeCount+=Time.deltaTime;

                    totalTimeinSafeZone += Time.deltaTime;
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
            if (level + 1 < homeCanvas.levels.Count)
            {
                //0-2 +1,3->5,4->8, 3&4->6&7
                /*                if (homeCanvas.levels[level + 1] == 0)
                                {
                                    homeCanvas.levels[level + 1] = 2;
                                }*/

                updateLevelScore();


            }

            ifStart =false;
            //nextLevel.SetActive(true);
            //valuePanel.SetActive(false);
            cardPanel.SetActive(false);
            cardPanel1.SetActive(false);
            menu.SetActive(false);
            componentCounterPanel.SetActive(false);
            SceneManager.LoadScene( "Home");
            
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


        if(playerControl.plant==playerControl.turret){
            cardPanel.GetComponent<UnityEngine.UI.Image>().color= new Color(1f,1f,1f,1f);
            cardPanel1.GetComponent<UnityEngine.UI.Image>().color= new Color(0.5f,0.5f,0.5f,1f);
        }else if(playerControl.plant==playerControl.radar){
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
        if (SceneManager.GetActiveScene().name!="Home"){
            cardPanel.SetActive(true);
            int level=(int.Parse( SceneManager.GetActiveScene().name));

            if(homeCanvas.levels[4]==1||level==4){
                cardPanel1.SetActive(true);
            }
            
            menu.SetActive(true);
            componentCounterPanel.SetActive(true);
        }
        
        ifStart=true;
    }

    void updateLevelScore()
    {
        update1to1Level(0, 1);
        update1to1Level(1, 2);

        update1to1Level(2, 3);
        update1to1Level(2, 4);

        update1to1Level(3, 5);
        update1to1Level(4, 8);
        update1to1Level(3, 9);
        update1to1Level(4, 9);

        update2to1Level(3, 4, 6);
        update2to1Level(3, 4, 7);



    }

    void update1to1Level(int pre, int act)
    {
        if (homeCanvas.levels[pre] == 1)
        {
            if (homeCanvas.levels[act] == 0)
            {
                homeCanvas.levels[act] = 2;
            }

        }
    }
    void update2to1Level(int pre,int pre2, int act)
    {
        if (homeCanvas.levels[pre] == 1 && homeCanvas.levels[pre2] == 1)
        {
            if (homeCanvas.levels[act] == 0)
            {
                homeCanvas.levels[act] = 2;
            }

        }
    }
}
