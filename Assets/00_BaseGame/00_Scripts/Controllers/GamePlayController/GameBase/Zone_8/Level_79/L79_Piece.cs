using UnityEngine;
using DG.Tweening;

public class L79_Piece : MonoBehaviour
{
    public L79_Item itemCtrl;
    [SerializeField] private float flyDuration = 1f; // Thời gian bay tung
    [SerializeField] private float fallDuration = 0.8f; // Thời gian rơi xuống
    [SerializeField] private float jumpPower = 1.5f; // Độ cao/độ mạnh của cú bay

    public bool isBroken = false;

    public void ScatterPiece()
    {
        if (isBroken) return;
        isBroken = true;

        // Tạo vector bay ngẫu nhiên
        Vector3 randomDirection = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(0.5f, 1f),
            0
        ).normalized;

        // Bay lên với DOJump
        transform.DOJump(transform.position + randomDirection * jumpPower,
                jumpPower, 1, flyDuration)
            .SetEase(Ease.OutCirc)
            .OnComplete(FallToGround); // Gọi hàm rơi xuống sau khi bay xong

        // Xoay vật thể khi bay
        transform.DORotate(
                new Vector3(0, 0, Random.Range(360f, 720f)),
                flyDuration,
                RotateMode.FastBeyond360)
            .SetEase(Ease.Linear);

        itemCtrl.ActionBreakAllList();
    }

    private void FallToGround()
    {
        // Sao chép vị trí hiện tại nhưng thay y thành -3.4f
        Vector3 groundPosition = new Vector3(transform.position.x, -3.4f, transform.position.z);

        // Di chuyển mượt xuống đất
        transform.DOMove(groundPosition, fallDuration)
            .SetEase(Ease.InSine); // Hiệu ứng rơi tự nhiên hơn
    }
}