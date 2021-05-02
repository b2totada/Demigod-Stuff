using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorOpen : MonoBehaviour
{
    public Sprite doorClosed;
    public Sprite doorOpened;
    public string loadSceneName;
    private GameObject player;
    private void Start()
    {
        player = GameObject.Find("Player");
        transform.GetComponent<SpriteRenderer>().sprite = doorClosed;
    }
    void Update()
    {
        if ((player.transform.position - transform.position).magnitude < 1)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                transform.GetComponent<SpriteRenderer>().sprite = doorOpened;
                StartCoroutine(LoadYourAsyncScene());
            }
        }
    }

    public IEnumerator LoadYourAsyncScene()
    {
        // Set the current Scene to be able to unload it later
        Scene currentScene = SceneManager.GetActiveScene();

        // The Application loads the Scene in the background at the same time as the current Scene.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(4, LoadSceneMode.Additive);

        // Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Move the GameObject (you attach this in the Inspector) to the newly loaded Scene
        SceneManager.MoveGameObjectToScene(GameObject.Find("Main Camera"), SceneManager.GetSceneByName("TowerInside"));
        SceneManager.MoveGameObjectToScene(GameObject.Find("GUI"), SceneManager.GetSceneByName("TowerInside"));
        SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByName("TowerInside"));
        SceneManager.MoveGameObjectToScene(GameObject.Find("CM vcam1"), SceneManager.GetSceneByName("TowerInside"));
        SceneManager.MoveGameObjectToScene(GameObject.Find("Canvas"), SceneManager.GetSceneByName("TowerInside"));
        SceneManager.MoveGameObjectToScene(GameObject.Find("Canvas_GameComplete"), SceneManager.GetSceneByName("TowerInside"));
        player.transform.position = GameObject.Find("TowerSpawn").transform.position;
        // Unload the previous Scene
        SceneManager.UnloadSceneAsync(currentScene);
    }
}
