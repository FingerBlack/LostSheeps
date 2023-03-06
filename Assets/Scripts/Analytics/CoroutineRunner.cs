using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;

public class CoroutineRunner : MonoBehaviour
{
    private static CoroutineRunner instance;

    private void Awake()
    {
        instance = this;
    }

    public static void StartMyCoroutine()
    {

        if (instance == null)
        {
            GameObject coroutineRunnerObject = new GameObject("Coroutine Runner");
            instance = coroutineRunnerObject.AddComponent<CoroutineRunner>();
        }
        instance.StartCoroutine(countEnemyStats());
    }

    public static void StopMyCoroutine()
    {

        if (instance == null)
        {
            GameObject coroutineRunnerObject = new GameObject("Coroutine Runner");
            instance = coroutineRunnerObject.AddComponent<CoroutineRunner>();
        }
        instance.StopCoroutine(countEnemyStats());
    }

    private static IEnumerator countEnemyStats()
    {
        int stage = 1;
        while (true)
        {
            // Call your function here
            List<Vector3> list = PlayingStats.enemyCount();
            EnemyStats data = new EnemyStats(list.Count.ToString(),list,PlayingStats.getDuration(),PlayingStats.currentSceneName);
            RestClient.Put("https://lostsheeps-26b16-default-rtdb.firebaseio.com/" + "enemyStats/" + PlayingStats.recordID + "/Stage" +stage+".json", data);

            stage = stage + 1;
            // Wait for one second before executing the next iteration
            yield return new WaitForSeconds(5);
        }
    }
}