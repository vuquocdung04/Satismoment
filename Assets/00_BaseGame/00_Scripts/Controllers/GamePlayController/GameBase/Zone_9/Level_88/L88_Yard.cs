using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L88_Yard : MonoBehaviour
{
    public List<Transform> lsDecors;
    public bool isAnimateCompleted;
    public IEnumerator AnimateWinCondition()
    {
        var waitTime = new WaitForSeconds(0.1f);
        foreach (var decor in lsDecors)
        {
            decor.localScale = Vector3.zero;
            decor.gameObject.SetActive(true);
        }

        for (int i = 0; i < lsDecors.Count; i += 2)
        {
            lsDecors[i].DOScale(Vector3.one, 0.3f);
            if (i + 1 < lsDecors.Count) lsDecors[i + 1].DOScale(Vector3.one, 0.3f);

            if (i < lsDecors.Count - 2)
                yield return waitTime;
        }
        isAnimateCompleted = true;
    }
}