using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickData
{
    
    public string userID;
    public string pickedItem;
    public string levelTime;
    public string level;
    public string eventTime;
    public PickData(string userID, string pickedItem, string levelTime, string level)
    {
        this.level = level;
        this.levelTime = levelTime;
        this.userID = userID;
        this.eventTime = PlayingStats.printDate(System.DateTime.Now);
        this.pickedItem = pickedItem;
    }
}
