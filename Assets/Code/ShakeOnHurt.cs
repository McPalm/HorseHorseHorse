using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeOnHurt : MonoBehaviour
{
    public AudioClip SoundClip;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Health>().OnHurt += (a) =>
        {
            ScreenShake.Shake(1f);
            ScreenFreeze.Freeze(15);
            AudioPool.PlaySound(transform.position, SoundClip, .5f);
        };
    }

}
