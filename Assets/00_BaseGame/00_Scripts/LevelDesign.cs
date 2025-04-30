using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
[CreateAssetMenu(menuName = "LevelDesign")]

public class LevelDesign : ScriptableObject
{
    public List<LevelDesign_Tile> lsLevelDesigns;
}

[System.Serializable]
public class LevelDesign_Tile
{
    public int level;
    public int size = 3;
    public int[] flatBoard;

    [Button("Setup",ButtonSizes.Large)]
    void ResizeBoard()
    {
        flatBoard = new int[size*size];
    }
    public int[,] GetMatrix()
    {
        int[,] matrix = new int[size, size];
        for (int i = 0; i <flatBoard.Length;i++)
        {
            matrix[i/size, i % size] = flatBoard[i];
        }
        return matrix;
    }

    public CheckWin checkWin;
    [Button("SetUp_Win", ButtonSizes.Large)]
    void SetUp()
    {
        checkWin = new CheckWin();
        checkWin.lsVector2 = new List<Vector2Int>();

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                int tileIndex = y * size + x + 1;

                if (tileIndex == size * size)
                {
                    checkWin.lsVector2.Add(new Vector2Int(x, y));
                }
                else
                {
                    checkWin.lsVector2.Add(new Vector2Int(x, y));
                }
            }
        }
    }
}

[System.Serializable]
public class CheckWin
{
    public List<Vector2Int> lsVector2;
}
