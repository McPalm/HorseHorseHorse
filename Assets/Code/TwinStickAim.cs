using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwinStickAim : MonoBehaviour, IControllable
{
    public InputToken InputToken { get; set; }

    public Transform AimAt;

    // Update is called once per frame
    void Update()
    {
        var dir = InputToken.SecondDirection;
        if (dir != Vector2.zero)
        {
            AimAt.localPosition = (Vector2)AimAt.localPosition * .8f + dir * .2f;
        }
    }
}
