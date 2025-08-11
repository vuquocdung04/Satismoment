using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_171Ctrl : BaseDragController<L171_Line>
{
    public L171_Line linePrefab;
    [SerializeField] List<L171_Line> lsLines;

    [Header("Matrix Settings")]
    public Vector2 startPosition = new Vector2(-1f, 2.5f);
    public float spacing = 1f;
    public int columns = 3; // Ngang
    public int rows = 4;    // Dọc

    protected override void OnDragEnded()
    {
        // Kiểm tra win sau khi drag kết thúc
        CheckWin();
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {

    }

    protected override void OnDragStarted()
    {
        draggableComponent.OnStartDrag();
    }

    private void SetupMatrix()
    {
        // Clear list cũ (nếu có)
        lsLines.Clear();

        // Vòng for dọc (rows = 4)
        for (int row = 0; row < rows; row++)
        {
            // Vòng for ngang (columns = 3)
            for (int col = 0; col < columns; col++)
            {
                // Tính vị trí cho từng object
                Vector3 position = new Vector3(
                    startPosition.x + col * spacing,  // X position
                    startPosition.y - row * spacing,  // Y position (trừ vì đi từ trên xuống)
                    0f                                // Z position
                );
                L171_Line newLine = Instantiate(linePrefab, position, Quaternion.identity);
                newLine.transform.SetParent(this.transform);
                newLine.InitState();

                // Add vào list
                lsLines.Add(newLine);
            }
        }
    }

    private void CheckWin()
    {
        // Kiểm tra xem tất cả các line có isGreen = true không
        foreach (L171_Line line in lsLines)
        {
            if (!line.isGreen)
            {
                return; // Nếu có line nào chưa green thì chưa win
            }
        }

        StartCoroutine(OnWin());
    }

    private IEnumerator OnWin()
    {
        isWin = true;
        Debug.Log("You Win!");
        yield return new WaitForSeconds(1f);
        WinBox.SetUp().Show();
    }

    // Gọi SetupMatrix trong Start hoặc Awake
    void Start()
    {
        SetupMatrix();
    }
}
