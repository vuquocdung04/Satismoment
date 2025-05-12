using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SnappingZones : MonoBehaviour, IEndDragHandler, IBeginDragHandler
{
    public ScrollRect scrollRect;
    public RectTransform contentPanel;

    // keo lan dau
    public void OnBeginDrag(PointerEventData eventData)
    {
        scrollRect.inertia = true;

    }

    // ket thuc keo
    public void OnEndDrag(PointerEventData eventData)
    {
        scrollRect.inertia = false;
        contentPanel.transform.position = new Vector2(-800, 0);
    }
}
