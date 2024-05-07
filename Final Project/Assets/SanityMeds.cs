using UnityEngine;

public class SanityMeds : MonoBehaviour
{
    private bool inReach;
    public AudioSource useMeds;
    [SerializeField]
    private float minRestorePercentage = 10f; // Minimum percentage of max sanity to restore
    [SerializeField]
    private float maxRestorePercentage = 20f; // Maximum percentage of max sanity to restore

    // Reference to the Sanity script on the player
    // This can be set up through dependency injection or by finding the player object
    private Sanity playerSanity;

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


    void Start()
    {
        // Find the player's Sanity component
        // This assumes the player has a tag called "Player" and has the Sanity script attached
        GameObject sanityController = GameObject.FindGameObjectWithTag("Sanity");
        if (sanityController != null)
        {
            playerSanity = sanityController.GetComponent<Sanity>();
            if (playerSanity == null)
            {
                Debug.LogError("Sanity component not found on the player!");
            }
        }
        else
        {
            Debug.LogError("Player object not found!");
        }
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.E) && inReach){
            Use();
            UIManager.Instance.HideTexts();
        }
    }
    // Call this method when the player interacts with the SanityMeds object
    public void Use()
    {
        if (playerSanity != null)
        {
            // Calculate the amount to restore
            float restoreAmount = Random.Range(minRestorePercentage, maxRestorePercentage) / 100 * playerSanity.maxSanity;
            int roundedRestoreAmount = Mathf.RoundToInt(restoreAmount);
            useMeds.Play();

            playerSanity.RestoreSanity(roundedRestoreAmount);
            UIManager.Instance.ShowMessage("Restored " + roundedRestoreAmount + "% sanity", 5f);

            // Optionally, deactivate or destroy the SanityMeds object after use
            ObjectPool.Instance.ReturnToPool(gameObject);
            // or Destroy(gameObject);
        }
    }
}
