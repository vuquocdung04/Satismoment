using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class L150_Fruit : MonoBehaviour
{
    public Transform colorPattern;
    public Transform item;
    public float sizeX;
    [Space(5),Header("Item")]
    public float limitXRight;
    public float limitXLeft;
    public void Drag(Vector2 mouseDelta)
    {
        colorPattern.localPosition += new Vector3(mouseDelta.x,0,0);
        item.localPosition += new Vector3(mouseDelta.x,0,0);
        if(colorPattern.localPosition.x >= sizeX/3)
        {
            colorPattern.localPosition = Vector3.zero;
        }
        else if(colorPattern.localPosition.x <= -sizeX/3)
        {
            colorPattern.localPosition = Vector3.zero;
        }

        if(item.localPosition.x >= limitXRight)
        {
            item.localPosition = new Vector3(-limitXLeft, item.localPosition.y);
        }
        else if(item.localPosition.x <= -limitXLeft)
        {
            item.localPosition = new Vector3(limitXRight, item.localPosition.y);
        }
    }


    [Button("Odin",ButtonSizes.Large)]
    void Setup()
    {
        colorPattern = transform.Find("color");
        item = transform.Find("item");
        sizeX = colorPattern.GetComponent<SpriteRenderer>().bounds.size.x;
    }
}
