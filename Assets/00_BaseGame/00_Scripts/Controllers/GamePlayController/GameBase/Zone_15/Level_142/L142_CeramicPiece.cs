using DG.Tweening;
using UnityEngine;

public class L142_CeramicPiece : BaseDraggableObject
{
    public int id;
    public void HandleCorrectPosition(Level_142Ctrl levelCtrl, System.Action callback = null)
    {
        posCorrect = levelCtrl.jar.GetPositionPointById(id);

        Vector2[] positions = {
            posCorrect,
            posCorrect + new Vector2(levelCtrl.jar.maxSizeX / 3, 0),
            posCorrect - new Vector2(levelCtrl.jar.maxSizeX / 3, 0)
        };

        foreach (Vector2 target in positions)
        {
            if (Vector2.Distance(transform.position, target) < 0.3f)
            {
                objectCollider.enabled = false;
                levelCtrl.winProgress++;
                transform.DOMove(target, 0.2f).SetEase(Ease.InElastic).OnComplete(() =>
                {
                    var pieceClone1 = Instantiate(this, positions[0], Quaternion.identity);
                    var pieceClone2 = Instantiate(this, positions[1], Quaternion.identity);
                    var pieceClone3 = Instantiate(this, positions[2], Quaternion.identity);

                    pieceClone1.transform.SetParent(levelCtrl.jar.ceramicPattern);
                    pieceClone2.transform.SetParent(levelCtrl.jar.ceramicPattern);
                    pieceClone3.transform.SetParent(levelCtrl.jar.ceramicPattern);

                    pieceClone1.spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                    pieceClone2.spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                    pieceClone3.spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;

                    pieceClone1.spriteRenderer.sortingOrder = orderIndex - 1;
                    pieceClone2.spriteRenderer.sortingOrder = orderIndex - 1;
                    pieceClone3.spriteRenderer.sortingOrder = orderIndex - 1;


                    gameObject.SetActive(false);
                });
                if (levelCtrl.winProgress == levelCtrl.lsT_ItemDragables.Count) callback?.Invoke();
                return;
            }
        }

        OnEndDrag();
    }

    protected override void ReturnToOriginalPosition()
    {
        objectCollider.enabled = false;
        transform.DOMove(posDefault, 0.2f).SetEase(Ease.OutBack).OnComplete(() =>
            objectCollider.enabled = true);
    }
}