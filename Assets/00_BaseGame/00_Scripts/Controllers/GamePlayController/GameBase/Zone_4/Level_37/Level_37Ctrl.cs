using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_37Ctrl : MonoBehaviour
{
    public bool isPlay;
    public bool isWin;
    public int winProgress;

    public L37_Card selectedCard1;
    public L37_Card selectedCard2;
    public L37_Card curSelectedCard;

    public Sprite cardHidden;
    public float startX;
    public float startY;

    public float spacingX;
    public float spacingY;
    [Space(10)]
    public List<L37_Card> lsCardPrefabs;
    [Header("Card Matrix")]
    public List<L37_Card> allCards;


    private void Start()
    {
        InitMatrix();
    }

    Vector3 mousePos;
    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        SelectCard();
    }

    void SelectCard()
    {
        if (isWin) return;
        if (isPlay) return;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider == null) return;

            curSelectedCard = hit.collider.GetComponent<L37_Card>();

            if (curSelectedCard == null) return;
            if(selectedCard1 == null)
            {
                selectedCard1 = curSelectedCard;
                selectedCard1.DoFlipingCard();
                return;
            }
            else
            {
                if (curSelectedCard != selectedCard1)
                {
                    selectedCard2 = curSelectedCard;
                    selectedCard2.DoFlipingCard();
                    StartCoroutine(HandleCheckMardMatch(selectedCard1, selectedCard2));
                }
            }
        }
    }
    
    IEnumerator HandleCheckMardMatch(L37_Card cardA, L37_Card cardB)
    {
        isPlay = true;
        yield return new WaitForSeconds(0.4f);
        if(IsCardDuplicate(cardA, cardB))
        {
            cardA.DoFlyingAfterDuplicate();
            cardB.DoFlyingAfterDuplicate();
            selectedCard1 = null;
            selectedCard2 = null;
            curSelectedCard = null;
            HandleWinCodition();
        }
        else
        {
            cardA.DoHidenCard(cardHidden);
            cardB.DoHidenCard(cardHidden);
            selectedCard1 = null;
            selectedCard2 = null;
            curSelectedCard = null;
        }
        isPlay = false;

    }

    public void HandleWinCodition()
    {
        winProgress++;
        if(winProgress >= lsCardPrefabs.Count)
        {
            isWin = true;
            WinBox.SetUp().Show();
        }
    }

    bool IsCardDuplicate(L37_Card cardA, L37_Card cardB)
    {
        if(cardA.idCard != cardB.idCard) return false;
        return true;
    }

    void InitMatrix()
    {
        RandomCard();
        int indexCard = 0;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                float posX = startX + i * spacingX;
                float posY = startY - j * spacingY;
                Vector3 position = new Vector3(posX, posY, 0);

                var card = Instantiate(allCards[indexCard], position, Quaternion.identity, transform);
                card.pos = position;
                card.spriteRenderer.sprite = cardHidden;
                indexCard++;
            }
        }
        void RandomCard()
        {
            allCards.Clear();
            foreach (var card in this.lsCardPrefabs)
            {
                allCards.Add(card);
                allCards.Add(card);
            }

            // sunfer
            System.Random rng = new System.Random();
            int n = allCards.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                L37_Card value = allCards[k];
                allCards[k] = allCards[n];
                allCards[n] = value;
            }
        }

    }

    [Button("Setup",ButtonSizes.Large)]
    void EdittorSetup()
    {
        for(int i = 0; i < lsCardPrefabs.Count; i++)
        {
            lsCardPrefabs[i].idCard = i;
            lsCardPrefabs[i].spriteRenderer = lsCardPrefabs[i].transform.GetComponent<SpriteRenderer>();
            lsCardPrefabs[i].spriteDefault = lsCardPrefabs[i].spriteRenderer.sprite;
        }
    }
}
