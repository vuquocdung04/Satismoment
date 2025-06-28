using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L95_Duck : MonoBehaviour
{
    // List để lưu trữ các GameObject của các mảnh hình
    public Transform egg;
    public List<GameObject> surprisePieces;

    [SerializeField] private float blinkDuration = 0.15f; // Thời gian mỗi lần nhấp
    [SerializeField] private int numberOfBlinks = 3;      // Số lần nhấp nháy

    public IEnumerator PlayBlinkThenMoveEgg()
    {
        // Chờ hiệu ứng nhấp nháy hoàn tất
        yield return new WaitForSeconds(0.4f);
        yield return StartCoroutine(PlaySimultaneousBlinkSequence());
        MoveEggToTargetPosition(new Vector3(1.69f, -1.34f,0));
    }


    private IEnumerator PlaySimultaneousBlinkSequence()
    {
        var waitTime = new WaitForSeconds(blinkDuration);
        for (int i = 0; i < numberOfBlinks; i++)
        {
            // Tắt tất cả các mảnh hình
            foreach (GameObject piece in surprisePieces)
            {
                piece.SetActive(false);
            }

            yield return waitTime;

            // Bật tất cả các mảnh hình
            foreach (GameObject piece in surprisePieces)
            {
                piece.SetActive(true);
            }

            yield return waitTime;
        }
    }

    private void MoveEggToTargetPosition(Vector3 targetPosition)
    {
        egg.DORotate(new Vector3(0,0,-70f),1.5f,RotateMode.Fast);
        egg.DOMove(targetPosition, 1f)
           .SetEase(Ease.OutBounce);
    }



}