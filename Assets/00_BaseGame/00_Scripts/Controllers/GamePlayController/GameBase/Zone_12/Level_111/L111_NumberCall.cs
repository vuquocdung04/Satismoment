using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L111_NumberCall : MonoBehaviour
{
    public int idNumber;
    public SpriteRenderer numberRenderer;
    public Color color = Color.green;
    public Color defaultColor;

    public void Init()
    {
        defaultColor = numberRenderer.color;
    }

    public void ChangeColor()
    {
        numberRenderer.color = color;
    }
    public void ResetColor()
    {
        numberRenderer.color = defaultColor;
    }


}
