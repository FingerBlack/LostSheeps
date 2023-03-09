using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlantData
{
    // Start is called before the first frame update
    public string userID;
    public string plantName;
    public string levelTime;
    public string level;
    public string eventTime;
    public string recordID;
    public PlantData(string userID,string plantName,string levelTime,string level)
    {
        this.level = level;
        this.levelTime = levelTime;
        this.userID = userID;
        this.eventTime = PlayingStats.printDate(System.DateTime.Now);
        this.plantName = plantName;
        this.recordID = PlayingStats.recordID;
    }
    
}
