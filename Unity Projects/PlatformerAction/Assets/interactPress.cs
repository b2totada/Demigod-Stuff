using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class interactPress : MonoBehaviour
{
    public GameObject player;
    public Sprite normal;
    public Sprite pressed;
    private SpriteRenderer sr;
    private void Start()
    {
        sr = transform.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (player.GetComponent<CharacterController2D>().m_FacingRight)
        {
            sr.flipX = false;
        }
        else
        {
            sr.flipX = true;
        }
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Level3"))
        {
            if ((player.transform.position - GameObject.Find("TowerDoor").transform.position).magnitude > 1)
            {
                sr.enabled = false;
            }
            else
            {
                sr.enabled = true;
            }
        }

        if (sr.enabled)
        {
            transform.GetComponent<Animation>().Play();
            if (Input.GetKey(KeyCode.E))
            {
                sr.sprite = pressed;
            }
            else
            {
                sr.sprite = normal;
            }
        }
    }
}
