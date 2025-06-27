using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L91_CatHand : MonoBehaviour
{
    public List<Transform> lsHands; // 2 tay cần animate
    public float rotateAmount = 20f; // Góc quơ tay
    public float moveAmount = 0.5f;  // Độ cao nhấp nhô
    public float duration = 0.5f;    // Thời gian mỗi bước
    public int loopCount = 2;        // Số lần lặp lại

    public IEnumerator AnimateHand()
    {
        Tween moveCat = transform.DOMoveY(-5f, 0.5f).SetEase(Ease.Linear);
        yield return moveCat.WaitForCompletion();

        // Tạo chuỗi hành động chung cho cả 2 tay
        Sequence fullSequence = DOTween.Sequence();

        foreach (var hand in lsHands)
        {
            Sequence handSeq = DOTween.Sequence();

            // Nhấc lên + quơ sang trái
            handSeq.Append(
                hand.DOLocalMoveY(hand.localPosition.y + moveAmount, duration)
                     .SetEase(Ease.InOutSine)
            );
            handSeq.Join(
                hand.DOLocalRotate(new Vector3(0, 0, -rotateAmount), duration)
                      .SetEase(Ease.InOutSine)
            );

            // Hạ xuống + quơ sang phải
            handSeq.Append(
                hand.DOLocalMoveY(hand.localPosition.y - moveAmount, duration)
                     .SetEase(Ease.InOutSine)
            );
            handSeq.Join(
                hand.DOLocalRotate(new Vector3(0, 0, rotateAmount), duration)
                      .SetEase(Ease.InOutSine)
            );

            // Trở về vị trí ban đầu + xoay về 0
            handSeq.Append(
                hand.DOLocalMoveY(hand.localPosition.y + moveAmount, duration)
                     .SetEase(Ease.InOutSine)
            );
            handSeq.Join(
                hand.DOLocalRotate(Vector3.zero, duration)
                      .SetEase(Ease.InOutSine)
            );

            // Lặp lại hành động
            handSeq.SetLoops(loopCount, LoopType.Restart);

            // Thêm vào chuỗi chính và chạy song song
            fullSequence.Join(handSeq);
        }

        // Chờ toàn bộ hiệu ứng hoàn tất
        yield return fullSequence.WaitForCompletion();
    }
}