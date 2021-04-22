using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditBehaviour : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;

    public Animator animator;

    //new
    public float speed;
    public float spot1;
    public float spot2;
    private float dist;
    private Transform find_player;

    private bool isMoving;
    private Transform myTransform;
    //



    void Start()
    {
        currentHealth = maxHealth;
    }

    //new
    void Update()
    {
        myTransform = gameObject.transform;
        find_player = transform.Find("/Player");
        dist = Vector2.Distance(find_player.position, transform.position);

        Movement();
    }

    void Movement()
    {
        animator.SetBool("IsMoving", true);

        if (dist < 10)
        {
            if (find_player.position.x < myTransform.position.x)
            {
                myTransform.position -= myTransform.right * speed * Time.deltaTime; // player is left of enemy, move left
                gameObject.transform.localScale = new Vector2(1.5f, 1.5f);
            }
            else if (find_player.position.x > myTransform.position.x)
            {
                myTransform.position += myTransform.right * speed * Time.deltaTime; // player is right of enemy, move right
                gameObject.transform.localScale = new Vector2(-1.5f, 1.5f);
            }
        }
        else
        {
            if (spot2 < myTransform.position.x)
            {
                myTransform.position -= myTransform.right * speed * Time.deltaTime; // spot is left of enemy, move left
                gameObject.transform.localScale = new Vector2(1.5f, 1.5f);
            }
            else if (spot1 > myTransform.position.x)
            {
                myTransform.position += myTransform.right * speed * Time.deltaTime; // spot is right of enemy, move right
                gameObject.transform.localScale = new Vector2(-1.5f, 1.5f);
            }
            else
            {
                animator.SetBool("IsMoving", false);
            }
        }
    }
    //

    public void TakeDamage(int damage)
    {
        if (currentHealth - damage > 0)
        {
            currentHealth -= damage;
            animator.SetTrigger("Hurt");
        }
        else if (currentHealth - damage <= 0)
        {
            currentHealth -= damage;
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("IsDead", true);
        Invoke("RealDeath", 2);
    }

    void RealDeath()
    {
        Destroy(gameObject);
        this.enabled = false;
    }
}
