using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_165Ctrl : MonoBehaviour
{
    public int killedBugAmount = 0;
    public L165_Slippers slipperPrefabs;
    public L165_Bug bugPrefab;
    public int totalSpawnBug = 10;
    public List<L165_Bug> lsBugs;
    Vector3 mousePosition;
    bool isWin = false;
    private void Start()
    {
        lsBugs.Clear();
        for (int i = 0; i < totalSpawnBug; i++)
        {
            var bugClone = Instantiate(bugPrefab, Vector2.zero, Quaternion.identity);
            bugClone.Init();
            lsBugs.Add(bugClone);
        }
    }


    void Update()
    {
        if (isWin) return;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(SpawnSlipper());
        }
    }

    List<L165_Bug> bugsToRemove;
    IEnumerator SpawnSlipper()
    {
        var slipperClone = SimplePool2.Spawn(slipperPrefabs, mousePosition, Quaternion.identity);
        slipperClone.objRenderer.sprite = slipperClone.defaultSprite;
        bugsToRemove.Clear();
        bugsToRemove = new List<L165_Bug>();
        foreach (var bug in lsBugs)
        {
            if (Vector2.Distance(slipperClone.transform.position, bug.transform.position) < 0.4f)
            {
                slipperClone.objRenderer.sprite = slipperClone.spriteWhenTouch;
                bugsToRemove.Add(bug);   // CHỈ THÊM VÀO TẠM
                bug.Kill();
                killedBugAmount++;
                CheckWin();
            }
        }
        foreach (var bug in bugsToRemove)
        {
            lsBugs.Remove(bug);
        }

        yield return new WaitForSeconds(0.2f);
        SimplePool2.Despawn(slipperClone.gameObject);
    }


    void CheckWin()
    {
        if(killedBugAmount == totalSpawnBug)
        {
            isWin = true;
            StartCoroutine(HandleWinCondition());
        }
    }

    IEnumerator HandleWinCondition()
    {
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();
    }
}
