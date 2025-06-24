using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L88_GrassTractor : MonoBehaviour
{
    public Level_88Ctrl levelCtrl;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.SetActive(false);
        levelCtrl.winProgress++;
    }
}
