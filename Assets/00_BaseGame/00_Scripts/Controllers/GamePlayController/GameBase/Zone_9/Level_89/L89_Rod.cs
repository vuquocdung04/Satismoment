using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class L89_Rod : MonoBehaviour
{
    public Level_89Ctrl levelCtrl;
    public Transform pointFall;
    public L89_HousePrefab housePrefab;
    public L89_HousePrefab currrentHousePrefab;
    public List<Sprite> lsHouseSrites;

    public bool rotationEnabled = true;
    public float swingSpeed = 15f;
    public float maxSwingAngle = 30f;
    private float currentPingPongTime = 0f;

    float pingPongValue;
    float currentSwingOffset;

    public void Init()
    {
        transform.SetParent(Camera.main.transform);
        currrentHousePrefab = Instantiate(housePrefab, Vector2.zero, Quaternion.identity);
        currrentHousePrefab.SetSprite(lsHouseSrites[0]);
        currrentHousePrefab.ResetStateAndSetParent();
    }


    void Update()
    {
        if (!rotationEnabled)
        {
            return;
        }
        currentPingPongTime += Time.deltaTime * swingSpeed;

        pingPongValue = Mathf.PingPong(currentPingPongTime, maxSwingAngle * 2f);
        currentSwingOffset = pingPongValue - maxSwingAngle;

        transform.localEulerAngles = new Vector3(
            transform.localEulerAngles.x,
            transform.localEulerAngles.y,
            currentSwingOffset
        );
    }

    public int index = 1;
    public void InitHouse()
    {
        levelCtrl.winProgress++;

        // Thêm cái cũ vào list trước
        levelCtrl.lsHouses.Add(currrentHousePrefab);

        if(levelCtrl.winProgress == lsHouseSrites.Count)
        {
            levelCtrl.isWin = true;
            StartCoroutine(levelCtrl.HandleWinCondition());
            return;
        }

        // Tạo nhà mới
        L89_HousePrefab newHouse = Instantiate(housePrefab, Vector2.zero, Quaternion.identity);
        newHouse.SetSprite(lsHouseSrites[index]);
        newHouse.ResetStateAndSetParent();
        newHouse.UpdateColliderSize();

        // Gán lại current house
        currrentHousePrefab = newHouse;
        index++;
    }
}