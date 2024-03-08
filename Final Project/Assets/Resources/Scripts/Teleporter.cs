using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform teleportDestination; // Assign the teleport destination in the Inspector

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Box Triggered");
        if (other.CompareTag("Player")) // Make sure the colliding object is the player
        {
            Debug.Log("Is a player");
            other.transform.position = teleportDestination.position; // Teleport the player
            Debug.Log("Teleported Player");
        }
    }
}
