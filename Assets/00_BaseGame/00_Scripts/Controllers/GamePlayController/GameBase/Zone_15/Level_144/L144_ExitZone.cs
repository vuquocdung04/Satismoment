using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L144_ExitZone : MonoBehaviour
{
    public Level_144Ctrl levelCtrl;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var coin = collision.GetComponent<L144_Coin>();
        if(coin != null)
        {
            coin.HandleOutPiggyBank();
            levelCtrl.withdrawnAmount++;
        }
    }
}
