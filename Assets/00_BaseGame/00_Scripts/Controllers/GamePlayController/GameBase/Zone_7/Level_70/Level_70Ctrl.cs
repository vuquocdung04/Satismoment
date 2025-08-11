using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_70Ctrl : BaseDragController<Transform>
{
    public List<SpriteRenderer> lsRenderers;
    public List<Sprite> lsSprites;
    float distanceY = 0.4f;
    protected override void OnDragEnded()
    {
        float distance = Mathf.Abs(draggableComponent.transform.position.y - distanceY);
        if (distance < 0.4f)
        {
            isWin = true;
            draggableComponent.transform.position = new Vector2(0, distanceY);
            StartCoroutine(HandleWinCodition());
        }
    }
    float deltaY;
    Vector3 newPosition;
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        deltaY = deltaMousePosition.y;

        newPosition = draggableComponent.transform.position + new Vector3(0, deltaY, 0);
        newPosition.y = Mathf.Clamp(newPosition.y, -1f, 0.2f);
        newPosition.x = 0; // giữ nguyên x = 0

        draggableComponent.transform.position = newPosition;
    }

    protected override void OnDragStarted()
    {

    }

    IEnumerator HandleWinCodition()
    {
        // mout
        lsRenderers[0].sprite = lsSprites[0];
        // milk
        yield return new WaitForSeconds(0.5f);
        lsRenderers[1].sprite = lsSprites[1];

        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();
    }
}
