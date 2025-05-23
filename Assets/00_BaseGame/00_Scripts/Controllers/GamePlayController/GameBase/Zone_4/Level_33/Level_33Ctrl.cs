using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class Level_33Ctrl : BaseDragController<L33_Item>
{
    public int winProgress = 0;
    public Sprite spriteValiWin;
    public SpriteRenderer srVali;
    public Transform hand;
    public List<L33_Item> lsItems;

    protected override void OnDragStarted()
    {
        base.OnDragStarted();
    }
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragEnded()
    {
        float distance = Vector2.Distance(draggableComponent.transform.position, draggableComponent.targetPosition);
        if (distance < 0.2f && distance > -0.2f)
        {
            draggableComponent.transform.position = draggableComponent.targetPosition;
            draggableComponent.colli.enabled = false;
            winProgress++;
            if(winProgress >= lsItems.Count)
            {
                StartCoroutine(HandleWinCodition());
            }

        }
    }

    IEnumerator HandleWinCodition()
    {
        isWin = true;
        yield return new WaitForSeconds(0.1f);
        srVali.sprite = spriteValiWin;
        foreach (var item in this.lsItems)
        {
            Destroy(item.gameObject);
        }

        hand.transform.DOMoveY(2.85f, 1f).SetEase(Ease.InOutQuad).OnComplete(delegate
        {
            srVali.transform.SetParent(hand);
            hand.transform.DOMoveY(10f, 2f).SetEase(Ease.InOutQuad);
        });
        yield return new WaitForSeconds(2.5f);
        WinBox.SetUp().Show();
    }

    [Button("SS")]
    void EditOdin()
    {
        foreach(var item in this.lsItems)
        {
            item.colli =  item.transform.GetComponent<BoxCollider2D>();
        }
    }

}
