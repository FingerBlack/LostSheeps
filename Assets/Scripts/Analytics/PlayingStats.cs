using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using UnityEngine.SceneManagement;
using System;
public class PlayingStats : MonoBehaviour
{
    public static User user;
    public static DateTime startTime;
    public static DateTime endTime;
    public static string currentSceneName;
    private string recordID;
    private PlaytimeData playtimeData;
    // Start is called before the first frame update
    void Start()

    {
        recordID = System.Guid.NewGuid().ToString();
        currentSceneName = SceneManager.GetActiveScene().name ;
        user = new User();
        startTime = System.DateTime.Now;
        playtimeData = new PlaytimeData(user.userID, printDate(startTime), currentSceneName);
        StartCoroutine(ExecuteEveryOneSecond());
        
}

    // Update is called once per frame
    void Update()
    {

        
    }





    private IEnumerator ExecuteEveryOneSecond()
    {
        while (true)
        {
            // Call your function here
            playingTime_updateEndTime();

            // Wait for one second before executing the next iteration
            yield return new WaitForSeconds(1);
        }
    }

    //update every 1 second
    void playingTime_updateEndTime()
    {
        endTime = System.DateTime.Now;
        playtimeData.end = printDate(endTime);
        RestClient.Put("https://lostsheeps-26b16-default-rtdb.firebaseio.com/" + "playTime/" + recordID + ".json", playtimeData);

    }



    public static void deathCount()
    {
        User user = PlayingStats.user;
        Debug.Log(user.userID);
       
        DeathData data = new DeathData(user.userID, printDate(System.DateTime.Now),currentSceneName, PlayingStats.getDuration());


        RestClient.Put("https://lostsheeps-26b16-default-rtdb.firebaseio.com/" + "deathData/" + System.Guid.NewGuid().ToString() + ".json", data);
    }

    public static string printDate(DateTime d)
    {
        return d.ToString("yyyy-MM-dd HH:mm:ss");
    }

    // get the running time of current level
    public static string getDuration()
    {
        TimeSpan timeDifference = PlayingStats.endTime - PlayingStats.startTime;
        return timeDifference.TotalSeconds.ToString();
    }
    public static void plantCount(string plantName)
    {

        PlantData d = new PlantData(PlayingStats.user.userID, plantName, PlayingStats.getDuration(), PlayingStats.currentSceneName);
        RestClient.Put("https://lostsheeps-26b16-default-rtdb.firebaseio.com/" + "plantData/" + System.Guid.NewGuid().ToString() + ".json", d);
    }
}
