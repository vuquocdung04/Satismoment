using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L124_Bucket : MonoBehaviour
{
    public Level_124Ctrl levelCtrl; // Vẫn cần để truy cập các hàm khác nếu cần
    public SpriteRenderer spriteRenderer;
    public Sprite defaultSprite;
    public Sprite spriteHaveWater;
    public BoxCollider2D objCollider;
    public Transform positionSpawn;
    bool isHaveWated;

    public void HandleCollisionWithWaterWell()
    {
        if (objCollider.IsTouching(levelCtrl.waterWellCollider))
        {
            StartCollisionState();
            var positionWaterWell = levelCtrl.waterWellCollider.transform.position + Vector3.up;
            StartCoroutine(levelCtrl.SpawnTimmingBar(positionWaterWell, delegate
            {
                transform.position = positionSpawn.position;
                spriteRenderer.sprite = spriteHaveWater;
                isHaveWated = true;
                EndCollisionState();
            }));
        }
    }

    List<Collider2D> contacts;
    public void HandleCollisionWithSeed()
    {
        if (!isHaveWated) // Nếu không có nước thì không cần làm gì
        {
            return;
        }
        Debug.LogError("Wtf");
        contacts.Clear();
        objCollider.GetContacts(contacts);

        L124_Seed targetSeed = null;

        // Duyệt qua tất cả các collider mà bucket đang chạm
        foreach (Collider2D hitCollider in contacts)
        {
            // Kiểm tra xem collider đó có phải là hạt giống không
            if (hitCollider != null && hitCollider.TryGetComponent<L124_Seed>(out L124_Seed seed))
            {
                targetSeed = seed; // Tìm thấy một hạt giống
                break; // Tìm thấy rồi thì dừng lại, xử lý hạt giống này
            }
        }

        if (targetSeed != null && targetSeed.inDir) // Nếu tìm thấy hạt giống để tương tác
        {
            StartCollisionState();
            var positionSeed = targetSeed.transform.position + Vector3.up; // Lấy vị trí của hạt giống tìm được
            StartCoroutine(levelCtrl.SpawnTimmingBar(positionSeed, delegate
            {
                transform.position = Vector3.zero; // Đặt lại vị trí của bucket
                spriteRenderer.sprite = defaultSprite; // Thay đổi sprite
                isHaveWated = false; // Đã dùng hết nước
                targetSeed.GrownSeed(); // Gọi hàm phát triển hạt giống đã tìm thấy
                EndCollisionState();
                Debug.Log($"Seed at {targetSeed.transform.position} has been watered and grown!");
            }));
        }
    }

    void StartCollisionState()
    {
        objCollider.enabled = false;
        spriteRenderer.sortingOrder = -1;
    }

    void EndCollisionState()
    {
        objCollider.enabled = true;
        spriteRenderer.sortingOrder = 5;
    }

    private void OnDrawGizmosSelected()
    {
        Vector2 boxSize = Vector2.one;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(boxSize.x, boxSize.y, 1f));
    }
}