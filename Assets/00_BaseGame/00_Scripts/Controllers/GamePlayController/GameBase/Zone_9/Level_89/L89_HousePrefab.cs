using UnityEngine;
using UnityEngine.U2D;

public class L89_HousePrefab : MonoBehaviour
{
    public Level_89Ctrl levelCtrl;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider2d;
    private int contactCount = 0; // Đếm số lượng va chạm hiện tại
    public bool isComplete;
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
        levelCtrl.PlayFallSound();
    }

    void OnCollisionExit2D(Collision2D col)
    {
        contactCount--;
    }

    void CheckCompletion()
    {
        float angleZ = transform.eulerAngles.z % 360;
        if (angleZ > 180) angleZ -= 360;

        if (Mathf.Abs(angleZ) <= 20f)
        {
            Debug.Log("Win");
            isComplete = true;
            ResetState();
            levelCtrl.rod.currrentHousePrefab = this; // Đảm bảo con trỏ chính xác
            levelCtrl.rod.InitHouse(); // Chuẩn bị nhà tiếp theo

            if (levelCtrl.winProgress == 3)
            {
                levelCtrl.MoveCamera();
            }
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
        transform.localEulerAngles = Vector3.zero;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }
    public void ResetStateAndSetParent()
    {
        transform.SetParent(levelCtrl.rod.pointFall);
        transform.localPosition = Vector3.zero;
        ResetState();
    }


    public void UpdateColliderSize()
    {
        if (boxCollider2d != null && spriteRenderer != null)
        {
            // Lấy kích thước sprite
            var spriteSize = spriteRenderer.sprite;

            float width = spriteSize.rect.width / spriteSize.pixelsPerUnit;
            float height = spriteSize.rect.height / spriteSize.pixelsPerUnit;
            boxCollider2d.size = new Vector2(width, height);

        }
    }
}