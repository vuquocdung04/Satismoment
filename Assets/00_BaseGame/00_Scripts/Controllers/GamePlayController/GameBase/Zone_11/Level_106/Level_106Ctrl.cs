using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_106Ctrl : MonoBehaviour
{
    public int winProgress;
    private bool isWin = false;
    public SpriteRenderer screenProtector;
    public List<L106_WaterDroplet> lsWaters;
    public Transform touch;

    Vector3 mousePosition;
    void Update()
    {
        if (isWin) return;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        if (Input.GetMouseButton(0))
        {
            touch.transform.position = mousePosition; 
        }

        if (Input.GetMouseButtonUp(0))
        {
            CheckWinCondition();
        }
    }

    bool CheckWinCondition()
    {
        if(winProgress == lsWaters.Count)
        {
            isWin = true;
            StartCoroutine(HandleWinCondition());
            return true;
        }
        return false;
    }

    private IEnumerator HandleWinCondition()
    {
        Tween screenHide = screenProtector.DOFade(0,0.5f);
        yield return screenHide.WaitForCompletion();
        yield return new WaitForSeconds(0.3f);
        WinBox.SetUp().Show();
    }

    [Button("Setup",ButtonSizes.Large)]
    void SetupWater()
    {
        foreach (var water in this.lsWaters) water.Init();
    }
}
