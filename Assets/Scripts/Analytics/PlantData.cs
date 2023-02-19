using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlantData
{
    // Start is called before the first frame update
    public string userID;
    public string plantName;
    public string plantTime;
    public string level;
    public PlantData(string userID,string plantName,string plantTime,string level)
    {
        this.level = level;
        this.plantTime = plantTime;
        this.userID = userID;
        
        this.plantName = plantName;
    }
    
}
