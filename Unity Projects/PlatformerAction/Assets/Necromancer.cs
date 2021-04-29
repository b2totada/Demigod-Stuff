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
    public int phaseController;
    private Animator animator;
    private bool inCombat;
    private int phase;
    private AudioSource AS;
    private int attack2Counter;
    private bool canCast;
    private bool IsJumping;
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
        if (trapDoor.GetComponent<BoxCollider2D>().enabled && !inCombat)
        {
            inCombat = true;
            phase = 1;
            Debug.Log("Combat started! Phase: " + phase);
            Invoke("CanCastAgain", 2f);
        }

        if (phaseController == 2 && phase == 1)
        {
            StartPhase2();
        }
        if (phaseController == 3 && phase == 2)
        {
            StartPhase3();
        }
        if (phaseController == 4 && phase == 3)
        {
            StartPhase4();
        }

        if (!IsJumping)
        {
            TowardsPlayer();
        }       

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        if (canCast && inCombat && phase < 3)
        {
            canCast = false;
            AS.clip = SummonSkullFlame;
            AS.PlayOneShot(AS.clip);
            animator.SetTrigger("Attack2");
            Invoke("Cast", 0.2f);
        }
        if (canCast && inCombat && phase == 3)
        {
            canCast = false;
            AS.clip = SummonSkullFlame;
            AS.PlayOneShot(AS.clip);
            animator.SetTrigger("Attack2");
            Invoke("Cast2", 0.2f);
        }
        if (canCast && inCombat && phase == 4)
        {
            canCast = false;
            AS.clip = SummonSkullFlame;
            AS.PlayOneShot(AS.clip);
            animator.SetTrigger("Attack2");
            Invoke("Cast2", 0.2f);
        }
        if (currentHealth < 350 && currentHealth > 300 && phase == 1)
        {
            IsJumping = true;
            CancelInvoke("CanCastAgain");
            CancelInvoke("Cast");
            canCast = false;
            animator.Play("JumpTo2");
            Invoke("CanCastAgain", 3f);
        }
        if (currentHealth < 250 && currentHealth > 200 && phase == 2)
        {
            IsJumping = true;
            CancelInvoke("CanCastAgain");
            CancelInvoke("Cast");
            canCast = false;
            animator.Play("JumpTo3");
            Invoke("CanCastAgain", 1.6f);
        }
        if (currentHealth < 150 && phase == 3)
        {
            IsJumping = true;
            CancelInvoke("CanCastAgain");
            CancelInvoke("Cast");
            canCast = false;
            animator.Play("JumpTo4");
            Invoke("CanCastAgain", 1f);
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
    void Cast2()
    {
        if (player.transform.position.x > transform.position.x)
        {
            Instantiate(skullFlame, orbspawner.transform.position, Quaternion.Euler(0, 0, 0));
        }
        if (player.transform.position.x < transform.position.x)
        {
            Instantiate(skullFlame, orbspawner.transform.position, Quaternion.Euler(0, 180, 0));
        }

        Invoke("CanCastAgain", 1f);
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
        animator.Play("Death");
        Invoke("RealDeath", 2);
        inCombat = false;
        canCast = false;
    }

    void RealDeath()
    {
        Destroy(gameObject);
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
    public void StartPhase2() 
    {
        IsJumping = false;
        phase = 2;
        animator.Play("Idle");
    }
    public void StartPhase3() 
    {
        IsJumping = false;
        phase = 3;
        animator.Play("Idle");
    }
    public void StartPhase4()
    {
        IsJumping = false;
        phase = 4;
        animator.Play("Idle");
    }
}
