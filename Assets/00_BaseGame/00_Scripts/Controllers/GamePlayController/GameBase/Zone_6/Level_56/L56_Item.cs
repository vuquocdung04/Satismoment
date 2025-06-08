using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L56_Item : MonoBehaviour
{
    public Vector2 posDefault;
    public BoxCollider2D _collider;
    
    public IEnumerator DoItemMoving()
    {
        transform.DOScale(new Vector3(0.5f,0.5f,0.5f),0.7f);
        transform.DOMoveY(-0.5f,0.5f);
        yield return new WaitForSeconds(0.9f);
        Destroy(gameObject);
    }
    public void DoMovingPosDefault()
    {
        transform.DOMove(posDefault,0.3f);
    }
}
