using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    public SpriteRenderer progressBar;      // reference to the square sprite renderer
    public CanvasManager canvasManager;     // reference to the CanvasManager script
    public float barHeight = 0f;            
    public float barWidth = 0f;             // the initial width of the progress bar
    public float captureTime = 3.0f;
    private float percentage;
    public float maxhei;
    public float maxwidth;
    Color newColor = new Color(0f, 1f, 0f, 0.5f);
    void Start()
    {
        canvasManager=GameObject.Find("Canvas").GetComponent<CanvasManager>();
        captureTime=canvasManager.timeNeed;
        progressBar = GetComponent<SpriteRenderer>();
    }

    void Update()
    {   
        if(!canvasManager.ifStart){
            return;
        }
        progressBar.color = newColor;
        percentage = Mathf.Clamp(canvasManager.timeCount ,0 , captureTime) / captureTime; // calculate the progress percentage
        barHeight= maxhei * percentage; // calculate the new width of the progress bar
        barWidth = maxwidth * percentage;
        Vector3 newScale = progressBar.transform.localScale;
        newScale.y = barHeight;
        newScale.x = barWidth;
        progressBar.transform.localScale = newScale;
    }
}
