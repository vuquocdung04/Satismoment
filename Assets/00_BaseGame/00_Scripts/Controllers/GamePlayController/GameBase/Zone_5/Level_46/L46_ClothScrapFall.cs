using UnityEngine;
using DG.Tweening;

public class L46_ClothScrapFall : MonoBehaviour
{
    public bool isClear;
    public float fallDuration = 2f;         // thời gian rơi
    public float rotateAngle = 180f;        // góc xoay khi rơi
    public float horizontalShake = 0.5f;    // dao động ngang


    public void PlayFallAnimation()
    {
        // Rơi xuống
        transform.DOMoveY(-15f, fallDuration)
            .SetEase(Ease.InQuad);

        // Xoay xoay khi rơi
        transform.DORotate(new Vector3(0, 0, rotateAngle), fallDuration, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear);
    }
}
