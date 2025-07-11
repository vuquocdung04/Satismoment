using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L125_Effect : MonoBehaviour
{
    public SpriteRenderer smokeRenderer;
    public List<Sprite> lsFrames;

    public void Init()
    {
        // Chọn frame ngẫu nhiên
        int rand = Random.Range(0, lsFrames.Count);
        smokeRenderer.sprite = lsFrames[rand];

        // Reset màu về trắng + hiện rõ
        smokeRenderer.color = new Color(1, 1, 1, 1);
        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DOMove(transform.position + Vector3.one * 1.5f, 1f)
            .SetEase(Ease.Linear));

        seq.Join(smokeRenderer.DOFade(0f, 2f));

        seq.OnComplete(() =>
        {
            SimplePool2.Despawn(gameObject);
        });
    }
}