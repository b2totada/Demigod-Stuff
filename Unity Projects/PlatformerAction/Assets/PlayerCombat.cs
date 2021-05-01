using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public GameObject bigSkele;
    public AudioClip SwordSlice1;
    public AudioClip SwordSlice2;
    public AudioClip SwordSlice3;
    public AudioClip SwordSlice4;
    public AudioClip Whoosh;
    public AudioClip explosion;

    private AudioSource AS;
    private BanditBehaviour bandit;
    private EnemyHealth enemyHealth;
    private Enemy_behaviour enemy_behaviour;
    private Enemy_behaviour_1 enemy_behaviour_1;
    public int maxHealth = 100;
    public int currentHealth;
    int enemyAttackDamage;
    private PlayerMovement playerMovement;
    private new Rigidbody2D rigidbody;
    private PlayerMovement playerMoves;
    public bool staggered;
    public bool frozen;
    public CircleCollider2D playerCircleColl;
    private CameraScript camScript;
    public Collider2D enemy;

    void Start()
    {
        AS = transform.GetComponent<AudioSource>();
        staggered = false;
        rigidbody = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        camScript = GameObject.Find("Main Camera").GetComponent<CameraScript>();
        enemy_behaviour = skele.GetComponent<Enemy_behaviour>();
        enemy_behaviour_1 = bigSkele.GetComponent<Enemy_behaviour_1>();
        enemyHealth = skele.GetComponentInChildren<EnemyHealth>();
        //bandit = GameObject.Find("Bandit1").GetComponent<BanditBehaviour>();

        playerMoves = transform.GetComponent<PlayerMovement>();
    }

    void Update()
    {

        if (currentHealth > 100) //Anti over-heal
        {
            currentHealth = 100;
        }

        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.Space) && !animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Jump") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Player_Fall"))
                {
                    Attack();
                    nextAttackTime = Time.time + 1f / attackRate;
                }
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

        //Makes attacks to be interruptable;
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Space)) 
        {
            CancelInvoke("DealDamage");
        }
    }

    void Attack()
    {
        //Play att anim
        animator.SetTrigger("Attack");
        Invoke("DealDamage", 0.20f);
    }
    void DealDamage() 
    {
        //Detect enemies in range of att
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        if (hitEnemies.Length != 0)
            enemy = hitEnemies[0];
        AS.PlayOneShot(Whoosh);

        //Damage them
        int dmg = 0;
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.gameObject.CompareTag("Bandit"))
            {
                dmg += attackDamage;
            }
            else if (enemy.gameObject.CompareTag("Skeleton"))
            {
                dmg += attackDamage;
            }
            else if (enemy.gameObject.CompareTag("SkullOrb"))
            {
                Destroy(enemy.gameObject);
            }
            else if (enemy.gameObject.CompareTag("Necromancer"))
            {
                dmg += attackDamage;
            }
            else if (enemy.gameObject.CompareTag("BanditStill"))
            {
                dmg += attackDamage;
            }
            else if (enemy.gameObject.CompareTag("Skeleton1"))
            {
                dmg += attackDamage;
            }
        }

        if (enemy != null)
        {
            int randomSound = Random.Range(1, 5);
            switch (randomSound)
            {
                case 1: AS.clip = SwordSlice1; break;
                case 2: AS.clip = SwordSlice2; break;
                case 3: AS.clip = SwordSlice3; break;
                case 4: AS.clip = SwordSlice4; break;
            }
            AS.PlayOneShot(AS.clip);
            if (enemy.gameObject.CompareTag("Bandit"))
            {
                enemy.GetComponent<BanditBehaviour>().TakeDamage(dmg);
            }
            else if (enemy.gameObject.CompareTag("Skeleton1"))
            {
                enemy.GetComponentInChildren<EnemyHealth1>().TakeDamage(dmg);
            }
            else if (enemy.gameObject.CompareTag("Skeleton"))
            {
                enemy.GetComponentInChildren<EnemyHealth>().TakeDamage(dmg);
            }
            else if (enemy.gameObject.CompareTag("Necromancer"))
            {
                enemy.GetComponent<Necromancer>().TakeDamage(dmg);
            }
            else if (enemy.gameObject.CompareTag("BanditStill"))
            {
                enemy.GetComponent<BanditBehaviour_1>().TakeDamage(dmg);
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
        if (!animator.GetBool("IsDead"))
        {
            animator.SetTrigger("Hurt");

            if (!playerMoves.IsGrounded())
            {
                Staggering();
                Invoke("NotStaggering", 0.5f);
            }

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
        Staggering();
        
        transform.GetComponent<PlayerMovement>().enabled = false;

        //animator.SetBool("IsFalling", false);
        animator.SetBool("IsDead", true);
        Invoke("YouDied", 2f);

        //enemy_behaviour.anim.SetBool("Attack", false);
        //enemy_behaviour.anim.SetBool("Rage", false);
        //enemy_behaviour.enabled = false;
        //enemyHealth.enabled = false;
        //bandit.enabled = false;
    }

    public void RealDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Invoke("EndYouDie", 1f);
    }

    void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Enemy_weapon")
        {
            enemy_behaviour = trig.gameObject.GetComponentInParent<Enemy_behaviour>();
            enemyAttackDamage = enemy_behaviour.attackDamage;
            TakeDamage(enemyAttackDamage);
        }
        else if (trig.gameObject.tag == "Enemy_weapon1")
        {
            enemy_behaviour_1 = trig.gameObject.GetComponentInParent<Enemy_behaviour_1>();
            enemyAttackDamage = enemy_behaviour_1.attackDamage;
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
    public void Explosion()
    {
        AS.clip = explosion;
        AS.PlayOneShot(AS.clip);
    }

    void YouDied()
    {
        camScript.cam.cullingMask = (1 << 11);
        Invoke("RealDeath", 2f);
    }

    void EndYouDied()
    {
        camScript.cam.cullingMask = -1;
    }
}


/*
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
*/