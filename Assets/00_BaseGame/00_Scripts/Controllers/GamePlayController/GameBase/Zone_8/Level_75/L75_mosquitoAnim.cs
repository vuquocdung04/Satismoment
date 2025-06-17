using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L75_mosquitoAnim : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public List<Sprite> lsSpriteFlys; // Có 3 sprite cho animation bay
    public Sprite spriteDie;

    private int currentFrame = 0;
    private bool goingForward = true;
    private Coroutine flyingAnimCoroutine;

    void Start()
    {
        flyingAnimCoroutine = StartCoroutine(AnimFly());
    }

    public void PlayDeathAnimation()
    {
        // Dừng animation bay
        if (flyingAnimCoroutine != null)
        {
            StopCoroutine(flyingAnimCoroutine);
            flyingAnimCoroutine = null;
        }

        // Hiển thị sprite chết
        spriteRenderer.sprite = spriteDie;
    }

    IEnumerator AnimFly()
    {
        float frameDuration = 0.15f;
        var waitTime = new WaitForSeconds(frameDuration);
        while (true)
        {
            spriteRenderer.sprite = lsSpriteFlys[currentFrame];

            if (goingForward)
                currentFrame++;
            else
                currentFrame--;

            if (currentFrame >= lsSpriteFlys.Count - 1)
                goingForward = false;
            else if (currentFrame <= 0)
                goingForward = true;

            yield return waitTime;
        }
    }
}