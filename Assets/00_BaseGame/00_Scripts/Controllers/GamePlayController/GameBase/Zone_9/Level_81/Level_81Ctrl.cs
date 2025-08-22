using DG.Tweening;
using System.Collections;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_81Ctrl : BaseDragController<L81_Earth>
{
    public L81_Spawner spawner;
    public Transform timmingBar;

    private void Start()
    {
        timmingBar.DOMoveX(1.53f, 10f).SetEase(Ease.Linear).OnComplete(delegate
        {
            isWin = true;
            spawner.StopSpawning();
            StopMoveMeteorite();
            StartCoroutine(HandleWinCodition());
        });
        StartCoroutine(spawner.SpawnRegularly());

    }


    protected override void OnDragEnded()
    {
        draggableComponent.ChangeSpriteSleep();
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {
        draggableComponent.ChangeSpritePlay();
    }

    public void HandleLoseCodition()
    {
        isWin = true;
        timmingBar.DOKill();
        spawner.StopSpawning();
        StopMoveMeteorite();
        Initiate.Fade(SceneName.GAME_PLAY,Color.black,3f);
    }
     
    public IEnumerator HandleWinCodition()
    {
        draggableComponent.ChangeSpriteWin();
        yield return new WaitForSeconds(2f);
        WinBox.SetUp().Show();
    }

    void StopMoveMeteorite()
    {
        foreach (var prefab in spawner.lsHolds)
        {
            prefab.transform.DOKill();
        }
    }
}
