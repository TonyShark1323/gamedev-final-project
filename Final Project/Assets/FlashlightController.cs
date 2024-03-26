using System;
using System.Collections;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public GameObject flashlight;
    private bool isFlashlightOn = false;
    public bool hasFlashlight = false; // Flag to check if the player has the flashlight


    public void PickupFlashlight()
    {
        hasFlashlight = true;
        UIManager.Instance.ShowMessage("Press 'F' to use the Flashlight", 5f);
    }

    void Start()
    {
        flashlight.SetActive(false); // Ensure flashlight is off at the start
    }

    void Update()
    {
        // Check if the player is in reach, has the flashlight, and presses the 'F' key
        if (hasFlashlight && Input.GetKeyDown(KeyCode.F))
        {
                // Toggle the flashlight on or off if the player already has it
                isFlashlightOn = !isFlashlightOn;
                flashlight.SetActive(isFlashlightOn);
                // Optionally play sound or show UI feedback for toggling the flashlight
        }
    }
}
