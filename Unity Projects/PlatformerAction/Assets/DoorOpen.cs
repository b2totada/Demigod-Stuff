using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorOpen : MonoBehaviour
{
    public GameObject player;
    public GameObject interact;
    public Sprite doorClosed;
    public Sprite doorOpened;
    public static bool interactSpawned;
    private void Start()
    {
        transform.GetComponent<SpriteRenderer>().sprite = doorClosed;
    }
    void Update()
    {
        if ((transform.position - player.transform.position).magnitude < 1f)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                transform.GetComponent<SpriteRenderer>().sprite = doorOpened;
                Invoke("LoadTowerScene", 1f);
            }
        }
    }
    void LoadTowerScene() 
    {
        SceneManager.LoadScene("TowerInside");
    }
}
