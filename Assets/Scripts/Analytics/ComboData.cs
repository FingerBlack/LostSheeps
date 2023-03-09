using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboData 
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    public string userID;
    public string comboName;
    public string levelTime;
    public string level;
    public string eventTime;
    public string recordID;
    public ComboData(string userID, string comboName, string levelTime, string level)
    {
        this.level = level;
        this.levelTime = levelTime;
        this.userID = userID;
        this.eventTime = PlayingStats.printDate(System.DateTime.Now);
        this.comboName = comboName;
        this.recordID = PlayingStats.recordID;
    }

}
