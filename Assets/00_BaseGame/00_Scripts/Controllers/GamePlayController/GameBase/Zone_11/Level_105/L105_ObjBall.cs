using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum L105_ObjBallType
{
    Red,
    Black,
}


public class L105_ObjBall : MonoBehaviour
{
    public L105_ObjBallType ballType;
    public Rigidbody2D rb;
    public bool isDone;
    public CircleCollider2D circleCollider;
    private IEnumerator CheckAndStopBall(float checkInterval = 1f)
    {
        while (!isDone)
        {
            yield return new WaitForSeconds(checkInterval);

            // Kiểm tra nếu vận tốc và xoay đều rất nhỏ
            if (rb.velocity.magnitude < 1f)
            {
                // Dừng hoàn toàn quả bóng
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;

                // Thoát khỏi vòng lặp
                break;
            }
        }
    }

    public void OnBallStopped()
    {
        isDone = true;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        circleCollider.enabled = false;
        transform.DOScale(Vector3.zero,0.2f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(CheckAndStopBall());
    }

    public void ResetState()
    {
        isDone = false;
        circleCollider.enabled = true;
        transform.localScale = Vector3.one;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }
}
