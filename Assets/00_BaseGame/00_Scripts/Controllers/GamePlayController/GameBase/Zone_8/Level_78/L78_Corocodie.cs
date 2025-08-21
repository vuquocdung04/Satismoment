using DG.Tweening;
using UnityEngine;

public class L78_Corocodie : MonoBehaviour
{
    public BoxCollider2D colliTriger;
    public BoxCollider2D colliTouch;
    public void Move()
    {
        Vector3 direction = transform.up; // Hướng hiện tại của cá sấu
        Vector3 targetPosition = transform.position - direction * 4f;

        transform.DOMove(targetPosition, 0.5f);
    }
    private void OnMouseDown()
    {
        Move();
        colliTouch.enabled = false;
        colliTriger.enabled = true;
        GameController.Instance.musicManager.PlayPick();
    }
}