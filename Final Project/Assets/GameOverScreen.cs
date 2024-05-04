using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameOverScreen : MonoBehaviour
{
    public GameObject player;
    
    void Start() {
        // Make the cursor visible and unlock it when the game over screen is shown
        player.GetComponent<Player>().enabled = false;
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void RestartButton() {
        // Optionally re-lock the cursor and make it invisible if that suits your game's design
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Time.timeScale = 1f;
        SceneManager.LoadScene("RoomMaze");
    }

    public void ExitButton() {
        // Assuming you want the cursor to remain visible in the main menu
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
