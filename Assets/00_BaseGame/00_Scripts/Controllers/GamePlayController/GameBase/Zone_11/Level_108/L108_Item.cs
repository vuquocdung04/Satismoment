using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L108_Item : MonoBehaviour
{
    public L108_Compartment compartment;
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider;

    public void OnDragStarted()
    {
        rb.simulated = false;
    }


    public void OnDragUpdate(Vector3 newPos, Vector3 mouseDelta , float maxDistanceX, float maxDistanceY)
    {
        newPos = transform.localPosition + mouseDelta;
        newPos.x = Mathf.Clamp(newPos.x,-maxDistanceX,maxDistanceX);
        newPos.y = Mathf.Clamp(newPos.y,-maxDistanceY * 2,maxDistanceY);

        transform.position = newPos;
    }


    public void OnDragEnded()
    {
        rb.simulated = true;
    }

    public bool CheckTochingWithZone()
    {
        if (boxCollider.IsTouching(compartment.boxCollider2d))
        {
            // Gọi hàm và nhận trực tiếp vị trí đích
            Vector3 targetPos = compartment.AssignItemToNearestAvailable(this, transform.position);

            // Kiểm tra xem có vị trí hợp lệ được trả về hay không
            if (targetPos != Vector3.zero) // Kiểm tra nếu vị trí không phải là Vector3.zero (giá trị báo lỗi)
            {
                transform.DOMove(targetPos, 0.2f).SetEase(Ease.OutQuad);
                boxCollider.enabled = false;
                return true;
            }
        }
        return false;
    }
}
