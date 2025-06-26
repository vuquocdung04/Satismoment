using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_89Ctrl : MonoBehaviour
{
    public L89_Rod rod;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rod.housePrefab.HandleFallCondition();
            rod.housePrefab.transform.SetParent(transform);
        }
    }
}
