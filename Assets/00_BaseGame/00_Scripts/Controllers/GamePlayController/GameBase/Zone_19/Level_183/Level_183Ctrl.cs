using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase.Zone_19.Level_183
{
    public class Level_183Ctrl : BaseDragController<L183_CowTeat>
    {
        [Header("UI")]
        public Transform mask;

        public L183_CowTeat cowTeat;
        [Header("Game Logic")]
        [SerializeField] private int targetMilkDrops = 20;
        [SerializeField] private float maskTargetY = -4f;
        [SerializeField] private float maskMoveDuration = 2f;
        
        private int currentMilkDropCount = 0;
        private float initialMaskY;
        
        private void Start()
        {
            // Lưu vị trí Y ban đầu của mask
            cowTeat.InitState();
            if (mask != null)
            {
                initialMaskY = mask.position.y;
            }
        }

        protected override void OnDragStarted()
        {
            draggableComponent.OnDragStart(mouseWorldPos);
                // Đăng ký sự kiện khi có giọt sữa được tạo
                draggableComponent.OnMilkDropCreated += HandleMilkDropCreated;
        }

        private float angle;
        private float currentZ;
        private Vector3 objectCenter;
        private Vector2 vectorToPrevMouse;
        private Vector2 vectorToCurrentMouse;
        private float newAngle;
        protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
        {
            if (isWin) return; // Không cho phép kéo nếu đã thắng
            
            // Áp dụng logic xoay từ code của bạn
            objectCenter = draggableComponent.transform.position;
            vectorToPrevMouse = (Vector2)prevMouseWorldPos - (Vector2)objectCenter;
            vectorToCurrentMouse = (Vector2)currentMousePosition - (Vector2)objectCenter;
            angle = Vector2.SignedAngle(vectorToPrevMouse, vectorToCurrentMouse);

            // Tính góc hiện tại và góc mới
            currentZ = draggableComponent.transform.eulerAngles.z;
            if (currentZ > 180f) currentZ -= 360f; // Chuyển về khoảng -180 đến 180
            
            newAngle = currentZ + (angle / 2);

            // Giới hạn góc từ -15° đến 15°
            newAngle = Mathf.Clamp(newAngle, -15f, 15f);
            
            // Áp dụng góc xoay đã được giới hạn
            draggableComponent.transform.rotation = Quaternion.Euler(0, 0, newAngle);
            
            // Gọi update cho CowTeat để xử lý scale
            draggableComponent.OnDragUpdate(currentMousePosition, deltaMousePosition);
        }

        protected override void OnDragEnded()
        {
                draggableComponent.OnDragEnd();
                // Hủy đăng ký sự kiện
                draggableComponent.OnMilkDropCreated -= HandleMilkDropCreated;
        }

        private void HandleMilkDropCreated()
        {
            currentMilkDropCount++;
            
            // Tính toán vị trí Y mới cho mask dựa trên số giọt sữa
            float progress = (float)currentMilkDropCount / targetMilkDrops;
            float newY = Mathf.Lerp(initialMaskY, maskTargetY, progress);
            
            // Di chuyển mask lên
            if (mask != null)
            {
                mask.DOMoveY(newY, maskMoveDuration / targetMilkDrops).SetEase(Ease.OutQuad);
            }
            
            // Kiểm tra điều kiện thắng
            if (currentMilkDropCount >= targetMilkDrops)
            {
                isWin = true;
                cowTeat.StopSpawnMilk();
                var hanleWinCondition = HanleWinCondition();
                StartCoroutine(hanleWinCondition);
            }
        }
        IEnumerator HanleWinCondition()
        {
            OnDragEnded();
            yield return new WaitForSeconds(1f);
            WinBox.SetUp().Show();
        }
    }
}
