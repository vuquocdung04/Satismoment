using DG.Tweening;
using System.Collections;
using UnityEngine;

public class L32_PiceCard : MonoBehaviour
{
    public bool isComplete;
    public int angle = 90;
    private bool isClick = false;

    public void DoRotatingCard()
    {
        if (isComplete || isClick) return;
        StartCoroutine(RotateCard());
    }

    IEnumerator RotateCard()
    {
        isClick = true;
        float targetZ = transform.eulerAngles.z + angle;
        transform.DORotate(new Vector3(0, 0, targetZ), 0.3f, RotateMode.Fast);

        yield return new WaitForSeconds(0.31f);
        if(transform.eulerAngles.z < 0.5f && transform.eulerAngles.z > - 0.5f)
        {
            isComplete = true;
        }

        yield return new WaitForSeconds(0.3f);
        isClick = false;
    }
}
