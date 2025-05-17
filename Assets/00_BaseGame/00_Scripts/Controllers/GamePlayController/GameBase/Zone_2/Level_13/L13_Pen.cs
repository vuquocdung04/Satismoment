using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L13_Pen : LoadAutoComponents
{
    public int idPen;
    public float angle;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        angle = this.transform.eulerAngles.z;
    }
}