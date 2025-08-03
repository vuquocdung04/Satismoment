using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum L166_DotType
{
    Red,
    Yellow,
    Blue,
    Green,
    None,
}

public class L166_Dot : MonoBehaviour
{
    public L166_DotType dotType;
    public SpriteRenderer objRenderer;
    public Color dotColor;

    public int row; // nên gán khi tạo/lấy
    public int col; // nên gán khi tạo/lấy
}
