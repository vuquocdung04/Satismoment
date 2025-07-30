using UnityEngine;
using DG.Tweening;

public class L164_CornHusk : MonoBehaviour
{
    public BoxCollider2D boxCollider;

    public float holdTime { get; private set; } = 0f;
    private bool isHolding = false;
    private bool isDropped = false;
    [SerializeField] private float scaleSpeed = 1f;

    public void StartHold()
    {
        isHolding = true;
        isDropped = false;
        holdTime = 0f;
        transform.localScale = Vector3.one;
    }

    public void UpdateHold(float deltaTime)
    {
        if (!isHolding || isDropped) return;

        holdTime += deltaTime;
        if (holdTime >= 0.3f)
        {
            isDropped = true;
            transform.DOScale(Vector3.one, 0.3f);
            transform.DOMoveY(-13f, 0.5f);
            // Để bên ngoài gọi xử lý bật collider tiếp theo
        }
        else
        {
            Vector3 scale = transform.localScale;
            scale.y = 1 + holdTime * scaleSpeed;
            transform.localScale = scale;
        }
    }

    public void EndHold()
    {
        isHolding = false;
        if (!isDropped)
        {
            transform.localScale = Vector3.one;
            holdTime = 0f;
        }
    }

    public bool IsDropped()
    {
        return isDropped;
    }
}
