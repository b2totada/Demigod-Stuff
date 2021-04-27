using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necromancer : MonoBehaviour
{
    public AudioClip SummonSkullFlame;  
    public GameObject player;
    public GameObject skullFlame;
    public GameObject trapDoor;
    public GameObject orbspawner;
    public int maxHealth;
    public int currentHealth;
    private Animator animator;
    private bool inCombat;
    private AudioSource AS;
    private int phase;
    private int attack2Counter;
    private bool canCast;
    void Start()
    {
        currentHealth = maxHealth;
        phase = 0;
        AS = GetComponent<AudioSource>();
        animator = transform.GetComponent<Animator>();
    }

    void Update()
    {
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        if (trapDoor.GetComponent<BoxCollider2D>().enabled && !inCombat)
        {
            inCombat = true;
            phase = 1;
            canCast = true;
        }

        if (canCast && inCombat)
        {
            canCast = false;
            AS.clip = SummonSkullFlame;
            AS.PlayOneShot(AS.clip);
            animator.SetTrigger("Attack2");
            Invoke("Cast", 1.5f);
        }
    }

    void Cast()
    {
        Instantiate(skullFlame, orbspawner.transform.position, Quaternion.Euler(0, 0, 0));
        canCast = true;
    }

    public void TakeDamage(int damage)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
        {

            if (currentHealth - damage > 0)
            {
                currentHealth -= damage;
            }
            else if (currentHealth - damage <= 0)
            {
                currentHealth -= damage;
                Die();
            }
        }
    }

    void Die()
    {
        animator.SetBool("IsDead", true);
        //("RealDeath", 2);
    }

    void RealDeath()
    {
        Destroy(gameObject);
        this.enabled = false;
    }

}
