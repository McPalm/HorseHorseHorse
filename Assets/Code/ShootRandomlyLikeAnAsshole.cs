using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Shoot))]
public class ShootRandomlyLikeAnAsshole : MonoBehaviour
{
    public float speed = 1f;

    Transform target;

    float ox, oy;

    void Start()
    {
        var shoot = GetComponent<Shoot>();
        var go = new GameObject();
        go.transform.SetParent(transform);
        target = go.transform;
        shoot.Target = target;
        ox = Random.value * 1000f;
        oy = Random.value * 1000f;
    }

    private void Update()
    {
        target.localPosition = new Vector2(
            .5f - Mathf.PerlinNoise(ox, Time.timeSinceLevelLoad * speed), 
            .5f - Mathf.PerlinNoise(oy, Time.timeSinceLevelLoad * speed)
            );
    }
}
