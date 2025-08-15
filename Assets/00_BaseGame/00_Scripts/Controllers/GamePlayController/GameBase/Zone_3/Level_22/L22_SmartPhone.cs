using DG.Tweening;
using System.Collections;
using UnityEngine;

public class L22_SmartPhone : MonoBehaviour
{
    public Level_22Ctrl levelCtrl;
    public Transform mask;
    public SpriteRenderer screen;
    public SpriteRenderer lemon;
    public Transform lowBattery;
    public Transform fullBattery;
    public Transform battery;
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
            lowBattery.gameObject.SetActive(true);
            yield return waitTime;
            lowBattery.gameObject.SetActive(false);
            yield return waitTime;
        }
    }


    public void HandleBattery()
    {
        isCharged = true;
        this.lowBattery.gameObject.SetActive(false);
        StartCoroutine(WaitAnim());
    }

    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator WaitAnim()
    {
        mask.transform.DOLocalMoveY(1f, 3f);
        yield return new WaitForSeconds(3f);
        levelCtrl.PlayingBatteryFullSound();
        fullBattery.gameObject.SetActive(false);
        battery.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        lemon.DOFade(1f, 1f);
        yield return new WaitForSeconds(1.1f);
        lemon.gameObject.SetActive(false);
        screen.DOFade(1f, 1f);
        yield return new WaitForSeconds(1.1f);
        WinBox.SetUp().Show();
    }


}
