using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L93_FakeMoveParent : MonoBehaviour
{
    public BoxCollider2D boxCollider2D;
    public float maxDistanceX;

    public bool CheckDistanceCorrect()
    {
        float distanceX = transform.localPosition.x;
        if(Mathf.Abs(distanceX) < 0.1f)
        {
            transform.localPosition = new Vector2(0,transform.localPosition.y);
            boxCollider2D.enabled = false;
            return true;
        }
        return false;
    }


    public void Init()
    {
        maxDistanceX = transform.GetComponent<SpriteRenderer>().bounds.size.x;
    }
}
