using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L155_Peanut : MonoBehaviour
{
    public int id;
    public Rigidbody2D rb;

    public void OnStart()
    {
        rb.gravityScale = 0;
    }

    public void OnEnd()
    {
        rb.gravityScale = 1;
    }
}
