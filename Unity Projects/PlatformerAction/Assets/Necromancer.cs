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
    [HideInInspector] public int phase;
    private AudioSource AS;
    private int attack2Counter;
    private bool canCast;
    void Start()
    {
        NecroDebug();
        currentHealth = maxHealth;
        phase = 0;
        AS = GetComponent<AudioSource>();
        animator = transform.GetComponent<Animator>();
    }

    void Update()
    {
        TowardsPlayer();

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        if (trapDoor.GetComponent<BoxCollider2D>().enabled && !inCombat)
        {            
            inCombat = true;
            phase = 1;
            Debug.Log("Combat started! Phase: " + phase);
            Invoke("CanCastAgain", 2f);
        }

        if (canCast && inCombat && phase < 3)
        {
            canCast = false;
            AS.clip = SummonSkullFlame;
            AS.PlayOneShot(AS.clip);
            animator.SetTrigger("Attack2");
            Invoke("Cast", 0.2f);
        }
        if (currentHealth < 400 && currentHealth > 300 && phase == 1)
        {
            CancelInvoke("CanCastAgain");
            CancelInvoke("Cast");
            canCast = false;
            animator.Play("JumpTo2");
            Invoke("CanCastAgain", 4f);
            Invoke("StartPhase2", 2.5f);
        }
    }

    void Cast()
    {
        if (player.transform.position.x > transform.position.x)
        {
            Instantiate(skullFlame, orbspawner.transform.position, Quaternion.Euler(0, 0, 0));
        }
        if (player.transform.position.x < transform.position.x)
        {
            Instantiate(skullFlame, orbspawner.transform.position, Quaternion.Euler(0, 180, 0));
        }
        
        Invoke("CanCastAgain", 1.3f);
    }

    void CanCastAgain() 
    {
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
    void TowardsPlayer() 
    {
        if (player.transform.position.x > transform.position.x && transform.rotation.y > 0)
        {
            transform.Rotate(new Vector3(0, 1, 0), 180);
        }
        if (player.transform.position.x < transform.position.x && transform.rotation.y == 0)
        {
            transform.Rotate(new Vector3(0, 1, 0), 180);
        }
    }
    void NecroDebug() 
    {
        Debug.Log("Boss hp: " + currentHealth + "   phase: " + phase);
        Invoke("NecroDebug", 1f);
    }
    void StartPhase2() 
    {
        phase = 2;
        animator.Play("Idle");
    }
}
