using Sirenix.OdinInspector;
using UnityEngine;


public class L158_Car : MonoBehaviour
{
    public Rigidbody2D rb;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    float forwardInput;
    Vector2 movement;
    public void Move(Vector2 mouseDelta)
    {
        // Tính projection của mouse delta lên hướng forward của vật
        forwardInput = Vector2.Dot(mouseDelta, transform.up);

        movement = transform.up * forwardInput * moveSpeed;
        rb.velocity += movement;
    }

    public void StopMovement()
    {
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    public void ResetCollisionState()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    [Button("Setup", ButtonSizes.Large)]
    void Setup()
    {
        rb = GetComponent<Rigidbody2D>();
    }
}
