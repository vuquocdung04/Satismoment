using DG.Tweening;
using UnityEngine;

namespace _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase.Zone_1.Level_5
{
    public class L5_Cup : BaseDraggableObject
    {
        public void CheckCorrectToDish(System.Action callback = null)
        {
            var distance = Vector2.Distance(transform.position, posCorrect);
            if (distance < 0.3f)
            {
                transform.DOMove(posCorrect, 0.1f).SetEase(Ease.Linear).OnComplete(delegate
                {
                    callback?.Invoke();
                });
                objectCollider.enabled = false;
            }
            else
            {
                OnEndDrag();
            }
        }

        protected override void ReturnToOriginalPosition()
        {
            
        }
    }
}
