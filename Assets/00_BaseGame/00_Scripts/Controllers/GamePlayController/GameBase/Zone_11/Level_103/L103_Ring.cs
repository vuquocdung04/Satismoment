using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L103_Ring : BaseDraggableObject
{
    public bool isDone;
    public bool CheckAngleCorrect()
    {
        float currentAngle = transform.eulerAngles.z; // Góc hiện tại
        float angleDifference = Mathf.DeltaAngle(currentAngle, angleDefault);
        if (Mathf.Abs(angleDifference) < 4f && !isDone)
        {
            transform.eulerAngles = new Vector3(0, 0, angleDefault);
            isDone = true;
            return true;
        }
        return false;
    }
    public override void ReturnToOriginalPosition()
    {
        
    }
}
