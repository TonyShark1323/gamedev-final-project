using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    [SerializeField] float mouseSensitivity = 3f;
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float sprintSpeed = 8f;
    [SerializeField] float mass = 1f;
    [SerializeField] float jumpSpeed = 5f;

    CharacterController controller;
    Vector3 velocity;
    Vector2 look;

    void Awake() {
        controller = GetComponent<CharacterController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update() {
        UpdateGravity();
        UpdateMovement();
        UpdateLook();
    }

    void UpdateGravity() {
        var gravity = Physics.gravity * mass * Time.deltaTime;
        velocity.y = controller.isGrounded ? -1f : velocity.y + gravity.y;
    }

    void UpdateMovement() {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        var input = new Vector3();
        input += transform.forward * y;
        input += transform.right * x;
        input = Vector3.ClampMagnitude(input, 1f);

        // Check for sprinting
        float currentSpeed = movementSpeed;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
            currentSpeed = sprintSpeed;
        }

        if (Input.GetButtonDown("Jump") && controller.isGrounded){
            velocity.y += jumpSpeed;
        }

        controller.Move((input * currentSpeed + velocity) * Time.deltaTime);

        // transform.Translate(input * movementSpeed * Time.deltaTime, Space.World);
        // controller.Move((input * movementSpeed + velocity) * Time.deltaTime); 
    }

    void UpdateLook()
    {
        look.x += Input.GetAxis("Mouse X") * mouseSensitivity;
        look.y += Input.GetAxis("Mouse Y") * mouseSensitivity;

        look.y = Mathf.Clamp(look.y, -89f, 89f);

        cameraTransform.localRotation = Quaternion.Euler(-look.y, 0, 0);
        transform.localRotation = Quaternion.Euler(0, look.x, 0);
        // Debug.Log(look);
    }

    public void Teleport(Vector3 position, Quaternion rotation) {
        transform.position = position;
        Physics.SyncTransforms();
        look.x = rotation.eulerAngles.y;
        look.y = rotation.eulerAngles.z;
        velocity = Vector3.zero;
    }
}
