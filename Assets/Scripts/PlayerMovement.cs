using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Références")]
    public Transform capsule;         // Référence vers la capsule avec le Rigidbody
    public Transform cameraTransform; // Référence vers la caméra (enfant)

    [Header("Mouvement")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    [Header("Rotation")]
    public float mouseSensitivity = 2f;
    public float minVerticalAngle = -80f;
    public float maxVerticalAngle = 80f;

    private Rigidbody rb;
    private float verticalLookRotation = 0f;

    void Start()
    {
        rb = capsule.GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // --- Rotation souris ---
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(Vector3.up * mouseX);

        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalLookRotation -= mouseY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, minVerticalAngle, maxVerticalAngle);
        cameraTransform.localEulerAngles = new Vector3(verticalLookRotation, 0f, 0f);

        // --- Saut (optionnel) ---
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        // --- Déplacement physique ---
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        Vector3 velocity = move * moveSpeed;
        velocity.y = rb.linearVelocity.y; // Garder vitesse verticale (gravité)
        rb.linearVelocity = velocity;
    }

    // Vérifie si la capsule touche le sol
    bool IsGrounded()
    {
        return Physics.Raycast(capsule.position, Vector3.down, 1.1f);
    }
}
