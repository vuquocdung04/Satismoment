using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L161_ScrewDriver : MonoBehaviour
{
    public Level_161Ctrl levelCtrl;
    public Transform pattern;
    public float sizeX;
    [SerializeField] Vector2 originalPatternPos;
    [SerializeField] Vector2 originalPosition;
    Coroutine patternCoroutine;
    public bool canDrag = true;
    bool canCollision;
    IEnumerator PatternMove(float speedAmount)
    {
        Vector3 moveAmount = new Vector3(speedAmount * Time.deltaTime,0,0);
        while (true)
        {
            pattern.localPosition += moveAmount;
            if(pattern.localPosition.x <= -sizeX / 3)
            {
                pattern.localPosition = originalPatternPos;
            }
            else if(pattern.localPosition.x >= sizeX/3)
                pattern.localPosition = originalPatternPos;
            yield return null;
        }
    }

    void StopPatternMove()
    {
        if(patternCoroutine!= null)
        {
            StopCoroutine(patternCoroutine);
            patternCoroutine = null;
        }
    }
    public void StartPatternMove(float speed)
    {
        canDrag = true;
        canCollision = true;
        if (patternCoroutine == null) patternCoroutine = StartCoroutine(PatternMove(speed));
    }

    public void OnDragEnd()
    {
        StopPatternMove();
        transform.DOMove(originalPosition,0.3f).SetEase(Ease.Linear);
        if(curScrew != null)
        {
            curScrew.StopPatternMove();
            curScrew = null;
        }
        canCollision = false;
    }



    L161_Screws curScrew;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canCollision) return;
        curScrew = collision.GetComponentInParent<L161_Screws>();
        if (curScrew == null) return;
        transform.position = new Vector3(curScrew.transform.position.x,transform.position.y);
        transform.SetParent(curScrew.transform);
        canDrag = false;
        curScrew.StartPatternMove(0.5f, delegate
        {
            OnDragEnd();
            levelCtrl.screwsCompleted++;
            levelCtrl.CheckWin();
        });
        levelCtrl.wood.StartMove();
    }




    [Button("Setup Mutual", ButtonSizes.Large)]
    void SetupMutual()
    {
        originalPosition = transform.position;
        pattern = transform.Find("pattern");
        sizeX = pattern.GetComponent<SpriteRenderer>().bounds.size.x;
        originalPatternPos = pattern.localPosition;
    }
}
