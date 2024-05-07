using UnityEngine;

public class PositionTracker : MonoBehaviour
{
    [SerializeField] AudioSource pushSound;
    [SerializeField] float movementThreshold = 0.1f; // Minimum distance the object needs to move to be considered as moving

    private Vector3 lastPosition;
    private bool isPlayingSound = false;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        CheckMovement();
    }

    void CheckMovement()
    {
        float distanceMoved = Vector3.Distance(transform.position, lastPosition);

        if (distanceMoved > movementThreshold)
        {
            if (!isPlayingSound)
            {
                pushSound.Play();
                isPlayingSound = true;
            }
        }
        else
        {
            isPlayingSound = false;
        }

        lastPosition = transform.position;
    }
}
