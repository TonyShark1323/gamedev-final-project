using UnityEngine;

public class DrawerController : MonoBehaviour
{
    public enum DrawerOrientation
    {
        XAxis,
        YAxis,
        ZAxis
    }

    public float spawnChance = 0.3f; // 30% chance to spawn SanityMeds
    public GameObject sanityMedsPrefab; // Assign this via the Inspector
    public bool allowItemSpawn = true;


    public Transform drawer; // Assign the drawer GameObject
    private float closedPositionZ; // The position when the drawer is closed
    public float openOffsetZ = 0.5f; // Offset from the closed position to the open position
    public float speed = 2f; // Speed at which the drawer opens
    public DrawerOrientation orientation = DrawerOrientation.ZAxis;

    public AudioSource openSound;
    public AudioSource closeSound;

    private bool isOpen = false; // Track whether the drawer is open or closed
    private bool inReach = false; // Whether the player is in reach to interact with the drawer

    void Start()
    {
        switch (orientation)
        {
            case DrawerOrientation.XAxis:
                closedPositionZ = drawer.localPosition.x;
                break;
            case DrawerOrientation.YAxis:
                closedPositionZ = drawer.localPosition.y;
                break;
            case DrawerOrientation.ZAxis:
                closedPositionZ = drawer.localPosition.z;
                break;
        }
    }

    void Update()
    {
        if (inReach && Input.GetKeyDown(KeyCode.E))
        {
            isOpen = !isOpen;
            if(!isOpen){
                if (openSound != null)
                    openSound.Play();
            
            }
            else if (isOpen){
                if (closeSound != null)
                    closeSound.Play();
                    TrySpawnSanityMeds();
            }
        }

        // MoveDrawer();
        InstantMoveDrawer(); // Use the new method to move the drawer instantly
        if (inReach)
        {
            UIManager.Instance.ShowDrawerText(!isOpen);
        }
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
        }
    }

    void MoveDrawer()
    {
        Vector3 targetPosition = drawer.localPosition;

        switch (orientation)
        {
            case DrawerOrientation.XAxis:
                targetPosition.x = isOpen ? closedPositionZ + openOffsetZ : closedPositionZ;
                break;
            case DrawerOrientation.YAxis:
                targetPosition.y = isOpen ? closedPositionZ + openOffsetZ : closedPositionZ;
                break;
            case DrawerOrientation.ZAxis:
                targetPosition.z = isOpen ? closedPositionZ + openOffsetZ : closedPositionZ;
                break;
        }

        drawer.localPosition = Vector3.Lerp(drawer.localPosition, targetPosition, Time.deltaTime * speed);
    }

    void InstantMoveDrawer() {
        Vector3 targetPosition = drawer.localPosition;

        switch (orientation)
        {
            case DrawerOrientation.XAxis:
                targetPosition.x = isOpen ? closedPositionZ + openOffsetZ : closedPositionZ;
                break;
            case DrawerOrientation.YAxis:
                targetPosition.y = isOpen ? closedPositionZ + openOffsetZ : closedPositionZ;
                break;
            case DrawerOrientation.ZAxis:
                targetPosition.z = isOpen ? closedPositionZ + openOffsetZ : closedPositionZ;
                break;
        }

        drawer.localPosition = targetPosition; // Set position directly without interpolation
    }

    void TrySpawnSanityMeds() {
    // Only attempt to spawn if item spawning is allowed
        if (allowItemSpawn && Random.value < spawnChance) // Random.value returns a value between 0.0 and 1.0
        {
            // Check if pooling is used, otherwise instantiate normally
            if (ObjectPool.Instance != null)
            {
                GameObject sanityMeds = ObjectPool.Instance.Get();
                sanityMeds.transform.position = drawer.position + new Vector3(0, 0, 0.1f);
            }
            else
            {
                Instantiate(sanityMedsPrefab, drawer.position + new Vector3(0, 0, 0.1f), Quaternion.identity);
            }
        }
    }
}
