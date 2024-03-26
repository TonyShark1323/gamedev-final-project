// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Flashlight : MonoBehaviour
// {
//     public GameObject flashlight;

//     // public AudioSource turnOn;
//     // public AudioSource turnOff;

//     public bool on;
//     public bool off;




//     void Start()
//     {
//         off = true;
//         flashlight.SetActive(false);
//     }




//     void Update()
//     {
//         if(off && Input.GetKeyDown(KeyCode.F))
//         {
//             flashlight.SetActive(true);
//             // turnOn.Play();
//             off = false;
//             on = true;
//         }
//         else if (on && Input.GetKeyDown(KeyCode.F))
//         {
//             flashlight.SetActive(false);
//             // turnOff.Play();
//             off = true;
//             on = false;
//         }



//     }
// }

using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public GameObject flashlight;
    private bool isFlashlightOn = false;
    public bool hasFlashlight = false; // Flag to check if the player has the flashlight

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
