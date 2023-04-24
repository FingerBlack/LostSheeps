using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
public class HomeCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    public List<int> levels=new List<int>{2,0,0,0,0,0,0};
    public List<bool> shouldDialogueAppears;
    public CanvasManager canvasManager;
    public static HomeCanvas instance;
    public bool firstWin;
    public bool isInStoryLine;

    void Start()
    {   
        firstWin = true;
        isInStoryLine = true;

        //
        //timeNeed=300f;
        //canvasManager=GameObject.Find("Canvas").GetComponent<CanvasManager>();

        // timeCount=0f;
        // wasInCapture = false;
        // //set timeNeed
        // ifTimeCount =false;
        // ifStart=true;
        // ifEnd=false;
        // homePanel=transform.GetChild(0).gameObject;
        // startButton=homePanel.transform.GetChild(0).gameObject.GetComponent<Button>();
        // startButton.onClick.AddListener(OnClick);
        // valuePanel=transform.GetChild(1).gameObject;
        // ui=valuePanel.transform.GetChild(0).gameObject;
        // cardPanel=transform.GetChild(2).gameObject;
        // cardPanel1=transform.GetChild(3).gameObject;
        // nextLevel=transform.GetChild(4).gameObject;
        // restart=transform.GetChild(5).gameObject;
        // playerControl=GameObject.Find("Player").GetComponent<PlayerControl>();
        // occupiedFloors=GameObject.Find("OccupiedFloors");
    }

    // Update is called once per frame
    void Update()
    {   
        /*if(Input.GetKeyDown(KeyCode.L)){
            for(int i=0;i<10;i++){
                levels[i] = 1;
            }
        }*/

        //canvasManager.ifStart=true;
        // if(timeCount>timeNeed-0.2f){
        //     PlayingStats.onLevelSuccess();
        //     ifEnd=true;
        // }
        // if(ifStart){
            
        //     foreach (Transform i in occupiedFloors.transform){
        //         if(i.gameObject.GetComponent<OccupiedFloor>().isOccupied){
        //             timeCount+=Time.deltaTime;
        //             wasInCapture = true;
        //             break;
        //         }
        //     }
        //     if (wasInCapture)
        //     {
        //         wasInCapture = false;
        //     }
        //     else
        //     {
        //         timeCount -= Mathf.Max(0f, timeCount - Time.deltaTime);
        //     }
        // }
        
        // if(ifEnd){//success
            
        //     ifStart =false;
        //     nextLevel.SetActive(true);
        //     valuePanel.SetActive(false);
        //     cardPanel.SetActive(false);
        //     cardPanel1.SetActive(false);
            
        // }else if(ifRestart){//fail
            
        //     ifStart =false;
        //     restart.SetActive(true);
        //     valuePanel.SetActive(false);
        //     cardPanel.SetActive(false);
        //     cardPanel1.SetActive(false);
        // }
        // if (Input.anyKey)
        // {
        //     homePanel.SetActive(false);
        //     valuePanel.SetActive(true);
        //     cardPanel.SetActive(true);
        //     cardPanel1.SetActive(true);
        // }
        // if(playerControl.plant==playerControl.pea){
        //     cardPanel.GetComponent<UnityEngine.UI.Image>().color= new Color(1f,1f,1f,1f);
        //     cardPanel1.GetComponent<UnityEngine.UI.Image>().color= new Color(0.5f,0.5f,0.5f,1f);
        // }else if(playerControl.plant==playerControl.cherry){
        //     cardPanel.GetComponent<UnityEngine.UI.Image>().color= new Color(0.5f,0.5f,0.5f,1f);
        //     cardPanel1.GetComponent<UnityEngine.UI.Image>().color= new Color(1f,1f,1f,1f);
        // }
        // TMP_Text uiDisplay = ui.GetComponent<TMP_Text>();
        // // TMP_Text peaSeedNumberDisplay = peaSeedNumber.GetComponent<TMP_Text>();
        // // TMP_Text cherrySeedNumberDisplay = cherrySeedNumber.GetComponent<TMP_Text>();
        // uiDisplay.text="HP: "+ playerControl.HP.ToString()+"      Capture Time: "+TimeSpan.FromSeconds(Mathf.Min(timeNeed-timeCount, timeNeed)).ToString(@"mm\:ss")
        // +"      Turrent Component: "+playerControl.peaNumber.ToString()
        // +"      Radar Component: "+playerControl.cherryNumber.ToString();
    }
    private void Awake()
    {
        // start of new code
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
