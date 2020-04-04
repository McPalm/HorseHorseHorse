using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public float Trauma = 0f;
    public float Drunk = 0f;
    Vector3 orig;

    float time;

    Camera c;

    private void Start()
    {
        orig = transform.localPosition;
        c = GetComponent<Camera>();
    }

    private void Update()
    {
        Vector3 offset = Vector3.zero;
        float roll = 0f;
        

        if (Trauma > 0f)
        {
            time = Time.timeSinceLevelLoad * 40f + Time.realtimeSinceStartup * 20f;
            float magnitude = Trauma * Trauma;
            offset += magnitude * new Vector3(.5f - Mathf.PerlinNoise(1f, time), .5f - Mathf.PerlinNoise(10f, time));
            roll += magnitude * ( .5f - Mathf.PerlinNoise(20f, time)) * 3f;
            Trauma -= (Trauma > .66f) ? Time.unscaledDeltaTime * Trauma  * 1.5f: Time.unscaledDeltaTime;
        }
        if(Drunk > 0f)
        {
            float magnitude = Drunk * Drunk;
            offset += magnitude * new Vector3( .5f - Mathf.PerlinNoise(1f, Time.timeSinceLevelLoad * 0.22f), .5f - Mathf.PerlinNoise(10f, Time.timeSinceLevelLoad * 0.17f));
            roll += magnitude * (.5f - Mathf.PerlinNoise(20f, Time.timeSinceLevelLoad * 0.2f)) * 1.5f;
        }
        if (c) offset *= c.orthographicSize / 6f;

        transform.localPosition = orig + offset;
        transform.localRotation = Quaternion.AngleAxis(roll, Vector3.forward);
    }
}
