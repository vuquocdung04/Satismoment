using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_115Ctrl : MonoBehaviour
{
    public int watermelonSeedsDropped; // Số hạt dưa đã bỏ
    public float totalSaltTime;         // Tổng thời gian rắc muối
    public Transform saltContainer;     // Vị trí spawn muối

    private bool isWin = false; // Trạng thái thắng

    public void SetIsWin()
    {
        if (!isWin)
        {
            isWin = true;
            StartCoroutine(HandleWinCondition());
        }
    }

    // Coroutine xử lý hành động khi thắng
    IEnumerator HandleWinCondition()
    {
        yield return new WaitForSeconds(1.25f);
        WinBox.SetUp().Show();
    }

    public void ActiveState2()
    {
        saltContainer.DOMoveX(1.25f,0.5f).SetEase(Ease.Linear);
    }
}