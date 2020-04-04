using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : MonoBehaviour
{

    bool sleeping = true;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var slep in GetComponents<ISleep>())
        {
            slep.Sleep();
        }
    }

    private void WakeUp()
    {
        if (sleeping == false)
            return;
        foreach (var slep in GetComponents<ISleep>())
        {
            slep.WakeUp();
        }
        enabled = false;
        sleeping = false;
    }

    public void Shout()
    {
        if (sleeping == false)
            return;
        var hits = Physics2D.CircleCastAll(transform.position, 5f, Vector2.zero);
        foreach (var hit in hits)
        {
            var slep = hit.transform.GetComponent<Sleep>();
            if (slep && slep != this)
                slep.WakeUp();
        }
        WakeUp();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 delta = transform.position - hors.Hors.transform.position;
        if (delta.sqrMagnitude < 50f)
            Shout();
    }
}
