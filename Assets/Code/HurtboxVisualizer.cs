using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtboxVisualizer : MonoBehaviour, IControllable
{
    public InputToken InputToken { get; set; }

    public SpriteRenderer HurtBoxDot;
    public SpriteRenderer Horse;

    // Update is called once per frame
    void Update()
    {
        HurtBoxDot.enabled = InputToken.HoldBlock;
        Horse.color = InputToken.HoldBlock ? new Color(1f, 1f, 1f, .35f) : Color.white;
    }
}
