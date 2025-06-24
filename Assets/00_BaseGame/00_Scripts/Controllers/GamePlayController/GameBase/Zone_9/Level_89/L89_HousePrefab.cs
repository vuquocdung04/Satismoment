using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L89_HousePrefab : MonoBehaviour
{
    public Rigidbody2D rb;


    public void HandleFallCondition()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
