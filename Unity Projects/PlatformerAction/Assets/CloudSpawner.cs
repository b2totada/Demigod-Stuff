using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject smallCloud;
    public GameObject bigCloud;
    // Update is called once per frame
    void Update()
    {
        int dice = Random.Range(0, 10000);
        if (dice < 5)
        {
            int x = Random.Range(0, 2);
            if (x == 1)
            {
                SpawnCloud(smallCloud);
            }
            else
            {
                SpawnCloud(bigCloud);
            }
        }
    }
    static void SpawnCloud(GameObject cloud) 
    {
        Vector2 spawnPos = new Vector2(15,Random.Range(1,9));
        Instantiate(cloud, spawnPos, Quaternion.Euler(0, 0, 0));
        Debug.Log(cloud.name + " spawned.");
    }
}
