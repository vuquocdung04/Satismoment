
using UnityEngine;

namespace _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase.Zone_19.Level_182
{
    public class Level_182Ctrl : BaseDragController<L182_Piece>
    {
        public L182_SetupPoint setupPoint;

        private void Start()
        {
            setupPoint.InitMatrix();
        }

        protected override void OnDragStarted()
        {
            
        }

        protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
        {
            draggableComponent.transform.position += mouseDelta;
        }

        protected override void OnDragEnded()
        {
            // Truyền setupPoint để piece có thể access đến cả 2 list
            draggableComponent.CheckCorrectToPosition(setupPoint);
        }
    }
}