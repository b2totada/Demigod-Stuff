using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 40;
    public float attackRate = 2f;
    float nextAttackTime = 0f;
    public GameObject skele;

    private BanditBehaviour bandit;
    private EnemyHealth enemyHealth;
    private Enemy_behaviour enemy_behaviour;
    public int maxHealth = 100;
    int currentHealth;
    int enemyAttackDamage;
    private PlayerMovement playerMovement;
    private new Rigidbody2D rigidbody;
    private PlayerMovement playerMoves;
    public bool staggered;
    public bool frozen;

    void Start()
    {
        staggered = false;
        rigidbody = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        enemy_behaviour = GameObject.Find("Skeleton1").GetComponent<Enemy_behaviour>();
        enemyHealth = GameObject.Find("skeleton1_collider").GetComponent<EnemyHealth>();
        bandit = GameObject.Find("Bandit1").GetComponent<BanditBehaviour>();
        playerMoves = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Hit") && playerMoves.IsGrounded())
        {
            rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            frozen = true;
        }
        else
        {
            rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            frozen = false;
        }
    }

    void Attack()
    {
        //Play att anim
        animator.SetTrigger("Attack");
        Invoke("DealDamage", 0.3f);
    }
    void DealDamage() 
    {
        //Detect enemies in range of att
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.gameObject.CompareTag("Bandit"))
            {
                enemy.GetComponent<BanditBehaviour>().TakeDamage(attackDamage);
            }
            else if (enemy.gameObject.CompareTag("Skeleton"))
            {
                skele = GameObject.Find("skeleton1_collider");
                skele.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    //new
    public void TakeDamage(int damage)
    {
        if (!playerMoves.IsGrounded())
        {
            Staggering();
            Invoke("NotStaggering", 0.5f);
        }

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
        Staggering();

        playerMovement = GetComponent<PlayerMovement>();
        playerMovement.enabled = false;

        //animator.SetBool("IsFalling", false);
        animator.SetBool("IsDead", true);
        Invoke("RealDeath", 2);

        enemy_behaviour.anim.SetBool("Attack", false);
        enemy_behaviour.anim.SetBool("Rage", false);
        enemy_behaviour.enabled = false;
        enemyHealth.enabled = false;
        bandit.enabled = false;
    }

    void RealDeath()
    {
        Destroy(gameObject);
        this.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Enemy_weapon")
        {
            enemy_behaviour = GameObject.Find("Skeleton1").GetComponent<Enemy_behaviour>();
            enemyAttackDamage = enemy_behaviour.attackDamage;
            TakeDamage(enemyAttackDamage);
        }
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
