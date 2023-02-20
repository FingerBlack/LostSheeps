using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
     //======================================== variables ============================================================
    // Dont Initiat the Value here plz.
    public float HP=5f;
    public float origHP = 5f;
    bool orig = false;
    public SpriteRenderer spriteRenderer;
    public Sprite newSprite;

    public GameObject Player;
    public float movementSpeed;
    public float Damage;
    public int isSlowed;
    public float slowDuration;
    public float slowedTime;
    public CanvasManager canvasManager;
    private float notSlowedSPeed;
    private float slowedSPeed;
    //=============================================================================================================
    // Start is called before the first frame update
    void Start()
    {
        // HP=5f;
        canvasManager=GameObject.Find("Canvas").GetComponent<CanvasManager>();
        Damage=40f;
        //movementSpeed;
        Player=GameObject.Find("Player");
        isSlowed = 0;
        slowDuration = 5;
        slowedTime = 0;
        notSlowedSPeed = 2.0f;
        slowedSPeed = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {   
        if(!canvasManager.ifStart){
            return;
        }
        if (isSlowed==1 )
        {
            if(slowedTime < slowDuration)
            {
                movementSpeed = slowedSPeed;
                slowedTime += Time.deltaTime;
            }
            else
            {
                slowedTime = 0;
                isSlowed = 0;
            }
        }
        else
        {
            movementSpeed = notSlowedSPeed;

        }

        if(HP<=0){
            if(orig){
                Debug.Log("second");
                Destroy(gameObject);
            } else{
                // turn into orginal enemy;
                Debug.Log("first");
                orig = true;
                HP = origHP;
                spriteRenderer.sprite = newSprite; 
            }
        }
        
        float distance=Vector3.Distance(transform.position,Player.transform.position);
        //Debug.Log(distance);
        if(distance<0.4f){
            Player.GetComponent<PlayerControl>().HP-=Damage;

            Player.GetComponent<PlayerControl>().attackedBy = GetType().Name;

        }

        transform.position=Vector3.MoveTowards(transform.position,Player.transform.position,movementSpeed*Time.deltaTime);
    }
}