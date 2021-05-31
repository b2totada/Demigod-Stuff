using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class staminaBarDisplay : MonoBehaviour
{
    private Slider slider;
    private PlayerMovement playerMovement;
    void Start()
    {
        slider = GetComponent<Slider>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        slider.maxValue = playerMovement.dashCd;
        slider.value = slider.maxValue;
    }

    void Update()
    {
        float lastDash = playerMovement.nextDash;
        if (Input.GetKeyDown(KeyCode.Space) && slider.value == slider.maxValue)
        {
            slider.value = 0f;
        }
        slider.value += Time.deltaTime;
        if (slider.value > slider.maxValue)
        {
            slider.value = slider.maxValue;
        }
    }
}
