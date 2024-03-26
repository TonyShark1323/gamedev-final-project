using UnityEngine;
using TMPro; // Make sure to include this for TextMeshPro
using UnityEngine.SceneManagement;

public class Sanity : MonoBehaviour
{
    public float maxSanity = 100f; // Maximum sanity level
    private float currentSanity; // Current sanity level
    public float sanityDecreaseRate = 20f; // Rate at which sanity decreases per minute
    public TextMeshProUGUI sanityText; // Reference to the TextMeshPro UI element to display sanity
    private bool sanityActive = true;
    public GameObject gameOverPanel; // Assign this in the Inspector



    void Start()
    {
        currentSanity = maxSanity;

        // Check if the current scene is not the first scene
        // if (SceneManager.GetActiveScene().buildIndex > 1)
        // {
        //     // Debug.Log(SceneManager.GetActiveScene().buildIndex);
        //     sanityActive = true;
        // }
    }

    void Update()
    {
        // Decrease sanity over time
        currentSanity -= (sanityDecreaseRate / 60) * Time.deltaTime;

        // Ensure current sanity does not go below 0
        currentSanity = Mathf.Max(currentSanity, 0);

        // Update the TextMeshPro text to display the current sanity percentage
        if (sanityText != null)
        {
            sanityText.text = "Sanity: " + ((currentSanity / maxSanity) * 100).ToString("F0") + "%";
        }

        // Check if sanity has reached 0
        if (currentSanity <= 0)
        {
            // Player has lost all sanity; trigger death or failure state
            ShowGameOverScreen();
        }
    }

    void ShowGameOverScreen()
    {
        sanityActive = false; // Stop decreasing sanity or other gameplay logic
        gameOverPanel.SetActive(true); // Show the game over screen
        // Optionally, freeze game time or disable player controls here
        //Time.timeScale = 0f;
    }
}
