using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotZoneCheck1 : MonoBehaviour
{
    private Enemy_behaviour_1 enemyParent;
    private EnemyHealth1 enemyHealth;
    private bool inRange;
    private Animator anim;

    private void Awake()
    {
        enemyParent = GetComponentInParent<Enemy_behaviour_1>();
        enemyHealth = GameObject.Find("BigFSkeleton/skeleton1_collider").GetComponent<EnemyHealth1>();
        anim = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        if (inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Skeleton_attack") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Skeleton_rage"))
        {
            enemyParent.Flip();
        }
        /*
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Skeleton_death"))
        {
            this.enabled = false;
        }
        */
        if (enemyHealth.currentHealth <= 0)
        {
            this.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            inRange = false;
            gameObject.SetActive(false);
            enemyParent.triggerArea.SetActive(true);
            enemyParent.inRange = false;
            enemyParent.SelectTarget();
        }
    }
}

