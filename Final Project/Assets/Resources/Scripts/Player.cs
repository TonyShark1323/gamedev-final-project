using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Parameters")]
    [SerializeField] Transform cameraTransform;
    [SerializeField] float mouseSensitivity = 3f;
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float sprintSpeed = 8f;
    [SerializeField] float crouchSpeed = 3f;
    [SerializeField] float mass = 1f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float normalHeight = 2f;
    [SerializeField] float crouchHeight = 1f;

    [Header("Sound Effects")]
    [SerializeField] AudioSource walkSound;
    // [SerializeField] AudioClip sprintSound;
    [SerializeField] float footstepInterval = 0.5f;

    private CharacterController controller;
    private Vector3 velocity;
    private Vector2 look;
    private bool isCrouching = false;
    private float footstepTimer;

    void Awake() 
    {
        controller = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        footstepTimer = footstepInterval;
    }

    // Update is called once per frame
    void Update() 
    {
        UpdateGravity();
        UpdateMovement();
        UpdateLook();
        UpdateFootsteps();
    }

    void UpdateFootsteps()
    {
        if (!controller.isGrounded || controller.velocity.magnitude < 0.1f)
        {
            return;
        }

        footstepTimer -= Time.deltaTime;
        if (footstepTimer <= 0)
        {
            walkSound.Play();
            footstepTimer = footstepInterval;
        }
    }

    void UpdateGravity() 
    {
        var gravity = Physics.gravity * mass * Time.deltaTime;
        velocity.y = controller.isGrounded ? -1f : velocity.y + gravity.y;
    }

    void UpdateMovement() 
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        var input = new Vector3(transform.forward.x * y + transform.right.x * x, 0, transform.forward.z * y + transform.right.z * x);
        input = Vector3.ClampMagnitude(input, 1f);

        // Check for sprinting
        float currentSpeed = isCrouching ? crouchSpeed : movementSpeed;
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && !isCrouching) 
        {
            currentSpeed = sprintSpeed;
        }

        // Check for crouching
        if (Input.GetKeyDown(KeyCode.LeftControl)) 
        {
            ToggleCrouch();
        }

        if (Input.GetButtonDown("Jump") && controller.isGrounded && !isCrouching)
        {
            velocity.y += jumpSpeed;
        }

        controller.Move((input * currentSpeed + velocity) * Time.deltaTime);
    }

    void UpdateLook()
    {
        look.x += Input.GetAxis("Mouse X") * mouseSensitivity;
        look.y -= Input.GetAxis("Mouse Y") * mouseSensitivity;

        look.y = Mathf.Clamp(look.y, -89f, 89f);

        cameraTransform.localEulerAngles = new Vector3(look.y, 0, 0);
        transform.localEulerAngles = new Vector3(0, look.x, 0);
    }

    void ToggleCrouch() 
    {
        isCrouching = !isCrouching;
        controller.height = isCrouching ? crouchHeight : normalHeight;
        // Optional: Adjust the center of the CharacterController if needed
        // controller.center = new Vector3(controller.center.x, isCrouching ? crouchCenterY : normalCenterY, controller.center.z);
    }

    public void Teleport(Vector3 position, Quaternion rotation) 
    {
        transform.position = position;
        Physics.SyncTransforms();
        look.x = rotation.eulerAngles.y;
        look.y = -rotation.eulerAngles.x; // Assuming the rotation is provided in Unity's standard format
        velocity = Vector3.zero;
    }
}
