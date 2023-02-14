using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathData
{
    
    public string userID;
    public string time;
    public int deathCount;

    public DeathData(string userID,string time)
    {   
       
        this.deathCount = 1;
        this.userID = userID;
        this.time = time;

    }
}
