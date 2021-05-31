using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject OptionsMenu;
    public bool gamePaused;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gamePaused)
        {
            PauseGame();
            gamePaused = true;
        }
    }

    void PauseGame()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true;
    }
    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
        AudioListener.pause = false;
        gamePaused = false;
    }
    public void OpenOptionsMenu() 
    {
        OptionsMenu.SetActive(true);
        PauseMenu.SetActive(false);
    }
    public void CloseOptionsMenu() 
    {
        OptionsMenu.SetActive(false);
        PauseMenu.SetActive(true);
    }
    public void Quit() 
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        SceneManager.LoadScene(0);
    }

}
