using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_129Ctrl : MonoBehaviour
{
    public L129_Thief thief;
    public L129_Police police;
    Vector3 mousePosition;

    [SerializeField] bool isClicked = true;
    bool isWin;
    private void Update()
    {
        if (isWin) return;
        if (!isClicked) return;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            var clickedCircle = hit.collider?.GetComponent<L129_Circle>();

            // Kiểm tra xem có click vào Circle không và có currentCirle không
            if (clickedCircle == null || police.currentCirle == null) return;

            // Kiểm tra xem ô click có thuộc neighbor của currentCirle không
            if (police.currentCirle.lsNeighBor.Contains(clickedCircle))
            {
                StartCoroutine(HandleThiefMoving(clickedCircle));
            }
            else
            {
                Debug.Log("Ô không nằm trong vùng lân cận. Không thể di chuyển!");
            }
        }
    }

    IEnumerator HandleThiefMoving(L129_Circle clickedCircle)
    {
        isClicked = false;
        police.Moving(clickedCircle);
        yield return new WaitForSeconds(0.51f);
        if (CheckWinCondition())
        {
            StartCoroutine(HandleWinCondition());
        }
        else
        {
            thief.Moving();
            yield return new WaitForSeconds(0.51f);
            isClicked = true;
        }
    }

    bool CheckWinCondition()
    {
        if (thief.currentCirle == police.currentCirle)
        {
            return true;
        }
        return false;
    }

    IEnumerator HandleWinCondition()
    {
        isWin = true;
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();
    }

}
