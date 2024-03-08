using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Import the SceneManager

public class Portal : MonoBehaviour
{
    [SerializeField] Transform destination;
    [SerializeField] string sceneToLoad; // Add a field to specify the scene name

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && other.TryGetComponent<Player>(out var player))
        {
            // Here you can still teleport the player if needed or directly load the new scene
            // player.Teleport(destination.position, destination.rotation);
            
            // Load the specified scene
            SceneManager.LoadScene("RoomMaze");
        }
    }
    
    void OnDrawGizmos()
    {
        if (destination != null) // Ensure destination is not null
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(destination.position, .4f);
            var direction = destination.TransformDirection(Vector3.forward);
            Gizmos.DrawRay(destination.position, direction);
        }
    }
}// public class Portal : MonoBehaviour
// {
//     [SerializeField] Transform destination;
//     private bool playerIsNear = false; // Track whether the player is near the portal

//     void Update()
//     {
//         // Check if the player is near the portal and if the E key is pressed
//         if (playerIsNear && Input.GetKeyDown(KeyCode.E))
//         {
//             Debug.Log("Pressed E");
//             // Check if the player component is available and then teleport
//             if (TryGetComponent<Player>(out var player))
//             {
//                 player.Teleport(destination.position, destination.rotation);
//             }
//         }
//     }

//     void OnTriggerEnter(Collider other)
//     {
//         // Check if the colliding object is the player
//         if (other.CompareTag("Player"))
//         {
//             playerIsNear = true; // Set flag to true when the player enters the trigger area
//             Debug.Log("Player is near");
//         }
//     }

//     void OnTriggerExit(Collider other)
//     {
//         // Reset the flag when the player exits the trigger area
//         if (other.CompareTag("Player"))
//         {
//             playerIsNear = false;
//         }
//     }

//     void OnDrawGizmos()
//     {
//         Gizmos.color = Color.white;
//         Gizmos.DrawWireSphere(destination.position, .4f);
//         var direction = destination.TransformDirection(Vector3.forward);
//         Gizmos.DrawRay(destination.position, direction);
//     }
// }