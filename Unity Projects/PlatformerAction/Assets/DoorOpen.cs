using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorOpen : MonoBehaviour
{
    public GameObject player;
    public Sprite doorClosed;
    public Sprite doorOpened;
    public string loadSceneName;
    private void Start()
    {
        transform.GetComponent<SpriteRenderer>().sprite = doorClosed;
    }
    void Update()
    {
        if ((player.transform.position - transform.position).magnitude < 1)
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
        SceneManager.LoadScene(loadSceneName);
    }
}
