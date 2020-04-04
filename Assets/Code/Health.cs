using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public bool DestroyOnDeath;
    public bool IFrames;

    public int MaxHealth;
    public int LostHealth { get; set; }
    public int CurrentHealth => MaxHealth - LostHealth;

    public bool Dead => LostHealth >= MaxHealth;

    public event System.Action OnKill;
    public event System.Action<int> OnHurt;
    public event System.Action<int, int> OnChange;

    float LastHit;
    public bool Invulnerable => IFrames && LastHit + .5f > Time.timeSinceLevelLoad;


    public void Hurt(int damage)
    {
        if (Invulnerable || Dead)
            return;
        LastHit = Time.timeSinceLevelLoad;
        damage = damage < 0 ? 0 : damage;
        LostHealth += damage;
        OnHurt?.Invoke(damage);
        OnChange?.Invoke(CurrentHealth, MaxHealth);
        if (Dead)
        {
            OnKill?.Invoke();
            if (DestroyOnDeath)
                Destroy(gameObject);
        }
        else
            StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        var sprite = GetComponent<SpriteRenderer>();
        if(sprite)
        {
            int counter = 0;
            sprite.color = Color.gray;
            for(int i = 0; i < 30; i++)
            {
                yield return new WaitForFixedUpdate();
                sprite.color = i % 2 == 1 ? new Color(1f, 1f, 1f, .5f) : new Color(1f, .8f, .8f);
            }
            sprite.color = Color.white;
        }
    }
}
