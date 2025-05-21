using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class L28_Bottle : MonoBehaviour
{
    public bool isMove;
    public int idBottle;
    public Vector2 posDefault;
    [Button("position", ButtonSizes.Large)]
    void Setup()
    {
        posDefault = transform.position;
    }

}
