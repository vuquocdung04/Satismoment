using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Level_98Ctrl : MonoBehaviour
{
    public int winProgress;
    bool isWin = false;
    public L98_HitEffect hitEffect;
    public L98_BladeEffect bladeEffect;
    public List<L98_Fruit> lsFruits;
    private Vector3 mousePosition;

    private void Start()
    {
        StartCoroutine(ThrowFruit());
    }

    private void Update()
    {
        if (isWin) return;
        // Luôn cập nhật vị trí chuột (chỉ lấy tọa độ)
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z =  1;


        if (Input.GetMouseButtonDown(0))
        {
            bladeEffect.transform.position = mousePosition;
            bladeEffect.OnStart();
        }

        if (Input.GetMouseButton(0))
        {
            bladeEffect.transform.position = mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            bladeEffect.OnEnd();
            if(winProgress == lsFruits.Count)
            {
                StartCoroutine(HandleWinCondition());
            }
        }
    }

    IEnumerator ThrowFruit()
    {
        var waitTime = new WaitForSeconds(0.1f);
        foreach(var fruit in this.lsFruits)
        {
            fruit.Init();
            yield return waitTime;
        }
    }
    IEnumerator HandleWinCondition()
    {
        isWin = true;
        yield return new WaitForSeconds(1f);
        WinBox.SetUp().Show();
    }


    [Button("Setup Fruit",ButtonSizes.Large)]
    void SetupFruit()
    {
        foreach(var fruit in this.lsFruits)
        {
            fruit.lsChilds.Clear();
            fruit.circleCollider2D = fruit.transform.GetComponent<CircleCollider2D>();
            fruit.lsChilds.Add(fruit.transform.Find("left"));
            fruit.lsChilds.Add(fruit.transform.Find("right"));
        }
    }
}