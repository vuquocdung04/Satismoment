using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_64Ctrl : MonoBehaviour
{
    public bool isWin;
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
        moveMap.Pause();
    }

    void ResetGame()
    {
        Initiate.Fade(SceneName.GAME_PLAY,Color.black, 3f);
    }

    void Update()
    {
        if (isWin) return;
        if (Input.GetMouseButtonDown(0))
        {
            dino.Jump();
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
