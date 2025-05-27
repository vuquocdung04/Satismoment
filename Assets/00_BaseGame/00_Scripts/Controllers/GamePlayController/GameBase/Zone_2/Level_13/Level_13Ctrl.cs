using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class Level_13Ctrl : BaseDragController<L13_Pen>
{
    [Tooltip("Chiều rộng của mỗi cây bút (đơn vị world space).")]
    public float penWidth = 0.5f;
    [Tooltip("Khoảng cách giữa các cây bút (đơn vị world space).")]
    public float spacing = 0.1f;
    public List<L13_Pen> lsPens;

    protected virtual void Start()
    {
        HandleSortPen(null, true);
    }
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.eulerAngles = Vector3.zero;
        draggableComponent.transform.Translate(mouseDelta);

        if (lsPens.Contains(draggableComponent))
        {
            // Lấy vị trí hiện tại của đối tượng kéo
            Vector3 currentPosition = draggableComponent.transform.position;

            // Giới hạn tọa độ x
            float clampedX = Mathf.Clamp(currentPosition.x, -1.5f, 1.5f);

            // Cập nhật lại vị trí của đối tượng kéo nếu tọa độ x bị giới hạn
            if (currentPosition.x != clampedX)
            {
                draggableComponent.transform.position = new Vector3(clampedX, currentPosition.y, currentPosition.z);
            }
        }


        HandleSortPen(draggableComponent, false);
    }

    protected override void OnDragEnded()
    {
        if (!lsPens.Contains(draggableComponent))
            draggableComponent.transform.eulerAngles = new Vector3(0, 0, draggableComponent.angle);
        HandleSortPen(null, true);
        if (CheckWinCondition())
        {
            foreach(var pen in this.lsPens)
            {
                pen.transform.DOMoveY(pen.transform.position.y + 0.4f, 0.3f)
                    .SetLoops(2, LoopType.Yoyo)
                    .SetEase(Ease.InOutSine);
            }
            DOVirtual.DelayedCall(0.7f, () => WinBox.SetUp().Show());
        }
    }

    void HandleSortPen(L13_Pen pen, bool snapPosition = false)
    {
        if (pen != null && lsPens.Contains(pen))
        {
            lsPens.Remove(pen);
            lsPens.Sort((a, b) => a.transform.position.x.CompareTo(b.transform.position.x));

            int indexPen = 0;
            for (int i = 0; i < lsPens.Count; i++)
            {
                if (pen.transform.position.x < lsPens[i].transform.position.x)
                {
                    indexPen = i; break;
                }
                indexPen++;
            }
            lsPens.Insert(indexPen, pen);
        }

        /// sap xep UI
        /// 
        // Xác định vị trí Y và Z mục tiêu dựa trên transform của GameObject này
        float targetY = this.transform.position.y;
        float targetZ = this.transform.position.z;
        // Xác định X tham chiếu để căn giữa
        float referenceX = this.transform.position.x;

        // Tính toán vị trí bắt đầu (startX) để dãy bút được căn giữa
        float totalWidthOfPens = (lsPens.Count * penWidth) + (Mathf.Max(0, lsPens.Count - 1) * spacing);
        float startX = referenceX - totalWidthOfPens / 2.0f + penWidth / 2.0f;

        for (int i = 0; i < lsPens.Count; i++)
        {

            L13_Pen p = lsPens[i];
            if (p == null) continue;
            Vector3 targetPosition = new Vector3(startX + i * (penWidth + spacing), targetY, targetZ);

            if (p == pen && !snapPosition) p.transform.position = new Vector3(p.transform.position.x, targetY, targetZ);

            else
            {
                if (snapPosition) p.transform.position = targetPosition;

                else
                    p.transform.position = Vector3.Lerp(p.transform.position, targetPosition, Time.deltaTime * 10f);

            }
        }
    }

    bool CheckWinCondition()
    {
        if (lsPens.Count < 6) return false;
        for(int i = 0; i < lsPens.Count; i++)
        {
            if (lsPens[i].idPen != i)
            {
                return false;
            }
        }
        return true;
    }

    protected override void OnDragStarted()
    {
        throw new System.NotImplementedException();
    }
}