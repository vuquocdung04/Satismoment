using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L166_InitMatrix : MonoBehaviour
{
    public L166_Dot dotPrefab;  // Prefab ô vuông
    public float startX;           // Vị trí x bắt đầu
    public float startY;           // Vị trí y bắt đầu
    public float spacing;          // Khoảng cách giữa các ô
    public int maxtrixCount;       // Số lượng hàng/cột

    [SerializeField] List<L166_Dot> lsDots;

    [Button("Init matrix", ButtonSizes.Large)]
    void InitMatrix()
    {
        for (int i = 0; i < maxtrixCount; i++)
        {
            for (int j = 0; j < maxtrixCount; j++)
            {
                Vector3 pos = new Vector3(
                    startX + i * spacing,
                    startY + j * spacing,
                    0);
                var dotClone = Instantiate(dotPrefab, pos, Quaternion.identity, transform);
                dotClone.name = $"dot_{i}_{j}";
                
                lsDots.Add(dotClone);
            }
        }
    }
    [Button("Clear matrix", ButtonSizes.Large)]
    void ClearMatrix()
    {
        foreach(var dot in this.lsDots) DestroyImmediate(dot.gameObject);
        lsDots.Clear();
    }
    [Button("Set Row Col", ButtonSizes.Large)]
    void SetRowCol()
    {
        if (lsDots == null || lsDots.Count == 0)
        {
            Debug.LogWarning("Danh sách lsDots đang rỗng!");
            return;
        }

        for (int index = 0; index < lsDots.Count; index++)
        {
            var dot = lsDots[index];
            int row = index / maxtrixCount;    // Chia lấy phần nguyên làm row
            int col = index % maxtrixCount;    // Lấy phần dư làm col

            dot.row = row;
            dot.col = col;

            // Nếu muốn debug
            // Debug.Log($"{dot.name} set row={row} col={col}");
        }
    }

}
