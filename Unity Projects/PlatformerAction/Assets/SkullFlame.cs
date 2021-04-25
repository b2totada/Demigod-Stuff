using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullFlame : MonoBehaviour
{
    private GameObject player;
    public int speed;
    private void Start()
    {
        
        player = GameObject.Find("Player");
    }

    void Update()
    {
        transform.LookAt(player.transform);
        transform.GetComponent<Rigidbody2D>().MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
        if (transform.GetComponent<CircleCollider2D>().IsTouchingLayers(6))
        {

        }
    }
}
