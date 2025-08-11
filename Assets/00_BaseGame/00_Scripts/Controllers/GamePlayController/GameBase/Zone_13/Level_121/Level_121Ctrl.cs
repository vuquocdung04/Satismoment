using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_121Ctrl : BaseDragController<L121_PopitButton>
{
    public int countRandom = 3; // Số lượng ô cần random
    public Sprite spriteNotion;
    public Sprite pressSprite;
    public Sprite spriteDefault;
    public List<L121_PopitButton> lsPopupButtons; // Danh sách các nút Pop It

    private List<int> randomIds; // Lưu ID của các ô được random
    private int currentTargetIndex = 0; // Chỉ số mục tiêu hiện tại (thứ tự người chơi cần bấm)
    bool canPlay = false;
    public int winProgress = 0;
    private void Start()
    {
        InitStateGame();
    }

    

    protected override void OnDragEnded()
    {
        // Xử lý khi kéo thả kết thúc
        if(winProgress == 3)
        {
            StartCoroutine(HandleWinCondition());
            Debug.LogError("Ketthuc");
        }
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        // Xử lý logic kéo thả
    }

    protected override void OnDragStarted()
    {
        if (!canPlay) return;
        StartCoroutine(CheckButtonClick(draggableComponent));
    }

    public void InitStateGame()
    {
        if (isWin) return;
        // Reset trạng thái game
        foreach (var button in lsPopupButtons)
        {
            button.ResetState(spriteDefault); // Đưa tất cả nút về trạng thái mặc định
        }

        // Random 3 ô từ danh sách
        randomIds = new List<int>();
        for (int i = 0; i < countRandom; i++)
        {
            int randomIndex = Random.Range(0, lsPopupButtons.Count);
            while (randomIds.Contains(randomIndex)) // Kiểm tra trùng lặp
            {
                randomIndex = Random.Range(0, lsPopupButtons.Count);
            }
            randomIds.Add(randomIndex);
        }

        // Gọi coroutine để thông báo trạng thái cho các ô
        StartCoroutine(ShowNotificationCoroutine());
    }

    [Button("Setup", ButtonSizes.Large)]
    public void Setup()
    {
        for (int i = 0; i < lsPopupButtons.Count; i++)
        {
            lsPopupButtons[i].id = i;
            lsPopupButtons[i].InitSetup();
        }
    }

    private IEnumerator ShowNotificationCoroutine()
    {
        canPlay = false;
        yield return new WaitForSeconds(0.1f);
        var waitTime = new WaitForSeconds(0.15f);
        foreach (int id in randomIds)
        {
            L121_PopitButton targetButton = lsPopupButtons[id];
            Debug.LogError(id);
            targetButton.NotionStateStart(spriteNotion); // Hiển thị thông báo
            yield return waitTime;
            targetButton.ResetState(spriteDefault);
        }
        canPlay = true;
    }

    public IEnumerator CheckButtonClick(L121_PopitButton button)
    {
        if (button.id == randomIds[currentTargetIndex])
        {
            // Bấm đúng
            button.OnClicked(pressSprite); // Đổi trạng thái nút
            currentTargetIndex++; // Tiến tới mục tiêu tiếp theo

            if (currentTargetIndex >= randomIds.Count)
            {
                Debug.Log("Chúc mừng! Bạn đã hoàn thành!");
                // Tăng số lượng ô cần random
                countRandom++;
                Debug.Log($"Số lượng ô cần random: {countRandom}");
                winProgress++;
                // Khởi tạo lại game
                ChangeColor(Color.green);
                yield return new WaitForSeconds(0.2f);
                InitStateGame();
            }
        }
        else
        {
            // Bấm sai
            Debug.Log("Sai rồi! Hãy thử lại.");
            ChangeColor(Color.red);
            yield return new WaitForSeconds(0.2f);
            ResetGame(); // Reset game và gọi lại
        }
    }

    public void ChangeColor(Color newColor)
    {
        if (isWin) return;
        foreach(var item in this.lsPopupButtons)
        {
            item.ChangeColor(newColor);
        }
    }

    IEnumerator HandleWinCondition()
    {
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();
    }

    /// <summary>
    /// Reset trạng thái game và gọi lại quá trình random
    /// </summary>
    private void ResetGame()
    {
        currentTargetIndex = 0; // Reset chỉ số mục tiêu
        randomIds.Clear(); // Xóa danh sách ID đã random
        InitStateGame(); // Gọi lại InitStateGame để random lại
    }
}