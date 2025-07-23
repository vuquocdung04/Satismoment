using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L154_Spice : MonoBehaviour
{
    public void Falling()
    {
        float randY = Random.Range(1.5f,2.5f);
        float randX = Random.Range(-0.3f,0.3f);
        transform.DOMove(new Vector3(transform.position.x + randX,transform.position.y - randY), 0.4f).SetEase(Ease.Linear);
    }
}
