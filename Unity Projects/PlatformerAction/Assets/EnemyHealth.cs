using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    private Enemy_behaviour enemy_behaviour;
    private Transform skeleton;

    //new
    public LayerMask wallsLayerMask;
    private CircleCollider2D circlecollider2d;
    private float rotation;
    public GameObject trigCheck;

    private TriggerAreaCheck trigArea;
    private Rigidbody2D rb;

    public bool canFlip = true;
    //

    //public Animator animator;

    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        currentHealth = maxHealth;
        skeleton = GetComponentInParent<Transform>();
        enemy_behaviour = GetComponentInParent<Enemy_behaviour>();
        trigArea = trigCheck.GetComponent<TriggerAreaCheck>();
    }

    //new
    void Awake()
    {
        circlecollider2d = transform.GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if (WallDetection())
            //Debug.Log("WALLLLLLLLLLLLLL!!!!!");
            if (!enemy_behaviour.InsideofLimits() && /*!enemy_behaviour.inRange && */!enemy_behaviour.anim.GetCurrentAnimatorStateInfo(0).IsName("Skeleton_attack") && !enemy_behaviour.anim.GetCurrentAnimatorStateInfo(0).IsName("Skeleton_rage"))
            {
                enemy_behaviour.SelectTarget();
                Invoke("TrigAreaActive", 1);
            }

        if (currentHealth <= 0)
        {
            canFlip = false;
            enemy_behaviour.anim.SetBool("IsDead", true);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            Die();
        }
    }
    //

    public void TakeDamage(int damage)
    {
        if (currentHealth - damage > 0)
        {
            currentHealth -= damage;
            enemy_behaviour.Hurt();
        }
        else if (currentHealth - damage <= 0)
        {
            currentHealth -= damage;
            trigArea.enabled = false;
            //Die();
        }
    }

    void Die()
    {
        enemy_behaviour.anim.SetBool("Rage", false);
        enemy_behaviour.anim.SetBool("Attack", false);
        enemy_behaviour.anim.SetBool("canWalk", false);
        enemy_behaviour.Die();

        Invoke("RealDeath", 3);

        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<PolygonCollider2D>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponentInParent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }

    void RealDeath()
    {
        //enemy_behaviour.anim.SetBool("IsDead", false);
        Destroy(transform.parent.gameObject);
        this.enabled = false;
    }

    //new
    bool WallDetection()
    {
        rotation = skeleton.transform.eulerAngles.y;
        if (rotation == 180f)   //facing left
        {
            return Physics2D.CircleCast(circlecollider2d.bounds.center, 0.1f, Vector2.left, 0.7f, wallsLayerMask).collider != null;
        }
        else if (rotation == 0f)    //facing right
        {
            return Physics2D.CircleCast(circlecollider2d.bounds.center, 0.1f, Vector2.right, 0.7f, wallsLayerMask).collider != null;
        }

        return false;
    }

    void TrigAreaActive()
    {
        trigCheck.SetActive(true);
    }
    //
}
