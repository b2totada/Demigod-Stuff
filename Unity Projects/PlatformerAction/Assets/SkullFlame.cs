using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullFlame : MonoBehaviour
{
    private GameObject player;
    public int speed;
    public float rotationSpeed;
    public int damage;
    private Rigidbody2D rb;

    private void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        FlipIfUpsideDown();
        Vector2 point2Target = (Vector2)transform.position - (Vector2)player.transform.position;
        point2Target.Normalize();
        float value = Vector3.Cross(point2Target, transform.right).z;
        rb.angularVelocity = rotationSpeed * value;
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerCombat>().TakeDamage(damage);
            //Destroy(this.gameObject);
        }
        else
        {
            //Destroy(this.gameObject);
        }
    }
    void FlipIfUpsideDown() 
    {
        if (transform.localEulerAngles.z < 90 || transform.localEulerAngles.z > 270)
        {
            transform.GetComponent<SpriteRenderer>().flipY = false;
        }

        if (transform.localEulerAngles.z > 90 && transform.localEulerAngles.z < 270)
        {
            transform.GetComponent<SpriteRenderer>().flipY = true;
        }

    }
}
