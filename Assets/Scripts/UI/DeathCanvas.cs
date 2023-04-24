using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathCanvas : MonoBehaviour
{
    public static DeathCanvas instance;

    private void Awake()
    {
        // start of new code
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if(gameObject.activeSelf && Input.GetKeyDown(KeyCode.E)){
            gameObject.SetActive(false);
            PlayingStats.onLevelFail();
            SceneManager.LoadScene( SceneManager.GetActiveScene().name);
        }
    }
}
