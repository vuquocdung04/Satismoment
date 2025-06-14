using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L68_Mouse : MonoBehaviour
{
    public L68_Cat cat;
    public Transform sweat;
    public Transform heart;
    public SpriteRenderer mouseSprite;
    public List<Sprite> lsRunning;

    private Coroutine runningCoroutine;
    private int currentFrame = 0;

    // Bắt đầu animation
    public void StartRunningAnimation(float interval = 0.2f)
    {
        if (runningCoroutine == null && lsRunning.Count > 0)
        {
            sweat.gameObject.SetActive(true);
            runningCoroutine = StartCoroutine(PlayRunningAnimation(interval));
        }
    }

    // Tạm dừng animation
    public void StopRunningAnimation()
    {
        if (runningCoroutine != null)
        {
            sweat.gameObject.SetActive(false);
            StopCoroutine(runningCoroutine);
            runningCoroutine = null;
        }
    }

    private IEnumerator PlayRunningAnimation(float interval)
    {

        var waitTime = new WaitForSeconds(interval);
        while (true)
        {
            currentFrame = (currentFrame + 1) % lsRunning.Count;
            mouseSprite.sprite = lsRunning[currentFrame];

            yield return waitTime;
        }
    }

    
}