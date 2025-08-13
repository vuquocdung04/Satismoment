
using DG.Tweening;
using UnityEngine;
using System.Collections;

namespace _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase.Zone_1.Level_1
{
    public class Level_1Ctrl : BaseDragController<L1_Btn>
    {
        public Transform lightBulb;
        public Transform mask;
        public AudioClip btnClickSound;

        private void Start()
        {
            mask.gameObject.SetActive(false);
        }

        protected override void OnDragStarted()
        {
            draggableComponent.ChangeSpriteOn(delegate
            {
                
                isWin = true;
                GameController.Instance.musicManager.PlaySingle(btnClickSound);
                StartCoroutine(HandleWinCondition());
            });
        }

        protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
        {
            
        }

        protected override void OnDragEnded()
        {
            
        }

        // ReSharper disable Unity.PerformanceAnalysis
        IEnumerator HandleWinCondition()
        {
            var lightMove = lightBulb.DOMoveY(3.8f,0.2f).SetEase(Ease.Linear);
            yield return lightMove.WaitForCompletion();
            mask.gameObject.SetActive(true);
            var maskScale = mask.DOScale(Vector3.one * 20, 0.5f);
            yield return maskScale.WaitForCompletion();
            yield return new WaitForSeconds(0.5f);
            WinBox.SetUp().Show();
        }
    }
}
