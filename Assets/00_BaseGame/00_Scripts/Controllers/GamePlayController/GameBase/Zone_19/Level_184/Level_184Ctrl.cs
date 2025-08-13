
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase.Zone_19.Level_184
{
    public class Level_184Ctrl : BaseDragController<L184_Fruit>
    {
        public Transform jar;
        private float boundSizeJar;
        [SerializeField] private int currentFruitCount;
        public List<L184_Fruit> fruits;
        private void Start()
        {
            boundSizeJar = jar.GetComponent<SpriteRenderer>().sprite.bounds.size.x/2;
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
            draggableComponent.CheckCollisionToJar(jar,boundSizeJar, delegate
            {
                currentFruitCount++;
                if (currentFruitCount == fruits.Count)
                {
                    isWin = true;
                    StartCoroutine(HandleWinCondition());
                }
            });
        }

        // ReSharper disable Unity.PerformanceAnalysis
        IEnumerator HandleWinCondition()
        {
            yield return new WaitForSeconds(1f);
            WinBox.SetUp().Show();
        }

        [Button("Setup Fruit", ButtonSizes.Medium)]
        private void SetupFruit()
        {
            foreach (var fruit in fruits)
            {
                fruit.InitCorrect();
                fruit.InitDefault();
            }
        }
    }
}