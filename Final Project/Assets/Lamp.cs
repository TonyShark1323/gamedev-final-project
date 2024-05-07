using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    public GameObject lightsParent; // Reference to the parent GameObject of the lights
    public AudioSource lightSound;

    private bool inReach;
    private bool on;
    
    // Start is called before the first frame update
    void Start()
    {
        on = false;
        // Ensure the lights start in the correct state (off).
        if (lightsParent != null)
        {
            lightsParent.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            UIManager.Instance.ShowLightText(!on);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            UIManager.Instance.HideTexts();
        }
    }

    void Update()
    {
        if (inReach && Input.GetKeyDown(KeyCode.E))
        {
            on = !on; // Toggle the state
            if (lightSound != null)
                lightSound.Play();

            // Ensure the lightsParent reference is valid before trying to access it.
            if (lightsParent != null)
            {
                lightsParent.SetActive(on); // Set the parent GameObject's active state.
            }

            if (inReach) // Check if still in reach to update the text correctly
            {
                UIManager.Instance.ShowLightText(!on);
            }
        }
    }
}
