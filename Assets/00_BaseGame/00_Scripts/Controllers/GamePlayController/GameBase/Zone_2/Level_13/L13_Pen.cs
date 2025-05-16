using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L13_Pen : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var item = collision.GetComponent<L13_CasePen>();
        if (item == null) return;

        this.transform.SetParent(item.holdPen);
    }
}
