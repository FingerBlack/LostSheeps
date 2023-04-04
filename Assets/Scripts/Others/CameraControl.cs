using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject player;
    public float speed = 10f;
    public float smoothTime = 0.3f;
    public CanvasManager canvasManager;

    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        // #if !UNITY_EDITOR && UNITY_WEBGL
        //     // disable WebGLInput.captureAllKeyboardInput so elements in web page can handle keyboard inputs
        //     WebGLInput.captureAllKeyboardInput = false;
        // #endif

        canvasManager = GameObject.Find("Canvas").GetComponent<CanvasManager>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {   
        if (!canvasManager.ifStart)
        {
            return;
        }
        Vector3 camOffset = new Vector3(0, 0, -10);
        Vector3 playerOffset = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
        
        // Use Lerp instead of MoveTowards to smooth the camera movement
        transform.position = Vector3.Lerp(transform.position, playerOffset, speed * Time.deltaTime);
    }
}
