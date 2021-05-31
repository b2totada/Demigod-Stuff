using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript_3 : MonoBehaviour
{
    private Vector2 point1;
    private Vector2 point2;
    private Vector2 target;
    public int speed;
    public bool playerReady = false;

    // Start is called before the first frame update
    void Start()
    {
        // Gameobjects original target -- CHANGE THIS FOR EVERY INSTANCE TO CUSTOMIZE MOVEMENT
        target = new Vector2(transform.position.x, 40f);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerReady)
        {
            // Move
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }

        if (transform.position.y == target.y)
        {
            playerReady = false;
        }
    }

    // This sets Player as child of Gameobject so he stays on it
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.collider.transform.SetParent(transform);
            playerReady = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.collider.transform.SetParent(null);
        }
    }
}
