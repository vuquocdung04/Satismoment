using System.Collections;
using UnityEngine;

public class Level_30Ctrl : MonoBehaviour
{
    
    public AudioClip knifeHitWoodSound;
    public AudioClip knifeHitKnifeSound;
    public int winProgess;
    public bool isWin;
    public L30_UILeft ui_Left;
    public L30_SpinLog spinLog;
    public L30_ThrowKnife knifePrefabs;
    public Transform pointSpawnKnife;
    private void Start()
    {
        spinLog.DoSpinning();
    }

    private void Update()
    {
        if (isWin) return;
        if (Input.GetMouseButtonDown(0))
        {
            ui_Left.RegisterHit();
            StartCoroutine(OnOffPoint());
            var knifeClone = Instantiate(knifePrefabs, pointSpawnKnife.position, Quaternion.identity);
            knifeClone.levelCtrl = this;
        }
    }

    IEnumerator OnOffPoint()
    {
        pointSpawnKnife.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        pointSpawnKnife.gameObject.SetActive(true);
    }

    public void HandleStatusGame()
    {
        if(ui_Left.amountHit == 0 && winProgess < 3)
        {
            StartCoroutine(HandleLoseCondition());
        }
        else if( winProgess > 3)
        {
            StartCoroutine(HandleWinCondition());
        }
    }

    IEnumerator HandleLoseCondition()
    {
        isWin = true;
        Debug.LogError("Lose");
        spinLog.ResetTween();
        spinLog.gameObject.SetActive(false);
        ui_Left.BreakPieces();
        yield return new WaitForSeconds(1.1f);
        Initiate.Fade(SceneName.GAME_PLAY, Color.black, 3f);
    }
    
    IEnumerator HandleWinCondition()
    {
        isWin = true;
        ui_Left.BreakPieces();
        spinLog.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();
    }

    public void PlayingKnifeHitWoodSound()
    {
        GameController.Instance.musicManager.PlaySingle(knifeHitWoodSound);
    }

    public void PlayingKnifeHitKnifeSound()
    {
        GameController.Instance.musicManager.PlaySingle(knifeHitKnifeSound);
    }
}
