using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_89Ctrl : MonoBehaviour
{
    public AudioClip fallSound;
    public int winProgress;
    public bool isWin;
    public Transform car;
    public L89_Rod rod;
    public L89_TriggerRestHouse triggerRestHouse;
    public List<L89_HousePrefab> lsHouses;

    private void Start()
    {
        Camera.main.orthographicSize = 4.5f;
        triggerRestHouse.Init();
        rod.Init();
    }

    void Update()
    {
        if (isWin) return;
        if (Input.GetMouseButtonDown(0))
        {
            rod.currrentHousePrefab.HandleFallCondition();
            rod.currrentHousePrefab.transform.SetParent(transform);
        }
    }

    public void MoveCamera()
    {
        Camera.main.transform.DOMoveY(1.5f,1f).SetEase(Ease.Linear);
        
        transform.position += new Vector3(1,0);
    }
    public IEnumerator HandleWinCondition()
    {
        Camera.main.transform.DOMoveY(0f,0.5f);
        Camera.main.orthographicSize = 5f;
        Tween carMove = car.DOMoveX(-3.6f,1f).SetEase(Ease.Linear);
        rod.rotationEnabled = false;
        rod.transform.localScale = Vector3.zero;
        yield return carMove.WaitForCompletion();
        foreach(var child in this.lsHouses)
        {
            Debug.LogError("test");
            child.transform.DOMoveX(0f,1f).SetEase(Ease.Linear);
        }
        yield return new WaitForSeconds(0.5f);
        WinBox.SetUp().Show();
    }

    public void PlayFallSound()
    {
        GameController.Instance.musicManager.PlaySingle(fallSound);
    }
}
