using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L161_Screws : MonoBehaviour
{
    public Transform pattern;
    public float sizeY;
    [SerializeField] Vector2 originalPatternPos;
    public bool isDone;
    [SerializeField] Vector2 correctPosition;
    Coroutine patternCoroutine;
    IEnumerator PatternMove(float speedAmount, System.Action callback = null)
    {
        Vector3 moveAmount = new Vector3(0,speedAmount * Time.deltaTime, 0);
        Debug.LogError("Swtawt");
        float distanceY;
        while (!isDone)
        {
            transform.position += moveAmount/5;
            pattern.localPosition += moveAmount;
            if (pattern.localPosition.y <= -sizeY / 3)
            {
                pattern.localPosition = originalPatternPos;
            }
            else if (pattern.localPosition.y >= sizeY / 3)
                pattern.localPosition = originalPatternPos;

            distanceY = transform.position.y - correctPosition.y;
            if(Mathf.Abs(distanceY) <= 0.01f)
            {
                isDone = true;
                callback?.Invoke();
            }
            yield return null;
        }
    }

    public void StopPatternMove()
    {
        if (patternCoroutine != null)
        {
            StopCoroutine(patternCoroutine);
            patternCoroutine = null;
        }
    }
    public void StartPatternMove(float speed, System.Action callback = null)
    {
        if (patternCoroutine == null) patternCoroutine = StartCoroutine(PatternMove(speed,callback));
    }

    [Button("Setup Mutual", ButtonSizes.Large)]
    void SetupMutual()
    {
        pattern = transform.Find("pattern");
        sizeY = pattern.GetComponent<SpriteRenderer>().bounds.size.y;
        originalPatternPos = pattern.localPosition;
    }

    [Button("Setup Correct Position",ButtonSizes.Large)]
    void SetupCorrect()
    {
        correctPosition = transform.position;
    }
}
