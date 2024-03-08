using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReadNotes : MonoBehaviour
{
    public GameObject player;
    public GameObject noteUI;
    public GameObject hud;
    public GameObject inv;

    public bool inReach;

    public TextMeshProUGUI pickUpText;

    public AudioSource pickUpSound;

    // This variable is no longer needed as the presence within the box collider determines reachability
    // public bool inReach;

    void Start()
    {
        if (hud != null) {
            hud.SetActive(true);
        }
        if (inv != null) {
            inv.SetActive(false);
        }

        noteUI.SetActive(false);
        hud.SetActive(true);
        inv.SetActive(true);
        pickUpText.gameObject.SetActive(false);
        inReach = false;
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the collider is the player
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            pickUpText.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the collider is the player
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            pickUpText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Check for the 'E' key press and if the pick up text is active as an indication of reachability
        if(Input.GetKeyDown(KeyCode.E) && inReach)
        {
            noteUI.SetActive(true);
            if (pickUpSound!= null) {
                pickUpSound.Play();
            }
            if (hud != null) {
                hud.SetActive(false);
            }
            if (inv != null) {
                inv.SetActive(false);
            }
            // Assuming FirstPersonController is the script that controls the player movement and camera,
            // disable it to stop player movement while reading the note.
            player.GetComponent<Player>().enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void ExitButton()
    {
        noteUI.SetActive(false);
        hud.SetActive(true);
        inv.SetActive(true);
        player.GetComponent<Player>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
