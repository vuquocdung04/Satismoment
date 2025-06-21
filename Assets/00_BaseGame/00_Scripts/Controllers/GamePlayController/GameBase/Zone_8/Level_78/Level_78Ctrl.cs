using Sirenix.OdinInspector;
using UnityEngine;
using System.Collections.Generic;

public class Level_78Ctrl : MonoBehaviour
{
    public Transform holdPoint;
    public L78_Point pointPrefab;
    public List<L78_Point> lsPoints;

    public Vector2 startPosSpawn = new Vector2(-2.1f, 3.75f); // Góc trên trái
    public float spacing = 0.69f; // Khoảng cách giữa các điểm


    List<L78_Point> edgePoints = new List<L78_Point>();

    [Button("Generate Edge Points", ButtonSizes.Large)]
    void Setup()
    {
        // Xóa các điểm cũ nếu có
        foreach (L78_Point t in lsPoints)
        {
            DestroyImmediate(t.gameObject);
        }
        lsPoints.Clear();

        int size = 7;
        edgePoints.Clear();
        // 1. Hàng trên cùng - trái sang phải (y = 0)
        for (int x = 0; x < size; x++)
        {
            SpawnAndAddPoint(x, 0, ref edgePoints);
        }

        // 2. Cột phải - từ trên xuống (x = 6, y = 1 -> 5)
        for (int y = 1; y < size - 1; y++)
        {
            SpawnAndAddPoint(size - 1, y, ref edgePoints);
        }

        // 3. Hàng dưới cùng - phải sang trái (y = 6)
        for (int x = size - 1; x >= 0; x--)
        {
            SpawnAndAddPoint(x, size - 1, ref edgePoints);
        }

        // 4. Cột trái - từ dưới lên (x = 0, y = 5 -> 1)
        for (int y = size - 2; y >= 1; y--)
        {
            SpawnAndAddPoint(0, y, ref edgePoints);
        }

        // Gán neighbor lần lượt
        for (int i = 0; i < edgePoints.Count - 1; i++)
        {
            edgePoints[i].neighbor = edgePoints[i + 1].transform;
        }

        // Có thể set neighbor cuối cùng là null hoặc quay lại đầu để tạo vòng lặp
        edgePoints[^1].neighbor = null; // hoặc edgePoints[0].transform nếu cần loop
    }

    // Hàm hỗ trợ sinh điểm và thêm vào danh sách
    void SpawnAndAddPoint(int x, int y, ref List<L78_Point> edgePoints)
    {
        Vector3 spawnPos = new Vector3(
            startPosSpawn.x + x * spacing,
            startPosSpawn.y - y * spacing,
            0
        );

        L78_Point point = Instantiate(pointPrefab, spawnPos, Quaternion.identity);
        point.transform.SetParent(holdPoint);
        point.name = $"Point_{x}_{y}";
        edgePoints.Add(point);
    }
}