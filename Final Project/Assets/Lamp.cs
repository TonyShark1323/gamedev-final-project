using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    public GameObject onText;
    public GameObject offText;
    public GameObject lightsParent; // Reference to the parent GameObject of the lights

    private bool inReach;
    private bool on;
    
    // Start is called before the first frame update
    void Start()
    {
        onText.SetActive(false);
        offText.SetActive(false);
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
            if (!on)
            {
                onText.SetActive(true);
            }
            else
            {
                offText.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            onText.SetActive(false);
            offText.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inReach && Input.GetKeyDown(KeyCode.E))
        {
            on = !on; // Toggle the state

            // Ensure the lightsParent reference is valid before trying to access it.
            if (lightsParent != null)
            {
                lightsParent.SetActive(on); // Set the parent GameObject's active state.
            }

            onText.SetActive(on && inReach);
            offText.SetActive(!on && inReach);
            inReach = false; // Optionally, move this line out if you want the text to update without leaving the trigger
        }
    }
}
