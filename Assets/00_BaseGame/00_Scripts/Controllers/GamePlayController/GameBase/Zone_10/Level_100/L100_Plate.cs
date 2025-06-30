using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L100_Plate : MonoBehaviour
{
    public Transform parent;
    public void SetParent()
    {
        transform.SetParent(parent);
    }
}
