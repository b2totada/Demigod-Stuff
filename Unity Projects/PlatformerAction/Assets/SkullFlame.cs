using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullFlame : MonoBehaviour
{
    private GameObject player;
    private void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        transform.LookAt(player.transform);
        transform.Translate(transform.forward);
    }
}
