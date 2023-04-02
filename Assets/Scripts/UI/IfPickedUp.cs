using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class IfPickedUp : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject buildPrompt;
    public List<GameObject> seedList;

    void Start()
    {
        buildPrompt.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

        bool allExist = seedList.All(obj => obj != null);
        if (!allExist)
        {
            Destroy(gameObject);
            buildPrompt.SetActive(true);
        }
    }
}
