using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        Debug.Log("touch");
        if(other.gameObject.TryGetComponent<playerControl>(out playerControl playerControl)){
            Destroy(gameObject);
            ScoreText.getCoin();
            CoinSpawner.removeCoin();
        }
    }
}
