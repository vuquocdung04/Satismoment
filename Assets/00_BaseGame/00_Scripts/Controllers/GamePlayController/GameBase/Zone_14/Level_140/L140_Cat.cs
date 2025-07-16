using DG.Tweening;
using System.Collections;
using UnityEngine;

public class L140_Cat : MonoBehaviour
{
    public Level_140Ctrl levelCtrl;
    public SpriteRenderer catRenderer;
    public Sprite spriteSuprise;
    public Sprite spriteSleep;
    public float moveDistance = 1f;

    bool isReady = true;

    // Giới hạn vùng di chuyển
    Vector2 minBounds = new Vector2(-2f, -2.49f);
    Vector2 maxBounds = new Vector2(2f, -0.5f);

    private void OnMouseDown()
    {
        if (levelCtrl.isWin) return;
        if (!isReady) return;
        StartCoroutine(HandleActionCat());
    }

    IEnumerator HandleActionCat()
    {
        isReady = false;
        // Tạo vector di chuyển ngẫu nhiên
        float randX = Random.Range(-moveDistance, moveDistance);
        float randY = Random.Range(-moveDistance, moveDistance);
        Vector2 proposedPosition = (Vector2)transform.position + new Vector2(randX, randY);

        // Giới hạn vị trí trong vùng cho phép
        Vector2 finalPosition = new Vector2(
            Mathf.Clamp(proposedPosition.x, minBounds.x, maxBounds.x),
            Mathf.Clamp(proposedPosition.y, minBounds.y, maxBounds.y)
        );

        catRenderer.sprite = spriteSuprise;
        Tween catMove = transform.DOMove(finalPosition, 0.5f).SetEase(Ease.Linear);
        yield return catMove.WaitForCompletion();
        levelCtrl.CheckClothOverlaping();

        catRenderer.sprite = spriteSleep;
        isReady = true;
    }
}