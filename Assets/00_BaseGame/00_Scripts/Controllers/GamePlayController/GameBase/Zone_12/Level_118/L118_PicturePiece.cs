using DG.Tweening;
using System.Collections;
using UnityEngine;

public class L118_PicturePiece : MonoBehaviour
{
    public BoxCollider2D objCollider;
    bool isReadyRotate = true;
    float targetZRotation = 0;
    public bool IsAtZeroDegree()
    {
        if (NormalizeAngle(targetZRotation) > -0.1f && NormalizeAngle(targetZRotation) < 0.1f)
        {
            objCollider.enabled = false;
            return true;
        }
        return false;
    }

    public void Rotate()
    {
        if (!isReadyRotate) return;
        StartCoroutine(PictureRotating());
    }

    IEnumerator PictureRotating()
    {
        isReadyRotate = false;
        // Use DORotate to smoothly rotate
        targetZRotation = transform.eulerAngles.z + 90f;
        Debug.LogError(targetZRotation);
        Tween rotateTween = transform.DORotate(new Vector3(0, 0, targetZRotation), 0.25f, RotateMode.FastBeyond360);
        yield return rotateTween.WaitForCompletion();

        isReadyRotate = true;
    }
    private float NormalizeAngle(float angle)
    {
        return (angle % 360 + 360) % 360;
    }
    //ODin
    public void Init()
    {
        objCollider = GetComponent<BoxCollider2D>();
    }
}