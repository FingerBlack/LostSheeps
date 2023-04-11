using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        checkPlayerAround(); 
        if (viewed == 0 && playerIsAround)
        {
            this.spriteRenderer.enabled = true;
            canvasManager.ifStart = false;
        }


    }

    void checkPlayerAround()
    {
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

    private void OnMouseDown()
    {
        if(playerIsAround)
        {
            this.spriteRenderer.enabled = false;
            gameObject.SetActive(false);
            viewed = 1;
            canvasManager.ifStart = true;
        }

    }
}
