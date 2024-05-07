// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;

// public class KeypadController : MonoBehaviour
// {
//     [SerializeField] GameObject player;
//     private bool inReach;
//     [SerializeField] TMP_Text displayText; // Assign this in the inspector with a UI Text to show the input
//     private string currentInput = ""; // Stores the current input from the user
//     private string validCode = "1234"; // The correct code to trigger an action
//     private bool isValidCodeEntered = false; // Flag to check if valid code was entered
//     [SerializeField] GameObject keypadUI;
//     [SerializeField] AudioSource buttonPress;
//     [SerializeField] AudioSource correctCode;
//     [SerializeField] AudioSource incorrectCode;

//     private void OnTriggerEnter(Collider other)
//     {
//         if (other.gameObject.CompareTag("Reach"))
//         {
//             inReach = true;
//             UIManager.Instance.ShowPickupText(true); // Show appropriate text based on the light's state
//         }
//     }

//     void OnTriggerExit(Collider other)
//     {
//         // Check if the collider is the player
//         if (other.gameObject.tag == "Reach")
//         {
//             inReach = false;
//             UIManager.Instance.ShowPickupText(false);
//         }
//     }

//     void Update()
//     {
//         if (inReach && Input.GetKeyDown(KeyCode.E))
//         {
//             keypadUI.SetActive(true);
//             player.GetComponent<Player>().enabled = false;
//             Time.timeScale = 0f;
//             Cursor.visible = true;
//             Cursor.lockState = CursorLockMode.None;
//         }
//     }

//     void Start()
//     {
//         ResetInput();
//     }

//     public void PressButton(string value)
//     {
//         // Update current input based on button pressed
//         if (!isValidCodeEntered) // Only allow input if the correct code hasn't been entered yet
//         {
//             currentInput += value;
//             buttonPress.Play();
//             UpdateDisplay();
//             CheckValidCode();
//         }
//     }

//     public void ExecuteButton()
//     {
//         // Here you can add what happens when the execute button is pressed
//         if (isValidCodeEntered)
//         {
//             correctCode.Play();
//             Debug.Log("Valid code executed!");
//             // Add more actions here as needed when the valid code is executed
//         }
//         else
//         {
//             incorrectCode.Play();
//             Debug.Log("Invalid code: " + currentInput);
//             ResetInput();
//         }
//     }

//     public void ClearButton()
//     {
//         // Clear the current input
//         ResetInput();
//     }

//     public void ExitButton()
//     {
//         // Code to execute on exit, like closing the keypad UI or the application
//         player.GetComponent<Player>().enabled = true;
//         keypadUI.SetActive(false);
//         Cursor.lockState = CursorLockMode.Locked;
//         Time.timeScale = 1f;
//     }

//     private void ResetInput()
//     {
//         currentInput = "";
//         isValidCodeEntered = false;
//         UpdateDisplay();
//     }

//     private void UpdateDisplay()
//     {
//         // Update the display text
//         displayText.text = currentInput;
//     }

//     private void CheckValidCode()
//     {
//         // Check if the current input matches the valid code
//         if (currentInput == validCode)
//         {
//             isValidCodeEntered = true;
//             Debug.Log("Correct code entered!");
//             // Optionally perform some immediate action when correct code is entered
//         }
//     }
// }


using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class KeypadController : MonoBehaviour
{
    [SerializeField] GameObject player;
    private bool inReach;
    [SerializeField] TMP_Text displayText; // Assign this in the inspector with a UI Text to show the input
    private string currentInput = ""; // Stores the current input from the user
    public string validCode = "1234"; // The correct code to trigger an action
    private bool isValidCodeEntered = false; // Flag to check if valid code was entered
    public string doorId = "Door1";  // Identifier for which door this keypad controls
    public static event Action<string, bool> OnCodeEntered; // Pass the doorId along with the event

    [SerializeField] GameObject keypadUI;
    [SerializeField] AudioSource buttonPress;
    [SerializeField] AudioSource correctCode;
    [SerializeField] AudioSource incorrectCode;

    public delegate void CodeEnteredHandler(bool codeIsValid);
    // public static event CodeEnteredHandler OnCodeEntered; // Event that other scripts can subscribe to

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Reach"))
        {
            inReach = true;
            UIManager.Instance.ShowKeypadText(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            UIManager.Instance.ShowKeypadText(false);
        }
    }

    void Update()
    {
        if (inReach && Input.GetKeyDown(KeyCode.E))
        {
            UIManager.Instance.HideTexts();
            keypadUI.SetActive(true);
            player.GetComponent<Player>().enabled = false;
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void Start()
    {
        ResetInput();
    }

    public void PressButton(string value)
    {
        if (!isValidCodeEntered)
        {
            currentInput += value;
            buttonPress.Play();
            UpdateDisplay();
            CheckValidCode();
        }
    }

    public void ExecuteButton()
    {
        if (isValidCodeEntered)
        {
            correctCode.Play();
            currentInput = "Correct";
            Debug.Log("Valid code executed!");
            OnCodeEntered?.Invoke(doorId, true); // Include the doorId when the correct code is entered
            UpdateDisplay();
        }
        else
        {
            incorrectCode.Play();
            Debug.Log("Invalid code: " + currentInput);
            ResetInput();
        }
    }

    public void ClearButton()
    {
        ResetInput();
    }

    public void ExitButton()
    {
        player.GetComponent<Player>().enabled = true;
        keypadUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }

    private void ResetInput()
    {
        currentInput = "";
        isValidCodeEntered = false;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        displayText.text = currentInput;
    }

    private void CheckValidCode()
    {
        if (currentInput == validCode)
        {
            isValidCodeEntered = true;
            Debug.Log("Correct code entered!");
            OnCodeEntered?.Invoke(doorId, true); // Include the doorId when the correct code is entered
        }
    }
}
