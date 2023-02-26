using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushData 
{
    // Start is called before the first frame update
    public string userID;
    
    public string levelTime;
    public string level;
    public string eventTime;
    public PushData(string userID,string levelTime,string level)
    {
        this.level = level;
        this.levelTime = levelTime;
        this.userID = userID;
        this.eventTime = PlayingStats.printDate(System.DateTime.Now);
      
    }
}
