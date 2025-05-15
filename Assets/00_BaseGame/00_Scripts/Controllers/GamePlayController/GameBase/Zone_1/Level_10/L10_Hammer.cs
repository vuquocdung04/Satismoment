using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class L10_Hammer : MonoBehaviour
{
    public Rigidbody2D rb;
    public float nailDepth;
    int amount = 0;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null) return;
        var nail = collision.collider.GetComponent<L10_Nail>();
        if (nail == null) return;
        var pos = new Vector3(0,nailDepth);
        nail.transform.position -= pos;
        if(nail.transform.position.y <= -3.9f)
        {
            nail.boxCollider2D.enabled = false;
            nail.transform.position = new Vector2(nail.transform.position.x, -3.95f);
            amount++;
        }
        this.transform.position -= pos;
        CheckWin();
    }

    void CheckWin()
    {
        if(amount > 1)
        {
            DOVirtual.DelayedCall(1f, () => WinBox.SetUp().Show());
        }
    }
}
