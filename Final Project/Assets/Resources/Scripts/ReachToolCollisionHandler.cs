using UnityEngine;
using System.Collections; // Required for using Coroutines

public class ReachToolCollisionHandler : MonoBehaviour
{
    // Reference to the reach tool's active state controller, assuming it's a separate script or gameObject
    public GameObject reachTool; 

    private void OnTriggerEnter(Collider other)
    {
        // If the reach tool itself collides with an object tagged "Unreachable", deactivate the reach tool
        if (other.CompareTag("Unreachable"))
        {
            reachTool.SetActive(false);
            // Start the coroutine to reactivate the reach tool after half a second
            StartCoroutine(ReactivateReachToolAfterDelay(0.5f));
        }
    }

    private IEnumerator ReactivateReachToolAfterDelay(float delay)
    {
        // Wait for the specified delay duration
        yield return new WaitForSeconds(delay);
        // Reactivate the reach tool
        reachTool.SetActive(true);
    }
}
