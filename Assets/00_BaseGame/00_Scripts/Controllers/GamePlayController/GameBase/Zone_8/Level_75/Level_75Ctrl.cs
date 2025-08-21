
using System.Collections;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using _00_BaseGame._00_Scripts.Controllers.MusicManager;
using UnityEngine;

public class Level_75Ctrl : BaseDragController<L75_MoosquitoSpray>
{
    public AudioClip spraySound;
    public AudioClip mosquitoSound;
    public int winProgress;

    private void Start()
    {
        GameController.Instance.musicManager.PlaySingle(mosquitoSound,true);
    }

    protected override void OnDragEnded()
    {
        draggableComponent.StopSpray();
        if (winProgress == 3)
        {
            StartCoroutine(HandleWinCodition());
        }
        GameController.Instance.musicManager.PauseSound(true, MusicManagerBase.SourceAudio.SoundBackup);
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        draggableComponent.transform.position += mouseDelta;
    }

    protected override void OnDragStarted()
    {
        draggableComponent.StartSpray();
        GameController.Instance.musicManager.PlaySingle(spraySound,true,MusicManagerBase.SourceAudio.SoundBackup);
    }

    IEnumerator HandleWinCodition()
    {
        isWin = true;
        yield return new WaitForSeconds(0.4f);
        WinBox.SetUp().Show();
    }

    public void StopMosquitoSound()
    {
        GameController.Instance.musicManager.PauseSound();
    }
}
