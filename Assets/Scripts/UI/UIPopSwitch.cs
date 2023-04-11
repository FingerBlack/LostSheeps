using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject player;
    [SerializeField] private int viewed = 0;
    [SerializeField] private bool playerCompleteLvl2 = false;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] float xDIst;
    [SerializeField] float yDIst;
    [SerializeField] private CanvasManager canvasManager;
    public GameObject canvas;
    private int count = 1;

    void Start()
    {
        player = GameObject.Find("Player");
        canvasManager = GameObject.Find("Canvas").GetComponent<CanvasManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.spriteRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //checkPlayerAround();
        CanvasManager cm = canvas.GetComponent<CanvasManager>();

        if (cm.homeCanvas.levels[3] == 1 && cm.homeCanvas.levels[4] == 2 && cm.homeCanvas.levels[5] == 2)
        {
            playerCompleteLvl2 = true;
        }
        if (cm.homeCanvas.levels[3] == 2 && cm.homeCanvas.levels[4] == 1 && cm.homeCanvas.levels[8] == 2)
        {
            playerCompleteLvl2 = true;
        }
        if (viewed == 0 && playerCompleteLvl2)
        {
            this.spriteRenderer.enabled = true;
            canvasManager.ifStart = false;
        }


    }

    void checkPlayerAround()
    {
        xDIst = player.transform.position.x - transform.position.x;
        yDIst = player.transform.position.y - transform.position.y;
        if (Mathf.Abs(xDIst) < 0.5 && Mathf.Abs(yDIst) < 0.5)
        {
            playerCompleteLvl2 = true;
        }
        else
        {
            playerCompleteLvl2 = false;
        }
    }

    private void OnMouseDown()
    {
        if (playerCompleteLvl2)
        {
            if(count == 0)
            {
                this.spriteRenderer.enabled = false;
                gameObject.SetActive(false);
                viewed = 1;
                canvasManager.ifStart = true;
            }
            else
            {
                count--;
            }
        }

    }
}
