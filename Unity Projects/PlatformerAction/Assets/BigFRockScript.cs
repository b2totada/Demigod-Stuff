using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigFRockScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public LayerMask playerLayer;
    private GameObject player;
    private bool canKill = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (DetectPlayer())
        {
            rb.gravityScale = 10;
            rb.mass = 10;
        }
    }

    bool DetectPlayer()
    {
        return Physics2D.BoxCast(gameObject.transform.position, new Vector2(1.5f, 0.19f), 0f, Vector2.down, 500f, playerLayer).collider != null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && canKill)
        {
            Destroy(player);
        }

        if (collision.gameObject.tag == "Foreground")
        {
            canKill = false;
        }
    }
}
