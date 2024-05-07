using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightPickup : MonoBehaviour
{
    public FlashlightController FlashlightController;
    private bool inReach;
    public AudioSource pickupSound;

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
        if (Input.GetKeyDown(KeyCode.E) && inReach)
        {
            if (FlashlightController != null)
            {
                FlashlightController.PickupFlashlight(); // Enable flashlight and show message
            }
            Destroy(gameObject); // Remove or disable the flashlight pickup object
            pickupSound.Play();
            UIManager.Instance.HideTexts();
        }
    }
}
