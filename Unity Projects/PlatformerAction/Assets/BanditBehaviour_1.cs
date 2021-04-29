using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditBehaviour_1 : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Animator animator;
    public int damage;
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
    public LayerMask wallsLayerMask;
    public LayerMask playerLayer;
    private CharacterController2D charCont;
    private bool playerIsFacingRight;
    public GameObject hitbox;

    void Start()
    {
        banditPolyColl = GetComponent<PolygonCollider2D>();
        playerCombat = GameObject.Find("Player").GetComponent<PlayerCombat>();
        currentHealth = maxHealth;
        rigidbody = GetComponent<Rigidbody2D>();
        charCont = GameObject.Find("Player").GetComponent<CharacterController2D>();
    }

    void Update()
    {
        myTransform = gameObject.transform;
        find_player = transform.Find("/Player");
        if (find_player != null)
            dist = Vector2.Distance(find_player.position, transform.position);

        if (currentHealth <= 0)
        {
            Die();
        }

        if (find_player.position.x < myTransform.position.x)
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
        else if (find_player.position.x > myTransform.position.x)
        {
            transform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
        }

        if (staggered || !canAttack)
        {
            rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
           
            Movement();
        }
    }
    void CanAttack()
    {
        canAttack = true;
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        canAttack = false;
        Invoke("CanAttack", 0.6f);
        Invoke("DealDamage", 0.2f);
    }

    void DealDamage()
    {
        if (hitbox.GetComponent<CircleCollider2D>().IsTouching(find_player.GetComponent<BoxCollider2D>()))
        {
            playerCombat.TakeDamage(damage);
        }
    }

    void Movement()
    {
        if (dist <= 1.5f && canAttack)
        {
            Attack();
        }
    }   

    public void TakeDamage(int damage)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("LightBandit_Death"))
        {
            Staggering();
            animator.SetTrigger("Hurt");

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
        animator.SetBool("IsDead", true);
        Invoke("RealDeath", 2);
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<PolygonCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
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

