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

    void Update()
    {
        if (deathCollider.IsTouching(player.GetComponent<CircleCollider2D>()))
        {
            player.GetComponent<PlayerCombat>().RealDeath();
        }
    }
}
