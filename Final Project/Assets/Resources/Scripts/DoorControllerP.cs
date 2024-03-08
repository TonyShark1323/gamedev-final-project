using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DoorControllerP : MonoBehaviour
{
    public bool keyNeeded = false;              //Is key needed for the door
    public bool gotKey;                  //Has the player acquired key
    public bool inReach;
    public GameObject keyGameObject;            //If player has Key,  assign it here
    public TextMeshProUGUI txtToDisplay;             //Display the information about how to close/open the door

    private bool playerInZone;                  //Check if the player is in the zone
    private bool doorOpened;                    //Check if door is currently opened or not

    private Animation doorAnim;
    private BoxCollider doorCollider;           //To enable the player to go through the door if door is opened else block him

    enum DoorState
    {
        Closed,
        Opened,
        Jammed
    }

    DoorState doorState = new DoorState();      //To check the current state of the door

    /// <summary>
    /// Initial State of every variables
    /// </summary>
    private void Start()
    {
        inReach = false;
        gotKey = false;
        doorOpened = false;                     //Is the door currently opened
        playerInZone = false;                   //Player not in zone
        doorState = DoorState.Closed;           //Starting state is door closed

        txtToDisplay.gameObject.SetActive(false);

        // doorAnim = transform.parent.gameObject.GetComponent<Animation>();
        // doorCollider = transform.parent.gameObject.GetComponent<BoxCollider>();

        doorAnim = transform.gameObject.GetComponent<Animation>();
        doorCollider = transform.gameObject.GetComponent<BoxCollider>();

        //If Key is needed and the KeyGameObject is not assigned, stop playing and throw error
        if (keyNeeded && keyGameObject == null)
        {
            //UnityEditor.EditorApplication.isPlaying = false;
            Debug.LogError("Assign Key GameObject");
        }
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject.tag != "Reach") {
    //         txtToDisplay.gameObject.SetActive(true);
    //         playerInZone = true;
    //     }
        
    // }

    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.gameObject.tag != "Reach") {
    //         playerInZone = false;
    //         txtToDisplay.gameObject.SetActive(true);
    //     }
    // }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            txtToDisplay.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            txtToDisplay.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        //To Check if the player is in Reach
        if (inReach)
        {
            if (doorState == DoorState.Opened)
            {
                txtToDisplay.text = "Press 'E' to Close";
                doorCollider.enabled = true;
            }
            else if (doorState == DoorState.Closed || gotKey)
            {
                txtToDisplay.text = "Press 'E' to Open";
                doorCollider.enabled = true;
            }
            else if (doorState == DoorState.Jammed)
            {
                txtToDisplay.text = "Needs Key";
                doorCollider.enabled = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && inReach)
        {
            doorOpened = !doorOpened;           //The toggle function of door to open/close

            if (doorState == DoorState.Closed && !doorAnim.isPlaying)
            {
                if (!keyNeeded)
                {
                    doorAnim.Play("Open");
                    doorState = DoorState.Opened;
                }
                else if (keyNeeded && !gotKey)
                {
                    // if (doorAnim.GetClip("Door_Jam") != null)
                    //     doorAnim.Play("Door_Jam");
                    doorState = DoorState.Jammed;
                }
            }

            if (doorState == DoorState.Closed && gotKey && !doorAnim.isPlaying)
            {
                doorAnim.Play("Open");
                doorState = DoorState.Opened;
            }

            if (doorState == DoorState.Opened && !doorAnim.isPlaying)
            {
                doorAnim.Play("Close");
                doorState = DoorState.Closed;
            }

            if (doorState == DoorState.Jammed && !gotKey)
            {
                // if (doorAnim.GetClip("Door_Jam") != null)
                //     doorAnim.Play("Door_Jam");
                doorState = DoorState.Jammed;
            }
            else if (doorState == DoorState.Jammed && gotKey && !doorAnim.isPlaying)
            {
                doorAnim.Play("Open");
                doorState = DoorState.Opened;
            }
        }
    }
}
