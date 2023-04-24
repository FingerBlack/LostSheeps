using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Restart : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemies;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick(){
        PlayingStats.onLevelFail();
        SceneManager.LoadScene( SceneManager.GetActiveScene().name);
    }
}
