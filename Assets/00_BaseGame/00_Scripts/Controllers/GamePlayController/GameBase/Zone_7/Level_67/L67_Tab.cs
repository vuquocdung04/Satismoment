using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L67_Tab : MonoBehaviour
{
    public int idTab;
    public SpriteRenderer tabRenderer;


    public void DOAnimClosing()
    {
       transform.DOScale(Vector3.zero,0.4f).OnComplete(()=> this.gameObject.SetActive(false));
    }
}
