using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L151_Candy : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer objRenderer;
    public List<Sprite> lsSpriteCandys;

    public void InitState()
    {
        int rand = Random.Range(0,lsSpriteCandys.Count);
        objRenderer.sprite = lsSpriteCandys[rand];

        float randomX = Random.Range(-2f, 2f); // thay đổi nếu muốn mạnh hơn
        rb.velocity = new Vector2(randomX, Random.Range(-3f, -5f));

        // Có thể thêm một chút xoáy (angular velocity)
        rb.angularVelocity = Random.Range(-100f, 100f);
    }
}
