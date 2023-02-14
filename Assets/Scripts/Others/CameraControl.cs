using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
public class CameraControl : MonoBehaviour
{
    public GameObject player;
    public Timer timer;
    public static User user;
    
    // Start is called before the first frame update
    void Start()
    {
        user = new User();
        timer = new Timer(user.userID);
        StartCoroutine(ExecuteEveryOneSecond());
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 camOffset = new Vector3(0,0,-10);
        transform.position = player.transform.position + camOffset;
    }
     private IEnumerator ExecuteEveryOneSecond()
    {
        while (true)
        {
            // Call your function here
            updateEndTime();

            // Wait for one second before executing the next iteration
            yield return new WaitForSeconds(1);
        }
    }

    //update every 1 second
    void updateEndTime()
    {
        timer.end = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        
        RestClient.Put("https://lostsheeps-26b16-default-rtdb.firebaseio.com/" + "playTime/" +user.userID + ".json", timer);
        
    }
}
