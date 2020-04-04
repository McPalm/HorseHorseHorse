using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().angularVelocity = speed;
    }
    
}
