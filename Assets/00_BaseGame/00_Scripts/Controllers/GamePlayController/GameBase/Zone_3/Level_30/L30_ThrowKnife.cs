using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L30_ThrowKnife : MonoBehaviour
{
    public float speedFly = 5f;
    public bool isFly = false;
    public bool isCollide = false;
    public BoxCollider2D knifeCollider2D;
    private void Update()
    {
        if (isFly) return;
        transform.Translate(Vector3.up * speedFly * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var log = collision.GetComponent<L30_SpinLog>();
        if (log != null)
        {
            isFly = true;
            isCollide = true;
            transform.position = collision.ClosestPoint(transform.position);
            transform.SetParent(log.transform);
            log.IncreaseWinProgress();
            
        }
        else
        {
            isFly = true;
            if (isCollide) return;
            knifeCollider2D.enabled = false;
            Vector3 direction = (transform.position - collision.transform.position).normalized;
            Vector3 targetPos = transform.position + direction * 2f + Vector3.down * 3f;

            transform.DOMove(targetPos, 1f).SetEase(Ease.InQuad);
            transform.DORotate(new Vector3(0, 0, 720), 1f, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear);

        }

    }

}
