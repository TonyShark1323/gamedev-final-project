using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject openDrawerText;
    public GameObject closeDrawerText;
    public GameObject openDoorText;
    public GameObject lockedDoorText;
    public GameObject closeDoorText;
    public GameObject lightOnText; // Add this for On text
    public GameObject lightOffText; // Add this for Off text
    public GameObject readNoteText;
    public GameObject pickupText;
    public GameObject keypadText;
    public TextMeshProUGUI messageText; // Assign this via the Inspector

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowDoorText(bool show)
    {
        openDoorText.SetActive(show);
        closeDoorText.SetActive(!show);
    }

    public void ShowDoorLockedText(bool show)
    {
        if (lockedDoorText != null)
        {
            lockedDoorText.SetActive(show);
        }
    }

    public void ShowDrawerText(bool show)
    {
        openDrawerText.SetActive(show);
        closeDrawerText.SetActive(!show);
    }

    public void ShowLightText(bool isOn)
    {
        lightOnText.SetActive(isOn);
        lightOffText.SetActive(!isOn);
    }

    public void HideTexts()
    {
        openDrawerText.SetActive(false);
        closeDrawerText.SetActive(false);
        openDoorText.SetActive(false);
        lockedDoorText.SetActive(false);
        pickupText.SetActive(false);
        closeDoorText.SetActive(false);
        lightOnText.SetActive(false);
        lightOffText.SetActive(false);
        keypadText.SetActive(false);
    }

    // Method to show/hide the read note text
    public void ShowReadNoteText(bool show)
    {
        if (readNoteText != null)
        {
            readNoteText.SetActive(show);
        }
    }

    // Method to show/hide the pickup text
    public void ShowPickupText(bool show)
    {
        if (pickupText != null)
        {
            pickupText.SetActive(show);
        }
    }

    public void ShowKeypadText(bool show)
    {
        if (keypadText != null)
        {
            keypadText.SetActive(show);
        }
    }

    public void ShowMessage(string message, float duration = 5f)
    {
        if (messageText != null)
        {
            StopAllCoroutines(); // Stop any previous message coroutines to avoid overlaps
            StartCoroutine(ShowMessageCoroutine(message, duration));
        }
    }

    private IEnumerator ShowMessageCoroutine(string message, float duration)
    {
        messageText.text = message; // Set the message text
        messageText.gameObject.SetActive(true); // Make sure the text is visible

        yield return new WaitForSeconds(duration); // Wait for the duration

        messageText.gameObject.SetActive(false); // Hide the text
    }

}
