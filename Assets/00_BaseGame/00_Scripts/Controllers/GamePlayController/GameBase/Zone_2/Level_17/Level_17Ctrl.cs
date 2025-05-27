using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_17Ctrl : BaseDragController<L17_Candy>
{
    public int winProgress = 0;
    public float animationDuration = 0.4f;
    public int efferversceneAmount = 20;
    public int turnSpam = 10;
    public L17_Coca coCa;
    [Header("Bot Khi")]
    public L17_Effervescence effervescene;
    public Transform posSpawn;
    public List<Color> lsColors;
    private L17_Candy candy;


    protected override void OnDragStarted()
    {
        candy = draggableComponent;
        draggableComponent.spriteRenderer.sortingOrder = 3;
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragEnded()
    {

        bool successfullyDropped = false;


        if (candy.candyCollider.IsTouching(coCa.cocaCollider))
        {
            candy.transform.DOMove(Vector3.zero, animationDuration)
            .SetEase(Ease.InQuad);
            Debug.LogError("Drop coplete");
            successfullyDropped = true;
            candy.spriteRenderer.sortingOrder = 1;
            HandleWin();
        }
        if (!successfullyDropped)
        {
            ReturnItemToDefault(candy);
        }
        
    }

    private void ReturnItemToDefault(L17_Candy candyItem)
    {
        candyItem.transform.DOMove(candy.posDefault, animationDuration).SetEase(Ease.InQuad);
    }

    private void HandleWin()
    {
        winProgress++;
        if(winProgress > 2)
        {
            StartCoroutine(SpawnEffervescenceInBursts());
            DOVirtual.DelayedCall(1.5f, () => WinBox.SetUp().Show());
        }
    }

    IEnumerator SpawnEffervescenceInBursts()
    {
        var time = new WaitForSeconds(0.1f);
        int burstsCompleted = 0;
        int rand = 0;
        while(burstsCompleted < turnSpam)
        {
            for(int j = 0; j < efferversceneAmount; j++)
            {
                rand = Random.Range(0, lsColors.Count);
                switch (rand)
                {
                    case 0:
                        effervescene.spriteRenderer.color = lsColors[0];
                        break;
                    case 1:
                        effervescene.spriteRenderer.color = lsColors[1];
                        break;
                    case 2:
                        effervescene.spriteRenderer.color = lsColors[2];
                        break;
                    default:
                        effervescene.spriteRenderer.color = lsColors[3];
                        break;
                }
                SimplePool2.Spawn(effervescene.gameObject, posSpawn.transform.position, Quaternion.identity);
            }
            burstsCompleted++;
            yield return time;
        }
    }
}
