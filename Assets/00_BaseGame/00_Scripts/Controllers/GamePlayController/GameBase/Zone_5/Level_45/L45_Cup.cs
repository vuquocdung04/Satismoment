using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L45_Cup : MonoBehaviour
{
    [Title("Setting Show")]
    [SerializeField]
    private bool showBall = false;
    [ShowIf("showBall")]
    public Transform ball;
    public Vector2 pos;
    public void DoOpeningCup()
    {
        if(showBall) ball.SetParent(null);
        transform.DOMoveY(this.transform.position.y + 0.5f, 0.5f).OnComplete(delegate
        {
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
