using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Hurt : MonoBehaviour
{
    public int damage = 1;
    public int MaxHits = 1;
    public bool pierceWall = false;

    [Range(0f, 1f)]
    public float Volume = .5f;
    public AudioClip HitSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var hit = collision.transform.GetComponent<Health>();
        if(hit)
        {
            hit.Hurt(damage);
            MaxHits--;
            if (MaxHits <= 0)
                Destroy(gameObject);
            if (HitSound)
                AudioPool.PlaySound(transform.position, HitSound, Volume);
        }
        else if(pierceWall == false)
            Destroy(gameObject);
    }

}
