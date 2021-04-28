using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueScript2 : MonoBehaviour
{
    private CameraScript cam;
    private bool done = false;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<CameraScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!done)
        {
            if (collision.gameObject.tag == "Player")
            {
                cam.Darkness();
                cam.Night();
                done = true;
            }
        }
    }
}

