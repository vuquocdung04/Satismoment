using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L136_Penguin : MonoBehaviour
{
    public SpriteRenderer objRenderer;
    public List<Sprite> lsFrames;

    public IEnumerator PlayAnimation()
    {
        int currentIndex = 0;
        float frameDuration = 0.2f; // Thời gian giữa các frame
        var waitTime = new WaitForSeconds(frameDuration);
        while (currentIndex < lsFrames.Count)
        {
            objRenderer.sprite = lsFrames[currentIndex];
            currentIndex++;
            yield return waitTime;
        }
    }
}