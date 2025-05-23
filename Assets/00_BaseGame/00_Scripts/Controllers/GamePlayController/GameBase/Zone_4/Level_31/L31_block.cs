using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L31_block : MonoBehaviour
{
    public int idBlock;
    public Vector2 posBlock;
    public L31_block neighbor_Left;
    public L31_block neighbor_Right;
    public L31_block neighbor_Top;
    public L31_block neighbor_Bottom;

    public void SetNeighbor()
    {
        var levelCtrl = Level_31Ctrl.Instance;

        neighbor_Left = levelCtrl.GetBlock(posBlock.x - levelCtrl.spacingX, posBlock.y);
        neighbor_Right = levelCtrl.GetBlock(posBlock.x + levelCtrl.spacingX, posBlock.y);
        neighbor_Top = levelCtrl.GetBlock(posBlock.x, posBlock.y + levelCtrl.spacingY);
        neighbor_Bottom = levelCtrl.GetBlock(posBlock.x, posBlock.y - levelCtrl.spacingY);
    }

    private void OnMouseDown()
    {
        // Gọi hàm xử lý trong Level_31Ctrl khi block này được click
        if (Level_31Ctrl.Instance.isWin) return;
        Level_31Ctrl.Instance.CheckAndDestroyConnectedBlocks(this);
    }
}
