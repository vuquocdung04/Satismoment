using DG.Tweening;
using System.Collections;
using UnityEngine;

public class L173_Picture : MonoBehaviour
{
    public SpriteRenderer objRenderer;
    public Sprite spriteOff;
    public Sprite spriteOn;

    public void InitState()
    {
        StartCoroutine(HandleActionStart());
    }

    IEnumerator HandleActionStart()
    {
        // Bật sprite
        objRenderer.sprite = spriteOn;

        // Rung và xoay cùng lúc
        transform.DOShakeRotation(1.3f, 0.3f, 15, 90f, true);
        transform.DORotate(new Vector3(0, 0, 35f), 1.5f, RotateMode.Fast);

        // Chờ 2.1 giây rồi tắt đèn
        yield return new WaitForSeconds(1.3f);
        objRenderer.sprite = spriteOff;
    }
}
