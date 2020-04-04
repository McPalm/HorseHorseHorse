using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ChoseYourHors : MonoBehaviour
{
    public GameObject HorsMenue;
    public Sprite[] Horses;
    public List<Sprite> Earthponies;
    public List<Sprite> Unicorns;
    public List<Sprite> Pegasi;

    public GameObject EPProjectile;
    public GameObject PegaProjectile;
    public GameObject UnicornProjectile;


    // Start is called before the first frame update
    void Start()
    {
        var player = hors.Hors;

        foreach (var slep in player.GetComponents<ISleep>())
        {
            slep.Sleep();
        }


        List<Button> Buttons = GetComponentsInChildren<Button>().ToList();

        for(int i = 0; i < Horses.Length; i++)
        {
            if (i == Buttons.Count)
                Buttons.Add(Instantiate(Buttons[0], transform));

            var button = Buttons[i];
            var sprite = Horses[i];
            button.GetComponent<Image>().sprite = sprite;
            button.onClick.AddListener(() =>
            {
                SelectHorse(sprite);
            });
        }
    }

    void SelectHorse(Sprite horse)
    {
        HorsMenue.SetActive(false);
        FindObjectOfType<Win>().Init();

        var player = hors.Hors;
        player.GetComponent<SpriteRenderer>().sprite = horse;
        foreach (var slep in player.GetComponents<ISleep>())
        {
            slep.WakeUp();
        }

        if (Unicorns.Contains(horse))
            MakeUnicorn();
        else if (Pegasi.Contains(horse))
            MakePegasus();
        else if (Earthponies.Contains(horse))
            MakeEarthpony();
        else
            Debug.LogWarning($"Missing type for {horse.name}");

        
    }

    void MakeEarthpony()
    {
        var player = hors.Hors;
        player.GetComponent<Health>().MaxHealth = 7;
        var shoot = player.GetComponent<Shoot>();

        shoot.Projectile = EPProjectile;
        shoot.inherit = .65f;
    }

    void MakeUnicorn()
    {
        var player = hors.Hors;
        var shoot = player.GetComponent<Shoot>();

        shoot.cooldown *= .35f;
        shoot.speed *= 1.6f;
        shoot.lifeTime *= 1.1f;
        shoot.Projectile = UnicornProjectile;
        shoot.inherit = 0f;
    }

    void MakePegasus()
    {
        var player = hors.Hors;
        player.speed *= 1.35f;
        var shoot = player.GetComponent<Shoot>();

        shoot.cooldown *= .2f;
        shoot.speed *= 2f;
        shoot.lifeTime *= .6f;
        shoot.Projectile = PegaProjectile;
        shoot.inherit = 1f;
    }
}
