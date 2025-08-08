using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L176_Apple : MonoBehaviour
{
    public int id;
    public SpriteRenderer objRenderer;
    public BoxCollider2D objCollider;
    public Vector2 position;

    public void InitState(Sprite sprite)
    {
        objRenderer.sprite = sprite;
        ResetColliderToSpriteBounds();


    }
    void ResetColliderToSpriteBounds()
    {
        if (objRenderer.sprite != null && objCollider != null)
        {
            // Lấy bounds của sprite
            Bounds spriteBounds = objRenderer.sprite.bounds;

            // Set size và offset của BoxCollider2D
            objCollider.size = spriteBounds.size;
            objCollider.offset = spriteBounds.center;
        }
    }

}
