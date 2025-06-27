using DG.Tweening;
using UnityEngine;

public class L91_Item : BaseDraggableObject
{
    public L91_ZoneItem zoneCorrect;

    public bool CheckTochingWithZone()
    {
        if (objectCollider.IsTouching(zoneCorrect.boxCollider2d))
        {
            // Gọi hàm và nhận trực tiếp vị trí đích
            Vector3 targetPos = zoneCorrect.AssignItemToNearestAvailable(this, transform.position);

            // Kiểm tra xem có vị trí hợp lệ được trả về hay không
            if (targetPos != Vector3.zero) // Kiểm tra nếu vị trí không phải là Vector3.zero (giá trị báo lỗi)
            {
                transform.DOMove(targetPos, 0.2f).SetEase(Ease.OutQuad);
                objectCollider.enabled = false;
                spriteRenderer.sortingOrder = orderIndex - 1;
                return true;
            }
        }
        return false;
    }

    public override void ReturnToOriginalPosition()
    {
        // Logic để item quay về vị trí ban đầu (nếu cần)
    }
}