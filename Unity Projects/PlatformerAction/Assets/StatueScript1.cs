using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatueScript1 : MonoBehaviour
{
    private bool done = false;
    private CameraScript cam;
    private void Start()
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
                StartCoroutine(LoadYourAsyncScene());
                done = true;
            }
        }
    }
    public IEnumerator LoadYourAsyncScene()
    {
        // Set the current Scene to be able to unload it later
        Scene currentScene = SceneManager.GetActiveScene();

        // The Application loads the Scene in the background at the same time as the current Scene.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);

        // Wait until the last operation fully loads to return anything
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Move the GameObject (you attach this in the Inspector) to the newly loaded Scene
        SceneManager.MoveGameObjectToScene(GameObject.Find("Main Camera"), SceneManager.GetSceneByName("Level2"));
        SceneManager.MoveGameObjectToScene(GameObject.Find("GUI"), SceneManager.GetSceneByName("Level2"));
        SceneManager.MoveGameObjectToScene(GameObject.Find("Player"), SceneManager.GetSceneByName("Level2"));
        SceneManager.MoveGameObjectToScene(GameObject.Find("CM vcam1"), SceneManager.GetSceneByName("Level2"));
        SceneManager.MoveGameObjectToScene(GameObject.Find("Canvas"), SceneManager.GetSceneByName("Level2"));
        SceneManager.MoveGameObjectToScene(GameObject.Find("Canvas_GameComplete"), SceneManager.GetSceneByName("Level2"));
        GameObject.Find("Player").GetComponent<PlayerCombat>().PlayerToSpawnPoint();

        // Unload the previous Scene
        SceneManager.UnloadSceneAsync(currentScene);
    }
}
