using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    private static int score = 0;

    public static void getCoin(){
        ++score;
        if(score >= 5)
            GameObject.Find("ScoreText").GetComponent<TMPro.TextMeshProUGUI>().text = "you win";
        else
            GameObject.Find("ScoreText").GetComponent<TMPro.TextMeshProUGUI>().text = "score: " + score.ToString();
    }
}
