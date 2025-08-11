
using System.Collections.Generic;
using UnityEngine;

namespace _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase.Zone_19.Level_182
{
    public class L182_SetupPoint : MonoBehaviour
    {
        public Transform pointPrefab;
        public Transform middlePointPrefab;
        public Vector2 startPosition;
        public Vector2 spacing;
        public List<Transform> points;
        public List<Transform> middlePoints;
        
        public void InitMatrix()
        {
            // Tạo ma trận chính 6x6 (thay đổi từ 4x4)
            for (var row = 0; row < 6; row++)
            {
                for (var col = 0; col < 6; col++)
                {
                    var position = new Vector3(
                        startPosition.x + col * spacing.x,
                        startPosition.y + row * spacing.y,
                        0f
                    );
                    
                    var point = Instantiate(pointPrefab, position, Quaternion.identity);
                    point.parent = this.transform;
                    points.Add(point);
                }
            }
            
            // Tạo các điểm ở giữa cho ma trận 6x6
            CreateMiddlePoints();
        }
        
        private void CreateMiddlePoints()
        {
            // Tạo điểm ở giữa theo chiều ngang (giữa các cột)
            for (var row = 0; row < 6; row++) // 6 hàng
            {
                for (var col = 0; col < 5; col++) // 5 điểm giữa cho 6 cột
                {
                    var position = new Vector3(
                        startPosition.x + col * spacing.x + spacing.x * 0.5f,
                        startPosition.y + row * spacing.y,
                        0f
                    );
                    
                    var middlePoint = Instantiate(middlePointPrefab, position, Quaternion.identity);
                    middlePoint.parent = this.transform;
                    middlePoints.Add(middlePoint);
                }
            }
            
            // Tạo điểm ở giữa theo chiều dọc (giữa các hàng)
            for (var row = 0; row < 5; row++) // 5 hàng giữa cho 6 hàng
            {
                for (var col = 0; col < 6; col++) // 6 cột
                {
                    var position = new Vector3(
                        startPosition.x + col * spacing.x,
                        startPosition.y + row * spacing.y + spacing.y * 0.5f,
                        0f
                    );
                    
                    var middlePoint = Instantiate(middlePointPrefab, position, Quaternion.identity);
                    middlePoint.parent = this.transform;
                    middlePoints.Add(middlePoint);
                }
            }
        }
    }
}
