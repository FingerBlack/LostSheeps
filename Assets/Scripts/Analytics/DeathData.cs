using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathData
{
    
    public string userID;
    public string duration;
    public int deathCount;
    public string level;
    private string time;
    

    public DeathData(string userID,string time,string level,string duration)
    {
        this.level=level;
        this.deathCount = 1;
        this.userID = userID;
        this.duration = duration;
        this.time = PlayingStats.printDate(System.DateTime.Now);
    }
}
