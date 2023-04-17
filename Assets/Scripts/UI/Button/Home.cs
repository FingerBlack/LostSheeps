using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Home : MonoBehaviour
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
    void OnClick(){
        SceneManager.LoadScene( "Home");
    }
}
