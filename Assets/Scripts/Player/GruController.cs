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
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector2 inputDirection = new Vector2(horizontalInput, verticalInput);

        if (inputDirection.sqrMagnitude > 1f)
        {
            inputDirection = inputDirection.normalized;
        }

        Vector2 targetPosition = rb.position + inputDirection * moveSpeed * Time.deltaTime;

        if (useBounds)
        {
            targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);
        }

        rb.MovePosition(targetPosition);
    }
}