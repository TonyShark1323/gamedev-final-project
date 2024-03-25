using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDrawer : MonoBehaviour
{
    public Animator ANI;

    // public GameObject openText;
    // public GameObject closedText;

    // public AudioSource openSound;
    // public AudioSource closeSound;

    private bool isOpen;

    private bool inReach;


    void Start()
    {
        // openText.SetActive(false);
        // closedText.SetActive(false);

        ANI.SetBool("open", false);
        ANI.SetBool("close", false);

        isOpen = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            UIManager.Instance.ShowDrawerText(!isOpen);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            UIManager.Instance.HideTexts();

            // openText.SetActive(false);
            // closedText.SetActive(false);
        }
    }



    void Update()
    {
        if (!isOpen && inReach && Input.GetKeyDown(KeyCode.E))
        {
            // openSound.Play();
            ANI.SetBool("open", true);
            ANI.SetBool("close", false);
            isOpen = true;
        }

        else if (isOpen && inReach && Input.GetKeyDown(KeyCode.E))
        {
            // closeSound.Play();
            ANI.SetBool("open", false);
            ANI.SetBool("close", true);
            isOpen = false;
        }
        // UpdateText();
        if (inReach) // Check if still in reach to update the text correctly
            {
                UIManager.Instance.ShowDrawerText(!isOpen);
            }
    }

    // void UpdateText()
    // {
    //     // Update the text based on the drawer's state and player's reach
    //     if (inReach)
    //     {
    //         openText.SetActive(!isOpen);
    //         closedText.SetActive(isOpen);
    //     }
    //     else
    //     {
    //         openText.SetActive(false);
    //         closedText.SetActive(false);
    //     }
    // }
}