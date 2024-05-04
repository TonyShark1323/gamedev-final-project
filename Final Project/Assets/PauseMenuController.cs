using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public static bool isPaused = false;
    [SerializeField] GameObject pauseMenu;

    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if (isPaused)
            {
                Time.timeScale = 1f;
                pauseMenu.SetActive(false);
            }
            else
            {
                pauseMenu.SetActive(true);
                isPaused = true;
            }
        }
    }
}
