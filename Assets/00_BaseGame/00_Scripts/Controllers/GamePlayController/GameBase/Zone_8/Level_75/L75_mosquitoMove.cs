using UnityEngine;
using DG.Tweening;

public class L75_mosquitoMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private float reachThreshold = 0.1f;
    private Vector3 currentTarget;
    private Vector3 pointA;
    private Vector3 pointB;

    // Giới hạn khu vực bay của muỗi
    private float minX = -2.81f;
    private float maxX = 2.81f;
    private float minY = -5f;
    private float maxY = 5f;

    void Start()
    {
        pointA = transform.position;
        GenerateNewPointB();
        currentTarget = pointA + pointB;

        MoveToTarget();
    }

    void MoveToTarget()
    {
        float distance = Vector3.Distance(transform.position, currentTarget);
        float duration = distance / moveSpeed;

        transform.DOMove(currentTarget, duration)
            .SetEase(Ease.Linear)
            .OnComplete(OnReachTarget);
    }

    void OnReachTarget()
    {
        if (Vector3.Distance(transform.position, currentTarget) <= reachThreshold)
        {
            pointA = transform.position;

            GenerateNewPointB();
            currentTarget = pointA + pointB;

            MoveToTarget(); // Tiếp tục di chuyển
        }
    }

    public void StopMoving()
    {
        DOTween.Kill(transform); // Dừng mọi tween đang chạy trên transform
    }

    void GenerateNewPointB()
    {
        int attempts = 0;
        do
        {
            float randomX = Random.Range(-1.5f, 1.5f);
            float randomY = Random.Range(-1f, 1f);
            pointB = new Vector3(randomX, randomY, 0);

            Vector3 potentialTarget = pointA + pointB;

            if (
                potentialTarget.x >= minX && potentialTarget.x <= maxX &&
                potentialTarget.y >= minY && potentialTarget.y <= maxY
               )
            {
                return;
            }

            attempts++;
        } while (attempts < 10);

        // Fallback nếu không tìm được điểm hợp lệ
        float safeX = Random.Range(minX, maxX);
        float safeY = Random.Range(minY, maxY);
        currentTarget = new Vector3(safeX, safeY, transform.position.z);
        pointB = currentTarget - pointA;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(pointA, 0.1f);
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(currentTarget, 0.1f);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, currentTarget);

        Gizmos.color = Color.white;
        Gizmos.DrawLine(new Vector3(minX, minY), new Vector3(maxX, minY));
        Gizmos.DrawLine(new Vector3(maxX, minY), new Vector3(maxX, maxY));
        Gizmos.DrawLine(new Vector3(maxX, maxY), new Vector3(minX, maxY));
        Gizmos.DrawLine(new Vector3(minX, maxY), new Vector3(minX, minY));
    }
}