using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L64_Dino : MonoBehaviour
{
    public Level_64Ctrl levelCtrl;

    public SpriteRenderer dinoSprite;
    public List<Sprite> lsSpriteMoves;
    public Sprite spriteJump;
    public Rigidbody2D rb;
    public float jumpForce = 5f;

    public Transform groundCheck;
    public float groundCheckDistance = 0.1f;

    public bool isGrounded = true;
    public bool isRunning = false;
    private Coroutine runningCoroutine;

    // Biến để lưu trạng thái isGrounded của frame trước
    private bool wasGrounded;

    void Start()
    {
        wasGrounded = true; 
        DoRunningAnim();
    }

    void Update()
    {
        if (levelCtrl.isWin) return;

        wasGrounded = isGrounded;
        CheckGrounded();

        // Chỉ bắt đầu lại animation chạy KHI vừa tiếp đất
        // (frame trước không chạm đất, frame này thì có)
        if (!wasGrounded && isGrounded)
        {
            DoRunningAnim();
        }
    }

    public void Jump()
    {
        // Chỉ cho phép nhảy khi đang trên mặt đất
        if (!isGrounded) return;

        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        // Khi nhảy, dừng animation chạy và hiển thị sprite nhảy
        StopRunningAnim();
        DoJumpingAnim();
    }

    public void CheckGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance);
        isGrounded = hit.collider != null;
    }

    private IEnumerator RunningAnimation()
    {
        isRunning = true;
        int spriteIndex = 0;

        while (true)
        {
            dinoSprite.sprite = lsSpriteMoves[spriteIndex];
            spriteIndex = (spriteIndex + 1) % lsSpriteMoves.Count;
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void DoRunningAnim()
    {
        if (isRunning) return;
        runningCoroutine = StartCoroutine(RunningAnimation());
    }

    public void StopRunningAnim()
    {
        if (!isRunning || runningCoroutine == null) return;

        StopCoroutine(runningCoroutine);
        isRunning = false;
        runningCoroutine = null;
    }

    private void DoJumpingAnim()
    {
        dinoSprite.sprite = spriteJump;
    }

    #region Gizmos (Debug Ray)
    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = isGrounded ? Color.green : Color.red;
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
        }
    }
    #endregion
}