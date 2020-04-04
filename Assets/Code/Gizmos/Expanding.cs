using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expanding : MonoBehaviour
{
    public float expansionSpeed;
    float multipler = 0f;

    // Start is called before the first frame update
    void Start()
    {
        multipler = 1f + expansionSpeed / 60f;
    }

    private void FixedUpdate()
    {
        transform.localScale *= multipler;
    }

}
