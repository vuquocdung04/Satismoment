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
    [SerializeField] private bool isWin = false;
    public bool isDragging;

    public List<L4_Item> lsItems;

    Coroutine coroutine;
    private void Update()
    {
        if(isWin) return;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        HandleDragItem();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void HandleDragItem()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var hit = Physics2D.Raycast(mousePos, Vector3.zero);
            if (hit.collider == null) return;
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
        var amount = 0;
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
            coroutine = StartCoroutine(PlayJumpAnimation());
            isWin = true;
        }
        isDragging = false;
        transItem = null;
    }

    IEnumerator PlayJumpAnimation()
    {
        var i = 0;
        var longestDelay = 0f;

        while (i < lsItems.Count)
        {
            var delay = i * 0.2f;
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
