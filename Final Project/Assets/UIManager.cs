using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject openDrawerText;
    public GameObject closeDrawerText;
    public GameObject lightOnText; // Add this for On text
    public GameObject lightOffText; // Add this for Off text
    public GameObject readNoteText;

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
        lightOnText.SetActive(false);
        lightOffText.SetActive(false);
    }

    // Method to show/hide the read note text
    public void ShowReadNoteText(bool show)
    {
        if (readNoteText != null)
        {
            readNoteText.SetActive(show);
        }
    }
}
