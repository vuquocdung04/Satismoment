using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // Import DOTween

public class L171_Line : MonoBehaviour
{
    public Transform btn;
    public SpriteRenderer objRenderer;
    public Sprite lineRed;
    public Sprite lineGreen;

    public float limitY = 0.4f;
    public float moveDuration = 0.5f; // Thời gian animation
    public float coolDownTime = 1f; // Thời gian cooldown

    public bool coolDown = true; // Trạng thái sẵn sàng
    public bool isGreen = false; // Biến kiểm tra trạng thái green

    public void InitState()
    {
        // Random số 0 hoặc 1
        int randomValue = Random.Range(0, 2);

        if (randomValue == 0)
        {
            // Nếu random = 0: btn.y = 0.4f và sprite = green
            btn.localPosition = new Vector3(0, limitY, 0);
            objRenderer.sprite = lineGreen;
            isGreen = true; // Set bool = true
        }
        else
        {
            // Nếu random = 1: btn.y khác 0.4f và sprite = red
            btn.localPosition = new Vector3(0, -limitY, 0);
            objRenderer.sprite = lineRed;
            isGreen = false; // Set bool = false
        }
    }

    public void OnStartDrag()
    {
        // Kiểm tra cooldown
        if (!coolDown)
        {
            return; // Nếu chưa sẵn sàng thì không làm gì cả
        }

        // Bắt đầu cooldown
        StartCoroutine(CoolDownCoroutine());

        if (btn.localPosition.y >= limitY)
        {
            isGreen = false; // Update bool
            // Nếu btn ở vị trí 0.4f -> di chuyển tới -0.4f và đổi sprite thành đỏ
            btn.DOLocalMoveY(-limitY, moveDuration).OnComplete(() => {
                objRenderer.sprite = lineRed;
            });
        }
        else
        {
            isGreen = true; // Update bool
            // Nếu btn ở vị trí -0.4f -> di chuyển tới 0.4f và đổi sprite thành xanh
            btn.DOLocalMoveY(limitY, moveDuration).OnComplete(() => {
                objRenderer.sprite = lineGreen;
            });
        }
    }

    private IEnumerator CoolDownCoroutine()
    {
        coolDown = false; // Đặt trạng thái không sẵn sàng
        yield return new WaitForSeconds(coolDownTime); // Chờ cooldown
        coolDown = true; // Đặt lại trạng thái sẵn sàng
    }
}
