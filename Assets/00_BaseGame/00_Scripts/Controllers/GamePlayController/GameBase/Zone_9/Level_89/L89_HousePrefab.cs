using UnityEngine;

public class L89_HousePrefab : MonoBehaviour
{
    public Level_89Ctrl levelCtrl;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    private int contactCount = 0; // Đếm số lượng va chạm hiện tại

    void Update()
    {
        // Chỉ kiểm tra khi đang có ít nhất 1 va chạm (chạm đất)
        if (contactCount > 0)
        {
            // Kiểm tra xem vật đã dừng hẳn chưa
            if (rb.velocity.magnitude < 0.1f && Mathf.Abs(rb.angularVelocity) < 0.1f)
            {
                CheckCompletion(); // Gọi hàm kiểm tra thành công
                contactCount = 0;   // Reset để tránh gọi lại nhiều lần
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        contactCount++;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        contactCount--;
    }

    void CheckCompletion()
    {
        float angleZ = transform.eulerAngles.z % 360;

        // Chuyển góc về dạng đối xứng quanh 0 độ (từ -180 đến 180)
        if (angleZ > 180) angleZ -= 360;

        if (Mathf.Abs(angleZ) <= 20f)
        {
            Debug.Log("Win");
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    public void SetSprite(Sprite spriteHouse)
    {
        spriteRenderer.sprite = spriteHouse;
    }

    public void HandleFallCondition()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    public void ResetState()
    {
        transform.SetParent(levelCtrl.rod.pointFall);
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }
}