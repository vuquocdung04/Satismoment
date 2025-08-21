using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Level_72Ctrl : MonoBehaviour
{
    public AudioClip missSound;
    public AudioClip hitSound;
    
    public int winProgress;

    // Biến isWin được quản lý qua property để xử lý khi có thay đổi
    private bool isWin;
    public bool IsWin
    {
        get { return isWin; }
        set
        {
            if (isWin == value) return;
            isWin = value;

            if (isWin)
            {
                StopTableLoop();
            }
            else
            {
                StartTableLoop();
            }
        }
    }

    public Transform hammer;
    public Transform effectBeat;
    private bool hasBeat;

    public Transform table;

    private Coroutine tableLoopCoroutine; // Quản lý coroutine để tránh duplicate
    private Tween moveTableTween;

    private void Start()
    {
        StartTableLoop();
    }

    private void StartTableLoop()
    {
        if (tableLoopCoroutine != null) return; // Tránh chạy nhiều coroutine cùng lúc
        tableLoopCoroutine = StartCoroutine(MoveTableLoop());
    }

    private void StopTableLoop()
    {
        if (tableLoopCoroutine != null)
        {
            StopCoroutine(tableLoopCoroutine);
            tableLoopCoroutine = null;
        }

        if (moveTableTween != null && moveTableTween.IsActive())
        {
            moveTableTween.Pause();
            moveTableTween.Kill();
        }
    }

    private IEnumerator MoveTableLoop()
    {
        while (true)
        {
            if (isWin) yield break;

            // Set vị trí ban đầu về -8 trước mỗi lần chạy
            table.position = new Vector3(-8f, table.position.y, table.position.z);

            // Di chuyển đến x = 8
            moveTableTween = table.DOMoveX(8f, 6f).SetEase(Ease.Linear);
            yield return moveTableTween.WaitForCompletion();

            // Nếu không thắng thì tiếp tục loop
            if (!isWin)
            {
                moveTableTween = null; // Reset tween cũ
            }
            else
            {
                break;
            }
        }
    }

    private void Update()
    {
        if (hasBeat) return;

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(HandleHammerAction());
        }
    }
    int prevWinProgress;
    IEnumerator HandleHammerAction()
    {
        hasBeat = true;

        Tween hammerBeat = hammer.DORotate(Vector3.zero, 0.07f);
        yield return hammerBeat.WaitForCompletion();
        if (prevWinProgress < winProgress)
        {
            PlayHitSound();
            prevWinProgress = winProgress;
        }
        else
        {
            PlayMissSound();
        }
        effectBeat.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.03f);
        effectBeat.gameObject.SetActive(false);

        Tween hammerBeat2 = hammer.DORotate(new Vector3(0, 0, -45f), 0.07f);
        yield return hammerBeat2.WaitForCompletion();

        hasBeat = false;
    }

    public IEnumerator HandleWinCondition()
    {
        IsWin = true; // Gọi setter -> Kill tween
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();
    }

    private void PlayHitSound()
    {
        GameController.Instance.musicManager.PlayMultiple(hitSound);
    }

    private void PlayMissSound()
    {
        GameController.Instance.musicManager.PlaySingle(missSound);
    }
    
}