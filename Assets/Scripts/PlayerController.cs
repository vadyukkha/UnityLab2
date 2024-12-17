using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    Camera cam;
    
    [SerializeField] Transform groundChecker;
    [SerializeField] LayerMask groundLayer;
    float groundCheckerDistance = 0.15f;
    bool isGrounded = false;
    float jumpForce = 5f;
    
    Vector2 movementInput = Vector2.zero;
    float movementSpeed = 5f;
    
    Vector2 lookInput = Vector2.zero;
    float xRotation = 0f;
    float yRotation = 0f;
    float maxAngle = 60f;
    float minAngle = -60f;
    float sensitivity = 0.15f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        cam = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        sensitivity = PlayerPrefs.GetFloat("sensitivity");
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && isGrounded)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        isGrounded = Physics.OverlapSphere(groundChecker.position, groundCheckerDistance, groundLayer).Length > 0;

        yRotation += lookInput.x * sensitivity;
        xRotation -= lookInput.y * sensitivity;
        xRotation = Mathf.Clamp(xRotation, minAngle, maxAngle); 
        transform.localRotation = Quaternion.Euler(0, yRotation, 0);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        Vector3 moveDirection = transform.forward * movementInput.y + transform.right * movementInput.x;
        rb.velocity = new Vector3(moveDirection.x * movementSpeed, rb.velocity.y, moveDirection.z * movementSpeed);
    }
}
