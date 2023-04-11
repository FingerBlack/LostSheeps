using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightUpTurret : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer FourShotTurret;
    public SpriteRenderer ThreeWayTurret;
    public SpriteRenderer SixteenTurret;
    public SpriteRenderer Turret;
    public SpriteRenderer LargeRadar;
    public SpriteRenderer Radar;
    public HomeCanvas homeCanvas;
    void Start()
    {
        homeCanvas=GameObject.Find("HomeCanvas").GetComponent<HomeCanvas>();
    }

    // Update is called once per frame
    void Update()
    {
        Color color=new Color(1,1,1,1);
        if(homeCanvas.levels[3]==1){
            Turret.color=color;
        }
        if(homeCanvas.levels[4]==1){
            Radar.color=color;
        }
        if(homeCanvas.levels[5]==1){
            FourShotTurret.color=color;
        }
        if(homeCanvas.levels[6]==1){
            ThreeWayTurret.color=color;
        }
        if(homeCanvas.levels[7]==1){
            SixteenTurret.color=color;
        }
        if(homeCanvas.levels[8]==1){
            LargeRadar.color=color;
        }
    }
}
