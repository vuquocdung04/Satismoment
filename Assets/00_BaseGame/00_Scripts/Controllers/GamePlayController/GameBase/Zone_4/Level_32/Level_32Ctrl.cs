using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_32Ctrl : MonoBehaviour
{
    L32_PiceCard pieceCard;
    Vector3 mousePos;
    public List<L32_PiceCard> lsCards;

    private void Start()
    {
        foreach(var card in this.lsCards)
        {
            int rand = Random.Range(1,4);
            card.transform.eulerAngles = new Vector3(0,0,rand * 90);
        }
    }
    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider == null) return;
            pieceCard = hit.collider.GetComponent<L32_PiceCard>();

            if (pieceCard == null) return;
            pieceCard.DoRotatingCard();
        }

        if (Input.GetMouseButtonUp(0))
        {
            pieceCard = null;
            if (HandleWinCodition())
            {
                WinBox.SetUp().Show();
            }
        }
    }

    bool HandleWinCodition()
    {
        foreach(var card in this.lsCards)
        {
            if (card.isComplete != true)
            {
                return false;
            }
        }
        return true;
    }
}
