using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L138_Valve : MonoBehaviour
{
    public SpriteRenderer objRenderer;
    public List<Sprite> lsFrames;

    private int currentFrameIndex = 0;
    public void UpdateSpriteByRotation(float rotationValue)
    {
        // Tính chỉ số frame dựa trên rotationValue, làm tròn xuống
        int newFrameIndex = Mathf.Clamp(Mathf.FloorToInt(rotationValue), 0, lsFrames.Count - 1);

        if (newFrameIndex != currentFrameIndex)
        {
            objRenderer.sprite = lsFrames[newFrameIndex];
            currentFrameIndex = newFrameIndex;
        }
    }
}
