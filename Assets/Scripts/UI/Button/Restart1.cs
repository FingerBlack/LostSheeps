using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Restart1 : MonoBehaviour
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
        SceneChanger.SampleScene();
    }
}
