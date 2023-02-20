using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User 
{
    public string userID;
    

    public  User()
    {
        this.userID = System.Guid.NewGuid().ToString();
        
         
    }

}
