using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathData
{
    
    public string userID;
    public string levelTime;
    public int deathCount;
    public string level;
    public string eventTime;
    public string attackedBy;
    public string recordID;

    public DeathData(string userID,string level,string levelTime,string attackedBy)
    {
        this.level=level;
        this.deathCount = 1;
        this.userID = userID;
        this.levelTime = levelTime;
        this.eventTime = PlayingStats.printDate(System.DateTime.Now);
        this.attackedBy = attackedBy;
        this.recordID = PlayingStats.recordID;
    }
}
