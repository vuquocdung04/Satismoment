using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // Đảm bảo đã thêm namespace DOTween

public class L115_SaltParticle : MonoBehaviour
{
    public void Falling()
    {
        float targetY = Random.Range(-0.5f, 0.2f);

        Vector3 targetPosition = new Vector3(transform.position.x, targetY, transform.position.z);

        transform.DOMove(targetPosition, 0.3f).SetEase(Ease.InCubic);
    }
}