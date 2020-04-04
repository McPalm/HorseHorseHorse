using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class ObjectPoolExtensions
{
    

    static public T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
    {
        return gameObject.GetComponent <T>() ?? gameObject.AddComponent<T>();
    }

    
}
