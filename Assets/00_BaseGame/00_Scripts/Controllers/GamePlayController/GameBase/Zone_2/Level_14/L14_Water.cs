using UnityEngine;

public class L14_Water : MonoBehaviour
{
    // Các thuộc tính này có thể được cấu hình sẵn trên Prefab của tia nước trong Inspector
    public float baseSpeed = 10f;         // Tốc độ cơ bản của tia nước
    public float speedVariation = 2f;     // Độ biến thiên của tốc độ (0 nghĩa là không biến thiên)
    public float particleLifetime = 1f;   // Thời gian tồn tại của tia nước
    public float resetLifeTime = 0.5f;
    public float sprayAngleDegrees = 45f;   // Tổng góc của hình chóp (ví dụ: 45 độ)

    public Rigidbody2D rb;
    float currentSpeed;
    float randomAngleOffset;
    Quaternion spreadRotation;
    Vector2 particleDirection;


    private void Update()
    {
        Launch();
    }
    public void Launch()
    {
        randomAngleOffset = Random.Range(-sprayAngleDegrees / 2f, sprayAngleDegrees / 2f);
        spreadRotation = Quaternion.AngleAxis(randomAngleOffset, Vector3.forward); // Xoay quanh trục Z cho 2D
        particleDirection = spreadRotation * transform.up; // Chuyển sang Vector2 cho vật lý 2D

        currentSpeed = baseSpeed;
        if (speedVariation > 0)
        {
            currentSpeed = Random.Range(baseSpeed - speedVariation, baseSpeed + speedVariation);
        }
        currentSpeed = Mathf.Max(0, currentSpeed);

        rb.velocity = particleDirection.normalized * currentSpeed;

        particleLifetime -= Time.deltaTime;
        if (particleLifetime < 0)
        {
            SimplePool2.Despawn(gameObject);
            particleLifetime = resetLifeTime;
        }
    }
}