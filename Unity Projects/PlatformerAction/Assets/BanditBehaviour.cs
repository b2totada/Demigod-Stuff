using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditBehaviour : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;

    public Animator animator;

    //new
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
    private bool wall = false;
    private bool canDetect;
    private bool detection;
    private CharacterController2D charCont;
    private bool playerIsFacingRight;
    public GameObject hitbox;
    //



    void Start()
    {
        banditPolyColl = GetComponent<PolygonCollider2D>();
        playerCombat = GameObject.Find("Player").GetComponent<PlayerCombat>();
        currentHealth = maxHealth;
        rigidbody = GetComponent<Rigidbody2D>();
        canDetect = true;
        charCont = GameObject.Find("Player").GetComponent<CharacterController2D>();
    }

    //new
    void Update()
    {
        myTransform = gameObject.transform;
        find_player = transform.Find("/Player");
        dist = Vector2.Distance(find_player.position, transform.position);

        //Avoid
        playerIsFacingRight = charCont.m_FacingRight;
        detection = AvoidTrigger();

        if (detection && canDetect)
        {
            canDetect = false;
            Avoid();
            Invoke("CanDetect", 0.5f);
        }
        //

        if (staggered || !canAttack)
        {
            rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;

            if (WallDetection())
                wall = true;

            if (wall)
            { 
                ReturnToSpot();
                Invoke("NoWall", 2f);
            }
            else
            {
                Movement();
            }
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

    void Avoid()
    {
        if (!animator.GetBool("IsDead"))
        {
            if (charCont.m_FacingRight)
            {
                transform.position += Vector3.right * -3;
            }
            else
            {
                transform.position += Vector3.right * 3;
            }
        }
    }
    void CanDetect()
    {
        canDetect = true;
    }

    bool AvoidTrigger()
    {
        return Physics2D.BoxCast(gameObject.GetComponent<BoxCollider2D>().transform.position, new Vector2(0.45f, 0.75f), 0f, new Vector2(0f, 1f), 20f,  playerLayer).collider != null;
    }

    void NoWall()
    {
        wall = false;
    }

    void ReturnToSpot()
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

    bool WallDetection()
    {
        float scale = gameObject.transform.localScale.x;
        if (scale == 1.5f)   //facing left
        {
            return Physics2D.CircleCast(gameObject.GetComponent<CircleCollider2D>().bounds.center, 0.1f, Vector2.left, 5f, wallsLayerMask).collider != null;
        }
        else if (scale == -1.5f)    //facing right
        {
            return Physics2D.CircleCast(gameObject.GetComponent<CircleCollider2D>().bounds.center, 0.1f, Vector2.right, 5f, wallsLayerMask).collider != null;
        }

        return false;
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
        if (dist > 1.5f)
        {
            animator.SetBool("IsMoving", true);
        }
        
        if (dist < 7)
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
            ReturnToSpot();
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
