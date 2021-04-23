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

    private new Rigidbody2D rigidbody;
    public bool staggered;
    //



    void Start()
    {
        currentHealth = maxHealth;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    //new
    void Update()
    {
        myTransform = gameObject.transform;
        find_player = transform.Find("/Player");
        dist = Vector2.Distance(find_player.position, transform.position);

        if (staggered)
        {
            rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            Movement();
        }
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
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("LightBandit_Death"))
        {
            Staggering();
            animator.SetTrigger("Hurt");
            animator.SetBool("IsMoving", false);

            if (currentHealth - damage > 0)
            {
                Invoke("NotStaggering", 0.25f);
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
        animator.SetBool("IsMoving", false);
        animator.SetBool("IsDead", true);
        Invoke("RealDeath", 2);
    }

    void RealDeath()
    {
        Destroy(gameObject);
        this.enabled = false;
    }

    void Staggering()
    {
        rigidbody.drag = 100f;
        rigidbody.gravityScale = 75f;
        staggered = true;
    }

    void NotStaggering()
    {
        rigidbody.drag = 0f;
        rigidbody.gravityScale = 5f;
        staggered = false;
    }
}
