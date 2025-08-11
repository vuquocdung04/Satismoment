using UnityEngine;
using System;
using System.Collections;
namespace _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase.Zone_19.Level_183
{
    public class L183_CowTeat : MonoBehaviour
    {
        [Header("Drag Settings")]
        [SerializeField] private float maxScaleY = 1.5f;
        [SerializeField] private float scaleSpeed = 2f;
        [Header("Prefab")]
        public L183_DropOfMilk dropOfMilk;
        [Header("Milk Spawn")]
        [SerializeField] private float spawnInterval = 0.3f;

        public Transform positionSpawn;
        
        private Vector3 originalScale;
        private Quaternion originalRotation;
        private Vector3 initialMousePosition;
        private Coroutine spawnCoroutine;

        // Event để thông báo khi tạo giọt sữa
        public event Action OnMilkDropCreated;

        public void InitState()
        {
            originalScale = transform.localScale;
            originalRotation = transform.rotation;
        }

        public void OnDragStart(Vector3 mousePosition)
        {
            initialMousePosition = mousePosition;
            
            // Bắt đầu spawn milk
            if (spawnCoroutine != null)
            {
                StopCoroutine(spawnCoroutine);
            }
            spawnCoroutine = StartCoroutine(SpawnMilkCoroutine());
        }

        private float dragDistance;
        private float scaleY;
        public void OnDragUpdate(Vector3 currentMousePosition, Vector3 deltaMousePosition)
        {
            // Chỉ xử lý scale Y, rotation đã được xử lý trong Level_183Ctrl
            dragDistance = Mathf.Abs(Vector3.Distance(currentMousePosition, initialMousePosition));
            scaleY = Mathf.Lerp(originalScale.y, maxScaleY, dragDistance * scaleSpeed);
            scaleY = Mathf.Clamp(scaleY, originalScale.y, maxScaleY);
            
            transform.localScale = new Vector3(originalScale.x, scaleY, originalScale.z);
        }

        public void OnDragEnd()
        {
            StopSpawnMilk();
            StartCoroutine(ReturnToOriginal());
        }

        public void StopSpawnMilk()
        {
            // Dừng spawn milk
            if (spawnCoroutine != null)
            {
                StopCoroutine(spawnCoroutine);
                spawnCoroutine = null;
            }
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        private System.Collections.IEnumerator SpawnMilkCoroutine()
        {
            var interval = new WaitForSeconds(spawnInterval);
            while (true)
            {
                // Spawn giọt sữa
                if (dropOfMilk != null)
                {
                    var milkDrop = SimplePool2.Spawn(dropOfMilk,positionSpawn.position,Quaternion.identity);
                    milkDrop.transform.SetParent(transform);
                    milkDrop.InitState(transform);
                    
                    // Thông báo đã tạo 1 giọt sữa
                    OnMilkDropCreated?.Invoke();
                }
                
                // Chờ 0.3f giây
                yield return interval;
            }
        }

        private IEnumerator ReturnToOriginal()
        {
            float elapsedTime = 0f;
            float duration = 0.3f;
            
            Vector3 currentScale = transform.localScale;
            Quaternion currentRotation = transform.rotation;
            
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / duration;
                
                transform.localScale = Vector3.Lerp(currentScale, originalScale, t);
                transform.rotation = Quaternion.Lerp(currentRotation, originalRotation, t);
                
                yield return null;
            }
            
            transform.localScale = originalScale;
            transform.rotation = originalRotation;
        }
    }
}
