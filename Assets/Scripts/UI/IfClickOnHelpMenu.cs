using UnityEngine;

using UnityEngine.EventSystems;
public class IfClickOnHelpMenu : MonoBehaviour,IPointerClickHandler
{
    public GameObject panel1;
    public GameObject panel2;
    

    void Start()
    {
        

        panel2.SetActive(false);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Panel clicked");
        panel1.SetActive(false);
        panel2.SetActive(true);
    }


    
    
}