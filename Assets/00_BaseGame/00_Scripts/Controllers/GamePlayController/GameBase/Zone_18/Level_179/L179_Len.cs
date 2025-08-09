using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L179_Len : MonoBehaviour
{
    public float correctAngle;
    public L179_Blur blur;

    public void Init()
    {
        // Khởi tạo blur effect ban đầu
        if (blur != null)
        {
            UpdateBlurEffect();
            blur.Init();
        }
    }

    // Method này có thể được gọi từ bên ngoài để cập nhật blur
    public void UpdateBlurEffect()
    {
        if (blur != null)
        {
            float currentZRotation = transform.eulerAngles.z;
            blur.UpdateBlur(currentZRotation);
        }
    }
}
