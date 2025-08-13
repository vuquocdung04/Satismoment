using DG.Tweening;
using UnityEngine;

namespace _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase.Zone_19.Level_184
{
    public class L184_Fruit : BaseDraggableObject
    {
        public void CheckCollisionToJar(Transform jarTrans, float boundsSizeJar, System.Action callback = null)
        {
            var distance = Vector3.Distance(jarTrans.transform.position, transform.position);
            var boundsSize = spriteRenderer.sprite.bounds.size;
            if (distance < boundsSizeJar - boundsSize.x/2)
            {
                transform.DOMoveY(-3.2f,0.3f).SetEase(Ease.Linear).OnComplete(delegate
                {
                    objectCollider.enabled = false;
                    callback?.Invoke();
                });
            }
            else
            {
                OnEndDrag();
            }
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