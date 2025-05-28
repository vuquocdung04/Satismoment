using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Level_45Ctrl : MonoBehaviour
{
    public bool isWin = false;
    public bool isPlay = false;
    public int amountSwap = 5;
    public int amountCupWithLevel = 3;

    L45_Cup curCup;
    public List<L45_Cup> lsCups;

    void Start()
    {
        SwapCup();
    }
    Vector3 mousePos;
    private void Update()
    {
        if (isWin) return;
        if (isPlay) return;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider == null) return;
            curCup = hit.collider.GetComponent<L45_Cup>();
            if (curCup == null) return;
            curCup.DoOpeningCup();
            if(curCup == lsCups[0])
            {
                if (amountCupWithLevel < 5)
                    StartCoroutine(Wait());
                else
                {
                    isWin = true;
                    isPlay = false;
                    WinBox.SetUp().Show();
                }
            }
            else
            {
                StartCoroutine(HandleFailCup());

            }
        }
    }

    IEnumerator HandleFailCup()
    {
        isPlay = true;
        yield return new WaitForSeconds(1.1f);
        SwapCup();

    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.1f);
        amountCupWithLevel++;
        switch (amountCupWithLevel)
        {
            case 4:
                lsCups[3].gameObject.SetActive(true);
                lsCups[4].gameObject.SetActive(true);
                break;
            case 5:
                lsCups[5].gameObject.SetActive(true);
                lsCups[6].gameObject.SetActive(true);
                break;
        }
        amountSwap += 4;
        SwapCup();
    }


    void SwapCup()
    {
        StartCoroutine(SwapRoutine());
    }


    IEnumerator SwapRoutine()
    {
        isPlay = true;
        lsCups[0].DoOpeningCup();
        yield return new WaitForSeconds(1.1f);

        float duration = 0.5f;
        for (int i = 0; i < amountSwap; i++)
        {
            int a = Random.Range(0, amountCupWithLevel);
            int b;
            do
            {
                b = Random.Range(0, amountCupWithLevel);
            } while (b == a);

            var cupA = lsCups[a];
            var cupB = lsCups[b];

            Vector2 posA = cupA.transform.position;
            Vector2 posB = cupB.transform.position;

            // Animate vị trí
            Tween tweenA = cupA.transform.DOMove(posB, duration);
            Tween tweenB = cupB.transform.DOMove(posA, duration);

            // Đợi 2 tween hoàn tất
            yield return tweenA.WaitForCompletion();
            yield return tweenB.WaitForCompletion();

            // Cập nhật lại vị trí logic sau khi swap
            Vector2 temp = cupA.pos;
            cupA.pos = cupB.pos;
            cupB.pos = temp;
        }
        isPlay = false;
    }

    [Button("Setup", ButtonSizes.Large)]
    private void Setup()
    {
        foreach (var cup in this.lsCups)
        {
            cup.pos = cup.transform.position;
        }
    }
}
