using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L179_Blur : MonoBehaviour
{
    public SpriteRenderer objRenderer;
    public Color originalColor;
    [Header("Blur Settings")]
    public float maxAlpha = 1f;
    public float zeroAngleTolerance = 5f; // Khoảng từ -5° đến +5° sẽ có alpha = 0

    public void Init()
    {
        if (objRenderer == null)
            objRenderer = GetComponent<SpriteRenderer>();

        originalColor = objRenderer.color;
    }

    public void UpdateBlur(float currentAngle)
    {
        // Chuẩn hóa góc về khoảng -180 đến +180
        float normalizedAngle = currentAngle;
        if (normalizedAngle > 180f)
            normalizedAngle -= 360f;

        // Nếu trong khoảng -5° đến +5° thì alpha = 0
        if (Mathf.Abs(normalizedAngle) <= zeroAngleTolerance)
        {
            Color newColor = originalColor;
            newColor.a = 0f;
            objRenderer.color = newColor;
            return;
        }

        // Tính khoảng cách từ vùng "zero zone" (-5° đến +5°)
        float distanceFromZeroZone;
        if (normalizedAngle > 0)
        {
            distanceFromZeroZone = normalizedAngle - zeroAngleTolerance;
        }
        else
        {
            distanceFromZeroZone = Mathf.Abs(normalizedAngle) - zeroAngleTolerance;
        }

        // Tính alpha dựa trên khoảng cách từ zero zone
        // Khoảng cách tối đa là (180 - 5) = 175°
        float maxDistance = 180f - zeroAngleTolerance;
        float normalizedDistance = Mathf.Clamp01(distanceFromZeroZone / maxDistance);
        float alphaValue = normalizedDistance * maxAlpha;

        Color finalColor = originalColor;
        finalColor.a = alphaValue;
        objRenderer.color = finalColor;
    }
}
