using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthDisplay : MonoBehaviour
{
    private Slider slider;
    public GameObject boss;

    void Start()
    {
        slider = transform.GetComponent<Slider>();
        slider.maxValue = boss.GetComponent<Necromancer>().maxHealth;
    }

    void Update()
    {
        float hp = boss.GetComponent<Necromancer>().currentHealth;
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
