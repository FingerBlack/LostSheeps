using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer 
{
    public string userID;
    public string start;
    public string end;
    public Timer(string userID)
    {
        this.userID = userID;
        this.start = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
    
}