using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L125_Charcoal : MonoBehaviour
{
    public SpriteRenderer charCoal;
    public List<Sprite> lsFrames;

    private Coroutine animationCoroutine;

    public void StartAnimation()
    {
        animationCoroutine = StartCoroutine(PlayCharcoalAnimation());
    }

    IEnumerator PlayCharcoalAnimation()
    {
        int index = 0;
        var waitTime = new WaitForSeconds(0.4f);

        while (true)
        {
            // Đổi sprite
            if (charCoal != null)
            {
                charCoal.sprite = lsFrames[index];
            }

            // Tăng index
            index = (index + 1) % lsFrames.Count;

            // Chờ 0.4 giây rồi tiếp tục
            yield return waitTime;
        }
    }

    void OnDestroy()
    {
        // Dừng coroutine khi object bị hủy
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }
    }
}