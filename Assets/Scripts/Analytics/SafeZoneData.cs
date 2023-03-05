using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZoneData
{
    // Start is called before the first frame update
    public string userID;
    public string level;
    public string eventTime;
    public string timeInSafeZone;

    public SafeZoneData(string userID, string level, string timeInSafeZone)
    {
        this.level = level;
        this.timeInSafeZone = timeInSafeZone;
        this.userID = userID;
        this.eventTime = PlayingStats.printDate(System.DateTime.Now);
        

    }
}
