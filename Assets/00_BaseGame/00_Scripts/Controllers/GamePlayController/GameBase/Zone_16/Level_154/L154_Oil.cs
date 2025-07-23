using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L154_Oil : MonoBehaviour
{
    public SpriteRenderer objRenderer;
    public List<Sprite> lsFrames;
    private Coroutine animCoroutine;
    public IEnumerator AnimateSprites(float interval)
    {
        for (int i = 0; i < lsFrames.Count; i++)
        {
            objRenderer.sprite = lsFrames[i];
            yield return new WaitForSeconds(interval);
        }
        animCoroutine = null; // Reset để biết đã xong
    }
    public void StartAnimation()
    {
        if (animCoroutine == null && lsFrames != null && lsFrames.Count > 0)
            animCoroutine = StartCoroutine(AnimateSprites(0.5f));
    }
}
