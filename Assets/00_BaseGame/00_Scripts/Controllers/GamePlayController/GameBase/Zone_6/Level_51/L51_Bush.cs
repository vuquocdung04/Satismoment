using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L51_Bush : MonoBehaviour
{
    public bool isDirectionLeft = false;
    public Vector2 posDefault;

    [Button("Setup", ButtonSizes.Large)]
    void SetupPos()
    {
        posDefault = transform.position;
    }
}
