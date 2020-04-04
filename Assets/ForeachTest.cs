using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.Linq;

public class ForeachTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(testing());
    }


    IEnumerator testing()
    {
        yield return new WaitForSeconds(5f);

        int count = 10000000;

        ForEach(count);
        For(count);
        Linq(count);


        float _foreach = 0;
        float _for = 0f;
        float _linq = 0f;

        for(int i = 0; i < 10; i++)
        {
            _foreach += ForEach(count);
            yield return new WaitForSeconds(.2f);
            _for += For(count);
            yield return new WaitForSeconds(.2f);
            _linq += Linq(count);
            yield return new WaitForSeconds(.2f);
        }

        UnityEngine.Debug.Log($"Foreach total time: {_foreach}");
        UnityEngine.Debug.Log($"For     total time: {_for}");
        UnityEngine.Debug.Log($"LINQ    total time: {_linq}");
    }

    float ForEach(int count)
    {
        var stopwatch = new Stopwatch();
        var list = GenerateTestList(count);
        stopwatch.Start();
        // START

        float value = 0f;
        foreach (var item in list)
        {
            value += item;
        }

        // END
        stopwatch.Stop();

        UnityEngine.Debug.Log($"Foreach ended after {stopwatch.ElapsedMilliseconds} milliseconds, adding the sum up to {count}, (expected {count}");
        return stopwatch.ElapsedMilliseconds;
    }

    float For(int count)
    {
        var stopwatch = new Stopwatch();
        var list = GenerateTestList(count);
        stopwatch.Start();
        // START


        float value = 0f;

        for (int i = 0; i < list.Count; i++)
        {
            value += list[i];
        }

        // END
        stopwatch.Stop();

        UnityEngine.Debug.Log($"For Loop ended after {stopwatch.ElapsedMilliseconds} milliseconds, adding the sum up to {count}, (expected {count}");
        return stopwatch.ElapsedMilliseconds;
    }

    float Linq(int count)
    {
        var stopwatch = new Stopwatch();
        var list = GenerateTestList(count);
        stopwatch.Start();
        // START

        var value = list.Aggregate((a, b) => a += b);

        // END
        stopwatch.Stop();

        UnityEngine.Debug.Log($"LINQ query ended after {stopwatch.ElapsedMilliseconds} milliseconds, adding the sum up to {count}, (expected {count}");
        return stopwatch.ElapsedMilliseconds;
    }

    List<float> GenerateTestList(int count)
    {
        var list = new List<float>();
        for (int i = 0; i < count; i++)
        {
            list.Add(1f);
        }
        return list;
    }
}
