using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Level_64Ctrl : MonoBehaviour
{
    public AudioClip jumpSound;
    public bool isWin;
    public bool isLose;
    public L64_Dino dino;
    public Button btnRetry;
    public Transform map;

    private Tween moveMap;
    private void Start()
    {
        btnRetry.onClick.AddListener(ResetGame);
        moveMap = map.DOMoveX(-34.2f, 10f).SetEase(Ease.Linear).OnComplete(delegate
        {
            StartCoroutine(HandleWinCodition());
        });
    }

    public void StopMoveMap()
    {
        isLose = true;
        moveMap.Pause();
    }

    void ResetGame()
    {
        GameController.Instance.ChangeScene(SceneName.GAME_PLAY);
    }

    void Update()
    {
        if (isWin) return;
        if (isLose) return;
        if (Input.GetMouseButtonDown(0))
        {
            dino.Jump(delegate
            {
                GameController.Instance.musicManager.PlaySingle(jumpSound);
            });
        }
    }


    IEnumerator HandleWinCodition()
    {
        isWin = true;
        dino.StopRunningAnim();
        yield return new WaitForSeconds(0.3f);
        WinBox.SetUp().Show();
    }

    private void OnDestroy()
    {
        if(moveMap != null && moveMap.IsActive())
                moveMap.Kill();
    }
}
