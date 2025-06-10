using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L61_Petal : MonoBehaviour
{
    public Vector2 defaultPos;
    public BoxCollider2D _collider;

    [Button("Setup",ButtonSizes.Large)]
    void Setup()
    {
        defaultPos = transform.position;
        _collider = GetComponent<BoxCollider2D>();
    }
}
