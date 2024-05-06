using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject player;

    public static bool isPaused = false;    

    void Start()
    {
        // Check if the pause menu UI is active on start and disable it if it is
        if (pauseMenuUI.activeSelf)
        {
            pauseMenuUI.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        player.GetComponent<Player>().enabled = true;
        pauseMenuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        isPaused = false;
        Debug.Log("Resume Called");
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        player.GetComponent<Player>().enabled = false;
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        isPaused = true;
    }

    public void MainMenu() {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1f;
        isPaused = false;
        Debug.Log("Returning to main menu");
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}