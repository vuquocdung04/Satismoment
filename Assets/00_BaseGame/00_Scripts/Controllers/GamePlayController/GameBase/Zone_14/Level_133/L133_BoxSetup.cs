using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L133_BoxSetup : MonoBehaviour
{
    public SpriteRenderer boxRenderer;
    public Sprite spriteOpen;
    public Sprite spriteClose;
    public Level_133Ctrl levelCtrl;

    int indexItem = 0;

    public void Init()
    {
        foreach(var item in this.levelCtrl.lsT_ItemDragables)
        {
            item.transform.position = transform.position + new Vector3(-0.2f,0.5f);
            item.objectCollider.enabled = false;
            item.transform.localScale = Vector3.zero;
        }
    }

    private void OnMouseDown()
    {
        boxRenderer.sprite = spriteOpen;
        var currentItem = levelCtrl.lsT_ItemDragables[indexItem];
        currentItem.transform.DOMoveY(transform.position.y + 3f,0.3f).SetEase(Ease.InBack);
        currentItem.transform.DOScale(Vector3.one,0.3f).SetEase(Ease.Linear);
        currentItem.objectCollider.enabled = true;
        currentItem.OnStartDrag();
        indexItem++;
    }
    private void OnMouseUp()
    {
        boxRenderer.sprite = spriteClose;
        if(indexItem == levelCtrl.lsT_ItemDragables.Count)
        {
            transform.DOMoveX(-6f, 0.5f).SetEase(Ease.OutBack);
        }
    }
}
