using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L78_SwimPool : MonoBehaviour
{
    public List<Transform> lsCorocodies;
    int i = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var corocodie = collision.GetComponent<L78_Corocodie>();
        if (corocodie == null) return;
        corocodie.transform.DOKill();
        corocodie.gameObject.SetActive(false);
        lsCorocodies[i].gameObject.SetActive(true);
        i++;

        if (i == lsCorocodies.Count)
            StartCoroutine(HandleWinCodition());
    }

    IEnumerator HandleWinCodition()
    {
        yield return new WaitForSeconds(0.4f);
        WinBox.SetUp().Show();
    }
}
