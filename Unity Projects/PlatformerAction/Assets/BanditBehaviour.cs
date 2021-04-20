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
    private float dist;
    private Transform find_player;
    private Vector2 spot = new Vector2(0.0f, 0.0f);
    private Transform myTransform;
    //



    void Start()
    {
        currentHealth = maxHealth;
        spot = gameObject.transform.position;//new
    }

    //new
    void Update()
    {
        myTransform = gameObject.transform;
        find_player = transform.Find("/Player");
        dist = Vector2.Distance(find_player.position, transform.position);

        Move();
    }

    void Move()
    {
        if (dist < 10)
        {
            if (find_player.position.x < myTransform.position.x)
            {
                myTransform.position -= myTransform.right * speed * Time.deltaTime; // player is left of enemy, move left
            }
            else if (find_player.position.x > myTransform.position.x)
            {
                myTransform.position += myTransform.right * speed * Time.deltaTime; // player is right of enemy, move right
            }
        }
        else
        {
            if (spot.x < myTransform.position.x)
            {
                myTransform.position -= myTransform.right * speed * Time.deltaTime; // spot is left of enemy, move left
            }
            else if (spot.x > myTransform.position.x)
            {
                myTransform.position += myTransform.right * speed * Time.deltaTime; // spot is right of enemy, move right
            }


        }
    }
    //

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth > 0)
        {
            animator.SetTrigger("Hurt");
        }

        if (currentHealth <= 0)
        {
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
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<PolygonCollider2D>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        Destroy(gameObject);
        this.enabled = false;
    }
}
