using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : Shake
{
    static ScreenShake _instance;

    void Awake()
    {
        _instance = this;
    }

    static public void Shake(float trauma) => _instance.Trauma = Mathf.Max(trauma, _instance.Trauma);
    static public void Wobble(float drunk) => _instance.Drunk = drunk;
	
    static public void Shake(float trauma, Vector2 sourceLocation)
    {
        var promoximity = Mathf.Max(Mathf.Abs(sourceLocation.x - _instance.transform.position.x) / 8f, Mathf.Abs(sourceLocation.y - _instance.transform.position.y) / 4f);
        if (promoximity < 1f)
            Shake(trauma);
        else if (promoximity < 1.4f)
            Shake(trauma * (1.5f - promoximity));
    }
}
