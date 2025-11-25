using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class GruStreetController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("World Bounds")]
    public bool useBounds = true;
    public float minX = -8f;
    public float maxX = 8f;
    public float minY = -4f;
    public float maxY = 4f;

    private Rigidbody2D rb;

    private void Awake()
    {
        // Cache reference to the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        // Read input from keyboard (WASD and arrow keys)
        float horizontalInput = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right
        float verticalInput = Input.GetAxisRaw("Vertical");     // W/S or Up/Down

        // Build movement vector (X,Y)
        Vector2 inputDirection = new Vector2(horizontalInput, verticalInput);

        // Normalize to avoid faster diagonal movement
        if (inputDirection.sqrMagnitude > 1f)
        {
            inputDirection = inputDirection.normalized;
        }

        // Compute target position based on current position and input
        Vector2 targetPosition = rb.position + inputDirection * moveSpeed * Time.deltaTime;

        // Optionally clamp position so Gru stays inside the visible area
        if (useBounds)
        {
            targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);
        }

        // Move using Rigidbody for smooth movement
        rb.MovePosition(targetPosition);
    }
}
