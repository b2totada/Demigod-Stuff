using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallDeath : MonoBehaviour
{
    public GameObject player;
    private BoxCollider2D deathCollider;
    void Start()
    {
        deathCollider = transform.GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.GetComponent<PlayerCombat>().RealDeath();
    }
}
