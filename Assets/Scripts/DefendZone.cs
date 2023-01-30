using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DefendZone : MonoBehaviour
{

    public float defendTime = 10;
    public float timeRemaining;
    public bool timeIsRunning = false;
    public TMP_Text timeText;
    // Start is called before the first frame update
    void Start()
    {
        timeIsRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                float minutes = Mathf.FloorToInt(timeRemaining / 60);
                float seconds = Mathf.FloorToInt(timeRemaining % 60);
                timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
                Debug.Log(string.Format("{0:00}:{1:00}", minutes, seconds));
            } 
            else
            {
                timeText.text = "Victory !";
                Debug.Log("Victory !");
                timeIsRunning = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // if no target and a new enemy appear inside the range, target on it 
        //Debug.Log(collider.name);
        if(collider.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            timeRemaining = defendTime;
        }
    }
}
