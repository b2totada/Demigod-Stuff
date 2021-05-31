using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class quitOptions : MonoBehaviour
{
    public GameObject Menu;
    public void ExitOptions() 
    {
        Menu.SetActive(true);
        gameObject.SetActive(false);
    }
}
