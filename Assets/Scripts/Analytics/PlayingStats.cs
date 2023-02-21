using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using UnityEngine.SceneManagement;
using System;

// class involved PlayerControl Enemy1 Enemy
public class PlayingStats : MonoBehaviour
{
    
    public static User user;
    public static DateTime startTime;
    public static DateTime currentTime;
    public static DateTime endTime;
    public static string currentSceneName;
    public static string recordID;
    public static PlaytimeData playtimeData;
    // Start is called before the first frame update
    void Start()

    {
        recordID = System.Guid.NewGuid().ToString();
        currentSceneName = SceneManager.GetActiveScene().name ;
        user = new User();
        
        

        StartCoroutine(ExecuteEveryOneSecond());
        
}

    // Update is called once per frame
    void Update()
    {
        

    }

    
    public static void onLevelStart()
    {
        
        recordID = System.Guid.NewGuid().ToString();
        startTime = System.DateTime.Now;
        playtimeData = new PlaytimeData(user.userID, currentSceneName);
        playtimeData.start = printDate(startTime);
        

    }

    public static void onLevelFail()
    {

        
        playtimeData.end = printDate(System.DateTime.Now);
        playtimeData.status = "Fail";
        RestClient.Put("https://lostsheeps-26b16-default-rtdb.firebaseio.com/" + "playTime/" + recordID + ".json", playtimeData);


    }

    public static void onLevelSuccess()
    {


        playtimeData.end = printDate(System.DateTime.Now);
        playtimeData.status = "Success";
        RestClient.Put("https://lostsheeps-26b16-default-rtdb.firebaseio.com/" + "playTime/" + recordID + ".json", playtimeData);


    }


    
    private IEnumerator ExecuteEveryOneSecond()
    {
        while (true)
        {
            // Call your function here
            playingTime_updateTime();

            // Wait for one second before executing the next iteration
            yield return new WaitForSeconds(1);
        }
    }

    //update every 1 second
    void playingTime_updateTime()
    {
        currentTime = System.DateTime.Now;
        Debug.Log(currentTime.ToString());
        Debug.Log(startTime.ToString());



    }
    


    public static void deathCount(string attackedBy)
    {
        User user = PlayingStats.user;
        Debug.Log(user.userID);
       
        DeathData data = new DeathData(user.userID,currentSceneName, PlayingStats.getDuration(),attackedBy);


        RestClient.Put("https://lostsheeps-26b16-default-rtdb.firebaseio.com/" + "deathData/" + System.Guid.NewGuid().ToString() + ".json", data);
    }


    public static string printDate(DateTime d)
    {
        return d.ToString("yyyy-MM-dd HH:mm:ss");
    }


    // get the running time of current level
    public static string getDuration()
    {
        TimeSpan timeDifference = PlayingStats.currentTime - PlayingStats.startTime;
        return timeDifference.TotalSeconds.ToString();
    }
    public static void plantCount(string plantName)
    {

        PlantData d = new PlantData(PlayingStats.user.userID, plantName, PlayingStats.getDuration(), PlayingStats.currentSceneName);
        RestClient.Put("https://lostsheeps-26b16-default-rtdb.firebaseio.com/" + "plantData/" + System.Guid.NewGuid().ToString() + ".json", d);
    }


    public static void comboCount(string comboName)
    {
        User user = PlayingStats.user;
        

        ComboData data = new ComboData(user.userID, comboName,  PlayingStats.getDuration(),currentSceneName);


        RestClient.Put("https://lostsheeps-26b16-default-rtdb.firebaseio.com/" + "comboData/" + System.Guid.NewGuid().ToString() + ".json", data);
    }
}
