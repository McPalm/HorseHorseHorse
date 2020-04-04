using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Health target;
    public IResourceDisplay display { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        display = GetComponent<IResourceDisplay>();
        target.OnChange += OnChange;
        OnChange(target.CurrentHealth, target.MaxHealth);
    }

    
    void OnChange(int current, int max)
    {
        display.SetScale(current, max);
    }
}
