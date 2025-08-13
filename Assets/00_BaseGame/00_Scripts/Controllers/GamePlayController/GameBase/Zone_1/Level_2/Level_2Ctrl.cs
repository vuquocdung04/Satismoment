
using UnityEngine;

namespace _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase.Zone_1.Level_2
{
    public class Level_2Ctrl : BaseDragControllerVer2<L2_CakeItem>
    {
        public AudioClip soundCake;
        private void Start()
        {
            foreach (var cake in this.lsItems)
            {
                cake.spriteRenderer.sprite = cake.spriteDragEnd;
            }
        }

        protected override void OnDragStarted()
        {
            draggableComponent.OnStartDrag();
            GameController.Instance.musicManager.PlaySingle(soundCake);
        }

        protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
        {
            draggableComponent.transform.position += mouseDelta;
        }

        protected override void OnDragEnded()
        {
            draggableComponent.CheckCorrectToPosition(delegate
            {
                winProgress++;
                if (winProgress == lsItems.Count)
                {
                    isWin =  true;
                    StartCoroutine(HandleWinCondition());
                }
            });
        }

        protected override void SetupAfter()
        {
            foreach (var cake in lsItems)
            {
                cake.InitAfter();
            }
        }

        protected override void SetupBefore()
        {
            foreach (var cake in lsItems)
            {
                cake.InitBefore();
            }
        }
    }
}
