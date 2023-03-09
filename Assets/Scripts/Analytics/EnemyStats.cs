using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemyStats
{   
    public string eventTime;
    public string enemyNumber;
    public List<Vector3> enemyLocation;
    public string levelTime;
    public string level;
    public string recordID;
    public EnemyStats(string enemyNumber, List<Vector3> enemyLocation, string levelTime, string level)
    {
        this.levelTime = levelTime;
        this.level = level;
        this.enemyNumber = enemyNumber;
        this.enemyLocation = enemyLocation;
        this.eventTime= PlayingStats.printDate(System.DateTime.Now);
        this.recordID = PlayingStats.recordID;
    }
}
