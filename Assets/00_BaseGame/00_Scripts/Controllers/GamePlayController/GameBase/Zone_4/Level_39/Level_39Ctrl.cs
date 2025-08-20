using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Level_39Ctrl : MonoBehaviour
{
    public bool isWin;
    public Transform paper;
    public float throwDuration = 0.5f;
    public Vector3 startPos;
    Vector3 mousePos;
    Vector3 curMousePos;
    private bool isReady;
    Vector3 directionFly;
    void Update()
    {
        if (isWin) return;
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        
        if(isReady) return;
        if (Input.GetMouseButtonDown(0))
        {
            curMousePos = mousePos;
            directionFly = curMousePos + new Vector3(0,2,0);
            Vector3 arcTarget = startPos + directionFly;

            StartCoroutine(Create(arcTarget));
        }
    }

    IEnumerator Create(Vector3 arcTarget)
    {
        isReady = true;
        GameController.Instance.musicManager.PlayPick();
        Transform paperPre = Instantiate(paper, startPos, Quaternion.identity);
        paperPre.localScale = Vector3.one;
        paper = paperPre;
        paper.DOJump(arcTarget, jumpPower: 1f, numJumps: 1, duration: throwDuration)
            .SetEase(Ease.OutQuad);

        paper.DOScale(0.4f, throwDuration);
        yield return new WaitForSeconds(throwDuration);
        isReady = false;
    }
}
