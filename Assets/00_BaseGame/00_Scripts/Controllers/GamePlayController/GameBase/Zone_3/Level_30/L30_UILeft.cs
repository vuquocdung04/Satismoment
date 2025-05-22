using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
public class L30_UILeft : MonoBehaviour
{
    public int amountHit = 0;
    public Color32 colorDefault;
    public Color32 colorUsed;
    public List<SpriteRenderer> lsKnifes;
    public List<Transform> lsPieceBreaks;
    public void RegisterHit()
    {
        if (amountHit <= 0) return;

        int indexToColor = lsKnifes.Count - amountHit;
        if (indexToColor >= 0 && indexToColor < lsKnifes.Count)
        {
            lsKnifes[indexToColor].color = colorUsed;
        }

        amountHit--;
    }

    public void BreakPieces(float radius = 2f, float duration = 1f)
    {
        foreach (Transform piece in lsPieceBreaks)
        {
            piece.gameObject.SetActive(true); // Bật nếu bị tắt

            Vector3 randomDirection = Random.onUnitSphere;
            randomDirection.z = 0; // Nếu chỉ muốn vỡ trong mặt phẳng 2D

            Vector3 targetPos = piece.localPosition + randomDirection * radius;

            piece.DOScale(Vector3.zero, duration).SetEase(Ease.InBack); // Co lại sau khi bay
            piece.DOLocalMove(targetPos, duration).SetEase(Ease.OutQuad);
            piece.DORotate(new Vector3(0, 0, Random.Range(-180, 180)), duration, RotateMode.Fast);
        }
    }

    [Button("SetUp", ButtonSizes.Large)]
    void EditorSetUp()
    {
        amountHit = lsKnifes.Count;
    }
}
