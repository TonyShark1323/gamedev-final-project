using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameTrigger : MonoBehaviour
{
    // Make sure this GameObject has a Collider component with the "Is Trigger" option enabled.

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            // Load the GameEnd scene
            SceneManager.LoadScene("GameEnd");
        }
    }
}
