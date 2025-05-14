using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_8Ctrl : MonoBehaviour
{
    public L8_Razor razor;

    Vector3 mousePos;
    public bool isDragging;

    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        HandleDraggRazor();
    }

    void HandleDraggRazor()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.zero);

            if (hit.collider == null) return;
            razor = hit.collider.GetComponent<L8_Razor>();
            if (razor == null) return;
            isDragging = true;

        }
        if (isDragging && razor != null)
        {
            razor.transform.position = mousePos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (razor == null) return;

            razor.transform.position = new Vector2(2,2);
            isDragging = false;
            razor = null;

        }
    }
}
