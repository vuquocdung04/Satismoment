using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase.Zone_1.Level_3
{
    public class Level_3Ctrl : BaseDragController<L3_Picture>
    {
        public AudioClip pictureCompletedSound;
        protected override void OnDragStarted()
        {
            
        }
        private float angle;
        private Vector3 objectCenter;
        private Vector2 vectorToPrevMouse;
        private Vector2 vectorToCurrentMouse;
        protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
        {
            objectCenter = draggableComponent.transform.position;

            vectorToPrevMouse = (Vector2)prevMouseWorldPos - (Vector2)objectCenter;

            vectorToCurrentMouse = (Vector2)currentMousePosition - (Vector2)objectCenter;

            angle = Vector2.SignedAngle(vectorToPrevMouse, vectorToCurrentMouse);

            draggableComponent.transform.Rotate(0, 0, angle / 2);
        }

        protected override void OnDragEnded()
        {
            var angleT = draggableComponent.transform.eulerAngles.z;
    
            // Kiểm tra góc gần 0 độ hoặc gần 360 độ (tương đương với 0 độ)
            Debug.LogError(angleT);
            if(angleT <= 5f && angleT >= -5f)
            {
                isWin = true;
                var pictureClone = draggableComponent;
                pictureClone.transform.DORotate(Vector3.zero, 0.2f).OnComplete(delegate
                {
                    GameController.Instance.musicManager.PlaySingle(pictureCompletedSound);
                    pictureClone.ChangeSprite(delegate
                    {
                        StartCoroutine(HandleWinCondition());
                    });
                });
            }
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator HandleWinCondition()
        {
            yield return new WaitForSeconds(1f);
            WinBox.SetUp().Show();
        }
    }
}
