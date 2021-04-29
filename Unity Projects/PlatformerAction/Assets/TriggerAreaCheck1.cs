using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaCheck1 : MonoBehaviour
{
    private Enemy_behaviour_1 enemyParent;

    private void Awake()
    {
        enemyParent = GetComponentInParent<Enemy_behaviour_1>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            enemyParent.target = collider.transform;
            enemyParent.inRange = true;
            enemyParent.hotZone.SetActive(true);
        }
    }
}
