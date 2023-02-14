using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void SampleScene(){
        SceneManager.LoadScene("SampleScene");
    }
    public static void TutorialScene(){
        SceneManager.LoadScene("TutorialScene");
    }
}
