using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_5Ctrl : MonoBehaviour
{
    public L5_Cup cup;
    Vector3 mousePos;
    public bool isDragging;
    public int amount;
    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        HandleDraggingCup();
    }

    void HandleDraggingCup()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.zero);
            if (hit.collider == null) return;

            cup = hit.collider.GetComponent<L5_Cup>();
            if (cup == null) return;
            isDragging = true;
        }

        if(isDragging && cup != null)
        {
            cup.transform.position = mousePos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            cup = null;
        }
    }
}
