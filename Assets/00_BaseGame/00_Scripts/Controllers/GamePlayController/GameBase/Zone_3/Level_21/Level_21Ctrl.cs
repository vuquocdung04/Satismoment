
using UnityEngine;

public class Level_21Ctrl : MonoBehaviour
{
    public AudioClip clickSound;
    public L21_Squirrel squirriel;
    public L21_Btn currentBtn;
    public float speed = 2f;
    public float durationAnim = 0.3f;
    public float resetDurationAnim = 0.3f;
    public bool isWin;
    Vector3 mousePos;
    private int animationFrameIndex;
    private void Update()
    {
        if (isWin) return;

        if (Camera.main != null) mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider == null) return;

            currentBtn = hit.collider.GetComponent<L21_Btn>();

            if (currentBtn == null) return;
            currentBtn.spriteRenderer.sprite = currentBtn.spriteClick;
            GameController.Instance.musicManager.PlaySingle(clickSound);
            if (squirriel.lsRunSprites != null && squirriel.lsRunSprites.Count > 0)
            {
                animationFrameIndex = 0;
                squirriel.spriteRenderer.sprite = squirriel.lsRunSprites[animationFrameIndex];
                durationAnim = resetDurationAnim; // Start timer for the first frame
            }
        }

        if (Input.GetMouseButton(0) && currentBtn != null)
        {
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
            if (currentBtn != null)
            {
                currentBtn.spriteRenderer.sprite = currentBtn.spriteDefault;
            }
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
