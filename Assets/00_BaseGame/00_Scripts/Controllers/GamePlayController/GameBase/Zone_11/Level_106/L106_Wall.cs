using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L106_Wall : MonoBehaviour
{
    public Level_106Ctrl levelCtrl;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var waterDroplet = collision.GetComponent<L106_WaterDroplet>();
        if (waterDroplet == null) return;
        waterDroplet.OnExitState();
        levelCtrl.winProgress++;
    }
}
