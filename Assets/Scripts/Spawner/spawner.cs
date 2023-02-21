using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject spawnObject;
    public float spawnRate;
    private float spawnTimer;
    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnObject != null)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnRate)
            {
                spawnTimer = 0;
                GameObject obj = Instantiate(spawnObject, transform.position, Quaternion.identity, GameObject.Find("/Enemies").transform);
            }

        }
    }
}
