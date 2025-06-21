using DG.Tweening;
using UnityEngine;

public class L78_Corocodie : MonoBehaviour
{
    public Vector2 defaultPosition; // Vị trí mặc định
    public BoxCollider2D _collider2D;

    [SerializeField] private bool isColliFirst = true;
    public void Move()
    {
        Vector3 direction = transform.up; // Hướng hiện tại của cá sấu
        Vector3 targetPosition = transform.position - direction * 2f;

        transform.DOMove(targetPosition, 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var point = collision.GetComponent<L78_Point>();

        transform.DOKill(); // Dừng mọi animation đang chạy

        if (point == null)
        {
            return;
        }

        if (isColliFirst)
        {
            RotateAndMove(-90f, point.neighbor != null ? point.neighbor.position : null);
            isColliFirst = false;
        }
        else
        {
            if (point.angle == 90f)
            {
                RotateAndMove(-90f, point.neighbor?.position);
            }
            else
            {
                if (point.neighbor != null)
                {
                    transform.DOMove(point.neighbor.position, 0.2f);
                }
                else
                {
                    EndAnimation();
                }
            }
        }
    }

    private void RotateAndMove(float angle, Vector3? targetPosition)
    {
        transform.DORotate(new Vector3(0, 0, transform.eulerAngles.z + angle), 0.2f)
            .OnComplete(() =>
            {
                if (targetPosition.HasValue)
                {
                    transform.DOMove(targetPosition.Value, 0.2f);
                }
            });
    }

    private void EndAnimation()
    {
        transform.DORotate(Vector3.zero, 0.2f)
            .OnComplete(() =>
            {
                transform.DOMoveY(transform.position.y - 2f, 0.3f);
            });
    }

    private void OnMouseDown()
    {
        Move();
    }
}