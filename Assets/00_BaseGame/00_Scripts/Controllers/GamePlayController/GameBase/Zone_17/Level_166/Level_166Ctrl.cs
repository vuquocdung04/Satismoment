using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_166Ctrl : BaseDragController<L166_Dot>
{
    private List<L166_Dot> currentPath = new List<L166_Dot>();
    private L166_Dot startDot = null;
    private L166_DotType activeDotType = L166_DotType.None;
    private bool pathFinished = false;
    private int successfulConnections = 0;

    // Danh sách các đường đã nối thành công
    private List<List<L166_Dot>> allPaths = new List<List<L166_Dot>>();

    protected override void OnDragStarted()
    {
        startDot = draggableComponent;
        if (startDot.dotType != L166_DotType.None && startDot.dotType != L166_DotType.Green)
        {
            activeDotType = startDot.dotType;
            currentPath.Clear();
            currentPath.Add(startDot);
            pathFinished = false;
        }
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        if (isWin || pathFinished || startDot == null) return;

        RaycastHit2D hit = Physics2D.Raycast(currentMousePosition, Vector2.zero);
        if (hit.collider != null)
        {
            var dot = hit.collider.GetComponent<L166_Dot>();
            if (dot != null && !currentPath.Contains(dot))
            {
                L166_Dot lastDot = currentPath[currentPath.Count - 1];
                if (!IsNeighbor(lastDot, dot))
                    return; // Không liền kề, bỏ qua

                // Nếu dot đã được tô màu trước đó từ đường khác (và không phải dot gốc đầu/đầu của đường hiện tại):
                if (dot.dotType != L166_DotType.None && dot.dotType != activeDotType)
                {
                    List<L166_Dot> oldPath = null;
                    foreach (var path in allPaths)
                    {
                        if (path.Contains(dot))
                        {
                            oldPath = path;
                            break;
                        }
                    }
                    if (oldPath != null)
                    {
                        // Reset màu các ô None của oldPath
                        foreach (var d in oldPath)
                        {
                            if (d.dotType == oldPath[0].dotType && d.dotType != L166_DotType.None && d != oldPath[0] && d != oldPath[oldPath.Count - 1])
                            {
                                d.objRenderer.color = new Color(1f, 1f, 1f, 1f);
                                d.dotType = L166_DotType.None;
                            }
                        }
                        // Reset currentPath các ô None
                        foreach (var d in currentPath)
                        {
                            if (d.dotType == activeDotType && d != startDot && d != dot)
                            {
                                d.objRenderer.color = new Color(1f, 1f, 1f, 1f);
                                d.dotType = L166_DotType.None;
                            }
                        }
                        if (successfulConnections > 0)
                            successfulConnections--;

                        allPaths.Remove(oldPath);
                        currentPath.Clear();
                        pathFinished = false;
                        startDot = null;
                        activeDotType = L166_DotType.None;
                        Debug.Log("Va chạm đường, cả hai line đều bị xóa! Số lần nối thành công: " + successfulConnections);
                        return;
                    }
                }

                // Bình thường, nối dot mới
                if (dot.dotType == L166_DotType.None)
                {
                    Color fadedColor = startDot.objRenderer.color;
                    fadedColor.a = 0.5f;
                    dot.objRenderer.color = fadedColor;
                    dot.dotType = activeDotType;
                    currentPath.Add(dot);
                }
                else if (dot.dotType == activeDotType && dot != startDot)
                {
                    currentPath.Add(dot);
                    pathFinished = true;
                }
            }
        }
    }

    protected override void OnDragEnded()
    {
        if (pathFinished && currentPath.Count >= 2)
        {
            // Kết thúc nối đường hợp lệ
            L166_Dot endDot = currentPath[currentPath.Count - 1];
            if (endDot.dotType == activeDotType)
            {
                successfulConnections++;
                Debug.Log("Số lần nối thành công: " + successfulConnections);

                if (successfulConnections >= 3)
                {
                    isWin = true;
                    Debug.Log("Bạn đã thắng!");
                    StartCoroutine(HandleWinCondition());
                }

                // Lưu đường vừa nối vào allPaths
                allPaths.Add(new List<L166_Dot>(currentPath));

                currentPath.Clear();
                startDot = null;
                activeDotType = L166_DotType.None;
                pathFinished = false;
                return;
            }
        }

        // Nếu không nối thành công, reset màu các ô None trong path
        foreach (var dot in currentPath)
        {
            if (dot != startDot && dot.dotType == activeDotType)
            {
                dot.objRenderer.color = new Color(1f, 1f, 1f, 1f);
                dot.dotType = L166_DotType.None;
            }
        }
        currentPath.Clear();
        startDot = null;
        activeDotType = L166_DotType.None;
        pathFinished = false;
    }

    private bool IsNeighbor(L166_Dot a, L166_Dot b)
    {
        int dx = Mathf.Abs(a.row - b.row);
        int dy = Mathf.Abs(a.col - b.col);
        return (dx + dy) == 1;
    }

    IEnumerator HandleWinCondition()
    {
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();
    }
}
