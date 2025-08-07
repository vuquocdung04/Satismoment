using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L175_Dog : MonoBehaviour
{
    public L175_Effect effect;
    public List<Transform> lsPoints; 
    bool isDone = false;
    public void Moving(float duration)
    {
        transform.DOMoveY(7f, duration).SetEase(Ease.Linear).OnComplete(delegate
        {
            isDone = true;
        });
        StartCoroutine(SpawnEffect());
    }
    IEnumerator SpawnEffect()
    {
        var waitTime = new WaitForSeconds(0.3f);
        while (!isDone)
        {
            for(int i = 0; i < lsPoints.Count; i++)
            {
                var cloneEffect = SimplePool2.Spawn(effect, lsPoints[i].position, Quaternion.identity);
                cloneEffect.InitState();
                yield return waitTime;
            }
        }
    }

}
