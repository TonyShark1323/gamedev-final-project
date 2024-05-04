using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject player;
    

    public static bool isPaused = false;

    // void Start() {
    //     // Make the cursor visible and unlock it when the game over screen is shown
    //     player.GetComponent<Player>().enabled = false;
    //     Time.timeScale = 0f;
    //     Cursor.visible = true;
    //     Cursor.lockState = CursorLockMode.None;
    // }

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
        // Cursor.visible = false;
        Debug.Log("Resume Called");
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        player.GetComponent<Player>().enabled = false;
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        // Assuming FirstPersonController is the script that controls the player movement and camera,
        // disable it to stop player movement while reading the note.
        isPaused = true;
    }

    public void MainMenu() {
        // Assuming you want the cursor to remain visible in the main menu
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