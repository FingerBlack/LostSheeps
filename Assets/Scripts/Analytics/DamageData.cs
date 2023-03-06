using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageData
{
    // Start is called before the first frame update
    public string userID;
    public string recordID;
    public string source;
    public string damage;
    public string target;
    public string levelTime;
    public string level;
    public string eventTime;
    
    public DamageData(string userID, string levelTime, string level,string recordID,string source,string damage,string target)
    {

        this.userID = userID;
        this.recordID = recordID;
        this.source = source;
        this.damage = damage;
        this.target = target;
        this.level = level;
        this.levelTime = levelTime;
        
        this.eventTime = PlayingStats.printDate(System.DateTime.Now);
        

    }
}
