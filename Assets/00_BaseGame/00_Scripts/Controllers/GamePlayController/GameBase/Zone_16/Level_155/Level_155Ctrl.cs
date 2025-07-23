using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_155Ctrl : BaseDragController<L155_Peanut>
{
    public List<L155_Peanut> lsPeanuts;

    [Header("Jar Settings")]
    public L155_JarPeanut redPeanutJar;    // Jar cho đậu đỏ (id = 0)
    public L155_JarPeanut greenPeanutJar;  // Jar cho đậu xanh (id = 1)

    [Header("Win Condition")]
    public int requiredRedPeanuts = 3;     // Jar đỏ cần 3 viên
    public int requiredGreenPeanuts = 5;   // Jar xanh cần 5 viên

    private bool isLevelCompleted = false;

    protected override void OnDragEnded()
    {
        draggableComponent.OnEnd();
        CheckWinCondition();
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.rb.MovePosition(currentMousePosition);
    }

    protected override void OnDragStarted()
    {
        draggableComponent.OnStart();
    }

    public void CheckWinCondition()
    {
        if (isLevelCompleted) return;

        bool redJarValid = IsJarValid(redPeanutJar, 0, requiredRedPeanuts);     // Jar đỏ: 3 viên id = 0
        bool greenJarValid = IsJarValid(greenPeanutJar, 1, requiredGreenPeanuts); // Jar xanh: 5 viên id = 1

        if (redJarValid && greenJarValid)
        {
            OnLevelCompleted();
        }
    }

    // Kiểm tra jar có đúng số lượng và toàn đậu cùng loại không
    private bool IsJarValid(L155_JarPeanut jar, int requiredId, int requiredCount)
    {
        // Phải có đúng số lượng yêu cầu
        if (jar.peanutsInJar.Count != requiredCount) return false;

        // Tất cả đậu phải cùng loại
        foreach (var peanut in jar.peanutsInJar)
        {
            if (peanut.id != requiredId)
            {
                return false; // Có đậu khác loại
            }
        }
        return true;
    }

    private void OnLevelCompleted()
    {
        isLevelCompleted = true;
        Debug.Log($"Level Completed! Red jar has {requiredRedPeanuts} red peanuts, Green jar has {requiredGreenPeanuts} green peanuts!");
        StartCoroutine(
                ShowWinUI());
    }

    private IEnumerator ShowWinUI()
    {
        isWin = true;
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();
    }

    [Button("Setup", ButtonSizes.Large)]
    void Setup()
    {
        foreach (var peanut in this.lsPeanuts)
        {
            peanut.rb = peanut.transform.GetComponent<Rigidbody2D>();
        }
    }
}
