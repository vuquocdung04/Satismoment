using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L177_Hand : MonoBehaviour
{
    Vector2 newPos;
    public float targetY = 0.85f;
    bool moveCompleted = false;
    public void OnLogic(Vector2 mouseDelta, System.Action callBack = null)
    {
        if (moveCompleted) return;
        newPos = transform.position + new Vector3(0, mouseDelta.y, 0);

        // Chỉ cho phép di chuyển xuống (giá trị y nhỏ hơn hoặc bằng vị trí hiện tại)
        newPos.y = Mathf.Min(newPos.y, transform.position.y);

        transform.position = newPos;

        if(transform.position.y <= 0.85f)
        {
            moveCompleted = true;
            callBack?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var btn = collision.GetComponent<Transform>();
        if(btn != null)
        {
            btn.SetParent(transform);
        }
    }
}
