using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject hudGameObject; // Assign the GameObject containing the HUD in the inspector
    
    void Awake()
    {
        // Optionally check if the HUD GameObject is assigned
        if (hudGameObject != null)
        {
            // Disable the HUD GameObject
            hudGameObject.SetActive(false);
            
            // Re-enable the HUD GameObject
            // This is immediate here, but you could delay this or trigger it based on specific conditions
            hudGameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("HUD GameObject is not assigned to the GameController.", this);
        }
    }
}
