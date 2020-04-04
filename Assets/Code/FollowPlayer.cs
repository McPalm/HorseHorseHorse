using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FollowPlayer : MonoBehaviour, ISleep
{
    public Transform Player => hors.Hors.transform;
    public float speed = 3f;

    Rigidbody2D Rigidbody { get; set; }
    SpriteRenderer Sprite { get; set; }

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Sprite = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        var direction = Player.transform.position - transform.position;
        Rigidbody.velocity = direction.normalized * speed;
        Sprite.flipX = direction.x > 0f;
    }

    public void Sleep()
    {
        enabled = false;
        if (Rigidbody)
            Rigidbody.velocity = Vector2.zero;
    }

    public void WakeUp()
    {
        enabled = true;
    }
}
