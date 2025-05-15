using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Level_9Ctrl : MonoBehaviour
{
    public Transform spriteMask;
    public Transform faceObj;
    Vector3 mousePos;
    Vector3 prevMousePos;
    Vector3 mouseDelta;
    public bool isDragging;
    float distance;

    public BoxCollider2D zonePlay;
    public SpriteRenderer facialExpression;
    public Sprite iconDefault;
    public Sprite iconDrag;
    public Sprite iconWin;
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
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider == null) return;
            spriteMask = hit.collider.GetComponent<Transform>();

            if (spriteMask == null) return;
            isDragging = true;
            prevMousePos = mousePos;
            facialExpression.sprite = iconDrag;
        }
        if (isDragging)
        {
            mouseDelta = mousePos - prevMousePos;
            spriteMask.transform.Translate(0,mouseDelta.y, 0);
            prevMousePos = mousePos;

            distance = 2.27f - spriteMask.transform.position.y; // 2.27 = pos spriteMask
            faceObj.transform.position = new Vector2(0, 1.65f - 2*distance); // 1.65 pos faceObj


            if (spriteMask.transform.position.y > 2.5)
            {
                isDragging = false;
            }

            CheckWin();
        }

        if (Input.GetMouseButtonUp(0))
        {
            facialExpression.sprite = iconDefault;
            isDragging = false;
            spriteMask = null;
        }
    }

    void CheckWin()
    {
        if (spriteMask.transform.position.y > -0.3f) return;
        isDragging = false;
        zonePlay.enabled = false;
        faceObj.transform.gameObject.SetActive(false);
        DOVirtual.DelayedCall(0.5f, delegate
        {
            facialExpression.sprite = iconWin;
            DOVirtual.DelayedCall(1f, () => WinBox.SetUp().Show());
        });
    }
}
