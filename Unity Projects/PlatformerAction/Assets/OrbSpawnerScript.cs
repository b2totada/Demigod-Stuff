using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbSpawnerScript : MonoBehaviour
{
    public GameObject SkullOrb;
    private bool canCast = true;
    void Update()
    {
        if (canCast)
        {
            canCast = false;
            Invoke("Cast", 1.5f);
        }
    }

    void Cast()
    {
        Instantiate(SkullOrb, transform.position, Quaternion.Euler(0, 0, 0));
        canCast = true;
    }
}
