using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class CanvasManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject homePanel;
    public GameObject valuePanel;
    public GameObject timer;
    public GameObject peaSeedNumber;
    public GameObject cherrySeedNumber;
    public float timeCount;
    void Start()
    {   
        timeCount=0f;
        homePanel=transform.GetChild(0).gameObject;
        valuePanel=transform.GetChild(1).gameObject;
        timer=valuePanel.transform.GetChild(0).gameObject;
        peaSeedNumber=valuePanel.transform.GetChild(2).gameObject;
        cherrySeedNumber=valuePanel.transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {   
        timeCount+=Time.deltaTime;
        if (Input.anyKey)
        {
            homePanel.SetActive(false);
            valuePanel.SetActive(true);
        }
        TMP_Text timerDisplay = timer.GetComponent<TMP_Text>();
        TMP_Text peaSeedNumberDisplay = peaSeedNumber.GetComponent<TMP_Text>();
        TMP_Text cherrySeedNumberDisplay = cherrySeedNumber.GetComponent<TMP_Text>();
        timerDisplay.text="Timer: "+TimeSpan.FromSeconds(1200.0-timeCount).ToString(@"mm\:ss");
        peaSeedNumberDisplay.text="Pea Seed: "+GameObject.Find("Player").GetComponent<PlayerControl>().peaNumber.ToString();
        cherrySeedNumberDisplay.text="Cherry Seed: "+GameObject.Find("Player").GetComponent<PlayerControl>().cherryNumber.ToString();
    }
}
