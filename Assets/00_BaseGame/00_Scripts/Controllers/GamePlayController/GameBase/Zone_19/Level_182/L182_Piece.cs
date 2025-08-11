using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase.Zone_19.Level_182
{
    public enum PiecePointType
    {
        MainPoint,
        MiddlePointHorizontal,
        MiddlePointVertical
    }

    public enum PieceType
    {
        Crocodile,
        Pig,
        Elephant,
        Chicken,
        Giraffe,
        Dog,
        Snail
    }

    public class L182_Piece : MonoBehaviour
    {
        [Header("Piece Settings")]
        public PiecePointType piecePointType = PiecePointType.MainPoint; // Đổi tên để tránh conflict
        public PieceType pieceType = PieceType.Crocodile; // Enum mới cho loại động vật
        
        [Header("Collision Detection")]
        public List<BoxCollider2D> pieceColliders;

        [Header("Position Tracking")]
        public Transform currentPoint;
        public Transform correctPoint;

        public SpriteRenderer objRenderer;
        
        public void CheckCorrectToPosition(L182_SetupPoint setupPoint)
        {
            objRenderer.sortingOrder = 2;
            
            if (HasCollision())
            {
                StartCoroutine(ChangeColor());
                return;
            }
            
            Transform closestPoint = null;
            float closestDistance = float.MaxValue;
            
            var targetPoints = GetTargetPoints(setupPoint);
            
            foreach (Transform point in targetPoints)
            {
                float distance = Vector2.Distance(transform.position, point.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPoint = point;
                }
            }
            
            if (closestPoint != null && closestDistance < 0.4f)
            {
                currentPoint = closestPoint;
                transform.DOMove(closestPoint.position, 0.2f);
            }
            else
            {
                currentPoint = null;
            }
        }
        
        public void OnDragged()
        {
            currentPoint = null;
            objRenderer.sortingOrder = 3;
        }
        
        private bool HasCollision()
        {
            foreach (BoxCollider2D collider in pieceColliders)
            {
                if (collider == null) continue;
                
                Collider2D[] hitColliders = Physics2D.OverlapBoxAll(
                    collider.bounds.center,
                    collider.bounds.size,
                    0f
                );
                
                foreach (Collider2D hitCollider in hitColliders)
                {
                    if (pieceColliders.Contains(hitCollider as BoxCollider2D)) continue;
                    
                    if (hitCollider.transform.IsChildOf(this.transform) || 
                        hitCollider.transform == this.transform) continue;
                    
                    return true;
                }
            }
            
            return false;
        }
        
        private System.Collections.Generic.List<Transform> GetTargetPoints(L182_SetupPoint setupPoint)
        {
            switch (piecePointType) // Sử dụng piecePointType thay vì pieceType cũ
            {
                case PiecePointType.MainPoint:
                    return setupPoint.points;
                case PiecePointType.MiddlePointHorizontal:
                    return setupPoint.middlePointsHorizontal;
                case PiecePointType.MiddlePointVertical:
                    return setupPoint.middlePointsVertical;
                default:
                    return setupPoint.points;
            }
        }
        
        private IEnumerator ChangeColor()
        {
            objRenderer.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            objRenderer.color = Color.white;
        }
    }
}
