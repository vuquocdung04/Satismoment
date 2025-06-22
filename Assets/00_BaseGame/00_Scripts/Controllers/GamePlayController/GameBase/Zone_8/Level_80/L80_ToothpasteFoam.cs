using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L80_ToothpasteFoam : MonoBehaviour
{
    public List<Sprite> lsSprites; // Danh sách sprite tạo thành animation
    public float frameRate = 0.1f;  // Tốc độ chuyển frame
    public SpriteRenderer spriteRenderer;
    private int currentFrame = 0;
    void OnEnable()
    {
        currentFrame = 0;
        StartCoroutine(PlayAnimation());
    }

    IEnumerator PlayAnimation()
    {
        while (currentFrame < lsSprites.Count)
        {
            spriteRenderer.sprite = lsSprites[currentFrame];
            currentFrame++;

            yield return new WaitForSeconds(frameRate);
        }

        SimplePool2.Despawn(gameObject);
    }
}