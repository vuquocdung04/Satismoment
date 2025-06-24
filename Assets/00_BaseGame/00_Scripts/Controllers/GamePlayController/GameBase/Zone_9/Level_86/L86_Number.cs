using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L86_Number : MonoBehaviour
{
    public Transform numberActive;

    public void ShowNumer()
    {
        StartCoroutine(AnimateNumber());
    }

    IEnumerator AnimateNumber()
    {
        numberActive.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        numberActive.gameObject.SetActive(false);
    }
}
