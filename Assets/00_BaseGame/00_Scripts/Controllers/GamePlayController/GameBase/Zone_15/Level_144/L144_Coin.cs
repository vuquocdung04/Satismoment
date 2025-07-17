using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L144_Coin : MonoBehaviour
{
    public CircleCollider2D objCollider;

    public void HandleOutPiggyBank()
    {
        objCollider.enabled = false;
    }
}
