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
    //

    //public Animator animator;

    void Start()
    {
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
    }
    //

    public void TakeDamage(int damage)
    {
        if (currentHealth - damage > 0)
        {
            currentHealth -= damage;
            enemy_behaviour.Hurt();
            //animator.SetTrigger("Hurt");
        }
        else if (currentHealth - damage <= 0)
        {
            currentHealth -= damage;
            trigArea.enabled = false;
            Die();
        }
    }

    void Die()
    {
        enemy_behaviour.Die();
        //animator.SetBool("IsDead", true);

        Invoke("RealDeath", 3);
    }

    void RealDeath()
    {
        /*
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<PolygonCollider2D>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        */
        Destroy(gameObject);
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
