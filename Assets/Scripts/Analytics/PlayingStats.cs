using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
// class involved PlayerControl Enemy1 Enemy
public class PlayingStats : MonoBehaviour
{
    
    public static User user = new User();
    public static DateTime startTime;
    
    public static DateTime endTime;
    public static string currentSceneName;
    public static string recordID;
    public static PlaytimeData playtimeData;
    
    
    // Start is called before the first frame update
    void Start()

    {
        recordID = System.Guid.NewGuid().ToString();
        currentSceneName = SceneManager.GetActiveScene().name ;
        
        Debug.Log(user.userID);


        

        
        

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



        CanvasManager.totalTimeinSafeZone = 0;
        CoroutineRunner.StartMyCoroutine();

    }

    public static void onLevelFail()
    {

        
        playtimeData.end = printDate(System.DateTime.Now);
        playtimeData.status = "Fail";
        RestClient.Put("https://lostsheeps-26b16-default-rtdb.firebaseio.com/" + "playTime/" + recordID + ".json", playtimeData);
        CoroutineRunner.StopMyCoroutine();
        sendSafeZoneTime();
    }

    public static void onLevelSuccess()
    {


        playtimeData.end = printDate(System.DateTime.Now);
        playtimeData.status = "Success";
        RestClient.Put("https://lostsheeps-26b16-default-rtdb.firebaseio.com/" + "playTime/" + recordID + ".json", playtimeData);

        CoroutineRunner.StopMyCoroutine();
        sendSafeZoneTime();

    }

    public static void sendSafeZoneTime()
    {
        
        SafeZoneData d = new SafeZoneData(PlayingStats.user.userID, PlayingStats.currentSceneName,CanvasManager.totalTimeinSafeZone.ToString());
        RestClient.Put("https://lostsheeps-26b16-default-rtdb.firebaseio.com/" + "safeZoneTime/" + recordID + ".json", d);
        




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
        TimeSpan timeDifference = System.DateTime.Now - PlayingStats.startTime;
        return timeDifference.TotalSeconds.ToString();
    }
    public static void plantCount(string plantName)
    {

        PlantData d = new PlantData(PlayingStats.user.userID, plantName, PlayingStats.getDuration(), PlayingStats.currentSceneName);
        RestClient.Put("https://lostsheeps-26b16-default-rtdb.firebaseio.com/" + "plantData/" + System.Guid.NewGuid().ToString() + ".json", d);
    }

    public static void pushCount()
    {

        PushData d = new PushData(PlayingStats.user.userID,  PlayingStats.getDuration(), PlayingStats.currentSceneName);
        RestClient.Put("https://lostsheeps-26b16-default-rtdb.firebaseio.com/" + "pushData/" + System.Guid.NewGuid().ToString() + ".json", d);
    }


    public static void pickCount(string pickedItem)
    {

        PickData d = new PickData(PlayingStats.user.userID, pickedItem, PlayingStats.getDuration(), PlayingStats.currentSceneName);
        RestClient.Put("https://lostsheeps-26b16-default-rtdb.firebaseio.com/" + "pickData/" + System.Guid.NewGuid().ToString() + ".json", d);
    }


    public static void comboCount(string comboName)
    {
        User user = PlayingStats.user;
        

        ComboData data = new ComboData(user.userID, comboName,  PlayingStats.getDuration(),currentSceneName);


        RestClient.Put("https://lostsheeps-26b16-default-rtdb.firebaseio.com/" + "comboData/" + System.Guid.NewGuid().ToString() + ".json", data);
    }


    public static void damageToEnemy(string source,string damage, string target)
    {
        User user = PlayingStats.user;


        DamageData data = new DamageData(user.userID, PlayingStats.getDuration(), PlayingStats.currentSceneName,PlayingStats.recordID,source,damage,target);


        RestClient.Put("https://lostsheeps-26b16-default-rtdb.firebaseio.com/" + "damageData/" + System.Guid.NewGuid().ToString() + ".json", data);
    }

    public static List<Vector3> enemyCount()
    {
        GameObject[] enemyLayerObjects = FindObjectsOfType<GameObject>()
         .Where(obj => obj.layer == 7)
         .ToArray();
        int enemyNumber = enemyLayerObjects.Length;
        List<Vector3> posList = new List<Vector3>();
        for (int i = 0; i < enemyNumber; i++)
        {
            posList.Add(enemyLayerObjects[i].transform.position);
        }
       
        return posList;
    }
}
