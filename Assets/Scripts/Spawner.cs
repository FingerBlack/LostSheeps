using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 Localposition;
    public float TimeCount;
    public float TimePeirod;
    public GameObject enemy;
    void Start()
    {
        TimeCount=0;
        TimePeirod=2f;
    }

    // Update is called once per frame
    void Update()
    {   
        TimeCount+=Time.deltaTime;
        Localposition=new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(-2.0f, 2.0f),0);
        
        if(TimeCount>TimePeirod){
            GameObject Enemies=GameObject.Find("/Enemies");
            if(Enemies.transform.childCount>50){
                return;
            }
            Instantiate(enemy, transform.position+Localposition, Quaternion.identity,Enemies.transform);
            TimeCount=0;
        }
    }
}
