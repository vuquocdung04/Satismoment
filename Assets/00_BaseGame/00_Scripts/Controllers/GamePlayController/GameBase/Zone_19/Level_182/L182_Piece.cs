using DG.Tweening;
using UnityEngine;

namespace _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase.Zone_19.Level_182
{
    public enum PieceType
    {
        MainPoint,
        MiddlePointHorizontal,
        MiddlePointVertical
    }

    public class L182_Piece : MonoBehaviour
    {
        [Header("Piece Settings")]
        public PieceType pieceType = PieceType.MainPoint;
        
        public void CheckCorrectToPosition(L182_SetupPoint setupPoint)
        {
            Transform closestPoint = null;
            float closestDistance = float.MaxValue;
            
            // Chọn list phù hợp dựa trên loại piece
            var targetPoints = GetTargetPoints(setupPoint);
            
            // Tìm điểm gần nhất trong list tương ứng
            foreach (Transform point in targetPoints)
            {
                float distance = Vector2.Distance(transform.position, point.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPoint = point;
                }
            }
            
            // Snap nếu khoảng cách đủ gần
            if (closestPoint != null && closestDistance < 0.4f)
            {
                transform.DOMove(closestPoint.position, 0.2f);
            }
        }
        
        private System.Collections.Generic.List<Transform> GetTargetPoints(L182_SetupPoint setupPoint)
        {
            switch (pieceType)
            {
                case PieceType.MainPoint:
                    return setupPoint.points;
                case PieceType.MiddlePointHorizontal:
                    return setupPoint.middlePointsHorizontal;
                case PieceType.MiddlePointVertical:
                    return setupPoint.middlePointsVertical;
                default:
                    return setupPoint.points;
            }
        }
    }
}