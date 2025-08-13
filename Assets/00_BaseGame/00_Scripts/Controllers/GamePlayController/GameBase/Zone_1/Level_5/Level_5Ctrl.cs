using UnityEngine;
namespace _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase.Zone_1.Level_5
{
    public class Level_5Ctrl : BaseDragControllerVer2<L5_Cup>
    {
        public AudioClip soundWhenCorrectPosition;
        protected override void OnDragStarted()
        {
            draggableComponent.OnStartDrag();
        }

        protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
        {
            draggableComponent.transform.position += mouseDelta;
        }

        protected override void OnDragEnded()
        {
            draggableComponent.CheckCorrectToDish(delegate
            {
                winProgress++;
                GameController.Instance.musicManager.PlaySingle(soundWhenCorrectPosition);
                if (winProgress == lsT_ItemDragables.Count)
                {
                    StartCoroutine(HandleWinCondition());
                }
            });
        }
        
        
        protected override void SetupComponent_PositionCorrect()
        {
            foreach (var cup in this.lsT_ItemDragables)
            {
                cup.InitCorrect();
            }
        }

        protected override void SetupPositionDefault()
        {
            
        }
    }
}
