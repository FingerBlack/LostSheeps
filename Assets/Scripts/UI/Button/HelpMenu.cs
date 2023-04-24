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
        void UnpauseGame()
    {
        Time.timeScale = 1f; // Set the time scale to one to unpause the game
    }
    private bool isHelpMenuActive = false;
void OnClick()
{
    // 切换布尔变量的值
    isHelpMenuActive = !isHelpMenuActive;

    if (isHelpMenuActive)
    {
        PauseGame();
    } 
    else 
    {
        UnpauseGame();
    }
    
    //启用或禁用helpMenu
    helpMenu.SetActive(isHelpMenuActive);
}

}
