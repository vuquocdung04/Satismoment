using UnityEngine;

public class L62_ConveyorBelt : MonoBehaviour
{
    public Level_62Ctrl levelCtrl;
    public float speedMoveX = 5f;
    void Update()
    {
        if (levelCtrl.isWin) return;

        // Di chuyển sang trái
        transform.Translate(Vector2.left * speedMoveX * Time.deltaTime);

        // Nếu đã di chuyển quá giới hạn thì reset
        if (transform.position.x < -21.09f)
        {
            ResetPosition();
        }
    }

    void ResetPosition()
    {
        this.transform.position = new Vector2(-3.09f, 2.34f);
    }
}