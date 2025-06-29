using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L97_T_Shirt : MonoBehaviour
{
    public Level_97Ctrl levelCtrl;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var water = collision.GetComponent<L97_Water>();
        if (water == null) return;
        levelCtrl.waterDropCount++;
        if(levelCtrl.waterDropCount == 10)
        {
            levelCtrl.T_ShirtWet.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
