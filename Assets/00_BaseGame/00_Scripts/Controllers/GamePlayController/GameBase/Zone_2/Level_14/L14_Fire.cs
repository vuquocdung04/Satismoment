using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase.Zone_2.Level_14;
using UnityEngine;

public class L14_Fire : MonoBehaviour
{
    public Level_14Ctrl levelCtrl;
    private bool isExtinguished = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isExtinguished) return;
        this.transform.localScale -= new Vector3(0.05f,0.02f,0f);
        if (this.transform.localScale.x > 0f) return;
        isExtinguished = true;
        Destroy(gameObject);
        levelCtrl.winProgress++;
    }
}
