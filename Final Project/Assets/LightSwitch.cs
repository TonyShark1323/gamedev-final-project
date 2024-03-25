using UnityEngine;
using System.Collections.Generic;

public class LightSwitch : MonoBehaviour
{
    public GameObject onModel; // Reference to the GameObject representing the "on" state model
    public GameObject offModel; // Reference to the GameObject representing the "off" state model
    public List<GameObject> lightsParents; // Hold references to multiple light parent objects


    private bool inReach;
    private bool on; // Represents whether the lights are on or off
    
    // Start is called before the first frame update
    void Start()
    {
        on = false;
        // Initialize models to reflect the light being off initially.
        if (onModel != null) onModel.SetActive(false);
        if (offModel != null) offModel.SetActive(true);

        foreach (var lightsParent in lightsParents)
        {
            if (lightsParent != null)
            {
                lightsParent.SetActive(false);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Reach"))
        {
            inReach = true;
            UIManager.Instance.ShowLightText(!on); // Show appropriate text based on the light's state
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Reach"))
        {
            inReach = false;
            UIManager.Instance.HideTexts(); // Hide text when out of reach
        }
    }

    void Update()
    {
        if (inReach && Input.GetKeyDown(KeyCode.E))
        {
            on = !on; // Toggle the state

            foreach (var lightsParent in lightsParents)
            {
                if (lightsParent != null)
                {
                    lightsParent.SetActive(on); // Set each parent GameObject's active state.
                }
            }

            // Update model visibility to reflect the new state
            if (onModel != null) onModel.SetActive(on);
            if (offModel != null) offModel.SetActive(!on);

            if (inReach) // Check if still in reach to update the text correctly
            {
                UIManager.Instance.ShowLightText(!on);
            }
        }
    }
}
