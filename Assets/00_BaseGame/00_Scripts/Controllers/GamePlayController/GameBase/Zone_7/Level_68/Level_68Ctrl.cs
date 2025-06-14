using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_68Ctrl : MonoBehaviour
{
    public bool isWin = false;
    public Transform map;
    public L68_Mouse mouse;
    public L68_Cat cat;
    public Transform timingBar;
    private Tween moveMap;
    private void Start()
    {
        moveMap = map.DOMoveX(-12.14f, 15f).SetEase(Ease.Linear).OnComplete(delegate
        {
            isWin = true;
            mouse.heart.gameObject.SetActive(true);
            mouse.sweat.gameObject.SetActive(false);
            mouse.StopRunningAnimation();
            cat.StopCatLogic();
            StartCoroutine(HandleWinCodition());
        });
        timingBar.DOMoveX(1f,15f).SetEase(Ease.Linear);
        mouse.StartRunningAnimation();
    }

    private void Update()
    {
        if (isWin) return;

        if (Input.GetMouseButtonDown(0))
        {
            moveMap.Pause();
            mouse.StopRunningAnimation();
            timingBar.DOPause();
        }

        if (Input.GetMouseButtonUp(0))
        {
            moveMap.Play();
            mouse.StartRunningAnimation();
            timingBar.DOPlay();

        }
    }

    public IEnumerator HandleLoseGame()
    {
        // Dừng DOTween di chuyển map
        if (moveMap != null && moveMap.IsPlaying())
        {
            moveMap.Kill(); // hoặc moveMap.Pause();
        }

        mouse.StopRunningAnimation();

        yield return new WaitForSeconds(0.5f);

        Initiate.Fade(SceneName.GAME_PLAY, Color.black, 3f);
    }

    IEnumerator HandleWinCodition()
    {
        yield return new WaitForSeconds(0.2f);
        WinBox.SetUp().Show();
    }
}
