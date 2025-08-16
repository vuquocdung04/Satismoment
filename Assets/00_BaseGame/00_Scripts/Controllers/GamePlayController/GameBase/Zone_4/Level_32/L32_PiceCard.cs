using DG.Tweening;
using System.Collections;
using UnityEngine;

public class L32_PiceCard : MonoBehaviour
{
    public bool isComplete;
    public int angle = 90;
    private bool isClick;

    public void DoRotatingCard(System.Action callback = null)
    {
        if (isComplete || isClick) return;
        StartCoroutine(RotateCard(callback));
    }

    IEnumerator RotateCard(System.Action callback = null)
    {
        isClick = true;
        float targetZ = transform.eulerAngles.z + angle;
        var cardRotate = transform.DORotate(new Vector3(0, 0, targetZ), 0.3f);
        // Normalize targetZ về khoảng [0, 360) để so sánh
        float normalizedTargetZ = targetZ % 360f;
        if (normalizedTargetZ < 0) normalizedTargetZ += 360f;
    
        if(normalizedTargetZ < 0.5f || normalizedTargetZ > 359.5f)
        {
            isComplete = true;
            callback?.Invoke();
        }
        yield return cardRotate.WaitForCompletion();
        isClick = false;
    }
}
