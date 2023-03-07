using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TwinkleText : MonoBehaviour
{
    [SerializeField] private bool isTwinkling;

    void Start()
    {
        isTwinkling = false;        
    }

    public void Twinkle()
    {
        if(!isTwinkling){
            StartCoroutine(ChangeColor());
        }
    }

    public IEnumerator ChangeColor()
    {
        isTwinkling = true;
        GetComponent<TMP_Text>().color = new Color(1.0f, 0.4f, 0.2f);
        yield return new WaitForSeconds(0.1f);
        GetComponent<TMP_Text>().color = Color.white;
        yield return new WaitForSeconds(0.1f);
        GetComponent<TMP_Text>().color = new Color(1.0f, 0.4f, 0.2f);
        yield return new WaitForSeconds(0.1f);
        GetComponent<TMP_Text>().color = Color.white;
        isTwinkling = false;
    }
}
