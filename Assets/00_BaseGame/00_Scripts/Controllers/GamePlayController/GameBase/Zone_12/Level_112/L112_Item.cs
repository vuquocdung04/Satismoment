using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L112_Item : MonoBehaviour
{
    public Rigidbody2D rb;
    public float weight;
    public BoxCollider2D boxCOllider2d;

    public void OnDragStarted()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
    }
    public void OnDragLogic(Vector3 mouseDelta)
    {
        transform.position += new Vector3(mouseDelta.x, mouseDelta.y, mouseDelta.z);
    }

    public void OnDragEnded()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    public void SetParen(Transform parent)
    {
        transform.SetParent(parent);
    }
    public void Init()
    {
        boxCOllider2d = GetComponent<BoxCollider2D>();
        rb = transform.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }
}
