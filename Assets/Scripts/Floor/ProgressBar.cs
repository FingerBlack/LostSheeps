using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    public SpriteRenderer progressBar; // reference to the square sprite renderer
    public CanvasManager canvasManager; // reference to the CanvasManager script
    public float barHeight = 0f; // the initial width of the progress bar
    public float captureTime = 3.0f;
    public float percentage;
    public float maxhei = 4.4f;
    void Start()
    {
        canvasManager=GameObject.Find("Canvas").GetComponent<CanvasManager>();
        progressBar = GetComponent<SpriteRenderer>();
         // set the color of the progress bar to green
        barHeight = 3.5f;
    }

    void Update()
    {   
        if(!canvasManager.ifStart){
            return;
        }
        progressBar.color = Color.green;
        percentage = Mathf.Clamp(canvasManager.timeCount ,0 , captureTime) / captureTime; // calculate the progress percentage
        barHeight= maxhei * percentage; // calculate the new width of the progress bar
        //Debug.Log("this is height right now" + barHeight);
        //progressBar.size = new Vector2( progressBar.size.x, barHeight; // set the new width of the progress bar
        Vector3 newScale = progressBar.transform.localScale;
        newScale.y = barHeight;
        progressBar.transform.localScale = newScale;
    }
}
