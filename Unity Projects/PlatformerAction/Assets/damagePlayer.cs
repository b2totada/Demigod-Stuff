using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damagePlayer : MonoBehaviour
{
    private PlayerCombat player;
    private Enemy_behaviour enemy;
    public int attackDamage;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy_weapon"))
        {
            enemy = GameObject.Find("Skeleton1").GetComponent<Enemy_behaviour>();
            attackDamage = enemy.attackDamage;
            player.TakeDamage(attackDamage);
        }
    }
}
