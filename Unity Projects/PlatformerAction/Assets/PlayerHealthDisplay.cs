using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthDisplay : MonoBehaviour
{
    private AudioSource AS;
    private Slider slider;
    private GameObject player;
    public GameObject potion1;
    public GameObject potion2;
    public GameObject potion3;    
    public AudioClip DrinkSound;
    private void Start()
    {
        player = GameObject.Find("Player");
        AS = GetComponent<AudioSource>();
        slider = transform.GetComponent<Slider>();
        slider.maxValue = player.GetComponent<PlayerCombat>().maxHealth;        
    }

    void Update()
    {
        float hp = player.GetComponent<PlayerCombat>().currentHealth;
        if (hp > 0)
        {
            slider.value = hp;
        }
        if (hp == 0)
        {
            slider.value = 0;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (player.GetComponent<PlayerCombat>().currentHealth > 0)
            {
                if (potion3.GetComponent<Image>().enabled == true)
                {
                    AS.PlayOneShot(DrinkSound);
                    potion3.GetComponent<Image>().enabled = false;
                    player.GetComponent<PlayerCombat>().currentHealth += 25;
                }
                else if (potion2.GetComponent<Image>().enabled == true)
                {
                    AS.PlayOneShot(DrinkSound);
                    potion2.GetComponent<Image>().enabled = false;
                    player.GetComponent<PlayerCombat>().currentHealth += 25;
                }
                else if (potion1.GetComponent<Image>().enabled == true)
                {
                    AS.PlayOneShot(DrinkSound);
                    potion1.GetComponent<Image>().enabled = false;
                    player.GetComponent<PlayerCombat>().currentHealth += 25;
                }
            }
        }
    }
}
