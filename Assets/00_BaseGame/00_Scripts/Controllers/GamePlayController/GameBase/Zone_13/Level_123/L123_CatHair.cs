using UnityEngine;
using DG.Tweening;

public class L123_CatHair : MonoBehaviour
{
    public Rigidbody2D rb;
    public CircleCollider2D circleCollider;

    [Header("Force Settings")]
    public Vector2 forceDirection = Vector2.up; // Hướng lực mặc định
    public float forceStrength = 5f;           // Cường độ lực
    public bool useRandomDirection = true;     // Có dùng hướng ngẫu nhiên không?
    public float torqueStrength = 0.5f;        // Độ xoáy ban đầu

    public void Init()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.AddTorque(torqueStrength, ForceMode2D.Impulse);
        Vector2 finalForce = useRandomDirection ? Random.insideUnitCircle.normalized * forceStrength : forceDirection * forceStrength;
        rb.AddForce(finalForce, ForceMode2D.Impulse);
        circleCollider.enabled = false;
        DOVirtual.DelayedCall(2f, () => gameObject.SetActive(false));
    }


    public void InitSetup()
    {
        rb = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
    }
}