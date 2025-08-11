using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase.Zone_19.Level_182
{
    public class Level_182Ctrl : BaseDragController<L182_Piece>
    {
        public L182_SetupPoint setupPoint;
        
        [Header("Game Pieces")]
        public List<L182_Piece> gamePieces;

        private void Start()
        {
            setupPoint.InitMatrix();
            SetupCorrectPoints();
        }
        
        /// <summary>
        /// Setup các correctPoint cho từng piece dựa trên PieceType enum
        /// </summary>
        private void SetupCorrectPoints()
        {
            // Dictionary map PieceType với correct point name
            var correctPointMap = new Dictionary<PieceType, string>
            {
                { PieceType.Crocodile, "MiddlePoint_H_4_1" },
                { PieceType.Pig, "MiddlePoint_V_4_4" },
                { PieceType.Elephant, "MiddlePoint_H_3_3" },
                { PieceType.Chicken, "Point_3_2" },
                { PieceType.Giraffe, "MiddlePoint_V_1_1" },
                { PieceType.Dog, "Point_1_2" },
                { PieceType.Snail, "Point_1_4" }
            };
            
            // Assign correctPoint cho mỗi piece dựa trên enum
            foreach (L182_Piece piece in gamePieces)
            {
                if (piece == null) continue;
                
                if (correctPointMap.ContainsKey(piece.pieceType))
                {
                    piece.correctPoint = FindPointByName(correctPointMap[piece.pieceType]);
                }
            }
        }
        
        /// <summary>
        /// Tìm point theo name trong các list của setupPoint
        /// </summary>
        private Transform FindPointByName(string pointName)
        {
            // Tìm trong points
            foreach (Transform point in setupPoint.points)
            {
                if (point.name == pointName)
                    return point;
            }
            
            // Tìm trong middlePointsHorizontal
            foreach (Transform point in setupPoint.middlePointsHorizontal)
            {
                if (point.name == pointName)
                    return point;
            }
            
            // Tìm trong middlePointsVertical
            foreach (Transform point in setupPoint.middlePointsVertical)
            {
                if (point.name == pointName)
                    return point;
            }
            
            return null;
        }

        protected override void OnDragStarted()
        {
            draggableComponent.OnDragged();
        }

        protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
        {
            draggableComponent.transform.position += mouseDelta;
        }

        protected override void OnDragEnded()
        {
            draggableComponent.CheckCorrectToPosition(setupPoint);
            
            if (CheckWin())
            {
                isWin = true;
                Debug.Log("Congratulations! You Win!");
                StartCoroutine(HandleWinCondition());
            }
        }
                
        /// <summary>
        /// Kiểm tra điều kiện thắng
        /// </summary>
        private bool CheckWin()
        {
            foreach (L182_Piece piece in gamePieces)
            {
                if (piece == null) continue;
                
                if (piece.currentPoint == null) return false;
                if (piece.currentPoint != piece.correctPoint) return false;
            }
            
            return true;
        }

        private IEnumerator HandleWinCondition()
        {
            yield return new WaitForSeconds(0.5f);
            WinBox.SetUp().Show();
        }

        

        protected override void TryStartDrag(Vector3 position)
        {
            var hit = Physics2D.Raycast(position, Vector2.zero);

            if (hit.collider == null) return;
            
            var component = hit.collider.GetComponentInParent<L182_Piece>();
            
            if (component == null || !CanStartDragCondition(component)) return;
            
            draggableComponent = component;
            isDragging = true;
            prevMouseWorldPos = mouseWorldPos;
            OnDragStarted();
        }
    }
}
