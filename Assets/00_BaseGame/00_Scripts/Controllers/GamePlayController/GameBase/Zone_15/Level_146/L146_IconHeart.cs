using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L146_IconHeart : MonoBehaviour
{
    public SpriteRenderer objRenderer;
    public BoxCollider2D objCollider;
    public Level_146Ctrl levelCtrl;
    private void OnMouseDown()
    {
        objCollider.enabled = false;
        objRenderer.sprite = levelCtrl.heartActive;
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one,0.3f).SetEase(Ease.Linear);
        levelCtrl.likedPostCount++;
        StartCoroutine(
                levelCtrl.HandleWinCondition());
    }
}
