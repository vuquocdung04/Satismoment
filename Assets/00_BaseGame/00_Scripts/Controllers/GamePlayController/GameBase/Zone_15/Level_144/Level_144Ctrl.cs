using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_144Ctrl : BaseDragController<L144_PiggyBank>
{
    public int withdrawnAmount = 0;
    public List<L144_Coin> lsCoins;
    protected override void OnDragEnded()
    {
        draggableComponent.StopVelocity();
        if(withdrawnAmount == lsCoins.Count)
        {
            StartCoroutine(HandleWinCondition());
        }
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.rb.MovePosition(currentMousePosition);
    }

    protected override void OnDragStarted()
    {
        
    }

    IEnumerator HandleWinCondition()
    {
        isWin = true;
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();
    }

    [Button("Setup Coin", ButtonSizes.Large)]
    void SetupCoin()
    {
        foreach(L144_Coin coin in lsCoins) coin.objCollider = coin.transform.GetComponent<CircleCollider2D>();
    }
}
