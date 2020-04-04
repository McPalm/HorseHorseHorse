using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Win : MonoBehaviour
{
    int count = 0;

    public GameObject ShowOnWin;
    public TextMeshProUGUI Counter;


    // Start is called before the first frame update
    public void Init()
    {
        foreach(var hp in FindObjectsOfType<Health>())
        {
            if (hp.gameObject == hors.Hors.gameObject)
                continue;
            hp.OnKill += OnKill;
            count++;
        }
        Counter.text = $"{count} enemies left";
        enabled = false;
    }

    void OnKill()
    {
        count--;
        if (count == 0)
            StartCoroutine(Winner());
        Counter.text = $"{count} enemies left";
    }


    IEnumerator Winner()
    {
        yield return new WaitForSeconds(.5f);
        ShowOnWin.gameObject.SetActive(true);
    }
}
