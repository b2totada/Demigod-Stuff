using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthDisplay : MonoBehaviour
{
    private Slider slider;
    public GameObject player;
    public GameObject potion1;
    public GameObject potion2;
    public GameObject potion3;
    private void Start()
    {
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
        else
        {
            slider.value = 0;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (potion3.GetComponent<Image>().enabled == true)
            {
                potion3.GetComponent<Image>().enabled = false;
                player.GetComponent<PlayerCombat>().currentHealth += 25;
            }
            else if (potion2.GetComponent<Image>().enabled == true)
            {
                potion2.GetComponent<Image>().enabled = false;
                player.GetComponent<PlayerCombat>().currentHealth += 25;
            }
            else if (potion1.GetComponent<Image>().enabled == true)
            {
                potion1.GetComponent<Image>().enabled = false;
                player.GetComponent<PlayerCombat>().currentHealth += 25;
            }
        }
    }
}
