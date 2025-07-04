using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L110_Fire : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public List<Sprite> lsFrames;

    public void PlayingAnim()
    {
        StartCoroutine(FiringAnim());
    }

    IEnumerator FiringAnim()
    {
        int frameIndex = 0;
        var waitTime = new WaitForSeconds(0.2f);
        while (true) // Vòng lặp vô hạn để đổi sprite liên tục
        {
            if (lsFrames.Count > 0)
            {
                // Cập nhật sprite hiện tại
                spriteRenderer.sprite = lsFrames[frameIndex];

                // Tăng chỉ số frame, quay lại 0 nếu vượt quá danh sách
                frameIndex = (frameIndex + 1) % lsFrames.Count;
            }

            // Đợi 0.2 giây trước khi đổi frame tiếp theo
            yield return waitTime;
        }
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
}