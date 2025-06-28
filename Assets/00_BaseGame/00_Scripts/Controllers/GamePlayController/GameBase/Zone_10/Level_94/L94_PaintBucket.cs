using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L94_PaintBucket : MonoBehaviour
{
    public Transform icon;
    public BoxCollider2D objCollider;
    public float maxDistanceX;
    public float posCorrect;

    public bool CheckCorrectCondition()
    {
        float distanceX = icon.transform.localPosition.x - posCorrect;
        if(Mathf.Abs(distanceX)< 0.1f)
        {
            icon.transform.localPosition = new Vector2(posCorrect,icon.transform.localPosition.y);
            objCollider.enabled = false;
            return true;
        }
        return false;
    }


    public void Init()
    {
        icon = transform.Find("icon");
        maxDistanceX = icon.transform.GetComponent<SpriteRenderer>().bounds.size.x /3;
        objCollider = transform.GetComponent<BoxCollider2D>();
        posCorrect = icon.transform.localPosition.x;
    }
}
