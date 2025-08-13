using DG.Tweening;
using UnityEngine;

namespace _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase.Zone_1.Level_2
{
    public class L2_CakeItem : BaseDraggableObject
    {
        public Sprite spriteDragStart;
        public Sprite spriteDragEnd;

        public void CheckCorrectToPosition(System.Action callback = null)
        {
            float distance = Vector2.Distance(transform.position, posCorrect);
            if (distance < 0.4f)
            {
                transform.DOMove(posCorrect, 0.2f).SetEase(Ease.Linear);
                objectCollider.enabled = false;
                callback?.Invoke();
                spriteRenderer.sortingOrder = 1;
            }
            else
            {
                OnEndDrag();
            }
        }

        public override void OnEndDrag()
        {
            base.OnEndDrag();
            spriteRenderer.sprite = spriteDragEnd;
        }

        public override void OnStartDrag()
        {
            base.OnStartDrag();
            spriteRenderer.sprite = spriteDragStart;
        }


        protected override void ReturnToOriginalPosition()
        {
            objectCollider.enabled = false;
            transform.DOMove(posDefault,0.2f).SetEase(Ease.Linear).OnComplete(delegate
            {
                objectCollider.enabled = true;
            });
        }
    }
}
