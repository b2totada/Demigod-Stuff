using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoor : MonoBehaviour
{
    public Sprite open;
    public Sprite closed;
    public GameObject BossHealthBar;
    private GameObject player;
    private PolygonCollider2D trigger;
    private BoxCollider2D doorColl;
    private BoxCollider2D playerColl;
    private void Start()
    {
        player = GameObject.Find("Player");
        player.GetComponent<PlayerCombat>().PlayerToSpawnPoint();
        trigger = transform.GetChild(0).GetComponent<PolygonCollider2D>();
        doorColl = transform.GetComponent<BoxCollider2D>();
        playerColl = player.GetComponent<BoxCollider2D>();
        transform.GetComponent<SpriteRenderer>().sprite = open;
        doorColl.enabled = false;
        trigger.enabled = true;
    }
    void Update()
    {
        if (trigger.IsTouching(playerColl))
        {
            doorColl.enabled = true;
            trigger.enabled = false;
            transform.GetComponent<SpriteRenderer>().sprite = closed;
            BossHealthBar.SetActive(true);
            transform.GetComponent<AudioSource>().Play();
        }
    }
}
