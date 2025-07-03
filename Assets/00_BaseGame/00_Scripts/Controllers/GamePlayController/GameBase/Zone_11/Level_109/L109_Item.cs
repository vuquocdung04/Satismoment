using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L109_Item : MonoBehaviour
{
    public int id;
    public int indexOrder;
    public Level_109Ctrl levelCtrl;
    public BoxCollider2D boxCollider;
    public SpriteRenderer itemRenderer;

    public void OnDragStarted()
    {
        itemRenderer.sortingOrder = indexOrder + 1;
    }


    public void OnDragUpdate(Vector3 newPos, Vector3 mouseDelta, float maxDistanceX, float maxDistanceY)
    {
        newPos = transform.localPosition + mouseDelta;
        newPos.x = Mathf.Clamp(newPos.x, -maxDistanceX, maxDistanceX);
        newPos.y = Mathf.Clamp(newPos.y, -maxDistanceY, maxDistanceY);

        transform.position = newPos;
    }


    public void OnDragEnded()
    {
        itemRenderer.sortingOrder = indexOrder;
        StartCoroutine(FallingItem());
    }

    IEnumerator FallingItem()
    {
        boxCollider.enabled = false;
        Tween fallItem = transform.DOMoveY(-3.5f, 0.3f).SetEase(Ease.Linear);
        yield return fallItem.WaitForCompletion();
        boxCollider.enabled = true;
    }

    public bool CheckTochingWithZone()
    {
        if (boxCollider.IsTouching(levelCtrl.GetCompartmentByID(id).boxCollider2d))
        {
            // Gọi hàm và nhận trực tiếp vị trí đích
            Vector3 targetPos = levelCtrl.GetCompartmentByID(id).AssignItemToNearestAvailable(this, transform.position);

            // Kiểm tra xem có vị trí hợp lệ được trả về hay không
            if (targetPos != Vector3.zero) // Kiểm tra nếu vị trí không phải là Vector3.zero (giá trị báo lỗi)
            {
                transform.DOMove(targetPos, 0.2f).SetEase(Ease.OutQuad);
                boxCollider.enabled = false;
                itemRenderer.sortingOrder = indexOrder - 1;
                return true;
            }
        }
        return false;
    }

    public void InitSetupOdin(Level_109Ctrl levelCtrl)
    {
        this.levelCtrl = levelCtrl;
        boxCollider = transform.GetComponent<BoxCollider2D>();
        itemRenderer = transform.GetComponent<SpriteRenderer>();
        indexOrder = itemRenderer.sortingOrder;
    }
}
