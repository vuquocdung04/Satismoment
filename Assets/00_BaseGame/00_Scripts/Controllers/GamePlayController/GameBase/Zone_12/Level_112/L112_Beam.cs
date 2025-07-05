using DG.Tweening;
using System.Reflection;
using UnityEngine;

public class L112_Beam : MonoBehaviour
{
    public Level_112Ctrl levelCtrl;
    public L112_Plate plateLeft;
    public float plateRightWeight = 5;
    public Transform plateRight;
    public Transform pointer;
    [Tooltip("Thời gian mượt mà để xoay beam")]
    public float rotationDuration = 0.5f;


    private float currentTargetAngle = 0f;

    void Start()
    {
        UpdateBeamTilt();
    }
    public void UpdateBeamTilt()
    {
        float difference = plateLeft.weight - plateRightWeight;
        float totalWeight = plateLeft.weight + plateRightWeight;

        // Nếu không có trọng lượng thì không nghiêng
        if (totalWeight == 0) return;

        // Tính tỷ lệ chênh lệch từ -1 đến 1
        float ratio = difference / totalWeight;

        // Góc nghiêng dựa trên tỷ lệ này
        currentTargetAngle = ratio * 20f;
        transform.DORotate(new Vector3(0, 0, currentTargetAngle), rotationDuration);
        plateLeft.transform.DOLocalRotate(new Vector3(0, 0, -currentTargetAngle),rotationDuration);
        plateRight.DOLocalRotate(new Vector3(0, 0, -currentTargetAngle),rotationDuration);
        pointer.DOLocalRotate(new Vector3(0, 0, currentTargetAngle), rotationDuration);
        if (CheckWin() && !levelCtrl.isWin)
        {
            levelCtrl.isWin = true;
            StartCoroutine(levelCtrl.HandleWinCondition());
            Debug.LogError("test");
        }
    }
    public bool CheckWin()
    {
        
        if(plateLeft.weight == plateRightWeight)
        {
            Debug.LogError("Balance");
            return true;
        }
        return false;
    }
}