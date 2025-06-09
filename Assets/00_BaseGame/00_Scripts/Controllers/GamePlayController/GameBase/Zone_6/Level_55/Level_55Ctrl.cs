using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_55Ctrl : BaseDragController<L55_Item>
{
    public int winProgress;
    public L55_Item itemPrefab;
    public List<L55_Btn> lsBtns;
    [Space(10)]
    public List<Sprite> lsSprites;
    public List<Vector2> lsPosSpawn;


    private void Start()
    {
        SpawnItem();
    }
    public void SpawnItem()
    {
        for(int i = 0; i < 6; i++)
        {
            var cloneItem = Instantiate(itemPrefab, lsPosSpawn[i], Quaternion.identity);
            cloneItem.spriteRenderer.sprite = lsSprites[i];
            cloneItem.idItem = i;
            cloneItem.name = "Item_" + i;
        }
    }

    L55_Btn GetPosBtnById(int id)
    {
        foreach (var btn in this.lsBtns) if (btn.idBtn == id) return btn;
        return null;
    }
    L55_Item currentItem;
    protected override void OnDragStarted()
    {
        currentItem = draggableComponent;
        StartCoroutine(HandleAnim());
    }
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        
    }

    protected override void OnDragEnded()
    {
        currentItem = null;
    }


    IEnumerator HandleAnim()
    {
        var itemClone = currentItem;
        Tween moveTween = currentItem.transform.DOMove(GetPosBtnById(draggableComponent.idItem).GetWorldPosition(), 0.5f);
        yield return moveTween.WaitForCompletion();

        var image = GetPosBtnById(itemClone.idItem).progress;
        Tween fillTween = DOVirtual.Float(
            image.fillAmount,
            1,
            0.5f,
            value => image.fillAmount = value
        );
        GetPosBtnById(itemClone.idItem).gameObject.SetActive( false );
        yield return fillTween.WaitForCompletion();
        winProgress++;
        if(winProgress == lsBtns.Count)
        {
            yield return new WaitForSeconds(0.1f);
            WinBox.SetUp().Show();
        }
    }


}
