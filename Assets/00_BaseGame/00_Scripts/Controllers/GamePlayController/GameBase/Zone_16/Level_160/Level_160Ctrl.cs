using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_160Ctrl : MonoBehaviour
{
    public Transform hand;
    public Transform effectBroken;
    public L160_Balloon ballonPrefabs;
    public int countSpawner = 15;
    public int brokenBalloonAmount;
    bool isReady = true;
    Vector3 mousePosition;
    [SerializeField] L160_Balloon curBalloon;
    bool isWin = false;
    private void Start()
    {
        StartCoroutine(InitBalloon());
    }

    IEnumerator InitBalloon()
    {
        int i = 0;
        var waitTime = new WaitForSeconds(0.3f);
        while (i < countSpawner)
        {
            float randX = Random.Range(-2f, 2f);
            float randY = Random.Range(-8f, -6.3f);
            var balloonClone = Instantiate(ballonPrefabs, new Vector2(randX, randY), Quaternion.identity);
            balloonClone.defaultPosition = new Vector3(randX, randY, 0);
            balloonClone.InitState();
            i++;
            yield return waitTime;
        }
    }


    private void Update()
    {
        if (isWin) return;
        if (!isReady) return;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            if(hit.collider == null)
            {
                curBalloon = null;
                Debug.LogError("Cur == null");
            }
            else
            {
                Debug.LogError("Cur # null");
                curBalloon = hit.collider.GetComponent<L160_Balloon>();
            }
            StartCoroutine(MoveHand(mousePosition));
        }
    }

    IEnumerator MoveHand(Vector3 target)
    {
        isReady = false;
        Tween handMove = hand.DOMove(target,0.4f).SetEase(Ease.Linear);
        yield return handMove.WaitForCompletion();
        if(curBalloon != null)
        {
            float distance = Vector3.Distance(hand.transform.position, curBalloon.transform.position);

            Debug.LogError(distance);
            if(Mathf.Abs(distance) < 0.9f)
            {
                StartCoroutine(HandleEffect());
            }
        }
        hand.DOMove(new Vector3(4f,target.y,0), 0.4f).SetEase(Ease.Linear);
        isReady = true;
    }

    IEnumerator HandleEffect()
    {
        brokenBalloonAmount++;
        var effectClone = SimplePool2.Spawn(effectBroken, curBalloon.transform.position, Quaternion.identity);
        curBalloon.StopMovement();
        curBalloon = null;
        yield return new WaitForSeconds(0.4f);
        SimplePool2.Despawn(effectClone.gameObject);
        if(brokenBalloonAmount == countSpawner)
        {
            StartCoroutine(HandleWinCondition());
        }
    }

    IEnumerator HandleWinCondition()
    {
        isWin = true;
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();
    }
}
