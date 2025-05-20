using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_21Ctrl : MonoBehaviour
{
    public L21_Squirrel squirriel;
    public L21_Btn currentBtn;
    public float speed = 2f;
    public float durationAnim = 0.3f;
    public float resetDurationAnim = 0.3f;
    public bool isWin = false;
    Vector3 mousePos;
    private int animationFrameIndex = 0;
    private void Update()
    {
        if (isWin) return;

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider == null) return;

            currentBtn = hit.collider.GetComponent<L21_Btn>();

            if (currentBtn == null) return;
            currentBtn.spriteRenderer.sprite = currentBtn.spriteClick;

            if (squirriel.lsRunSprites != null && squirriel.lsRunSprites.Count > 0)
            {
                animationFrameIndex = 0;
                squirriel.spriteRenderer.sprite = squirriel.lsRunSprites[animationFrameIndex];
                durationAnim = resetDurationAnim; // Start timer for the first frame
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (currentBtn == null) return;
            switch (currentBtn.btnType)
            {
                case L21_BtnType.Left:
                    squirriel.rb.velocity = new Vector2(-speed,0);
                    squirriel.transform.eulerAngles = new Vector2(0,180);
                    break;
                case L21_BtnType.Right:
                    squirriel.rb.velocity = new Vector2(speed, 0);
                    squirriel.transform.eulerAngles = Vector2.zero;
                    break;
                case L21_BtnType.Top:
                    squirriel.rb.velocity = new Vector2(0, speed);
                    break;
                case L21_BtnType.Bottom:
                    squirriel.rb.velocity = new Vector2(0, -speed);
                    break;
            }
            HandleAnim();

        }

        if (Input.GetMouseButtonUp(0))
        {
            squirriel.rb.velocity = Vector2.zero;
            currentBtn.spriteRenderer.sprite = currentBtn.spriteDefault;
            currentBtn = null;
        }
    }


    public void HandleAnim()
    {
        if (squirriel.lsRunSprites != null && squirriel.lsRunSprites.Count > 0)
        {
            durationAnim -= Time.deltaTime;
            if (durationAnim <= 0f)
            {
                animationFrameIndex++;
                if (animationFrameIndex >= squirriel.lsRunSprites.Count)
                {
                    animationFrameIndex = 0; // Loop back to the first sprite
                }
                squirriel.spriteRenderer.sprite = squirriel.lsRunSprites[animationFrameIndex];
                durationAnim = resetDurationAnim; // Reset timer for the new frame
            }
        }
    }
}
