using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    private Vector2 point1;
    private Vector2 point2;
    private Vector2 target;
    public int speed;

    // Start is called before the first frame update
    void Start()
    {
        // Gameobjects starting point
        point1 = transform.position;

        // Gameobjects original target -- CHANGE THIS FOR EVERY INSTANCE TO CUSTOMIZE MOVEMENT
        point2 = new Vector2(220f, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        // Select target to move to
        if (transform.position.x == point1.x)
        {
            target = point2;
        }
        else if (transform.position.x == point2.x)
        {
            target = point1;
        }

        // Move
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    // This sets Player as child of Gameobject so he stays on it
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.collider.transform.SetParent(transform);
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
