using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coin;

    private static int limit = 3;
    private static int now = 0;

    public float spawnRnageX = 5.0f;
    public float spawnRnageY = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawnCoin", 2.0f, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void spawnCoin(){
        if(now < limit){
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnRnageX, spawnRnageX), Random.Range(-spawnRnageY, spawnRnageY), 0.0f);
            Instantiate(coin, spawnPosition, coin.transform.rotation);
            ++now;
        }
    }

    public static void removeCoin(){
        --now;
    }
}
