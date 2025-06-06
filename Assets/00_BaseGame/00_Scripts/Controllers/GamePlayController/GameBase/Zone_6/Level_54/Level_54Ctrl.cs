using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_54Ctrl : MonoBehaviour
{
    public L54_Hand hand;
    public L54_TimingBar timingBar;

    bool isClick = false;
    private void Start()
    {
        timingBar.DoThumbMoving();
        hand.DoHandMoving();
    }

    private void Update()
    {
        if (isClick) return;
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(HandleActiveGame());
        }
    }

    IEnumerator HandleActiveGame()
    {
        isClick = true; 
        timingBar.DoThumbPausing();
        StartCoroutine(hand.DoMoveDown());
        yield return new WaitForSeconds(2.1f);
        if (hand.isResumeGame)
        {
            timingBar.DoThumbResuming();
            hand.DoHandResuming();
            isClick = false;
        }
        else
        {
            yield return new WaitForSeconds(0.1f);
            WinBox.SetUp().Show();
        }
    }
}
