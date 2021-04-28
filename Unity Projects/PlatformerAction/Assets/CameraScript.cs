using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraScript : MonoBehaviour
{
    private Scene scene;
    private Camera cam;
    private int oldMask;
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
        if (scene.name == "Level1")
        {
            
        }
    }

    public void Night()
    {
        transform.GetComponentInChildren<SpriteRenderer>().sprite = night;
    }

    public void Evening()
    {
        transform.GetComponentInChildren<SpriteRenderer>().sprite = evening;
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
