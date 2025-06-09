using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L59_Pig : MonoBehaviour
{
    public Level_59Ctrl levelCtrl;
    public Vector2 defaultPos;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var wood = collision.GetComponent<L59_Wood>();
        if (wood != null)
        {
            levelCtrl.winProgress++;
            transform.SetParent(wood.transform);
            if(levelCtrl.winProgress == 5)
            {
                StartCoroutine(levelCtrl.OnWin());
            }
        }
        else
        {
            levelCtrl.winProgress = 0;
            transform.position = defaultPos;
            transform.SetParent(null);
        }
        
    }
}
