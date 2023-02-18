using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantsData
{
    // Start is called before the first frame update
    public string userID;
    public string time;
    public int plant0;
    public int plant1;
    public int plant2;
    public int plant3;
    public int plant4;
    public int totalPlants;
    public PlantsData(string userID,string time,int totalPlants,int plant0,int plant1,int plant2,int plant3,int plant4)
    {   
       
        this.userID = userID;
        this.time = time;
        this.totalPlants = totalPlants;
        this.plant0 = plant0;
        this.plant1 = plant1;
        this.plant2 = plant2;
        this.plant3 = plant3;
        this.plant4 = plant4;
    }
    
}
