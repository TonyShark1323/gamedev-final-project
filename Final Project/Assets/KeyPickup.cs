using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public DoorControllerNoAnim specificDoorToUnlock; // Assign the specific DoorController in the Inspector
    private bool inReach;
    public AudioSource keyPickup;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Reach"))
        {
            inReach = true;
            UIManager.Instance.ShowPickupText(true); // Show appropriate text based on the light's state
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the collider is the player
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            UIManager.Instance.ShowPickupText(false);
        }
    }

    void Update()
    {
        // Check for the 'E' key press and if the pick up text is active as an indication of reachability
        if(Input.GetKeyDown(KeyCode.E) && inReach)
        {
            if (specificDoorToUnlock != null)
            {
                specificDoorToUnlock.isLocked = false; // Unlock the specific door
                // Optionally, provide feedback to the player
                // UIManager.Instance.ShowKeyPickupText(specificDoorToUnlock.gameObject.name + " unlocked!");
            }

            Destroy(gameObject);
            keyPickup.Play();
            UIManager.Instance.HideTexts();
        }
    }
}
