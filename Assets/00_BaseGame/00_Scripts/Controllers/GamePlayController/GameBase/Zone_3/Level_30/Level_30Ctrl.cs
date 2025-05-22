using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_30Ctrl : MonoBehaviour
{
    public int winProgess;
    public bool isWin = false;
    public L30_UILeft ui_Left;
    public L30_SpinLog spinLog;
    public Transform knifePrefabs;
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
            Instantiate(knifePrefabs, pointSpawnKnife.position, Quaternion.identity);
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
            isWin = true;
            Debug.LogError("Lose");
            spinLog.gameObject.SetActive(false);
            ui_Left.BreakPieces();
            Initiate.Fade(SceneName.GAME_PLAY,Color.black,3f);
        }
        else if( winProgess > 3)
        {
            isWin = true;
            ui_Left.BreakPieces();
            spinLog.gameObject.SetActive(false);
            WinBox.SetUp().Show();
        }
    }
}
