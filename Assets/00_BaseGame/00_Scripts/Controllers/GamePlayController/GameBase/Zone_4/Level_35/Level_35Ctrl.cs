using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_35Ctrl : MonoBehaviour
{
    public int winProgress = 0;
    public bool isWin = false;
    public Transform prefabCandySugar;
    public Transform curCandySugar;
    L35_pieceCandy curCandy;
    Vector3 posMouse;

    private void Start()
    {
        curCandySugar = Instantiate(prefabCandySugar);
    }

    private void Update()
    {
        if (isWin) return;

        posMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        posMouse.z = 0;

        if (Input.GetMouseButtonDown(0))
        {

            RaycastHit2D hit = Physics2D.Raycast(posMouse, Vector2.zero);
            if (hit.collider == null) return;

            curCandy = hit.collider.GetComponent<L35_pieceCandy>();
            if (curCandy == null) return;
            if (curCandy.isToching) return;
            curCandy.isToching = true;
            if (curCandy.idPiece == 0)
            {
                winProgress++;
                Explode(0.5f,0.5f);
            }
            else
            {
                StartCoroutine(ResetLevel());
            }
        }
        if (Input.GetMouseButtonUp(0))
        {

            HandleWinCodition();
            curCandy = null;
        }
    }
    void Explode(float durationMove, float durationRotate)
    {
        Vector3 dir = (curCandy.transform.position - Vector3.zero).normalized;
        Vector3 targetPos = curCandy.transform.position + dir * 2f;
        curCandy.transform.DOMove(targetPos, durationMove).SetEase(Ease.OutBack);
        curCandy.transform.DORotate(new Vector3(0, 0, Random.Range(-180f, 180f)), durationRotate, RotateMode.FastBeyond360);

    }
    IEnumerator ResetLevel()
    {
        Explode(0.4f,0);
        yield return new WaitForSeconds(0.41f);
        Destroy(curCandySugar.gameObject);
        var curCandyy =  Instantiate(prefabCandySugar);
        curCandyy.localScale = Vector3.zero;
        curCandyy.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce);
        curCandySugar = curCandyy;
        winProgress = 0;
    }

    void HandleWinCodition()
    {
        if(winProgress > 24)
        {
            isWin = true;
            WinBox.SetUp().Show();
        }
    }

}
