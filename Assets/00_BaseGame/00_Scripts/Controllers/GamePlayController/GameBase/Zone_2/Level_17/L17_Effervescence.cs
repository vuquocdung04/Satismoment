using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L17_Effervescence : MonoBehaviour
{
    public float baseSpeed = 10f;
    public float speedVariation = 5f;
    public float lifeTime = 1f;
    public float resetLifeTime = 1f;
    public float sprayAngleDegrees = 45;

    public Rigidbody2D rb;

    public SpriteRenderer spriteRenderer;
    float currentSpeed;
    float randomAngleOffset;
    Quaternion spreadRotation;
    Vector2 particleDirection;
    void Update()
    {
        Launch();
    }

    void Launch()
    {
        
        randomAngleOffset = Random.Range(-sprayAngleDegrees / 2f, sprayAngleDegrees / 2f);
        spreadRotation = Quaternion.AngleAxis(randomAngleOffset, Vector3.forward); // Xoay quanh trục Z cho 2D
        particleDirection = spreadRotation * transform.up; // Chuyển sang Vector2 cho vật lý 2D

        currentSpeed = baseSpeed;
        if (speedVariation > 0)
        {
            currentSpeed = Random.Range(baseSpeed - speedVariation, baseSpeed + speedVariation);
        }
        currentSpeed = Mathf.Max(0, currentSpeed);

        rb.velocity = particleDirection.normalized * currentSpeed;

        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            SimplePool2.Despawn(gameObject);
            lifeTime = resetLifeTime;
        }
    }
}
