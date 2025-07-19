using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_148Ctrl : MonoBehaviour
{
    public Transform knifeBlade;
    public List<L148_Carrot> lsCarrots;
    public Sprite carrotStart;
    public Sprite carrotEnd;
    public Sprite carrotNormal;
    int cutCarrotCount;
    public bool isWin = false;
    void Update()
    {
        if (isWin) return;
        if (Input.GetMouseButtonDown(0))
        {

            knifeBlade.DOMoveY(-0.82f, 0.2f).SetEase(Ease.OutBack).OnComplete((() =>
            {
                lsCarrots[cutCarrotCount].InitEffect();
                cutCarrotCount++;

                if (cutCarrotCount == lsCarrots.Count - 1 && lsCarrots.Count >= 2)
                {
                    // Bắt đầu Coroutine để chờ 0.2s rồi rơi củ cuối
                    isWin = true;
                    StartCoroutine(DropLastCarrotAfterDelay(0.2f));
                    StartCoroutine(HanleWinCondition());
                }

                knifeBlade.DOMoveY(2.63f, 0.2f).SetEase(Ease.OutBack);
            }));

        }
    }
    IEnumerator DropLastCarrotAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // Gọi InitEffect cho củ cà rốt cuối cùng
        lsCarrots[lsCarrots.Count - 1].InitEffect();
    }
    IEnumerator HanleWinCondition()
    {
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();
    }
    
    [Button("Setup",ButtonSizes.Large)]
    void Setup()
    {
        foreach(var carrot in this.lsCarrots)
        {
            carrot.objRenderer = carrot.transform.GetComponent<SpriteRenderer>();
        }
        for(int i = 0; i < lsCarrots.Count; i++)
        {
            if (i == 0) lsCarrots[i].fall = carrotStart;
            else if (i == lsCarrots.Count - 1) lsCarrots[i].fall = carrotEnd;
            else
            lsCarrots[i].fall = carrotNormal;
        }
    }
}
