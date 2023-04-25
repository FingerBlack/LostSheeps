using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class HelpMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject helpMenu0;
    public GameObject helpMenu1;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {   
        if(Input.GetKeyDown(KeyCode.E)){
            if (helpMenu0.activeSelf==true){
                helpMenu0.SetActive(false);
                helpMenu1.SetActive(true);
            }else if (helpMenu1.activeSelf==true){
                helpMenu1.SetActive(false);
                UnpauseGame();
            }
        }

    }
    void PauseGame()
    {
        Time.timeScale = 0f; // Set the time scale to zero to pause the game
    }
        void UnpauseGame()
    {
        Time.timeScale = 1f; // Set the time scale to one to unpause the game
    }
   
void OnClick()
{
        
    bool isHelpMenuActive0 = helpMenu0.activeSelf;
    bool isHelpMenuActive1 = helpMenu1.activeSelf;
        

    if (!isHelpMenuActive0&&!isHelpMenuActive1)
    {
        PauseGame();
         //启用或禁用helpMenu
        helpMenu0.SetActive(true);
    } 
    
   
}

}
