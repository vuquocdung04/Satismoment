using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Level_4Ctrl : MonoBehaviour
{
    public L4_Item transItem;
    Vector3 mousePos;
    Vector3 prevMousePos;
    Vector3 mousePosDelta;

    public bool isDragging;

    public List<L4_Item> lsItems;

    Coroutine coroutine;
    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        HandleDragItem();
    }

    void HandleDragItem()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.zero);
            if (hit.collider == null) return;
            Debug.LogError(hit.collider.name);

            transItem = hit.collider.GetComponent<L4_Item>();

            if (transItem == null) return;
            isDragging = true;
            prevMousePos = mousePos;
        }

        if (isDragging && transItem != null)
        {
            mousePosDelta = mousePos - prevMousePos;
            transItem.transform.Translate(mousePosDelta.x, 0, 0);
            prevMousePos = mousePos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            CheckComplete();
        }
    }

    void CheckComplete()
    {
        int amount = 0;
        foreach (var item in this.lsItems)
        {
            if (item.transform.position.x < 0.1f && item.transform.position.x > -0.1f)
            {
                item.transform.position = new Vector2(0,item.transform.position.y);
                amount++;
            }
        }

        if(amount > 4)
        {
            foreach(var item in this.lsItems) item.boxCollider2D.enabled = false;
            Debug.LogError("Test");
            coroutine = StartCoroutine(PlayJumpAnimation());
        }
        isDragging = false;
        transItem = null;
    }

    IEnumerator PlayJumpAnimation()
    {
        int i = 0;
        float longestDelay = 0f;

        while (i < lsItems.Count)
        {
            float delay = i * 0.2f;
            longestDelay = delay;

            // Item i sẽ bắt đầu nhảy sau i*0.2 giây
            lsItems[i].transform.DOJump(lsItems[i].transform.position, 1f, 1, 1f)
                .SetEase(Ease.OutBack)
                .SetDelay(delay);
            i++;
        }

        // Tính tổng thời gian cần đợi: delay của item cuối + thời gian animation (1 giây)
        float totalWaitTime = longestDelay + 0.5f;

        // Đợi cho đến khi tất cả animation hoàn thành
        yield return new WaitForSeconds(totalWaitTime);

        // Hiển thị WinBox sau khi tất cả animation hoàn thành
        WinBox.SetUp().Show();
    }
}
