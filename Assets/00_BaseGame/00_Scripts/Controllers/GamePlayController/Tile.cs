using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum TileType
{
    None,
    Type1,
}
public class Tile : MonoBehaviour
{
    public TileType tileType;
    public Vector2Int posTile;
    public TextMeshPro text;
    public void UpdatePos(Vector2Int index)
    {
        this.posTile = index;
    }


}
