using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class HelpMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject helpMenu;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void PauseGame()
    {
        Time.timeScale = 0f; // Set the time scale to zero to pause the game
    }
    void OnClick()
    {
        PauseGame();
       
        helpMenu.SetActive(true);
        
        
    }
}
