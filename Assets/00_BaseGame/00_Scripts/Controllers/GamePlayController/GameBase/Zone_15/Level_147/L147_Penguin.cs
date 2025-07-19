using UnityEngine;

public class L147_Penguin : MonoBehaviour
{
    public Rigidbody2D rb;
    private Vector3 originalPosition;
    public bool canJump;
    public bool waitTimeResetGame = false;
    private void Start()
    {
        // Lưu vị trí ban đầu khi game bắt đầu
        originalPosition = transform.position;
    }

    public void AddForceY(float force)
    {
        if (waitTimeResetGame) return;
        if (canJump)
        {
            rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            canJump = false;
        }
    }

    public void ResetPosition()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        transform.position = originalPosition;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        canJump = true;
    }
}