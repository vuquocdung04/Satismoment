using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_126Ctrl : Singleton<Level_126Ctrl>
{
    public float limitPos = 1.5f;
    public Transform mask;
    public int highIdFruitCount = 0;
    public List<L126_Fruit> lsFruitPrefabs;
    public L126_Fruit currentFruit;
    bool isClicked = true;
    bool isWin = false;
    private HashSet<int> _countedHighIds = new HashSet<int>();

    Vector3 mousePosition;
    void Start()
    {
        SpawnFruit();
    }

    Vector3 newPos;
    void Update()
    {
        if (isWin) return; // Nếu đã thắng, thoát ngay

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        // Cho phép người chơi di chuyển trái cây nếu isClicked là true
        if (isClicked)
        {
            if (Input.GetMouseButton(0))
            {
                newPos = mousePosition;
                newPos.y = 3f;
                newPos.x = Mathf.Clamp(newPos.x, -limitPos, limitPos);
                currentFruit.transform.position = newPos;
            }

            // Chỉ gọi Coroutine khi chuột được nhả VÀ isClicked đang true
            if (Input.GetMouseButtonUp(0))
            {
                isClicked = false; // Ngăn người chơi nhấp chuột tiếp trong khi Coroutine đang chạy
                StartCoroutine(HandleSpawnFruit());
            }
        }
    }

    void SpawnFruit()
    {
        int randomFruit = Random.Range(0, 3);
        Vector2 randomPosition = new Vector2(Random.Range(-limitPos, limitPos), 3f);
        var fruitClone = SimplePool2.Spawn(lsFruitPrefabs[randomFruit], randomPosition, Quaternion.identity);
        fruitClone.Init();
        currentFruit = fruitClone;
    }

    IEnumerator HandleSpawnFruit()
    {
        currentFruit.Falling();

        // Chờ một khoảng thời gian trước khi spawn quả mới để tránh nhấp liên tục
        // Coroutine này sẽ "khóa" input cho đến khi nó hoàn thành
        yield return new WaitForSeconds(0.5f);

        SpawnFruit();

        // Cho phép người chơi điều khiển quả mới sau một khoảng thời gian ngắn
        yield return new WaitForSeconds(0.5f);
        isClicked = true; // Cho phép nhấp chuột trở lại
    }

    public void GetFruitWithId(int id, Vector3 position)
    {
        if (id >= lsFruitPrefabs.Count)
        {
            Debug.LogWarning($"Attempted to spawn fruit with ID {id}, but only {lsFruitPrefabs.Count} prefabs are available. Not spawning.");
            return;
        }

        SimplePool2.Spawn(lsFruitPrefabs[id], position, Quaternion.identity);

        // Kiểm tra ID của quả mới và tăng biến đếm CHỈ LẦN ĐẦU TIÊN
        if ((id == 4 || id == 5 || id == 6) && !_countedHighIds.Contains(id))
        {
            highIdFruitCount++;
            _countedHighIds.Add(id);
            MaskMoving();
            if (highIdFruitCount == 3) // Điều kiện thắng khi đã đếm đủ 3 ID cao
            {
                StartCoroutine(HandleWinCondition());
            }
            Debug.Log($"High ID Fruit Count: {highIdFruitCount} (ID {id} counted for the first time)");
        }
        else if ((id == 4 || id == 5 || id == 6) && _countedHighIds.Contains(id))
        {
            Debug.Log($"Fruit with ID {id} spawned again, but already counted.");
        }
    }


    void MaskMoving()
    {
        mask.DOLocalMoveX(1.25f * highIdFruitCount, 0.5f).SetEase(Ease.Linear);
    }

    IEnumerator HandleWinCondition()
    {
        isWin = true; // Đặt cờ win để dừng game
        yield return new WaitForSeconds(1f);
        WinBox.SetUp().Show(); // Giả định WinBox là một UI panel
    }
}