using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L78_Point : MonoBehaviour
{
    public Transform nextPoint;
    public float angle;
    public bool isCorrect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var corocodieClone = collision.GetComponent<L78_Corocodie>();
        if (corocodieClone == null) return;
        corocodieClone.transform.DOKill();

        var angleClone = corocodieClone.transform.eulerAngles.z + angle;
        corocodieClone.transform.DORotate(new Vector3(0, 0, angleClone), 0.2f).OnComplete(delegate
        {
            Debug.LogError(transform.name);
            StartCoroutine(Move(corocodieClone));
        });
    }


    IEnumerator Move(L78_Corocodie corocodieClone)
    {
        Tween move = corocodieClone.transform.DOMove(nextPoint.position, 0.5f);
        yield return move.WaitForCompletion();
        if (isCorrect)
        {
            corocodieClone.transform.DORotate(Vector3.zero, 0.2f);
            yield return new WaitForSeconds(0.3f);
            corocodieClone.Move();
        }
    }
}
