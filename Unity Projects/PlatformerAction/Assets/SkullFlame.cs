using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullFlame : MonoBehaviour
{
    private GameObject player;
    public int speed = 5;
    public float rotationSpeed = 1f;
    public int damage = 10;
    private Quaternion rotateToPlayer;
    private Vector3 dir;
    private Rigidbody2D rb;

    private void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        dir = (player.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rotateToPlayer = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotateToPlayer, Time.deltaTime * rotationSpeed);
        rb.velocity = new Vector2(dir.x * speed, dir.y * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerCombat>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
