using UnityEngine;
using DG.Tweening; // Cần import DOTween nếu dùng tween

public class L164_CornKernel : MonoBehaviour
{
    public Level_164Ctrl levelCtrl; // Tham chiếu tới controller để kiểm tra trạng thái

    private bool hasFallen = false; // Để đảm bảo bay rơi chỉ chạy 1 lần

    private void OnMouseEnter()
    {
        if (hasFallen) return; // Nếu đã bay rơi rồi thì không làm nữa

        if (levelCtrl == null)
        {
            Debug.LogWarning("Level_164Ctrl reference chưa được gán trong CornKernel");
            return;
        }

        // Kiểm tra điều kiện tất cả các lá đều đã rơi
        bool allDropped = true;
        foreach (var cornHusk in levelCtrl.lsCornHusks)
        {
            if (!cornHusk.IsDropped())
            {
                allDropped = false;
                break;
            }
        }

        if (!allDropped) return;

        hasFallen = true;

        // Random hướng bay y = +1 hoặc -1
        float randomDir = Random.value < 0.5f ? -1f : 1f;
        Vector3 targetUpDown = transform.position + new Vector3(0, randomDir, 0);

        // Dùng DOTween để tween lần lượt bay lên/xuống, rồi rơi xuống y = -9f
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(targetUpDown, 0.3f).SetEase(Ease.OutQuad));
        seq.Append(transform.DOMoveY(-9f, 0.5f).SetEase(Ease.InQuad));
        levelCtrl.CheckWin();
    }
}
