using System.Collections;
using UnityEngine;

public class L110_Diem : MonoBehaviour
{
    public Level_110Ctrl levelCtrl;
    public Transform pointSpawn;
    private Coroutine fireCoroutine;
    private int counter = 0;
    public bool isFired = false;

    private Collider2D currentCollision; // Lưu lại collider đang va chạm

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentCollision = collision;
        var candle = collision.GetComponent<L110_Candle>();

        if (candle == null)
        {
            // Không phải va chạm với nến
            if (fireCoroutine == null && !isFired)
            {
                fireCoroutine = StartCoroutine(HandleFireAtMatch());
            }
        }
        else
        {
            // Va chạm với nến
            if (isFired && fireCoroutine == null)
            {
                fireCoroutine = StartCoroutine(HandleFireAtCandle(candle));
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Cập nhật liên tục collider đang va chạm
        currentCollision = collision;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (currentCollision == collision)
        {
            StopCurrentCoroutine();
            counter = 0;
            currentCollision = null;
        }
    }

    private void StopCurrentCoroutine()
    {
        if (fireCoroutine != null)
        {
            StopCoroutine(fireCoroutine);
            fireCoroutine = null;
        }
    }

    // Coroutine: Tạo lửa tại que diêm (khi không chạm vào nến)
    IEnumerator HandleFireAtMatch()
    {
        var waitTime = new WaitForSeconds(0.3f);
        while (counter < 5)
        {
            yield return waitTime;
            counter++;

            if (counter >= 5)
            {
                var fire = levelCtrl.SpawnFire(Vector3.one);
                fire.transform.SetParent(transform);
                fire.transform.position = pointSpawn.position;
                isFired = true;
            }
        }
        fireCoroutine = null;
    }

    // Coroutine: Tạo hiệu ứng ở nến nếu que diêm đã cháy
    IEnumerator HandleFireAtCandle(L110_Candle candle)
    {
        var waitTime = new WaitForSeconds(0.3f);
        while (counter < 5) // Khoảng 3 giây nếu mỗi bước là 0.3s
        {
            yield return waitTime;
            counter++;

            if (counter >= 5)
            {
                candle.OnLitUp(); // Ví dụ gọi hàm bật sáng nến
                break;
            }
        }
        fireCoroutine = null;
    }
}