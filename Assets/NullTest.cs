using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullTest : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);

        GameObject go = null; // new GameObject("testificate");

        if (go)
            Debug.Log("Go is a thing");
        else
            Debug.Log("Go should be a thing");
        Destroy(go);
        yield return new WaitForSeconds(1f);

        long truuthy = 0;
        long nullcheck = 0;

        for(int i = 0; i < 10; i++)
        {

        yield return TestTruthy(l => truuthy += l);
        yield return TestNull(l => nullcheck += l);
        }

        Debug.Log("End Results");
        Debug.Log($"Truuhty total time {truuthy}");
        Debug.Log($"Null total time {nullcheck}");


    }

    IEnumerator TestTruthy(System.Action<long> endCount)
    {
        GameObject go = null; // new GameObject("testificate");
        Destroy(go);
        var stopwatch = new System.Diagnostics.Stopwatch();
        int dummy = 1;

        yield return new WaitForSeconds(1f);

        stopwatch.Start();
        for(int i = 0; i < 1000000; i++)
        {
            if(go)
            {
                dummy = i;
            }/*
            if (i == 500000)
                go = new GameObject();*/
        }

        stopwatch.Stop();
        endCount(stopwatch.ElapsedMilliseconds);
        Debug.Log($"Truthy {stopwatch.ElapsedMilliseconds}");
    }

    IEnumerator TestNull(System.Action<long> endCount)
    {
        GameObject go = null; // new GameObject("testificate");
        Destroy(go);
        var stopwatch = new System.Diagnostics.Stopwatch();
        int dummy = 1;

        yield return new WaitForSeconds(1f);

        stopwatch.Start();
        for (int i = 0; i < 1000000; i++)
        {
            if (go != null)
            {
                dummy = i;
            }/*
            if (i == 500000)
                go = new GameObject();*/
        }

        stopwatch.Stop();
        endCount(stopwatch.ElapsedMilliseconds);
        Debug.Log($"Null Check {stopwatch.ElapsedMilliseconds}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
