using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    public GameObject options;
    public GameObject menu;
    public void PlayButtonPress()
    {
        SceneManager.LoadScene("Level1");
    }
    public void OptionsButtonPress()
    {
        options.SetActive(true);
        menu.SetActive(false);
    }
    public void QuitButtonPress()
    {
        Application.Quit();
    }
}
