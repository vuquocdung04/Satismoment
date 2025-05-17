using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L13_CasePen : MonoBehaviour
{
    public Level_13Ctrl levelCtrl;
    public SpriteRenderer flap;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var pen = collision.GetComponent<L13_Pen>();
        if (levelCtrl.lsPens.Contains(pen)) return;
        flap.transform.eulerAngles = new Vector3(0,0,180);
        flap.transform.position = new Vector2(0,3.1f);
        levelCtrl.lsPens.Add(pen);
    }

}
