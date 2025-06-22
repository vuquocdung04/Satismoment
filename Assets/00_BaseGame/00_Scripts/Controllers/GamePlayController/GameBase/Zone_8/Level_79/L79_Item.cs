using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L79_Item : MonoBehaviour
{
    public Level_79Ctrl levelCtrl;
    int progressBreak = 0;
    public List<L79_Piece> lsPieces;

    public void ActionBreakAllList()
    {
        progressBreak++;
        if(progressBreak == 3)
        {
            foreach (var piece in this.lsPieces)
            {
                piece.ScatterPiece();
            }

            StartCoroutine(levelCtrl.HandleWinCodition());
        }

    }
}
