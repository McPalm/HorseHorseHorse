using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hors : MonoBehaviour, IControllable
{
    static public hors Hors { get; private set; }

    public InputToken InputToken { get; set; }

    public float speed = 5f;
    public GameObject RIP;

    public Vector2 Velocity { get; set; }

    Rigidbody2D Rigidbody { get; set; }

    void Awake()
    {
        Hors = this;
    }

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        GetComponent<Health>().OnKill += OnKill;
    }

    void OnKill()
    {
        GetComponent<SpriteRenderer>().flipY = true;
        enabled = false;
        GetComponent<Shoot>().StopAllCoroutines();
        RIP.SetActive(true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var dir = new Vector2(InputToken.Horizontal, InputToken.Vertical);

        if(InputToken.HoldBlock)
            Velocity = dir * 2f;
        else
            Velocity = dir * speed;

        GetComponent<Shoot>().Velocity = Velocity;

        Rigidbody.MovePosition((Vector2)transform.position + Velocity * Time.fixedDeltaTime);
        if (dir.x != 0f)
            GetComponent<SpriteRenderer>().flipX = dir.x > 0f;
    }
}
