using System;
using System.Collections.Generic;
using UnityEngine;

namespace _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase.Zone_19.Level_182
{
    public class L182_SetupPoint : MonoBehaviour
    {
        public Transform pointPrefab; // Chỉ cần 1 prefab dùng chung
        public Vector2 startPosition;
        public Vector2 spacing;
        public List<Transform> points;
        public List<Transform> middlePointsHorizontal; // List điểm giữa ngang
        public List<Transform> middlePointsVertical;   // List điểm giữa dọc
        
        public void InitMatrix()
        {
            // Tạo ma trận chính 6x6
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
                    point.name = $"Point_{row}_{col}"; // Gán name cho point chính
                    points.Add(point);
                }
            }
            
            // Tạo các điểm ở giữa
            CreateMiddlePoints();
        }
        
        private void CreateMiddlePoints()
        {
            // Tạo điểm ở giữa theo chiều ngang (horizontal)
            for (var row = 0; row < 6; row++)
            {
                for (var col = 0; col < 5; col++)
                {
                    var position = new Vector3(
                        startPosition.x + col * spacing.x + spacing.x * 0.5f,
                        startPosition.y + row * spacing.y,
                        0f
                    );
                    
                    var middlePoint = Instantiate(pointPrefab, position, Quaternion.identity);
                    middlePoint.parent = this.transform;
                    middlePoint.name = $"MiddlePoint_H_{row}_{col}"; // Gán name cho middle point horizontal
                    middlePointsHorizontal.Add(middlePoint);
                }
            }
            
            // Tạo điểm ở giữa theo chiều dọc (vertical)
            for (var row = 0; row < 5; row++)
            {
                for (var col = 0; col < 6; col++)
                {
                    var position = new Vector3(
                        startPosition.x + col * spacing.x,
                        startPosition.y + row * spacing.y + spacing.y * 0.5f,
                        0f
                    );
                    
                    var middlePoint = Instantiate(pointPrefab, position, Quaternion.identity);
                    middlePoint.parent = this.transform;
                    middlePoint.name = $"MiddlePoint_V_{row}_{col}"; // Gán name cho middle point vertical
                    middlePointsVertical.Add(middlePoint);
                }
            }
        }
    }
}
