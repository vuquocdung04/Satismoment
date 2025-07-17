using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L144_PiggyBank : MonoBehaviour
{
    public Rigidbody2D rb;

    public void StopVelocity()
    {
        rb.velocity = Vector3.zero;
    }

}
