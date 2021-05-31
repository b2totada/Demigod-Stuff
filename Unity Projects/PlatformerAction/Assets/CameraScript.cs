using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraScript : MonoBehaviour
{
    private Scene scene;
    [HideInInspector] public Camera cam;
    public Sprite sunShine;
    public Sprite evening;
    public Sprite night;
    // Start is called before the first frame update
    void Start()
    {       
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        scene = SceneManager.GetActiveScene();

        if (scene == SceneManager.GetSceneByName("Level1") && transform.GetComponentInChildren<SpriteRenderer>().sprite != sunShine)
        {
            transform.GetComponentInChildren<SpriteRenderer>().sprite = sunShine;
        }
        if (scene == SceneManager.GetSceneByName("Level2") && transform.GetComponentInChildren<SpriteRenderer>().sprite != evening)
        {
            transform.GetComponentInChildren<SpriteRenderer>().sprite = evening;
        }
        if (scene == SceneManager.GetSceneByName("Level3") && transform.GetComponentInChildren<SpriteRenderer>().sprite != night)
        {
            transform.GetComponentInChildren<SpriteRenderer>().sprite = night;
        }
    }

    public void Darkness()
    {
        cam.cullingMask = 0;
        Invoke("NoDarkness", 2);
    }

    public void NoDarkness()
    {
        cam.cullingMask = -1;
    }
}
