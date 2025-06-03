using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform capsule;
    public Transform cameraTransform;
    public Animator animator;

    [Header("Mouvement")]
    public float moveSpeed = 5f;
    public float runSpeed = 8f;
    public float currentSpeed;
    public float jumpForce = 5f;

    public float mouseSensitivity = 2f;
    public float minVerticalAngle = -80f;
    public float maxVerticalAngle = 80f;

    private Rigidbody rb;
    private float verticalLookRotation = 0f;
    private CharacterController controller;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        rb = capsule.GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(Vector3.up * mouseX);

        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalLookRotation -= mouseY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, minVerticalAngle, maxVerticalAngle);
        cameraTransform.localEulerAngles = new Vector3(verticalLookRotation, 0f, 0f);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            currentSpeed = runSpeed;
        }
        else
        {
            currentSpeed = moveSpeed;
        }
  
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        Vector3 velocity = move * currentSpeed;
        velocity.y = rb.linearVelocity.y; // Garder vitesse verticale (gravité)
        rb.linearVelocity = velocity;

        animator.SetFloat("Speed", new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z).magnitude / runSpeed);


    }

    // Vérifie si la capsule touche le sol
    bool IsGrounded()
    {
        return Physics.Raycast(capsule.position, Vector3.down, 1.1f);
    }
}
