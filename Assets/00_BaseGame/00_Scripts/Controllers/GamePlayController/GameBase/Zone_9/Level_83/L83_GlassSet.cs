using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L83_GlassSet : MonoBehaviour
{
    public SpriteMask maskAfterWin;
    public SpriteRenderer targetSprite; // Sprite hình ảnh cái kính
    public SpriteMask mask;             // SpriteMask để che phủ vùng vẽ

    [HideInInspector] public Texture2D maskTexture;
    [HideInInspector] public Sprite maskSprite;

    public int textureWidth;
    public int textureHeight;
    public int drawRadius = 20;
    public Color drawColor = Color.white;

    [HideInInspector] public bool completed = false;
}
