using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class L89_Rod : MonoBehaviour
{
    public Transform pointFall;
    public L89_HousePrefab housePrefab;
    public List<Sprite> lsHouseSrites;

    public bool rotationEnabled = true;
    public float swingSpeed = 15f;
    public float maxSwingAngle = 30f;
    private float currentPingPongTime = 0f;

    float pingPongValue;
    float currentSwingOffset;

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

    public int  index = 0;
    public void InitHouse()
    {
        index++;
        var houseClone = Instantiate(housePrefab, Vector2.zero, Quaternion.identity);
        houseClone.SetSprite(lsHouseSrites[index]);
    }
}