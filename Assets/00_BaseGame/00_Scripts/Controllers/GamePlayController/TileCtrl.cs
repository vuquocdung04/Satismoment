using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TileCtrl : MonoBehaviour
{
    public Tile tilePrefab;
    public bool isWin;
    [Space(10)]
    public Tile selectTile1;
    public Tile selectTile2;
    public Tile selected_Tile;

    public List<Tile> lsTiles;

    public void Init()
    {
        this.GenarateTile();
    }

    void GenarateTile()
    {
        var levelDesign = GameController.Instance.dataContain.levelDesign;
        var matrix = levelDesign.lsLevelDesigns[UseProfile.SelectedLevel - 1].GetMatrix();
        int size = levelDesign.lsLevelDesigns[UseProfile.SelectedLevel - 1].size;
        int value;
        for(int i = 0; i < size; i++)
        {
            for(int j = 0; j < size; j++)
            {
                value = matrix[i, j];

                var tile = Instantiate(tilePrefab, new Vector2(j,-i), Quaternion.identity);
                tile.posTile = new Vector2Int(j, i);
                lsTiles.Add(tile);
                tile.name = $"Tile_{j}_{i}";
                tile.tileType = value == 0 ? TileType.None : TileType.Type1;
                tile.spriteRenderer.sortingOrder = value == 0 ? 0 : 2;
                tile.text.text = value == 0 ? "" : value.ToString();
            }
        }

    }

    private void Update()
    {
        this.CheckSelectTile1();
        this.CheckSelectTile2();
    }
    void CheckSelectTile1()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if (selectTile1 != null) return;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.zero);

        if (hit.collider == null) return;

        selected_Tile = hit.collider.GetComponent<Tile>();
        if (selected_Tile.tileType == TileType.None) return;
        selectTile1 = selected_Tile;
    }

    void CheckSelectTile2()
    {
        if (!Input.GetMouseButton(0)) return;
        if (selectTile1 == null) return;
        if (selectTile2 != null) return;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector3.zero);

        if (hit.collider == null) return;
        selected_Tile = hit.collider.GetComponent<Tile>();
        if (selected_Tile == selectTile1) return;

        if (selected_Tile.tileType != TileType.None) return;

        if((Mathf.Abs(selected_Tile.posTile.x - selectTile1.posTile.x)
            + Mathf.Abs(selected_Tile.posTile.y - selectTile1.posTile.y)) == 1)
        {
            selectTile2 = selected_Tile;
        }
        if (selectTile2 == null)
        {
            selectTile1 = null;
            return;
        }

        this.SwapTile(selectTile1, selectTile2, () =>
        {
            selectTile1 = null;
            selectTile2 = null;
            selected_Tile = null;
        });
    }

    void SwapTile(Tile tile1, Tile tile2, System.Action callback = null)
    {
        Vector2 pos1 = tile1.transform.position;
        Vector2 pos2 = tile2.transform.position;

        Vector2Int posTile1 = selectTile1.posTile;
        Vector2Int posTile2 = selectTile2.posTile;

        tile1.transform.DOMove(pos2,0.2f);

        tile2.transform.DOMove(pos1, 0f).OnComplete(() =>
        {
            tile1.UpdatePos(posTile2);
            tile2.UpdatePos(posTile1);
            this.CheckWin();
            callback?.Invoke();
        });
    }
    
    public Tile GetTile(Vector2Int posTile)
    {
        var levelDesign = GameController.Instance.dataContain.levelDesign;
        if (posTile.x < 0 || posTile.y < 0) return null;
        if(posTile.x >= levelDesign.lsLevelDesigns[UseProfile.SelectedLevel].size ||
            posTile.y >= levelDesign.lsLevelDesigns[UseProfile.SelectedLevel].size)
        {
            Debug.LogError("Check");
            return null;
        }
        foreach(var tile in this.lsTiles)
        {
            if(tile.posTile == posTile) return tile;
        }
        return null;
    }

    public void CheckWin()
    {
        var levelDesign = GameController.Instance.dataContain.levelDesign;
        var winPattern = levelDesign.lsLevelDesigns[UseProfile.SelectedLevel - 1].checkWin.lsVector2;
        for (int i = 0; i < lsTiles.Count; i++)
        {
            var tile = lsTiles[i];

            // Skip ô trống
            if (tile.tileType == TileType.None) continue;

            int tileValue = int.Parse(tile.text.text); // lấy số hiển thị

            // So sánh vị trí của số i+1 với vị trí chuẩn
            if (tile.posTile != winPattern[tileValue - 1]) // -1 vì tileValue bắt đầu từ 1
            {
                return;
            }
        }

        isWin = true;
        if (isWin)
        {
            WinBox.SetUp().Show();
            GameController.Instance.musicManager.PlayWinLevelSound();
        }
    }

}
