using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L132_SpaceShip : MonoBehaviour
{
    public Level_132Ctrl levelCtrl;
    public Rigidbody2D rb;
    public float flySpeed = 2f;
    public Transform rocketRight;
    public Transform rocketLeft;

    // Hàm kích hoạt khi bắt đầu điều khiển
    public void OnStartState()
    {
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }

    // Hàm dừng lại
    public void OnEndState()
    {
        rb.gravityScale = 1;
        rb.velocity = Vector2.zero;     // Đặt vận tốc về 0
        rb.angularVelocity = 0f;

        RotateShipCenter();
        rocketLeft.gameObject.SetActive(false);
        rocketRight.gameObject.SetActive(false);
    }
    // Di chuyển lên trên theo trục Y
    public void SpaceShipFlying()
    {
        // Bay theo hướng mũi tàu (transform.up)
        transform.position += transform.up * flySpeed * Time.deltaTime;
    }

    // Xoay tàu theo trục Z
    public void RotateShipRight()
    {
        transform.rotation = Quaternion.Euler(0, 0, -20);
        rocketRight.gameObject.SetActive(true);
        rocketLeft.gameObject.SetActive(false);
    }
    public void RotateShipLeft()
    {
        transform.rotation = Quaternion.Euler(0,0,20f);
        rocketRight.gameObject.SetActive(false);
        rocketLeft.gameObject.SetActive(true);
    }
    public void RotateShipCenter()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0f);
        rocketRight.gameObject.SetActive(true);
        rocketLeft.gameObject.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (transform.position.y > -3f) return;
        if(transform.position.x >= -0.2f && transform.position.x <= 0.2f)
        {
            Debug.LogError("Win");
            StartCoroutine(levelCtrl.HandleWinCondition());
        }
    }
}