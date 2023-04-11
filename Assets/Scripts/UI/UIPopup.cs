using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIPopup : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject player;
    [SerializeField] private int viewed = 0;
    [SerializeField] private bool playerIsAround = false;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] float xDIst;
    [SerializeField] float yDIst;
    [SerializeField] private CanvasManager canvasManager;
    [SerializeField] private GameObject canvas;
    public GameObject comic;
    public HomeCanvas homeCanvas;
    public Button button;
    void Start()
    {
        player = GameObject.Find("Player");
        canvas=GameObject.Find("Canvas");
        homeCanvas=GameObject.Find("HomeCanvas").GetComponent<HomeCanvas>();
        canvasManager = canvas.GetComponent<CanvasManager>();
        //GameObject = GetComponent<SpriteRenderer>();
        //this.spriteRenderer.enabled = false; 
        comic.SetActive(false);
        button.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        checkPlayerAround(); 
        if (viewed == 0 && playerIsAround)
        {
            comic.SetActive(true);
            canvasManager.ifStart = false;
        }


    }

    void checkPlayerAround()
    {   
        string name=SceneManager.GetActiveScene().name;

        if(name=="Home"){
            if(canvasManager.ifStart == false){
            return;
        }
            if(homeCanvas.levels[3]==1&&homeCanvas.levels[4]==2){
                playerIsAround = true;

            }
            if(homeCanvas.levels[3]==2&&homeCanvas.levels[4]==1){

                playerIsAround = true;
            }
            return;
        }
        xDIst = player.transform.position.x - transform.position.x;
        yDIst = player.transform.position.y - transform.position.y;
        if (Mathf.Abs( xDIst) < 0.5 && Mathf.Abs(yDIst) < 0.5)
        {
            playerIsAround = true;
        }
        else
        {
            playerIsAround = false;
        }
    }

    private void OnClick()
    {
        if(playerIsAround)
        {
            // this.spriteRenderer.enabled = false;
            // gameObject.SetActive(false);
            comic.SetActive(false);
            viewed = 1;
            canvasManager.ifStart = true;
        }

    }
}
