using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlaytimeData 
{
    public string userID;
    public string start;
    public string end;
    public string level;
    public string status;
    public string recordID;
    public PlaytimeData(string userID,string sceneName)
    {
        this.userID = userID;
        this.level = sceneName ;
        this.recordID = PlayingStats.recordID;

    }
    
}
