
using DG.Tweening;
using UnityEngine;

public class L17_Coca : MonoBehaviour
{
    public BoxCollider2D cocaCollider;

    public void AnimationScale()
    {
        StartCoroutine(AnimScale());
    }
    private System.Collections.IEnumerator AnimScale()
    {
        var scaleClone1 = transform.DOScale(new Vector3(1.2f, 0.8f, 1), 0.2f);
        yield return scaleClone1.WaitForCompletion();
        var scaleClone2 = transform.DOScale(new Vector3(0.8f, 1f, 1), 0.2f);
        yield return scaleClone2.WaitForCompletion();
        transform.DOScale(Vector3.one, 0.2f);
    }
}
