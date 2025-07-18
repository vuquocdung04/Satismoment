using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L145_CheeseShaving : MonoBehaviour
{
    public void Falling()
    {
        float randY = transform.position.y - Random.Range(1f,1.5f);
        transform.DOMoveY(randY,0.5f).SetEase(Ease.Linear);
    }
}
