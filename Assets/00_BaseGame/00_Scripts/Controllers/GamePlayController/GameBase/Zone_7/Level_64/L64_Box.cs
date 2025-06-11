using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L64_Box : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var dino = collision.collider.GetComponent<L64_Dino>();

        if (dino == null) return;
        dino.levelCtrl.StopMoveMap();
        dino.levelCtrl.btnRetry.gameObject.SetActive(true);
        dino.gameObject.SetActive(false);
    }
}
