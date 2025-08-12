using UnityEngine;
using DG.Tweening;

namespace _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase.Zone_19.Level_185
{
    public class L185_Item : MonoBehaviour
    {
        public BoxCollider2D objCollider;
        public SpriteRenderer objRenderer;
        public Sprite spriteWhenCompleted;
        public Vector2 positionWhenCompleted;
        [Space(5)]
        public SpriteRenderer effect;
        
        private float collisionTimer = 0f;
        private bool isTimerActive = true; // Để kiểm soát việc đếm thời gian
        private bool isCompleted = false; // Biến kiểm tra đã lau xong chưa

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!isTimerActive || isCompleted) return;
            collisionTimer += Time.deltaTime;
                
            if (collisionTimer >= 1f && effect != null)
            {
                FadeEffect();
            }
        }
        
        private void FadeEffect()
        {
            if (effect != null)
            {
                Color currentColor = effect.color;
                currentColor.a = Mathf.Lerp(currentColor.a, 0f, Time.deltaTime * 2f);
                effect.color = currentColor;
                
                // Kiểm tra nếu alpha gần bằng 0 thì coi như hoàn thành
                if (currentColor.a <= 0.1f)
                {
                    CompleteItem();
                }
            }
        }
        
        private void CompleteItem()
        {
            isCompleted = true;
            objCollider.enabled = false;
            // Tắt effect
            if (effect != null)
            {
                effect.gameObject.SetActive(false);
            }
            
            // Đổi sprite nếu có
            if (objRenderer != null && spriteWhenCompleted != null)
            {
                objRenderer.sprite = spriteWhenCompleted;
            }
            
            // Di chuyển đến vị trí hoàn thành trong 0.5f
            if (positionWhenCompleted != Vector2.zero)
            {
                transform.DOMove(positionWhenCompleted, 0.5f).SetEase(Ease.OutQuad);
            }
        }
        
        public void StopTimer()
        {
            isTimerActive = false;
            collisionTimer = 0f;
        }
        
        public void StartTimer()
        {
            isTimerActive = true;
        }
        
        public bool IsCompleted()
        {
            return isCompleted;
        }
    }
}
