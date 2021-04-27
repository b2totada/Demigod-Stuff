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
    public PolygonCollider2D banditPolyColl;
    private PlayerCombat playerCombat;

    public Vector3 breakFree = new Vector3(10.0f, 10.0f, 0.0f);
    private bool canAttack = true;
    //



    void Start()
    {
        banditPolyColl = GetComponent<PolygonCollider2D>();
        playerCombat = GameObject.Find("Player").GetComponent<PlayerCombat>();
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

        /*
        //BREAKFREE
        if (banditPolyColl.IsTouching(playerCombat.playerCircleColl))
        {
            GetComponent<Rigidbody2D>().AddForce(breakFree, ForceMode2D.Impulse);
            
            if (find_player.position.x < myTransform.position.x)
                transform.position += Vector3.right * -2;
            else if (find_player.position.x > myTransform.position.x)
                transform.position += Vector3.right * 2;
            
        }*/ 

    }

    void CanAttack()
    {
        canAttack = true;
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        canAttack = false;
        Invoke("CanAttack", 1f);
    }

    void Movement()
    {
        if (dist > 1.5f)
        {
            animator.SetBool("IsMoving", true);
        }
        
        if (dist < 10)
        {
            if (dist <= 1.5f && canAttack)
            {
                Attack();
            }
            else if (dist > 1.5f && !animator.GetCurrentAnimatorStateInfo(0).IsName("LightBandit_Attack"))
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
                animator.SetBool("IsMoving", false);
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
        rigidbody.gravityScale = 50f;
        staggered = false;
    }

    
}
