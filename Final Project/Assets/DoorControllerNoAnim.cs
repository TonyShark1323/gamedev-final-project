using UnityEngine;

public class DoorControllerNoAnim : MonoBehaviour
{
    public Transform door; // Assign the door GameObject
    public float openAngle = 90f; // Angle to open the door
    public float openSpeed = 2f; // Speed at which the door opens
    public bool isLocked = true; // Initially, the door is locked

    public AudioSource open;
    public AudioSource jammed;


    private bool isOpen = false; // Track whether the door is open or closed
    private bool inReach = false; // Whether the player is in reach to interact with the door
    private float currentAngle = 0f; // Current rotation angle of the door

    // Static variable to track if the player has picked up the key
    // This can be set to true from another script when the player picks up the key
    public static bool playerHasKey = false;

    void Start()
    {
        // Initialize currentAngle based on the door's starting rotation
        currentAngle = door.localEulerAngles.y;
    }

    void Update()
    {
        if (inReach && Input.GetKeyDown(KeyCode.E))
        {
            if (isLocked)
            {
                if (playerHasKey)
                {
                    isLocked = false; // Unlock the door if the player has the key
                    isOpen = true; // And open the door
                    open.Play();
                }
                else
                {
                    // If the door is locked and the player doesn't have the key, show locked door text
                    UIManager.Instance.HideTexts();
                    UIManager.Instance.ShowDoorLockedText(true);
                    // jammed.Play();
                    return; // Skip attempting to open the door this frame
                }
            }
            else
            {
                isOpen = !isOpen; // Toggle the door open state if it's not locked
                open.Play();
            }
        }

        RotateDoor();
        if (inReach && isLocked == false) // Check if still in reach to update the text correctly
            {
                UIManager.Instance.ShowDoorText(!isOpen);
            }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Reach")) // Make sure this tag matches your player GameObject
        {
            inReach = true;
            UIManager.Instance.ShowDoorText(!isOpen);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Reach"))
        {
            inReach = false;
            UIManager.Instance.HideTexts(); // Adjust UIManager to hide interaction text
        }
    }

    void RotateDoor()
    {
        // Calculate target angle based on whether the door is opening or closing
        float targetAngle = isOpen ? openAngle : 0f;
        // Smoothly rotate the door towards the target angle
        currentAngle = Mathf.Lerp(currentAngle, targetAngle, Time.deltaTime * openSpeed);
        door.localEulerAngles = new Vector3(door.localEulerAngles.x, currentAngle, door.localEulerAngles.z);
    }
}
