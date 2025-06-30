using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L102_FramePicture : MonoBehaviour
{
    public Transform targetMove;
    public SpriteRenderer spriteRendere;


    void ShowFramePicture(Sprite sprite)
    {
        gameObject.SetActive(true);
        transform.localScale = new Vector3(0.8f, 0.9f, 1f);
        transform.position = Vector3.zero;
        spriteRendere.sprite = sprite;
    }

     void HideFramePicture()
    {
        gameObject.SetActive(false);
    }

    public IEnumerator HandleAction(Sprite sprite)
    {
        ShowFramePicture(sprite);
        yield return new WaitForSeconds(1f);
        transform.DOScale(Vector3.zero, 0.5f);
        transform.DOMove(targetMove.position, 0.5f).SetEase(Ease.Linear).OnComplete(delegate
        {
            HideFramePicture();
        });
    }
}
