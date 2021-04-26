using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necromancer : MonoBehaviour
{
    public GameObject player;
    public GameObject skullFlame;
    public GameObject trapDoor;
    private bool inCombat;
    private int phase;
    private int attack2Counter;
    private OrbSpawnerScript orbSpawnerScript;
    void Start()
    {
        phase = 0;
        GameObject orbspawner = GameObject.Find("OrbSpawner");
        orbSpawnerScript = orbspawner.GetComponent<OrbSpawnerScript>();
    }

    void Update()
    {
        if (trapDoor.GetComponent<BoxCollider2D>().enabled && !inCombat)
        {
            inCombat = true;
            phase = 1;
        }
        if (inCombat && phase == 1)
        {
            //Invoke("Attack2", 3f);
        }
    }
    void Attack2() 
    {
        transform.GetComponent<Animator>().SetTrigger("Attack2");

       
        attack2Counter++;
        if (attack2Counter == 3)
        {
            
            //CancelInvoke();
            attack2Counter = 0;
        }
    }
}
