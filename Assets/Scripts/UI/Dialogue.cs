using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private int representLevel;
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] [TextArea] private string[] lines;
    [SerializeField] private float textSpeed;
    private int index;
    public bool dialogueEnded;
    [SerializeField] private bool appearOnce;
    private HomeCanvas homeCanvas;
    private GameObject homeDialogue;
    public CanvasManager canvasManager;


    // Start is called before the first frame update
    void Start()
    {
        canvasManager = GameObject.Find("Canvas").GetComponent<CanvasManager>();
        homeCanvas = GameObject.Find("HomeCanvas").GetComponent<HomeCanvas>();

        if(representLevel == 12){
            homeDialogue = GameObject.Find("DialogueCanvas").gameObject.transform.GetChild(0).gameObject;
            homeDialogue.SetActive(false);
        }

        if(GameObject.Find("HomeCanvas").GetComponent<HomeCanvas>().shouldDialogueAppears[representLevel]) {
            canvasManager.ifStart = false;

            gameObject.SetActive(true);

            if(lines.Length == 0){
                dialogueEnded = true;
                gameObject.SetActive(false);
            }
            textComponent.text = "";
            if(representLevel != 0)
                StartDialogue();
            dialogueEnded = false;
        }
        else{
            canvasManager.ifStart = true;
            dialogueEnded = true;
            gameObject.SetActive(false);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && (!homeCanvas.isInStoryLine || representLevel == 12)){
            if(textComponent.text == lines[index]){
                nextLine();
            }
            else{
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    public void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        for(int i=0;i<lines[index].Length;i++){
            if(lines[index][i] == '<'){
                while(lines[index][i] != '>'){
                    textComponent.text += lines[index][i];
                    ++i;
                }
                textComponent.text += lines[index][i];
            }
            else
                textComponent.text += lines[index][i];
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void nextLine()
    {
        if(index < lines.Length - 1){
            ++index;
            textComponent.text = "";
            StartCoroutine(TypeLine());
        }
        else{
            if(representLevel != 12){
                gameObject.SetActive(false);
                canvasManager.ifStart = true;
            }

            dialogueEnded = true;

            if(representLevel == 12)
                StartCoroutine(fadeout());
            if(representLevel == 0 || representLevel == 11 || representLevel == 12)
                GameObject.Find("HomeCanvas").GetComponent<HomeCanvas>().shouldDialogueAppears[representLevel] = false;
        }
    }

    IEnumerator fadeout(){
        yield return new WaitForSeconds(0.2f);
        homeDialogue.SetActive(true);
        homeDialogue.GetComponent<Dialogue>().StartDialogue();
        homeCanvas.isInStoryLine = false;

        gameObject.SetActive(false);
    }
}
