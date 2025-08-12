
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase.Zone_19.Level_185
{
    public class Level_185Ctrl : BaseDragController<L185_Sponge>
    {
        public int currentItemCleaned;
        public Transform foamPumpBottle;
        public List<Transform> lsEffects;
        public List<L185_Item> lsItems; // List items
        
        private int currentItemIndex = 0; // Chỉ số item hiện tại đang được xử lý

        private void Start()
        {
            
            foreach (var item in this.lsItems)
            {
                item.transform.position = new Vector2(0.2f,-2f);
                item.gameObject.SetActive(false);
            }
            StartFoamAnimation();
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
            // Dừng timer của item hiện tại
            if (lsItems != null && currentItemIndex < lsItems.Count && lsItems[currentItemIndex] != null)
            {
                lsItems[currentItemIndex].StopTimer();
                
                // Kiểm tra nếu item hiện tại đã hoàn thành
                if (lsItems[currentItemIndex].IsCompleted())
                {
                    MoveToNextItem();
                    currentItemCleaned++;
                }
            }

            if (currentItemCleaned >= lsEffects.Count)
            {
                
            }
        }
        
        private void MoveToNextItem()
        {
            currentItemIndex++;
            
            // Kiểm tra nếu còn item tiếp theo thì cho bay lên
            if (currentItemIndex < lsItems.Count && lsItems[currentItemIndex] != null)
            {
                lsItems[currentItemIndex].transform.DOMoveY(lsItems[currentItemIndex].transform.position.y + 3f, 1f);
            }
            else
            {
                // Đã hoàn thành tất cả items
                OnAllItemsCompleted();
            }
        }
        
        private void OnAllItemsCompleted()
        {
            // Logic khi hoàn thành tất cả items
            Debug.Log("All items completed!");
            isWin = true;
            StartCoroutine(HandleWinCondition());
        }

        private IEnumerator HandleWinCondition()
        {
            yield return new WaitForSeconds(1f);
            WinBox.SetUp().Show();
        }
        
        

        private void StartFoamAnimation()
        {
            StartCoroutine(FoamAnimationCoroutine());
        }
        
        private IEnumerator FoamAnimationCoroutine()
        {
            // Di chuyển đến vị trí đầu tiên
            foamPumpBottle.DOMove(new Vector2(2.5f, -2f), 0.2f).SetEase(Ease.Linear);
            yield return new WaitForSeconds(0.2f);
            
            // Xoay chai
            foamPumpBottle.DORotate(new Vector3(0, 0, 54f), 0.2f);
            yield return new WaitForSeconds(0.2f);
            
            // Bắt đầu di chuyển theo trục X và kích hoạt effects
            foamPumpBottle.DOMoveX(-1.4f, 1f).SetEase(Ease.Linear).OnComplete(delegate
            {
                // Khi chai di chuyển xong, cho item đầu tiên bay lên
                MoveFirstItemUp();
            });
            
            // Kích hoạt effects mỗi 1/3 giây (0.333f)
            StartCoroutine(ActivateEffectsCoroutine());
        }
        
        private IEnumerator ActivateEffectsCoroutine()
        {
            int effectIndex = 0;
            float interval = 1f / 3f; // 1/3 giây
            
            while (effectIndex < lsEffects.Count)
            {
                if (lsEffects[effectIndex] != null)
                {
                    lsEffects[effectIndex].gameObject.SetActive(true);
                }
                effectIndex++;
                
                yield return new WaitForSeconds(interval);
            }
        }
        
        private void MoveFirstItemUp()
        {
            foreach (var item in this.lsItems)
            {
                item.gameObject.SetActive(true);
            }
            
            foamPumpBottle.gameObject.SetActive(false);
            if (lsItems != null && lsItems.Count > 0 && lsItems[0] != null)
            {
                lsItems[0].transform.DOMoveY(lsItems[0].transform.position.y + 3f, 1f);
                currentItemIndex = 0; // Bắt đầu từ item đầu tiên
            }
        }

        [Button("Setup Item", ButtonSizes.Large)]
        private void SetupItem()
        {
            foreach (var item in lsItems)
            {
                item.objRenderer = item.transform.GetComponent<SpriteRenderer>();
                item.effect = item.transform.Find("effect").GetComponent<SpriteRenderer>();
                item.positionWhenCompleted = item.transform.position;
                item.objCollider = item.transform.GetComponent<BoxCollider2D>();
            }
        }
    }
}
