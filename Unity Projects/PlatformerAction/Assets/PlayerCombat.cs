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

    private Enemy_behaviour enemy_behaviour;
    public int maxHealth = 100;
    int currentHealth;
    int enemyAttackDamage;

    void Start()
    {
        currentHealth = maxHealth;
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

        //Detect enemies in range of att
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Damage them
        foreach(Collider2D enemy in hitEnemies)
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

        Debug.Log("Hurt");

        if (currentHealth <= 0)
        {
            Debug.Log("Death");//Die();
        }
    }

    void Die()
    {
        enemy_behaviour = GameObject.Find("Skeleton1").GetComponent<Enemy_behaviour>();
        enemy_behaviour.Die();
        //animator.SetBool("IsDead", true);

        Invoke("RealDeath", 2);
    }

    void RealDeath()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<PolygonCollider2D>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        this.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Enemy_weapon")
        {
            enemy_behaviour = GameObject.Find("Skeleton1").GetComponent<Enemy_behaviour>();
            enemyAttackDamage = enemy_behaviour.attackDamage;
            TakeDamage(attackDamage);
        }
    }
}
