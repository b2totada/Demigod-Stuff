using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * Random.Range(-0.5f, -0.2f) * Time.deltaTime);
        if (transform.position.x < -20)
        {
            Destroy(this.gameObject);
        }
    }
}
