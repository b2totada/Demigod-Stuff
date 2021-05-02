using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScriptTower : MonoBehaviour
{
    private Camera cam; //try public if not working
    private GameObject panel;
    private GameObject textGame;
    private GameObject textComplete;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        panel = GameObject.Find("Canvas_GameComplete/PanelGameCompleted");
        textGame = GameObject.Find("Canvas_GameComplete/PanelGameCompleted/TextGame");
        textComplete = GameObject.Find("Canvas_GameComplete/PanelGameCompleted/TextComplete");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameComplete()
    {
        //panel.SetActive(true);
        //textGame.SetActive(true);
        cam.cullingMask = (1 << LayerMask.NameToLayer("GameComplete"));
        Invoke("TextComplete", 2f);
    }

    void TextComplete()
    {
        textGame.SetActive(false);
        textComplete.SetActive(true);
        Invoke("FinalMethod", 2f);
    }

    void FinalMethod()
    {
        textComplete.SetActive(false);
        panel.SetActive(false);
        Destroy(gameObject);
    }
}
