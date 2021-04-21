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

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        enemy_behaviour = GameObject.Find("Skeleton1").GetComponent<Enemy_behaviour>();
        enemyHealth = GameObject.Find("skeleton1_collider").GetComponent<EnemyHealth>();
        bandit = GameObject.Find("Bandit1").GetComponent<BanditBehaviour>();
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
        currentHealth -= damage;

        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        rigidbody.drag = 100f;
        rigidbody.gravityScale = 75f;
        //rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;

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
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
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
}
