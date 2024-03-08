using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Script on Drag and drop will create box collider component automatically
[RequireComponent(typeof(BoxCollider))]
public class KeyControllerP : MonoBehaviour
{
    BoxCollider keyCollider;
    Rigidbody keyRB;
    public TextMeshProUGUI txtToDisplay;
    public DoorControllerP DC;
    public bool inReach = false;

    /// <summary>
    /// Incase user forgets to uncheck isTrigger in box collider
    /// This sets them automatically
    /// </summary>
    private void Start()
    {
        keyCollider = GetComponent<BoxCollider>();

        keyCollider.isTrigger = true;

        txtToDisplay.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = true;
            // DC.gotKey = true;
            txtToDisplay.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            inReach = false;
            txtToDisplay.gameObject.SetActive(false);
        }
    }

    private void Update(){
        if (Input.GetKeyDown(KeyCode.E) && inReach) {
            DC.gotKey = true;
            txtToDisplay.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
    
}
