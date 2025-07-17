using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L142_CeramicJar : MonoBehaviour
{
    public Transform ceramicPattern;
    public float maxSizeX;
    public List<L142_Point> lsPoints;
    public void RotateJar(Vector3 rotateSpeed)
    {
        ceramicPattern.localPosition += new Vector3(rotateSpeed.x,0,0);
        if(ceramicPattern.localPosition.x >= maxSizeX/3)
        {
            ceramicPattern.localPosition = Vector3.zero;
        }
        else if(ceramicPattern.localPosition.x <= -maxSizeX/3)
        {
            ceramicPattern.localPosition = Vector3.zero;
        }
    }

    public Vector2 GetPositionPointById(int id)
    {
        foreach (var point in this.lsPoints) if (id == point.id) return lsPoints[id].transform.position;
        return Vector2.zero;
    }

    [Button("Setup", ButtonSizes.Large)]
    void Setup()
    {
        ceramicPattern = transform.Find("ceramic pattern");
        maxSizeX = ceramicPattern.GetComponent<SpriteRenderer>().bounds.size.x;
    }
}
