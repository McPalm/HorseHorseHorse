using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    Camera Camera;


    private void Start()
    {
        Camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = Camera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = point;
    }
}
