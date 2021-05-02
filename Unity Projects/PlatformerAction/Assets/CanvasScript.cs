using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{
    private Camera cam;
    private Canvas myCanvas;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        myCanvas = GetComponent<Canvas>();
        myCanvas.worldCamera = cam;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
