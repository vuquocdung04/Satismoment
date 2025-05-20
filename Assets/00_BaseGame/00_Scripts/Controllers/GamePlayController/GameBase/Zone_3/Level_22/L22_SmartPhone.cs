using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L22_SmartPhone : MonoBehaviour
{
    public Transform mask;
    public SpriteRenderer screen;
    public SpriteRenderer lemon;
    public Transform low_Battery;
    public Transform full_Battery;
    public Transform Battery;
    bool isCharged;
    private void Start()
    {
        StartCoroutine(HandleLowBattery());
    }

    IEnumerator HandleLowBattery()
    {
        var waitTime = new WaitForSeconds(1f);
        while (!isCharged)
        {
            low_Battery.gameObject.SetActive(true);
            yield return waitTime;
            low_Battery.gameObject.SetActive(false);
            yield return waitTime;
        }
    }


    public void HandleBattery()
    {
        isCharged = true;
        this.low_Battery.gameObject.SetActive(false);
        StartCoroutine(WaitAnim());
    }

    IEnumerator WaitAnim()
    {
        mask.transform.DOLocalMoveY(1f, 3f);
        yield return new WaitForSeconds(3f);
        full_Battery.gameObject.SetActive(false);
        Battery.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        lemon.DOFade(1f, 1f);
        yield return new WaitForSeconds(1.1f);
        lemon.gameObject.SetActive(false);
        screen.DOFade(1f, 1f);
        yield return new WaitForSeconds(1.1f);
        WinBox.SetUp().Show();
    }


}
