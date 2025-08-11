using DG.Tweening;
using UnityEngine;

namespace _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase.Zone_19.Level_183
{
    public class L183_DropOfMilk : MonoBehaviour
    {
        [Header("Drop Settings")]
        [SerializeField] private float dropDistance = 5f;
        [SerializeField] private float dropDuration = 2f;
        [SerializeField] private Ease dropEase = Ease.InQuad;

        public void InitState(Transform cowTeatTransform)
        {
            // Tính toán hướng rơi dựa trên rotation của CowTeat
            Vector3 dropDirection = GetDropDirection(cowTeatTransform);
            
            // Vị trí đích
            Vector3 targetPosition = transform.position + (dropDirection * dropDistance);
            
            // Animate rơi xuống theo hướng của CowTeat
            transform.DOMove(targetPosition, dropDuration)
                .SetEase(dropEase)
                .OnComplete(action: () => SimplePool2.Despawn(gameObject));
        }

        private Vector3 GetDropDirection(Transform cowTeatTransform)
        {
            // Lấy góc xoay Z của CowTeat
            float angleZ = cowTeatTransform.eulerAngles.z;
            if (angleZ > 180f) angleZ -= 360f; // Chuyển về khoảng -180 đến 180
            float radians = angleZ * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(
                Mathf.Sin(radians),  // x component
                -Mathf.Cos(radians), // y component (âm để rơi xuống)
                0f                   // z component
            );
            
            return direction.normalized;
        }
        
    }
}
