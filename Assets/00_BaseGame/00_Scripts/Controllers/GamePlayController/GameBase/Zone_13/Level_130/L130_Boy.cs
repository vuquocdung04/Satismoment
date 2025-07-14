using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L130_Boy : MonoBehaviour
{
    public SpriteRenderer boyRenderer;

    public Sprite spriteChill;
    public List<Sprite> lsFramesCold;
    public List<Sprite> lsFramesHot;

    private Coroutine currentAnimationCoroutine;

    public void ChangeSpriteCold()
    {
        boyRenderer.sprite = lsFramesCold[0];
    }


    public void ChangeSpriteHot()
    {
        boyRenderer.sprite = lsFramesHot[0];
        Debug.LogError("Hot");
    }
    public void ChangeSpriteDefault()
    {
        boyRenderer.sprite = spriteChill;
    }

    // Thêm phương thức StopCurrentAnimation
    public void StopCurrentAnimation()
    {
        if (currentAnimationCoroutine != null)
        {
            StopCoroutine(currentAnimationCoroutine);
            currentAnimationCoroutine = null;
        }
    }
    public void PlayHotAnimation()
    {
        currentAnimationCoroutine = StartCoroutine(PlayHotAnimationCoroutine());
    }
    public void PlayColdAnimation()
    {
        currentAnimationCoroutine = StartCoroutine(PlayColdAnimationCoroutine());
    }

    // Coroutine for Hot animation
    private IEnumerator PlayHotAnimationCoroutine()
    {
        int index = 0;
        var waitTime = new WaitForSeconds(0.5f);
        while (true)
        {
            boyRenderer.sprite = lsFramesHot[index];
            index = (index + 1) % lsFramesHot.Count;
            yield return waitTime;
        }
    }

    // Coroutine for Cold animation
    private IEnumerator PlayColdAnimationCoroutine()
    {
        int index = 0;
        var waitTime = new WaitForSeconds(0.5f);
        while (true)
        {
            boyRenderer.sprite = lsFramesCold[index];
            index = (index + 1) % lsFramesCold.Count;
            yield return waitTime;
        }
    }
}