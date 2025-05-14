using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Level_7Ctrl : MonoBehaviour
{
    public L7_IceCream iceCream;

    Vector3 mousePos;
    Vector3 prevMousePos;
    Vector3 mouseDelta;
    float rotationAmount;
    public bool isDragging;

    public List<Transform> lsItem;
    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        HandleDragIceCream();
    }

    void HandleDragIceCream()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.zero);

            if (hit.collider == null) return;
            iceCream = hit.collider.GetComponent<L7_IceCream>();

            if (iceCream == null) return;
            isDragging = true;
            prevMousePos = mousePos;
        }
        if (isDragging && iceCream != null)
        {
            mouseDelta = mousePos - prevMousePos;

            rotationAmount = -mouseDelta.x * 15;
            iceCream.transform.Rotate(0, 0, rotationAmount);
            prevMousePos = mousePos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            HandleWin();
            isDragging = false;
            if (iceCream == null) return;
            iceCream = null;

        }
    }

    void HandleWin()
    {
        // Lấy góc hiện tại
        float currentAngle = iceCream.transform.eulerAngles.z;

        // Chuyển đổi về khoảng -180 đến 180 độ để dễ so sánh
        if (currentAngle > 180)
            currentAngle -= 360;

        // Kiểm tra nếu góc gần với 0 (với sai số nhỏ)
        if (Mathf.Abs(currentAngle) < 1f) // Cho phép sai số 5 độ
        {
            isDragging = false;
            iceCream.boxCollider.enabled = false;
            iceCream.transform.eulerAngles = Vector3.zero;
            iceCream.StopCoroutine(iceCream.corotine);
            HandleAnimWin();
            StartCoroutine(WaitShowPopup(iceCream));
            iceCream = null;
        }
    }

    void HandleAnimWin()
    {
        foreach (var item in this.lsItem)
        {
            item.gameObject.SetActive(true);
            item.localScale = Vector3.zero;
        }
        lsItem[0].DOScale(Vector2.one, 0.5f).SetEase(Ease.InOutSine).OnComplete(delegate
        {
            lsItem[0].DOMove(new Vector2(-0.113f, 1.595f), 0.5f);
        });
        lsItem[1].DOScale(Vector2.one, 0.5f).SetEase(Ease.InOutSine).OnComplete(delegate
        {
            lsItem[1].DOMove(new Vector2(-0.306f, 1.652f), 0.5f);
        });
        lsItem[2].DOScale(Vector3.one,1f).SetEase(Ease.InOutSine);
    }

    IEnumerator WaitShowPopup(L7_IceCream iceCream)
    {
        yield return new WaitForSeconds(1.5f);
        iceCream.spriteRenderer.sprite = iceCream.spriteWin;
        foreach(var item in this.lsItem) item.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        WinBox.SetUp().Show();
    }
}
