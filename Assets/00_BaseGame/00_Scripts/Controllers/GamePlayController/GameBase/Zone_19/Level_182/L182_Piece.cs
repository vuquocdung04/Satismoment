using DG.Tweening;
using UnityEngine;

namespace _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase.Zone_19.Level_182
{
    public enum PieceType
    {
        MainPoint,
        MiddlePoint
    }

    public class L182_Piece : MonoBehaviour
    {
        [Header("Piece Settings")]
        public PieceType pieceType = PieceType.MainPoint;
        
        public void CheckCorrectToPosition(L182_SetupPoint setupPoint)
        {
            Transform closestPoint = null;
            var closestDistance = float.MaxValue;
            
            // Chọn list phù hợp dựa trên loại piece
            var targetPoints = pieceType == PieceType.MainPoint ? 
                setupPoint.points : setupPoint.middlePoints;
            
            // Tìm điểm gần nhất trong list tương ứng
            foreach (var point in targetPoints)
            {
                var distance = Vector2.Distance(transform.position, point.position);
                if (!(distance < closestDistance)) continue;
                closestDistance = distance;
                closestPoint = point;
            }
            
            // Snap nếu khoảng cách đủ gần
            if (closestPoint is not null && closestDistance < 0.4f)
            {
                transform.DOMove(closestPoint.position, 0.2f);
            }
        }
    }
}