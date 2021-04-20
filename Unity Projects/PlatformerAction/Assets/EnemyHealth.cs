using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;

    private Enemy_behaviour enemy_behaviour;
    private GameObject skeleton;
    //public Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        enemy_behaviour = GameObject.Find("Skeleton1").GetComponent<Enemy_behaviour>();
        enemy_behaviour.Hurt();
        //animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        enemy_behaviour = GameObject.Find("Skeleton1").GetComponent<Enemy_behaviour>();
        enemy_behaviour.Die();
        //animator.SetBool("IsDead", true);

        Invoke("RealDeath", 2);
    }

    void RealDeath()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<PolygonCollider2D>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        skeleton = GameObject.Find("Skeleton1");
        Destroy(skeleton);
        this.enabled = false;
    }
}
