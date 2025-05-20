using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L22_ChargingCable : MonoBehaviour
{
    public Level_22Ctrl levelCtrl;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.transform.position = new Vector2(0, -3.67f);
        levelCtrl.isWin = true;
        levelCtrl.smartPhone.HandleBattery();
    }
}
