using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMove : MonoBehaviour
{
    public Vector2 speed;
    public Vector2 inherit;

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = (speed.y * (Vector2)transform.up) + inherit;
    }
}
