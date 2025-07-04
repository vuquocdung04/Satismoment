using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L111_NumberDial : MonoBehaviour
{
    public SpriteRenderer numberRenderer;
    public CircleCollider2D circleCollider;
    public List<Sprite> lsSprites;

    public void SetSpriteNumber(int index)
    {
        numberRenderer.sprite = lsSprites[index];
    }
    public void SetPosition(Transform newPos)
    {
        transform.position = newPos.position;
    }
}
