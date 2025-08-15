using UnityEngine;

namespace _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase.Zone_2.Level_14
{
    public class Level_14Ctrl : BaseDragController<L14_Nozzle>
    {
        public int winProgress;
        public AudioClip soundWater;
        protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
        {
            draggableComponent.transform.position += mouseDelta;
            draggableComponent.SpawnWater();
        }

        protected override void OnDragEnded()
        {
            draggableComponent.transform.position = new Vector3(-1,-3);
            GameController.Instance.musicManager.PauseSound();
            if(winProgress > 3)
            {
                StartCoroutine(HandleWinCondition());
            }
        }

        protected override void OnDragStarted()
        {
            GameController.Instance.musicManager.PlaySingle(soundWater,true);
        }

        System.Collections.IEnumerator HandleWinCondition()
        {
            yield return new WaitForSeconds(0.5f);
            WinBox.SetUp().Show();
        }

    }
}
