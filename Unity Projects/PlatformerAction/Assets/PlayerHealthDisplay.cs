using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthDisplay : MonoBehaviour
{
    private Slider slider;
    public GameObject player;
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


    }
}
