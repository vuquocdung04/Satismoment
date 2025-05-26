using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L40_Item : MonoBehaviour
{
    public Vector2 posCorrect;

    public void DoFalling()
    {
        gameObject.SetActive(true);
        transform.DOMoveY(posCorrect.y, 0.4f);
    }
}
