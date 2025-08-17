using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

public class L45_Cup : MonoBehaviour
{
    [Title("Setting Show")]
    [SerializeField]
    private bool showBall;
    [ShowIf("showBall")]
    public Transform ball;
    public Vector2 pos;
    public void DoOpeningCup(System.Action callback = null)
    {
        if(showBall) ball.SetParent(null);
        transform.DOMoveY(this.transform.position.y + 0.5f, 0.5f).OnComplete(delegate
        {
            callback?.Invoke();
            StartCoroutine(OpenBall());
        });
    }

    IEnumerator OpenBall()
    {
        transform.DOMoveY(pos.y, 0.5f);
        yield return new WaitForSeconds(0.51f);
        if (showBall)
            ball.SetParent(transform);
    }
}
