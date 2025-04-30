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

    public Tile neighBorLeft;
    public Tile neighBorRight;
    public Tile neighBorUp;
    public Tile neighBorDown;

    public void UpdatePos(Vector2Int index)
    {
        this.posTile = index;
        this.SetNeighBorTile();
    }

    public void SetNeighBorTile()
    {
        this.neighBorLeft = TileCtrl.Instance.GetTile(posTile + Vector2Int.left);
        this.neighBorRight = TileCtrl.Instance.GetTile(posTile + Vector2Int.right);
        this.neighBorUp = TileCtrl.Instance.GetTile(posTile - Vector2Int.up);
        this.neighBorDown = TileCtrl.Instance.GetTile(posTile - Vector2Int.down);
    }


}
