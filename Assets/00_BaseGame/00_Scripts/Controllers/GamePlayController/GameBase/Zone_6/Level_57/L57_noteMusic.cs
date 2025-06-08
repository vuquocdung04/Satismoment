using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // <- Import DOTween namespace

public class L57_noteMusic : MonoBehaviour
{
    public float moveDistance = 2f;
    public float duration = 1.5f;
    public float fadeDuration = 1.5f;

    public SpriteRenderer spriteRenderer;
    public List<Sprite> lsSprites;

    public void Effect()
    {
        int rand = Random.Range(0, lsSprites.Count);
        spriteRenderer.sprite = lsSprites[rand];

        Vector3 startPos = transform.position;

        // Tạo điểm đỉnh hình chóp (lệch trái hoặc phải ngẫu nhiên)
        float xOffset = Random.Range(-0.5f, 0.5f); // Lệch ngang
        float peakHeight = moveDistance + Random.Range(0.5f, 1f); // Đỉnh cao hơn

        Vector3 peakPoint = startPos + new Vector3(xOffset, peakHeight, 0);
        Vector3 endPoint = startPos + new Vector3(xOffset * 2f, moveDistance, 0);

        Vector3[] path = new Vector3[] { peakPoint, endPoint };

        transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        transform.eulerAngles = new Vector3(0, 0, Random.Range(-20, 20));

        transform.DOPath(path, duration, PathType.CatmullRom)
            .SetEase(Ease.OutCubic);

        transform.DOScale(Vector3.zero, duration).SetEase(Ease.InQuad);

        spriteRenderer.DOFade(0, fadeDuration).SetEase(Ease.Linear);
    }

}
