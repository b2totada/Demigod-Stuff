using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;

    private Enemy_behaviour enemy_behaviour;
    private GameObject skeleton;

    //new
    public LayerMask wallsLayerMask;
    private CircleCollider2D circlecollider2d;
    private float rotation;
    private GameObject trigCheck;
    //

    //public Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        skeleton = GameObject.Find("Skeleton1");
        enemy_behaviour = skeleton.GetComponent<Enemy_behaviour>();
        trigCheck = GameObject.Find("triggerArea");
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
            if (!enemy_behaviour.InsideofLimits() && /*!enemy_behaviour.inRange && */!enemy_behaviour.anim.GetCurrentAnimatorStateInfo(0).IsName("Skeleton_attack"))
            {
                enemy_behaviour.SelectTarget();
                Invoke("TrigAreaActive", 1);
            }
    }
    //

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        enemy_behaviour = GameObject.Find("Skeleton1").GetComponent<Enemy_behaviour>();
        enemy_behaviour.Hurt();
        //animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        enemy_behaviour = GameObject.Find("Skeleton1").GetComponent<Enemy_behaviour>();
        enemy_behaviour.Die();
        //animator.SetBool("IsDead", true);

        Invoke("RealDeath", 1);
    }

    void RealDeath()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<PolygonCollider2D>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        Destroy(skeleton);
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
