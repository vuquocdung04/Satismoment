using DG.Tweening;
using UnityEngine;

public class L30_SpinLog : MonoBehaviour
{
    public Level_30Ctrl levelCtrl;
    private Tween spinTween; // Lưu reference của tween

    public void DoSpinning()
    {
        // Lưu tween vào biến để có thể quản lý sau này
        spinTween = transform.DORotate(new Vector3(0, 0, 360), 5f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1);
    }

    public void IncreaseWinProgress()
    {
        levelCtrl.winProgess++;
    }
    
    public void ResetTween()
    {
        if (spinTween != null && spinTween.IsActive())
        {
            spinTween.Restart(); // Reset và chay lại từ đầu
        }
    }
    
    private void DestroyTween()
    {
        if (spinTween != null)
        {
            spinTween.Kill(); // Hủy tween
            spinTween = null; // Set về null để tránh reference
        }
    }
    // Tự động hủy tween khi object bị destroy
    private void OnDestroy()
    {
        DestroyTween();
    }
}