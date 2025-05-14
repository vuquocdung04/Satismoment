using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_3Ctrl : MonoBehaviour
{
    public L3_picture selectedPicture;
    public bool isDragging = false;
    [SerializeField] float rotationSpeed = 5f;
    Vector3 mousePos;
    Vector3 prevMousePos;
    Vector3 mouseDelta;

    float rotationAmount;
    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        SelectedPicture();
        HandlePictureDragging();
    }



    void SelectedPicture()
    {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.zero);
            if (hit.collider == null) return;
            selectedPicture = hit.collider.GetComponent<L3_picture>();

            if(selectedPicture != null)
            {
                isDragging = true;
                prevMousePos = mousePos;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            selectedPicture = null;
        }
    }

    void HandlePictureDragging()
    {
        if (isDragging && selectedPicture != null)
        {
            mouseDelta = mousePos - prevMousePos;

            rotationAmount = mouseDelta.y * rotationSpeed;
            selectedPicture.transform.Rotate(0,0,rotationAmount);
            prevMousePos = mousePos;

            CheckWin();
        }
    }

    void CheckWin()
    {
        // Lấy góc hiện tại
        float currentAngle = selectedPicture.transform.eulerAngles.z;

        // Chuyển đổi về khoảng -180 đến 180 độ để dễ so sánh
        if (currentAngle > 180)
            currentAngle -= 360;

        // Debug để xem giá trị thực của góc
        Debug.Log("Current angle (adjusted): " + currentAngle);

        // Kiểm tra nếu góc gần với 0 (với sai số nhỏ)
        if (Mathf.Abs(currentAngle) < 1.5f) // Cho phép sai số 5 độ
        {
            isDragging = false;
            selectedPicture.circleCollider.enabled = false;
            selectedPicture.AfterWin();
            selectedPicture = null;
        }
    }
}
