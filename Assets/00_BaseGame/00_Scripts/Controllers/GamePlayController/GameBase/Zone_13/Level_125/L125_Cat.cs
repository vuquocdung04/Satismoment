using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L125_Cat : MonoBehaviour
{
    public BoxCollider2D catCollider;
    public SpriteRenderer catRenrerer;
    public Sprite spriteCatMountOpen;
    public Sprite spriteCatDefault;
    public List<Sprite> lsSprites;

    private Coroutine chewCoroutine;
    private bool isAnimating = false;
    private bool isPauseChewing = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        catRenrerer.sprite = spriteCatMountOpen;
        isPauseChewing = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isAnimating)
        {
            isPauseChewing = false;
        }
        else
        {
            catRenrerer.sprite = spriteCatDefault;
        }

    }

    public void ResetChewingAnimation()
    {
        isPauseChewing = false;
        if (chewCoroutine != null)
        {
            StopCoroutine(chewCoroutine);
        }

        chewCoroutine = StartCoroutine(AnimateCatChewing());
    }

    public IEnumerator AnimateCatChewing()
    {
        isAnimating = true;
        var waitTime = new WaitForSeconds(0.2f);
        for (int i = 0; i < lsSprites.Count; i++)
        {
            catRenrerer.sprite = lsSprites[i];
            while (isPauseChewing)
            {
                yield return null;
            }

            yield return waitTime;
        }

        isAnimating = false;
        catRenrerer.sprite = spriteCatDefault;
    }
}