using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L41_Screw : MonoBehaviour
{
    public float scaleParam = 0.01f;
    public bool isScale;
    public void DoScalingScrew()
    {
        if (isScale) return;
        if (transform.localScale.x > 1.5f)
        {
            isScale = true;
        }
        transform.localScale += new Vector3(scaleParam, scaleParam, scaleParam);
        transform.eulerAngles += new Vector3(0,0,15);
    }
}
