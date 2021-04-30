using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraScript : MonoBehaviour
{
    private Scene scene;
    private Camera cam;
    private int oldMask;
    public Sprite sunShine;
    public Sprite evening;
    public Sprite night;
    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        cam = GetComponent<Camera>();
        int oldMask = cam.cullingMask;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level1") && transform.GetComponentInChildren<SpriteRenderer>().sprite != sunShine)
        {
            transform.GetComponentInChildren<SpriteRenderer>().sprite = sunShine;
        }
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level2") && transform.GetComponentInChildren<SpriteRenderer>().sprite != evening)
        {
            transform.GetComponentInChildren<SpriteRenderer>().sprite = evening;
        }
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level3") && transform.GetComponentInChildren<SpriteRenderer>().sprite != night)
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
