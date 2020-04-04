using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnAwake : MonoBehaviour
{
    [Range(0f, 1f)]
    public float Volume = .5f;
    public AudioClip AudioClip;

    // Start is called before the first frame update
    void Start()
    {
        AudioPool.PlaySound(transform.position, AudioClip, Volume);
    }
}
